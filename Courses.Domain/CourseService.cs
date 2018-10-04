using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Courses.Domain.Services;
using Courses.Domain.Interfaces;
using Courses.Domain.Entities;

namespace Courses.Domain
{
    public class CourseService : ServiceBase<Course>, IServiceCourse
    {
        private readonly ICourseRepository _courseRepository;
      
        public CourseService(ICourseRepository courseRepository)
            : base(courseRepository)
        {
            _courseRepository = courseRepository;
          
        }

        public IList<Error> CourseAvailable(Course course)
        {
            int courseId = course.Students.First().CourseId;
            int actualAvailability = 0;
            try
            {

                var existsCourse = _courseRepository.GetById(courseId);
                var totals = _courseRepository.GetCoursesTotal().Where(p => p.Id == courseId);
                if (totals != null && totals.Count() >0)
                {
                    actualAvailability = totals.First().ActualAvalability;
                    existsCourse.ActualAvalability = actualAvailability;
                }
                return course.CourseAvailable(existsCourse );
            }
            catch (Exception e)
            {
                //TODO: IMPLEMENT LOG
            }
            return null;

        }

        public IList<Error> CourseValidStudents(Course course)
        {
            return course.CourseValidStudents(course);
        }

        public IList<Course> GetCoursesTotal()
        {
            return _courseRepository.GetCoursesTotal();
        }

        /// <summary>
        /// Create a Subscription in a Course
        /// </summary>
        /// <returns>Course</returns>
        public Course CourseSubscription(Course course)
        {
            try
            {
                course.Errors = CourseAvailable(course);
                var courseValidaton = CourseValidStudents(course);
                if (courseValidaton.Count > 0)
                {
                    foreach (var error in courseValidaton)
                    {
                        course.Errors.Add(error);
                    }
                }

                if (course.Errors.Count > 0)
                    return course;
                else
                    return _courseRepository.CourseSubscription(course);
            }
            catch (Exception e)
            {
                course.Errors.Add(new Error() {Message = e.Message});

            ;
            //TODO: IMPLEMENT LOG
        }
            return null;

        }
    }
}
