using System;
using System.Collections.Generic;
using System.Text;

namespace Courses.Domain.Entities
{
    /// <summary>
    /// Class with Student Behaviors and States
    /// </summary>
    public class Student
    {
        #region properties
        //Student ID
        public int Id { get; set; }
        //Student Name
        public string Name { get; set; }
        //Student Age
        public short Age { get; set; }
        //Student Date Register
        public DateTime Created { get; set; }
        //Student Date Register
        public int CourseId { get; set; }

        #endregion

        #region Methods

        #endregion Methods
    }
}
