using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using AngleSharp.Html.Dom;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using TestProject.Helpers;
using WebApp;
using Xunit;
using Xunit.Abstractions;

namespace TestProject.IntegrationTests
{
    public class AuthenticationIntegrationTests : IClassFixture<CustomWebApplicationFactory<WebApp.Startup>>
    {
        private readonly CustomWebApplicationFactory<WebApp.Startup> _factory;
        private readonly HttpClient _client;
        private readonly ITestOutputHelper _testOutputHelper;
        

        public AuthenticationIntegrationTests(CustomWebApplicationFactory<Startup> factory,
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
        public async Task TestAction_HasSuccessStatusCode()
        {
            // ARRANGE
            var uri = "/";
            
            // ACT
            var getTestResponse = await _client.GetAsync(uri);
            
            // ASSERT
            getTestResponse.EnsureSuccessStatusCode();
            Assert.InRange((int)getTestResponse.StatusCode, 200, 299);
        }
        
        [Fact]
        public async Task TestAuthAction_AuthRedirect()
        {
            // ARRANGE
            var uri = "/Identity/Account/Manage";
            
            // ACT
            var getTestResponse = await _client.GetAsync(uri);
            
            // ASSERT
            Assert.Equal(302, (int) getTestResponse.StatusCode);
        }

        [Fact]
        public async Task TestAuthAction_Register()
        {
            // ARRANGE
            var uri = "/Identity/Account/Manage";
            
            // ACT
            var getTestResponse = await _client.GetAsync(uri);
            
            // ASSERT
            Assert.Equal(302, (int) getTestResponse.StatusCode);
            var redirectUri = getTestResponse.Headers.FirstOrDefault(x => x.Key == "Location").Value.FirstOrDefault();
            redirectUri.Should().NotBeNull();

            await Get_Login_Page(redirectUri!);
            // we need to follow the redirect
            // get the login page
            // get the registration page
            // fill up the reg form
            // submit form
            // try to access auth resource again - we should have new user and be logged in
        }
        
        
        private async Task Get_Login_Page(string uri)
        {
            var getLoginPageResponse = await _client.GetAsync(uri);
            getLoginPageResponse.EnsureSuccessStatusCode();

            // get the document
            var getLoginDocument = await HtmlHelpers.GetDocumentAsync(getLoginPageResponse);
            
            var registerAnchorElement = (IHtmlAnchorElement) getLoginDocument.QuerySelector("#login-register");
            var registerUrl = registerAnchorElement.Href;
            _testOutputHelper.WriteLine("Register url: " + registerUrl);

            await Get_Register_Page(registerUrl);
        }

        private async Task Get_Register_Page(string uri)
        {
            var getRegisterPageResponse = await _client.GetAsync(uri);
            getRegisterPageResponse.EnsureSuccessStatusCode();
            
            // get the document
            var getRegisterDocument = await HtmlHelpers.GetDocumentAsync(getRegisterPageResponse);

            var regForm = (IHtmlFormElement) getRegisterDocument.QuerySelector("#form-register");
            var regFormValues = new Dictionary<string, string>()
            {
                ["Input_Email"] = "test@user.ee",
                ["Input_UserName"] = "test",
                ["Input_Password"] = "DucksG0Quack!",
                ["Input_ConfirmPassword"] = "DucksG0Quack!",
                ["Input_FirstName"] = "Test",
                ["Input_LastName"] = "User",
            };

            var regPostResponse = await _client.SendAsync(regForm, regFormValues);

            regPostResponse.StatusCode.Should().Equals(302);

            var regPostHeader = regPostResponse.Headers.FirstOrDefault(x => x.Key == "Location");
            
            var redirectUri = regPostHeader.Value.FirstOrDefault();
            redirectUri.Should().NotBeNull();
            
            await Get_TestAuthAction_Authenticated(redirectUri!);

        }

        private async Task Get_TestAuthAction_Authenticated(string uri)
        {
            var getTestResponse = await _client.GetAsync(uri);
            getTestResponse.EnsureSuccessStatusCode();
            getTestResponse.StatusCode.Should().Be(200);
            _testOutputHelper.WriteLine($"Uri '{uri}' was accessed with response status code '{getTestResponse.StatusCode}'.");
        }
        
                [Fact]
        public async Task TestAction_Register_School_Owner_Account()
        {
            // ARRANGE
            var uri = "/";

            // ACT
            var getTestResponse = await _client.GetAsync(uri);

            // ASSERT
            Assert.Equal(200, (int) getTestResponse.StatusCode);
            var ownerUsername = "SchoolOwner";
            var ownerPassword = "P@ssw0rd";

            // Register Account
            uri = await RegisterAccount(uri, "school@owner.mail", ownerUsername, "Õpetaja", "Kraavis", ownerPassword);
            (await IsLoggedIn()).Should().BeTrue();
            uri.Should().Be("/");

            // Log out
            uri = await LogOut(uri);
            (await IsLoggedIn()).Should().BeFalse();
            uri.Should().Be("/");
            
            // Log in
            uri = await LoginAccount(uri, ownerUsername, ownerPassword);
            (await IsLoggedIn()).Should().BeTrue();
            uri.Should().Be("/");

            // Log out
            uri = await LogOut(uri);
            (await IsLoggedIn()).Should().BeFalse();
            uri.Should().Be("/");
        }
        
        [Fact]
        public async Task TestAction_Login_School_Owner_Account()
        {
            // ARRANGE
            var uri = "/";

            // ACT
            var getTestResponse = await _client.GetAsync(uri);

            Assert.Equal(200, (int) getTestResponse.StatusCode);
            var ownerUsername = "admin";
            var ownerPassword = "DucksG0Quack!";

            // Log in
            uri = await LoginAccount(uri, ownerUsername, ownerPassword);
            (await IsLoggedIn()).Should().BeTrue();
            uri.Should().Be("/");

            // Log out
            uri = await LogOut(uri);
            (await IsLoggedIn()).Should().BeFalse();
            uri.Should().Be("/");
        }

        private async Task<string> LogOut(string baseUri)
        {
            var pageResponse = await _client.GetAsync(baseUri);
            pageResponse.EnsureSuccessStatusCode();
            // get the document
            var pageDocument = await HtmlHelpers.GetDocumentAsync(pageResponse);
            var verificationTokenElement = (IHtmlInputElement) pageDocument.QuerySelector("#nav-logout input");
            var logoutForm = (IHtmlFormElement) pageDocument.QuerySelector("#nav-logout");
            var logoutFormValues = new Dictionary<string, string>()
            {
                ["__RequestVerificationToken"] = verificationTokenElement.Value,
            };

            await _client.SendAsync(logoutForm, logoutFormValues);

            return baseUri!;
        }


        private async Task<bool> IsLoggedIn()
        {
            var pageResponse = await _client.GetAsync("/");
            pageResponse.EnsureSuccessStatusCode();
            var pageDocument = await HtmlHelpers.GetDocumentAsync(pageResponse);
            var logoutForm = (IHtmlFormElement) pageDocument.QuerySelector("#nav-logout");
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