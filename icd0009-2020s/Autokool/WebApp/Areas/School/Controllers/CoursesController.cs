using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Contracts.BLL.App;
using Domain.App.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BLL.App.DTO;
using Extensions.Base;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebApp.Areas.School.ViewModels.CourseRequirement;
using WebApp.Debugging;

namespace WebApp.Areas.School.Controllers
{
    /// <summary>
    /// Course controller with full CRUD functionality for course object
    /// </summary>
    [Area(nameof(WebApp.Areas.School))]
    [Route("School/StudentCourses/{action=index}/{username?}")]
    [Authorize(Roles = AppRoles.Owner)]
    public class CoursesController : Controller
    {
        private readonly IAppBLL _bll;

        /// <summary>
        /// Course controller constructor
        /// </summary>
        /// <param name="bll"></param>
        public CoursesController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: StudentCourses
        /// <summary>
        /// Get init data for main course page
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Index()
        {
            var drivingSchool = await _bll.DrivingSchools.GetAppUserDrivingSchool(User.GetUserId()!.Value);
            var courses = drivingSchool?.Courses?.ToList() ?? new List<Course>();
            return View(courses);

        }



        // GET: StudentCourses/Create
        /// <summary>
        /// Get init data for creation page
        /// </summary>
        /// <returns></returns>
        public IActionResult Create()
        {
            return View();
        }

        // POST: StudentCourses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        /// <summary>
        /// Handle Course creation using user inputs
        /// </summary>
        /// <param name="course"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Description,Price,Category,DrivingSchoolId,Id")] Course course)
        {
            if (ModelState.IsValid)
            {
                var drivingSchool = await _bll.DrivingSchools.GetAppUserDrivingSchool(User.GetUserId()!.Value);
                course.DrivingSchoolId = drivingSchool?.Id;
                _bll.Courses.Add(course);
                await _bll.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(course);
        }

        // GET: StudentCourses/Edit/5
        /// <summary>
        /// Get Course edit init data
        /// </summary>
        /// <param name="id">Course Id</param>
        /// <returns></returns>
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var drivingSchool = await _bll.DrivingSchools.GetAppUserDrivingSchool(User.GetUserId()!.Value);
            var collection = drivingSchool?.Courses;
            var exists = collection != null && collection.Any(cs => cs.Id == id);
            if (!exists)
            {
                return NotFound();
            }
            var course = await _bll.Courses.FirstOrDefaultAsync(id.Value);
            
            var vm = new EditViewModel()
            {
                Course = course!,
                AllCourseRequirements = await _bll.CourseRequirements.GetAllByCourseId(course!.Id),
            };
            vm.RequirementSelectList = new SelectList(await _bll.Requirements.GetCourseMissingRequirements(vm.Course.Id),
                nameof(Requirement.Id),
                nameof(Requirement.Name));
            return View(vm);
        }

        // POST: StudentCourses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        /// <summary>
        /// Handle Course editing using user inputs
        /// </summary>
        /// <param name="vm">Edit view model</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditViewModel vm)
        {
            var drivingSchool = await _bll.DrivingSchools.GetAppUserDrivingSchool(User.GetUserId()!.Value);
            var collection = drivingSchool?.Courses;
            var exists = collection != null;
            vm.Course.DrivingSchoolId = drivingSchool?.Id;
            ModelState.Remove(nameof(vm.AllCourseRequirements));
            if (!ModelState.IsValid || !exists) {
                return View(vm);
            }
            

            _bll.Courses.Update(vm.Course);
            await _bll.SaveChangesAsync();

            return RedirectToAction(nameof(Edit), new{id = vm.Course.Id});
        }
        

        // POST: StudentCourses/Delete/5
        /// <summary>
        /// Delete course handling
        /// </summary>
        /// <param name="id">Course Id</param>
        /// <returns></returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var drivingSchool = await _bll.DrivingSchools.GetAppUserDrivingSchool(User.GetUserId()!.Value);
            var collection = drivingSchool?.Courses;
            var exists = collection != null && collection.Any(cs => cs.Id == id);
            if (!exists)
            {
                return NotFound();
            }
            
            await _bll.Courses.RemoveAsync(id);
            await _bll.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        
        // GET: CourseRequirements/Edit/5
        /// <summary>
        /// Requirement edit page init data
        /// </summary>
        /// <param name="id">Requirement Id</param>
        /// <returns></returns>
        public async Task<IActionResult> RequirementEdit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var courseRequirement = await _bll.CourseRequirements.FirstOrDefaultAsync(id.Value);

            if (courseRequirement == null)
            {
                return NotFound();
            }

            var vm = new CourseRequirementEditViewModel();
            vm.CourseRequirement = courseRequirement;
            vm.RequirementSelectList = new SelectList(await _bll.Requirements.GetAllAsync(),
                nameof(Requirement.Id),
                nameof(Requirement.Name),
                vm.CourseRequirement.RequirementId);
            
            vm.Course = courseRequirement.Course!;

            
            return View(vm);
        }

        // POST: CourseRequirements/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        /// <summary>
        /// Edit requirement using user input
        /// </summary>
        /// <param name="vm">CourseRequirementEditViewModel</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RequirementEdit(CourseRequirementEditViewModel vm)
        {
            ModelState.Remove("Course");
            if (ModelState.IsValid)
            {
                _bll.CourseRequirements.Update(vm.CourseRequirement);
                await _bll.SaveChangesAsync();
                return RedirectToAction(nameof(Edit), new{id = vm.CourseRequirement.CourseId});
            }

            vm.RequirementSelectList = new SelectList(await _bll.Requirements.GetAllAsync(),
                nameof(Requirement.Id),
                nameof(Requirement.Name),
                vm.CourseRequirement.RequirementId);

            return View(vm);
        }
        
        // POST: CourseRequirements/Delete/5
        /// <summary>
        /// Delete requirement using ID
        /// </summary>
        /// <param name="id">Delete requirement ID</param>
        /// <returns></returns>
        [HttpPost, ActionName("RequirementDelete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RequirementDeleteConfirmed(Guid id)
        {
            var courseId = (await _bll.CourseRequirements.FirstOrDefaultAsync(id))!.CourseId;
            await _bll.CourseRequirements.RemoveAsync(id);
            await _bll.SaveChangesAsync();
            return RedirectToAction(nameof(Edit), new{ id = courseId});
        }
        

        // POST: CourseRequirements/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        /// <summary>
        /// Requirement Creation handling using ID
        /// </summary>
        /// <param name="vm">EditViewModel</param>
        /// <returns></returns>
        /// <exception cref="DataException"></exception>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RequirementCreate(EditViewModel vm)
        {
            ModelState.Remove("Course");
            ModelState.Remove("CourseRequirement");
            ModelState.Remove("AllCourseRequirements");
            ModelState.Remove("CourseRequirement.Description");
            if (ModelState.IsValid)
            {
                vm.CourseRequirement = await _bll.CourseRequirements.CreateWithRequirementFields(vm.CourseRequirement.RequirementId ?? throw new DataException("No Course Requirement"), vm.CourseRequirement.CourseId ?? throw new DataException("No Course Requirement"));
                return RedirectToAction(nameof(Edit), new{ id = vm.CourseRequirement.CourseId});
            }
            else
            {
                Console.WriteLine(ErrorMessages.GetModelStateErrors(ModelState));
            }

            
            vm.RequirementSelectList = new SelectList(await _bll.Requirements.GetAllAsync(),
                nameof(Requirement.Id),
                nameof(Requirement.Name),
                vm.CourseRequirement.RequirementId);
            
            
            
            
            return RedirectToAction(nameof(Edit), new{ id = vm.CourseRequirement.CourseId});
        }

    }
}
