using Courses.Domain.Entities;
using Courses.Domain.Interfaces;
using Courses.Infra.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Runtime.InteropServices;
using System.Text;
using Castle.DynamicProxy.Generators.Emitters.SimpleAST;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions.Internal;
using Microsoft.EntityFrameworkCore.SqlServer;

namespace Courses.Infra.Data.Repositories
{
    /// <summary>
    /// Course Repository
    /// </summary>
    public class CourseRepository : RepositoryBase<Course>, ICourseRepository
    {
        #region Variables 

        protected new readonly ProjectCourseContext _context;

        #endregion Variables 

        #region Constructor

        /// <summary>
        /// Constructor Context
        /// </summary>
        /// <param name="context">context</param>
        public CourseRepository(ProjectCourseContext context) : base(context)
        {
            _context = context;
        }

        #endregion Constructor

        #region methods


        /// <summary>
        /// Get the totals from students, average age, min/max age, availability
        /// </summary>
        /// <returns></returns>
        public IList<Course> GetCoursesTotal()
        {
            IList<Course> lstCourses = new List<Course>();
            try
            {
                using (var command = _context.Database.GetDbConnection().CreateCommand())
                {
                    command.CommandText = "EXEC GetCoursesTotal";
                    _context.Database.OpenConnection();
                    using (var result = command.ExecuteReader())
                    {
                        while (result.Read())
                        {
                            Course courses = new Course();
                            courses.Id = (int) result.GetValue(0);
                            courses.IsActive = (bool) result.GetValue(1);
                            courses.Created = (DateTime) result.GetValue(2);
                            courses.MaxAvailability = (short) result.GetValue(4);
                            courses.ActualAvalability = (int) result.GetValue(5);
                            courses.MinAge = (short) result.GetValue(6);
                            courses.MaxAge = (short) result.GetValue(7);
                            courses.AverageAge = (int) result.GetValue(8);
                            courses.Name = (string) result.GetValue(9);
                            courses.NumberOfStudents = (int) result.GetValue(10);
                            lstCourses.Add(courses);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //TODO:IMPLENTO LOG
                throw ex;
            }
            return lstCourses;
        }
    
        /// <summary>
        /// Create a Subscription in a Course
        /// </summary>
        /// <returns>Course</returns>
        public Course CourseSubscription(Course course)
        {
            try
            {
                _context.Set<Student>().Add(course.Students.First());
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                //TODO: IMPLEMENT LOG
                if (course.Errors == null)
                    course.Errors = new List<Error>();
                course.Errors.Add(new Error() { Message = ex.Message});
            }

            return course;
        }
        
        #endregion methods
    }
}
