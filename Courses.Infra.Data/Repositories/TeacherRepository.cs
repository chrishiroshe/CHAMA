using Courses.Domain.Entities;
using Courses.Domain.Interfaces;
using Courses.Infra.Data.Context;
using System;
using System.Collections.Generic;
using System.Text;

namespace Courses.Infra.Data.Repositories
{
    /// <summary>
    /// Teacher Repository
    /// </summary>
    public class TeacherRepository : RepositoryBase<Teacher>, ITeacherRepository
    {
        #region Variables 
        protected new readonly ProjectCourseContext _context;
        #endregion Variables 

        #region Constructor
        /// <summary>
        /// Teacher Context
        /// </summary>
        /// <param name="context"></param>
        public TeacherRepository(ProjectCourseContext context) : base(context)
        {
            _context = context;
        }
        #endregion Constructor
    }
}
