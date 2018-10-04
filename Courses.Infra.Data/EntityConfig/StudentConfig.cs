using Courses.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Courses.Infra.Data.EntityConfig
{
   
    public class StudentConfig: IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder.Property(p => p.Name).HasColumnType("varchar(200)").IsRequired(true);
            builder.Property(p => p.Age).IsRequired(true);
            builder.Property(p => p.Created).HasColumnType("datetime").IsRequired(true).HasDefaultValueSql("getdate()");
        }
    }
}
