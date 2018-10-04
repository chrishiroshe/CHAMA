using Courses.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using Courses.Domain.Interfaces;

namespace Courses.Domain.Services
{
    public interface IServiceCourse : IServiceBase<Course>
    {
        IList<Course> GetCoursesTotal();

        Course CourseSubscription(Course course);
    }
}
