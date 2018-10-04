using Courses.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Courses.Domain.Interfaces
{
    public interface ICourseRepository : IRepositoryBase<Course>
    {
        IList<Course> GetCoursesTotal();

        Course CourseSubscription(Course course);
    }
}
