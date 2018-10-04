using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Courses.Application.Interfaces;
using Courses.Domain.Entities;

namespace Courses.WebApi.Controllers
{
    [Route("api/course")]
    public class CourseController : Controller
    {
        private readonly ICourseAppService _courseApp;
        private readonly IAppServiceBase<Course> _courseBaseApp;

        public CourseController(ICourseAppService courseApp, IAppServiceBase<Course> courseBaseApp)
        {
            _courseApp = courseApp;
            _courseBaseApp = courseBaseApp;
         
        }
         /// <summary>
         /// Get all Courses
         /// </summary>
         /// <returns></returns>
        [HttpGet("GetAll/")]
        public JsonResult Get()
        {
            try
            {
                var ret = _courseApp.GetAll();
                return Json(ret);
            }
            catch (Exception e)
            {
                //TODO: IMPLEMENT LOG AND ERRO TREATMENT
                return Json("Error");
            }
        }
        /// <summary>
        /// Get Details of a specific Course
        /// </summary>
        /// <returns>Json for Courses Details</returns>
        [HttpGet("GetDetails/{id}")]
        public JsonResult GetDetails(int id)
        {
            try
            {
                var ret = _courseApp.GetById(id);
                return Json(ret);
            }
            catch (Exception e)
            {
                //TODO: IMPLEMENT LOG AND ERRO TREATMENT
                return Json("Error");
            }
        }
    
        /// <summary>
        /// GetTotals Of Available Spots in a Course, Min Age, Max Age, Average Age
        /// </summary>
        /// <returns>Json of Totals from every courses</returns>
        [HttpGet("GetTotals/")]
        public JsonResult GetTotals()
        {
            try
            {

                var ret = _courseApp.GetCoursesTotal();
                return Json(ret);
            }
            catch (Exception e)
            {
                //TODO: IMPLEMENT LOG AND ERRO TREATMENT
                return Json("Error");
            }
        }
        /// <summary>
        /// Subscribe a Course
        /// </summary>
        /// <param name="course">Course Json</param>
        [HttpPost("Subscribe/")]
        public JsonResult Subscribe([FromBody]Courses.Domain.Entities.Course course)
        {
            try
            {
                var ret = _courseApp.CourseSubscription(course);
            }
            catch (Exception e)
            {
                //TODO: IMPLEMENT LOG AND ERRO TREATMENT
                return Json("Error");
            }
            return Json("OK");
        }

        /// <summary>
        /// Put a Course in a Async Queue
        /// </summary>
        /// <param name="course">Course Json</param>
        [HttpPost("SubscribeAsync/")]
        public async Task SubscribeAsync([FromBody]Course course)
        {
            try
            {
                await _courseApp.PutCourseSubscriptionQueue(course);
            }
            catch (Exception e)
            {
                //TODO: IMPLEMENT LOG AND ERRO TREATMENT
                //return Json("Error");
            }
           // return Json("OK");
        }

        /// <summary>
        /// Subscribe a Course Async
        /// </summary>
        
        [HttpGet("SubscribeAsync/")]
        public JsonResult SubscribeAsync()
        {
            try
            {
                var ret = _courseApp.ProcessCourseSubscribeTask();
            }
            catch (Exception e)
            {
                //TODO: IMPLEMENT LOG AND ERRO TREATMENT
                return Json("Error");
            }
            return Json("OK");
        }


    }
}
