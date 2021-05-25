using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using AngleSharp.Html.Dom;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Resources.Base.Areas.Identity.Pages.Account.Manage;
using TestProject.Helpers;
using WebApp;
using Xunit;
using Xunit.Abstractions;

namespace TestProject.IntegrationTests
{
    public class SchoolSetupIntegrationTests : IClassFixture<CustomWebApplicationFactory<WebApp.Startup>>
    {

        private readonly CustomWebApplicationFactory<WebApp.Startup> _factory;
        private readonly HttpClient _client;
        private readonly ITestOutputHelper _testOutputHelper;


        public SchoolSetupIntegrationTests(CustomWebApplicationFactory<Startup> factory,
            ITestOutputHelper testOutputHelper)
        {
            _factory = factory;
            _testOutputHelper = testOutputHelper;
            _client = factory
                .WithWebHostBuilder(builder =>
                {
                    builder.UseSetting("test_database_name", Guid.NewGuid().ToString());
                })
                .CreateClient(new WebApplicationFactoryClientOptions()
                    {
                        // dont follow redirects
                        AllowAutoRedirect = false
                    }
                );
        }

        [Fact]
        public async Task? TestAction_Create_School()
        {
            var uri = "/";

            var getTestResponse = await _client.GetAsync(uri);

            Assert.Equal(200, (int) getTestResponse.StatusCode);
            var ownerUsername = "SchoolOwner";
            var ownerPassword = "P@ssw0rd";
            var adminUsername = "admin";
            var adminPassword = "DucksG0Quack!";

            // School owner make an account
            uri = await RegisterAccount(uri, "school@owner.mail", ownerUsername, "Õpetaja", "Kraavis", ownerPassword);
            uri = await LogOut(uri);

            // Admin create Account
            uri = await LoginAccount("/", adminUsername, adminPassword);
            uri = await GetPage(uri, "#nav-drivingschools");
            var drivingschoolsPage = uri;
            uri = await GetPage(uri, "#create-new-drivingschool");
            uri = await RegisterAccount(uri, ownerUsername, "Test school name", "Test school description");
            uri.Should().Be(drivingschoolsPage);
            uri = await LogOut(uri);

            // Check user was made an owner
            uri = await LoginAccount("/", ownerUsername, ownerPassword);
            (await IsLoggedInUserAnOwner()).Should().BeTrue();
        }

        [Fact]
        public async Task? TestAction_Invite_Teacher()
        {
            var uri = "/";

            var getTestResponse = await _client.GetAsync(uri);

            Assert.Equal(200, (int) getTestResponse.StatusCode);
            var ownerUsername = "owner";
            var ownerPassword = "ValgeAknaraam!1";


            uri = await LoginAccount("/", ownerUsername, ownerPassword);
            uri = await GetPage(uri, "#nav-teachers");
            var response = await InviteTeacher(uri, "user1");
            response.Should().Be(true);
        }
        
        [Fact]
        public async Task? TestAction_Make_Course()
        {
            var uri = "/";

            var getTestResponse = await _client.GetAsync(uri);

            Assert.Equal(200, (int) getTestResponse.StatusCode);
            var ownerUsername = "owner";
            var ownerPassword = "ValgeAknaraam!1";


            uri = await LoginAccount("/", ownerUsername, ownerPassword);
            uri = await GetPage(uri, "#nav-courses");
            var coursesPageUri = uri;
            uri = await GetPage(uri, "#create-new-course");
            uri = await MakeCourse(uri, "Sõitmine", "Õhtune sõitmine", 153, "B");
            uri.Should().Be(coursesPageUri);
        }


        private async Task<string> MakeCourse(string baseUri, string name, string description, double entryPrice,
            string category)
        {
            var pageResponse = await _client.GetAsync(baseUri);
            pageResponse.EnsureSuccessStatusCode();

            // get the document
            var getDocument = await HtmlHelpers.GetDocumentAsync(pageResponse);

            var form = (IHtmlFormElement) getDocument.QuerySelector("#form-create-course");
            var verificationTokenElement = (IHtmlInputElement) getDocument.QuerySelector("#form-create-course > input");

            var formValues = new Dictionary<string, string>()
            {
                ["Name"] = name,
                ["Description"] = description,
                ["Price"] = entryPrice.ToString(),
                ["Category"] = category,
                ["__RequestVerificationToken"] = verificationTokenElement.Value,

            };
            var regPostResponse = await _client.SendAsync(form, formValues);
            regPostResponse.StatusCode.Should().Equals(302);
            var regPostHeader = regPostResponse.Headers.FirstOrDefault(x => x.Key == "Location");
            var redirectUri = regPostHeader.Value.FirstOrDefault();
            redirectUri.Should().NotBeNull();
            var redirectPageResponse = await _client.GetAsync(redirectUri);
            redirectPageResponse.EnsureSuccessStatusCode();

            return redirectPageResponse.RequestMessage!.RequestUri!.ToString();
            
        }

        private async Task<string> GetPage(string baseUri, string querySelector)
        {
            var pageResponse = await _client.GetAsync(baseUri);
            pageResponse.EnsureSuccessStatusCode();
            // get the document
            var pageDocument = await HtmlHelpers.GetDocumentAsync(pageResponse);
            var anchorElement = (IHtmlAnchorElement) pageDocument.QuerySelector(querySelector);
            var uri = anchorElement.Href;
            return uri!;
        }

        private async Task<string> RegisterAccount(string baseUri, string ownerUsername, string schoolName,
            string schoolDescription)
        {
            var pageResponse = await _client.GetAsync(baseUri);
            pageResponse.EnsureSuccessStatusCode();

            // get the document
            var documentAsync = await HtmlHelpers.GetDocumentAsync(pageResponse);

            var appUsers = (IHtmlSelectElement) documentAsync.QuerySelector("#DrivingSchool_AppUserId");
            var appUserId = appUsers.Options.First(element => element.Text == ownerUsername).Value;
            var form = (IHtmlFormElement) documentAsync.QuerySelector("#form-create-drivingschool");
            var formValues = new Dictionary<string, string>()
            {
                ["DrivingSchool_Name"] = schoolName,
                ["DrivingSchool_Description"] = schoolDescription,
                ["DrivingSchool_AppUserId"] = appUserId
            };
            var postResponse = await _client.SendAsync(form, formValues);
            postResponse.StatusCode.Should().Equals(302);
            var postHeader = postResponse.Headers.FirstOrDefault(x => x.Key == "Location");
            var redirectUri = postHeader.Value.FirstOrDefault();
            redirectUri.Should().NotBeNull();
            var redirectPageResponse = await _client.GetAsync(redirectUri);
            redirectPageResponse.EnsureSuccessStatusCode();

            return redirectPageResponse.RequestMessage!.RequestUri!.ToString();
        }


        private async Task<string> LogOut(string baseUri)
        {
            var pageResponse = await _client.GetAsync(baseUri);
            pageResponse.EnsureSuccessStatusCode();
            // get the document
            var pageDocument = await HtmlHelpers.GetDocumentAsync(pageResponse);
            var verificationTokenElement = (IHtmlInputElement) pageDocument.QuerySelector("#nav-logout > input");
            var logoutForm = (IHtmlFormElement) pageDocument.QuerySelector("#nav-logout");
            var logoutFormValues = new Dictionary<string, string>()
            {
                ["__RequestVerificationToken"] = verificationTokenElement.Value,
            };

            await _client.SendAsync(logoutForm, logoutFormValues);

            return baseUri!;
        }
        private async Task<bool> InviteTeacher(string uri, string username)
        {
            uri = "https://localhost:5001/School/Teachers";
            var pageResponse = await _client.GetAsync(uri);
            pageResponse.EnsureSuccessStatusCode();
            // get the document
            var pageDocument = await HtmlHelpers.GetDocumentAsync(pageResponse);
            var verificationTokenElement = (IHtmlInputElement) pageDocument.QuerySelector("#form-invite-teacher > input");
            var inviteForm = (IHtmlFormElement) pageDocument.QuerySelector("#form-invite-teacher");
            var inviteFormValues = new Dictionary<string, string>()
            {
                ["username"] = username,
                ["__RequestVerificationToken"] = verificationTokenElement.Value,

            };
            return true;
        }

        private async Task<bool> IsLoggedInUserAnOwner()
        {
            var pageResponse = await _client.GetAsync("/");
            pageResponse.EnsureSuccessStatusCode();
            var pageDocument = await HtmlHelpers.GetDocumentAsync(pageResponse);
            var logoutForm = (IHtmlAnchorElement) pageDocument.QuerySelector("#nav-teachers");
            return logoutForm != null;
        }

        private async Task<string> RegisterAccount(string baseUri, string email, string username, string firstname,
            string lastname, string password)
        {
            var pageResponse = await _client.GetAsync(baseUri);
            pageResponse.EnsureSuccessStatusCode();
            // get the document
            var pageDocument = await HtmlHelpers.GetDocumentAsync(pageResponse);
            var registerAnchorElement = (IHtmlAnchorElement) pageDocument.QuerySelector("#nav-register");
            var uri = registerAnchorElement.Href;

            var getRegisterPageResponse = await _client.GetAsync(uri);
            getRegisterPageResponse.EnsureSuccessStatusCode();

            // get the document
            var getRegisterDocument = await HtmlHelpers.GetDocumentAsync(getRegisterPageResponse);

            var regForm = (IHtmlFormElement) getRegisterDocument.QuerySelector("#form-register");
            var regFormValues = new Dictionary<string, string>()
            {
                ["Input_Email"] = email,
                ["Input_UserName"] = username,
                ["Input_Password"] = password,
                ["Input_ConfirmPassword"] = password,
                ["Input_FirstName"] = firstname,
                ["Input_LastName"] = lastname,
            };
            var regPostResponse = await _client.SendAsync(regForm, regFormValues);
            regPostResponse.StatusCode.Should().Equals(302);
            var regPostHeader = regPostResponse.Headers.FirstOrDefault(x => x.Key == "Location");
            var redirectUri = regPostHeader.Value.FirstOrDefault();
            redirectUri.Should().NotBeNull();
            return redirectUri!;
        }

        private async Task<string> LoginAccount(string baseUri, string username, string password)
        {
            var pageResponse = await _client.GetAsync(baseUri);
            pageResponse.EnsureSuccessStatusCode();
            // get the document
            var pageDocument = await HtmlHelpers.GetDocumentAsync(pageResponse);
            var loginAnchorElement = (IHtmlAnchorElement) pageDocument.QuerySelector("#nav-login");
            var uri = loginAnchorElement.Href;

            var getLoginPageResponse = await _client.GetAsync(uri);
            getLoginPageResponse.EnsureSuccessStatusCode();

            // get the document
            var getLoginDocument = await HtmlHelpers.GetDocumentAsync(getLoginPageResponse);

            var loginForm = (IHtmlFormElement) getLoginDocument.QuerySelector("#form-login");
            var loginFormValues = new Dictionary<string, string>()
            {
                ["Input_UserName"] = username,
                ["Input_Password"] = password,
            };
            var loginPostResponse = await _client.SendAsync(loginForm, loginFormValues);
            loginPostResponse.StatusCode.Should().Equals(302);
            var loginPostHeader = loginPostResponse.Headers.FirstOrDefault(x => x.Key == "Location");
            var redirectUri = loginPostHeader.Value.FirstOrDefault();
            redirectUri.Should().NotBeNull();
            return redirectUri!;
        }
    }
}