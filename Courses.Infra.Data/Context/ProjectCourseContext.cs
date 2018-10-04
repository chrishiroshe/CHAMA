using Courses.Domain.Entities;
using Courses.Infra.Data.EntityConfig;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography;
using System.Text;
using Courses.Infra.Data.Migrations;

namespace Courses.Infra.Data.Context
{
    /// <summary>
    /// Class with Base Project Database Context
    /// </summary>
  
    public class ProjectCourseContext: DbContext
    {
        public DbSet<Course> Course { get; set; }
        public DbSet<Student> Student { get; set; }
        public DbSet<Teacher> Teacher { get; set; }
     
        public ProjectCourseContext(DbContextOptions<ProjectCourseContext> options) : base(options)
        {
           
        }
        /// <summary>
        /// Override some fiels database behaviors
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region Identification Fields
            modelBuilder.Entity<Course>().HasKey(p => p.Id);
            modelBuilder.Entity<Student>().HasKey(p => p.Id);
            modelBuilder.Entity<Teacher>().HasKey(p => p.Id);
            #endregion Identification Fields

            #region Ignored Fields
            modelBuilder.Entity<Course>().Ignore(p=> p.Errors);
            modelBuilder.Entity<Course>().Ignore(p => p.ActualAvalability);
            modelBuilder.Entity<Course>().Ignore(p => p.NumberOfStudents);
            modelBuilder.Entity<Course>().Ignore(p => p.AverageAge);
            modelBuilder.Entity<Course>().Ignore(p => p.MaxAge);
            modelBuilder.Entity<Course>().Ignore(p => p.MinAge);
        
            #endregion Ignored Fields

            modelBuilder.ApplyConfiguration(new CourseConfig())
                .ApplyConfiguration(new StudentConfig())
                .ApplyConfiguration(new TeacherConfig());


            base.OnModelCreating(modelBuilder);

        }

        /// <summary>
        /// Override Configuring Context and Enable Lazy Load
        /// </summary>
        /// <param name="optionsBuilder"></param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder
                .UseLazyLoadingProxies();

        public override int SaveChanges()
        {
            ChangeTracker.DetectChanges();
            return base.SaveChanges();
        }
     
    }
}
