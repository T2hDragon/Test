using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using BLL.App.DTO;
using Contracts.BLL.App;
using Domain.App.Constants;
using Domain.App.Identity;
using Extensions.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Logging;
using Microsoft.JSInterop;
using Newtonsoft.Json.Serialization;
using WebApp.Models;

namespace WebApp.Controllers
{
    /// <summary>
    /// Home controller 
    /// </summary>
    public class HomeController : Controller
    {
        private readonly IAppBLL _bll;

        /// <summary>
        /// Home controller constructor
        /// </summary>
        /// <param name="bll"></param>
        public HomeController(IAppBLL bll)
        {
            _bll = bll;
        }

        /// <summary>
        /// Get data for Index page
        /// </summary>
        /// <param name="drivingSchool"></param>
        /// <returns></returns>
        public IActionResult Index(DrivingSchool? drivingSchool)
        {

            var userId = new Guid?();

            try
            {
                userId = User.GetUserId();

            }
            catch (Exception)
            {
                return View(new DrivingSchool());
            }

            if (userId == null)
            {
                return View(new DrivingSchool());
            }
            
            drivingSchool = _bll.DrivingSchools.GetAppUserDrivingSchool(userId.Value).Result!;
            return View(drivingSchool);
        }
        
        // POST: DrivingSchools/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        /// <summary>
        /// Edit Users Driving school name and description
        /// </summary>
        /// <param name="drivingSchool"></param>
        /// <returns></returns>
        [HttpPost, ActionName("Index")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(DrivingSchool drivingSchool)
        {
            if (ModelState.IsValid)
            {
                _bll.DrivingSchools.Update(drivingSchool);
                await _bll.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }


            return View(drivingSchool);
        }

        /// <summary>
        /// Privacy page get data
        /// </summary>
        /// <returns></returns>
        public IActionResult Privacy()
        {
            return View();
        }

        /// <summary>
        /// Get errors
        /// </summary>
        /// <returns></returns>
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }
        
        /// <summary>
        /// Set page language
        /// </summary>
        /// <param name="culture"></param>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        public IActionResult SetLanguage(string culture, string returnUrl)
        {
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions()
                {
                    Expires = DateTimeOffset.UtcNow.AddYears(1)
                }
            );

            return LocalRedirect(returnUrl);
        }
    }
}