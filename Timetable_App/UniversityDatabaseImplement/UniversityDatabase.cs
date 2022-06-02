using TimetableDatabaseImplement.Models;
using Microsoft.EntityFrameworkCore;

namespace  TimetableDatabaseImplement
{
    class UniversityDatabase : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder.IsConfigured == false)
            {
                optionsBuilder.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog= TimetableDB;Integrated Security=True;MultipleActiveResultSets=True;");
            }
            base.OnConfiguring(optionsBuilder);
        }
        public virtual DbSet<Group> Groups { set; get; }
        public virtual DbSet<Subject> Subjects { set; get; }
        public virtual DbSet<Lector> Lectors { set; get; }
        public virtual DbSet<Deneary> Denearies { set; get; }
        public virtual DbSet<Timetable> Timetables { set; get; }

    }
}
