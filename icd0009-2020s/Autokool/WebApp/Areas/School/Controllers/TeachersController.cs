using System;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using BLL.App.DTO;
using Contracts.BLL.App;
using DAL.App.EF;
using Domain.App;
using Domain.App.Constants;
using Domain.App.Identity;
using Extensions.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Microsoft.EntityFrameworkCore;
using WebApp.Areas.School.ViewModels.Teacher;
using Contract = DAL.App.DTO.Contract;
using Course = DAL.App.DTO.Course;

namespace WebApp.Areas.School.Controllers
{
    /// <summary>
    /// Teacher controller with full CRUD functionality
    /// </summary>
    [Area(nameof(WebApp.Areas.School))]
    [Route("School/Teachers/{action=index}/{username?}")]
    [Authorize(Roles = AppRoles.Owner)]
    public class TeachersController : Controller
    {
        private readonly IAppBLL _bll;
        private readonly UserManager<Domain.App.Identity.AppUser> _userManager;


        /// <summary>
        /// Teacher controller constructor
        /// </summary>
        /// <param name="bll"></param>
        /// <param name="userManager"></param>
        public TeachersController(IAppBLL bll, UserManager<AppUser> userManager)
        {
            _bll = bll;
            _userManager = userManager;
        }

        // GET: Teachers
        /// <summary>
        /// Get main page data
        /// </summary>
        /// <param name="inviteSucceeded"></param>
        /// <returns></returns>
        public async Task<IActionResult> Index(string? inviteSucceeded)
        {
            var school = await _bll.DrivingSchools.GetAppUserDrivingSchool(User.GetUserId()!.Value);
            var teachers = await _bll.Contracts.GetSchoolTeachers(school!.Id);
            return View(new TeacherIndexViewModel
            {
                Teachers = teachers,
                InviteSucceeded = inviteSucceeded == null? null : inviteSucceeded.ToLower() == "true"
            });

        }
        
        // GET: Teacher/ContactCourses
        /// <summary>
        /// Get teacher page init data
        /// </summary>
        /// <param name="contractId"></param>
        /// <param name="monthShift"></param>
        /// <returns></returns>
        public async Task<IActionResult> Teacher(Guid? contractId, string? monthShift)
        {
            var monthShiftInt = 0;
            if (monthShift != null)
            {
                monthShiftInt = Convert.ToInt32(monthShift);
            }
            
            var teacher = await _bll.Contracts.GetTeacher(contractId!.Value);

            var contract = await _bll.Contracts.FirstOrDefaultAsync(contractId!.Value);
            var school = await _bll.DrivingSchools.GetAppUserDrivingSchool(User.GetUserId()!.Value);
            if (contract!.DrivingSchoolId != school!.Id)
            {
                return RedirectToAction("");
            }

            
            var time = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(monthShiftInt).ToString("MM-yyyy",
                    CultureInfo.CurrentCulture);
            
            var dateTime = DateTime.ParseExact(time, "MM-yyyy", CultureInfo.CurrentCulture);

            var vm = new TeacherViewModel()
            {
                Teacher = teacher,
                MonthShift = monthShiftInt,
                Time = dateTime,
                PeriodReport = await _bll.Contracts.GetContractPeriodReport((Guid) contractId!, dateTime, dateTime.AddMonths(1).AddSeconds(-1))
            };
            
            return View(vm);

        }
        
        /// <summary>
        /// Invite teacher command
        /// </summary>
        /// <param name="username">Teacher username</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> InviteTeacher(string username)
        {
            var school = await _bll.DrivingSchools.GetAppUserDrivingSchool(User.GetUserId()!.Value);
            var inviteSucceeded =
                await _bll.DrivingSchools.InviteUserToSchool(school!.Id, username, Domain.App.Constants.Titles.Teacher);
            return RedirectToAction(nameof(Index), new {inviteSucceeded= inviteSucceeded.ToString()});
        }
        
        

        // GET: Teachers/Add/5
        /// <summary>
        /// Teacher add to course page init data
        /// </summary>
        /// <param name="contractId">Teacher contract ID</param>
        /// <returns></returns>
        public async Task<IActionResult> AddTeacherToCourse(Guid?  contractId)
        {
            if (contractId == null)
            {
                return NotFound();
            }
            

            var missingCourses = (await _bll.Courses.GetContractMissingCourses(contractId.Value)).ToList();
            var vm = new TeacherCourseAddViewModel
            {
                ContractCourse = new BLL.App.DTO.ContractCourse
                {
                    ContractId = contractId
                },
                CourseSelectList = new SelectList(missingCourses,
                    nameof(Course.Id),
                    nameof(Course.Name)),
                Name = await _bll.Contracts.GetContractorName(contractId.Value)
            };
            return View(vm);
        }
        
        // POST: StudentCourses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        /// <summary>
        /// Add teacher to course handling
        /// </summary>
        /// <param name="vm">TeacherCourseAddViewModel</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddTeacherToCourse(TeacherCourseAddViewModel vm)
        {

            
            ModelState.Remove(nameof(vm.Name));
            if (!ModelState.IsValid) {
                var missingCourses = (await _bll.Courses.GetContractMissingCourses(vm.ContractCourse.Id)).ToList();
                vm.CourseSelectList = new SelectList(missingCourses,
                    nameof(Course.Id),
                    nameof(Course.Name),
                    vm.ContractCourse.Id);
                return View(vm);
            }
            
            var contract = await _bll.Contracts.FirstOrDefaultAsync(vm.ContractCourse.ContractId!.Value);
            var school = await _bll.DrivingSchools.GetAppUserDrivingSchool(User.GetUserId()!.Value);
            if (contract!.DrivingSchoolId != school!.Id)
            {
                return RedirectToAction("");
            }
            
            var status = await _bll.Statuses.GetStatusByName(Domain.App.Constants.Statuses.Active);
            vm.ContractCourse.StatusId = status!.Id;
            _bll.ContractCourses.Add(vm.ContractCourse);
            await _bll.SaveChangesAsync();

            return RedirectToAction(nameof(Teacher), new{contractId = vm.ContractCourse.ContractId});
        }
        

        // POST: Teachers/Delete/5
        /// <summary>
        /// Delete Teacher contract
        /// </summary>
        /// <param name="id">Teacher contract ID</param>
        /// <returns></returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var contract = await _bll.Contracts.FirstOrDefaultAsync(id);
            var school = await _bll.DrivingSchools.GetAppUserDrivingSchool(User.GetUserId()!.Value);
            if (contract!.DrivingSchoolId != school!.Id)
            {
                return RedirectToAction("");
            }
            
            await _bll.Contracts.RemoveAsync(id);
            await _bll.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        
        // POST: TeacherCourse/Delete/5
        /// <summary>
        /// Teacher Course removal request
        /// </summary>
        /// <param name="teacherContractId">Teacher Contract ID</param>
        /// <param name="teacherCourseId">Contract Course ID</param>
        /// <returns></returns>
        [HttpPost, ActionName("TeacherCourseDelete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> TeacherCourseDeleteConfirmed(Guid teacherContractId, Guid teacherCourseId)
        {
            var drivingSchool = await _bll.DrivingSchools.GetDrivingSchoolByContract(teacherContractId);
            if (drivingSchool == null)
            {
                return RedirectToAction(nameof(Index));
            }
            var validatedUser = await _bll.DrivingSchools.IsOwner(User.GetUserId()!.Value, drivingSchool.Id);
            if (!validatedUser)
            {
                return RedirectToAction(nameof(Index));
            }
            await _bll.ContractCourses.RemoveAsync(teacherCourseId);
            await _bll.SaveChangesAsync();
            return RedirectToAction(nameof(Teacher), new {contractId = teacherContractId});
        }
        
        // POST: TeacherCourse/Edit/5
        /// <summary>
        /// Teacher confirms course invite
        /// </summary>
        /// <param name="teacherContractId">Teacher contract ID</param>
        /// <param name="teacherCourseId">Invited Course (Contract Course in Domain) ID</param>
        /// <returns></returns>
        [HttpPost, ActionName("EditTeacherCourse")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditTeacherCourseConfirmed(Guid teacherContractId, Guid teacherCourseId)
        {
            var hourlyPayString = String.Format("{0}", Request.Form["courseHourlyPay"]);
            double hourlyPay = 0;
            bool validInput = false;
            try
            {
                hourlyPay = Double.Parse(hourlyPayString);
                if (hourlyPay >= 0)
                {
                    validInput = true;
                }
            } catch (Exception ) { }

            if (!validInput)
            {
                return RedirectToAction(nameof(Index));
            }
            var drivingSchool = await _bll.DrivingSchools.GetDrivingSchoolByContract(teacherContractId);
            if (drivingSchool == null)
            {
                return RedirectToAction(nameof(Index));
            }

            var validatedUser = await _bll.DrivingSchools.IsOwner(User.GetUserId()!.Value, drivingSchool.Id);
            if (!validatedUser)
            {
                return RedirectToAction(nameof(Index));
            }

            var contractCourse = await _bll.ContractCourses.FirstOrDefaultAsync(teacherCourseId);
            if (contractCourse == null)
            {
                return RedirectToAction(nameof(Index));
            }
            contractCourse.HourlyPay = hourlyPay;
            _bll.ContractCourses.Update(contractCourse);
            await _bll.SaveChangesAsync();
            return RedirectToAction(nameof(Teacher), new {contractId = teacherContractId});
        }
        
        
        
    }
}
