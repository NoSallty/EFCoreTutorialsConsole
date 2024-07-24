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
        public DbSet<StudentAddress> StudentAddresses { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<StudentCourse> StudentCourses { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=LAPTOP-RC7DGFLL\\SQLEXPRESS;Initial Catalog=SchoolDomain;Integrated Security=True;Encrypt=False");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Write Fluent API configurations here

            //Property Configurations
            modelBuilder.Entity<Student>()
                    .Property(s => s.FirstName)
                    .HasColumnName("FirstName")
                    .HasDefaultValue(0)
                    .IsRequired();
            //Separate method calls
            //modelBuilder.Entity<Student>().Property(s => s.StudentId).HasColumnName("Id");
            //modelBuilder.Entity<Student>().Property(s => s.StudentId).HasDefaultValue(0);
            //modelBuilder.Entity<Student>().Property(s => s.StudentId).IsRequired();

            modelBuilder.Entity<Grade>()
                    .Property(g => g.GradeName)
                    .HasColumnName("GradeName")
                    .IsRequired();

            modelBuilder.Entity<Student>()
                    .HasOne<Grade>(s => s.Grade)
                    .WithMany(g => g.Students)
                    .HasForeignKey(s => s.GradeId)
                    .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<StudentAddress>()
                    .HasOne<Student>(ad => ad.Student)
                    .WithOne(s => s.Address)
                    .HasForeignKey<StudentAddress>(ad => ad.AddressOfStudentId);

            modelBuilder.Entity<StudentCourse>().HasKey(sc => new { sc.StudentId, sc.CourseId });

            modelBuilder.Entity<StudentCourse>()
                .HasOne<Student>(sc => sc.Student)
                .WithMany(s => s.StudentCourses)
                .HasForeignKey(sc => sc.StudentId);


            modelBuilder.Entity<StudentCourse>()
                .HasOne<Course>(sc => sc.Course)
                .WithMany(s => s.StudentCourses)
                .HasForeignKey(sc => sc.CourseId);
        }
    }
}
