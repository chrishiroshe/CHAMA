using System;
using System.Collections.Generic;
using System.Linq;
using Courses.Domain.Interfaces;

namespace Courses.Domain.Entities
{
    /// <summary>
    /// Class with Course Behaviors and States
    /// </summary>
    public class Course
    {
        #region properties
        //Course ID
        public int Id { get; set; }
        //Course Name
        public string Name { get; set; }
        //Is Course Active
        public bool IsActive { get; set; }
        //Course Create Date
        public DateTime Created { get; set; }
        //Max of Students Available in a Course
        public short MaxAvailability { get; set; }
        //Actual Numbers Of Students in the Course
        public int NumberOfStudents { get; set; }
        //Actual Numbers Of Spaces Available in the Course
        public int ActualAvalability { get; set; }
        //Minimum age
        public int MinAge { get; set; }
        //Max age
        public short MaxAge { get; set; }
        //Average age
        public int AverageAge { get; set; }
        //Responsable Teacher for this Course
        public virtual Teacher Teacher { get; set; }
        // Students who subscribed the Course
        public virtual IEnumerable<Student> Students { get; set; }

        //List Of errors
        public IList<Error> Errors { get; set; }
        #endregion properties

        #region Methods

        public IList<Error> CourseAvailable(Course course)
        {
            if (course == null)
            {
                course = new Course();
            }
            if (course.Errors == null)
            {
                course.Errors = new List<Error>();
            }
            if (course.Id <= 0 )
            {
                course.Errors.Add(new Error() { Message = "Course does not exists" });
            }
            if ((course.MaxAvailability - course.ActualAvalability) <= 0)
            {
                course.Errors.Add(new Error() { Message = "Course unavailable" });
            }
            return course.Errors;
        }

        public IList<Error> CourseValidStudents(Course course)
        {
            if (course.Errors == null)
            {
                course.Errors = new List<Error>();
            }
            if (course.Students == null)
            {
                course.Errors.Add(new Error() { Message = "Course must have a student" });
            }
            if (course.Students!=null && course.Students.Count() > 1)
            {
                course.Errors.Add(new Error() { Message = "One student can be subscribed at time" });
            }
            return course.Errors;
        }
        #endregion Methods

    }
}
