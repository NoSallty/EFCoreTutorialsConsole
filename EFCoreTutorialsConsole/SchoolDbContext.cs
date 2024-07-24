using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCoreTutorialsConsole
{
    internal class SchoolDbContext : DbContext
    {
        public DbSet<Student> Students { get; set; }
        public DbSet<Grade> Grades { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=LAPTOP-RC7DGFLL\\SQLEXPRESS;Initial Catalog=SchoolDomain;Integrated Security=True;Encrypt=False");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Write Fluent API configurations here

            //Property Configurations
            modelBuilder.Entity<Student>()
                    .Property(s => s.StudentId)
                    .HasColumnName("Id")
                    .HasDefaultValue(0)
                    .IsRequired();
            modelBuilder.Entity<Student>()
                    .HasOne<Grade>(s => s.Grade)
                    .WithMany(g => g.Students)
                    .HasForeignKey(s => s.GradeId);
            //Separate method calls
            //modelBuilder.Entity<Student>().Property(s => s.StudentId).HasColumnName("Id");
            //modelBuilder.Entity<Student>().Property(s => s.StudentId).HasDefaultValue(0);
            //modelBuilder.Entity<Student>().Property(s => s.StudentId).IsRequired();
        }
    }
}
