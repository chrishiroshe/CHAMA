using Courses.Application.Interfaces;
using Courses.Domain.Entities;
using Courses.Domain.Interfaces;
using Courses.Domain.Services;
using System;
using System.Collections.Generic;
using System.Text;
using Courses.ServiceBus;
using Courses.Email.Interface;
using System.Threading.Tasks;
using Courses.Email.Providers;
using Courses.Email;
using Newtonsoft.Json;

namespace Courses.Application
{
    public class CourseAppService : AppServiceBase<Course>, ICourseAppService
    {
        private readonly IServiceCourse _courseService;
        public IEmail _courseEmail;
        public CourseAppService(IServiceCourse courseService)
            : base(courseService)
        {
            _courseService = courseService;
        }


        public IList<Course> GetCoursesTotal()
        {
            return _courseService.GetCoursesTotal();
        }

        /// <summary>
        /// Create a Subscription in a Course
        /// </summary>
        /// <returns>Course</returns>
        public Course CourseSubscription(Course course)
        {
            return _courseService.CourseSubscription(course);
        }
        /// <summary>
        /// Put a Course Subscription in Async Queue With Azure Service Bus
        /// </summary>
        /// <returns>Task</returns>
        public async Task PutCourseSubscriptionQueue(Course course)
        {
            CourseBUS courseServiceBus = new CourseBUS();
            await courseServiceBus.PutCourseSubscriptionQueue(course);
        }

        /// <summary>
        /// Get a Course Subscription in Async Queue With Azure Service Bus
        /// </summary>
        /// <returns>Task</returns>
        public Course ProcessCourseSubscribeTask()
        {
            try
            {
                CourseBUS courseServiceBus = new CourseBUS();
                var message = courseServiceBus.ProcessCourseSubscriptionQueue();
                var course = JsonConvert.DeserializeObject<Course>(Encoding.UTF8.GetString(message.Result.Body));
                _courseEmail = new SMTPProvider();
                _courseEmail.SendMail(new Email.Email());
                return CourseSubscription(course);
            }
            catch (Exception e)
            {
                //TODO: IMPLEMENT ERROR ANT TREAMENT
                Console.WriteLine(e);
                throw;
            }

            //return CourseSubscription(course);
        }
    }
}
