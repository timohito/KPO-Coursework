using Microsoft.EntityFrameworkCore;
using TimetableDatabaseImplement.Models;

namespace TimetableDatabaseImplement
{
    public class TimetableDatabase : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder.IsConfigured == false)
            {
                optionsBuilder.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog= TimetableDB_V3;Integrated Security=True;MultipleActiveResultSets=True;");
            }
            base.OnConfiguring(optionsBuilder);
        }
        public virtual DbSet<Group> Groups { set; get; }
        public virtual DbSet<Subject> Subjects { set; get; }
        public virtual DbSet<Lector> Lectors { set; get; }
        public virtual DbSet<Deneary> Denearies { set; get; }
        public virtual DbSet<Timetable> Timetables { set; get; }
        public virtual DbSet<LectorSubject> LectorSubjects { set; get; }
        public virtual DbSet<Classroom> Classrooms { set; get; }
    }
}