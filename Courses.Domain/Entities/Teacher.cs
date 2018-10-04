using System;
using System.Collections.Generic;
using System.Text;

namespace Courses.Domain.Entities
{
    /// <summary>
    /// Class with Teacher Behaviors and States
    /// </summary>
    public class Teacher
    {
        #region properties
        //Teacher ID
        public int Id { get; set; }
        //Teacher Name
        public string Name { get; set; }
        //Teacher Date Register
        public DateTime Created { get; set; }
        //Teacher Courses
        public virtual IEnumerable<Course> Courses { get; set; }

         #endregion

        #region Methods
        #endregion Methods
    }
}
