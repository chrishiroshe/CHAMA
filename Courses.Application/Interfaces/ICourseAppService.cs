using Courses.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Courses.Domain.Interfaces;

namespace Courses.Application.Interfaces
{
    public interface ICourseAppService : IAppServiceBase<Course>
    {
       
        IList<Course> GetCoursesTotal();

        Course CourseSubscription(Course course);

        Task PutCourseSubscriptionQueue(Course course);

        Course ProcessCourseSubscribeTask();

    }

}
