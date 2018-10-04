using Courses.Domain.Entities;
using Courses.Domain.Interfaces;
using Courses.Infra.Data.Context;
using System;
using System.Collections.Generic;
using System.Text;

namespace Courses.Infra.Data.Repositories
{
    /// <summary>
    /// Student Repository
    /// </summary>
    public class StudentRepository : RepositoryBase<Student>, IStudentRepository
    {
        #region Variables 
        protected new readonly ProjectCourseContext _context;
        #endregion Variables 

        #region Constructor
        /// <summary>
        /// Student Context
        /// </summary>
        /// <param name="context"></param>
        public StudentRepository(ProjectCourseContext context) : base(context)
        {
            _context = context;
        }
        #endregion Constructor
    }
}
