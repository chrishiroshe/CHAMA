using Courses.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Courses.Infra.Data.EntityConfig
{
    /// <summary>
    /// Course database config
    /// </summary>
    public class CourseConfig: IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> builder)
        {
            builder.Property(p => p.Name).HasColumnType("varchar(100)").IsRequired(true);
            builder.Property(p => p.Created).HasColumnType("datetime").IsRequired(true).HasDefaultValueSql("getdate()");
            builder.Property(p => p.IsActive).HasDefaultValue(true);
            
        }
    }
}
