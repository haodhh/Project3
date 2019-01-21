namespace Project3.Data.Database
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class ProjectDbContext : DbContext
    {
        public ProjectDbContext()
            : base("name=ProjectDbContext")
        {
        }

        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<Car> Cars { get; set; }
        public virtual DbSet<CurrentState> CurrentStates { get; set; }
        public virtual DbSet<History> Historys { get; set; }
        public virtual DbSet<KeyMonitor> KeyMonitors { get; set; }
        public virtual DbSet<Log> Logs { get; set; }
        public virtual DbSet<MonitorOnline> MonitorOnlines { get; set; }
        public virtual DbSet<RFIDCard> RFIDCards { get; set; }
        public virtual DbSet<Schedule> Schedules { get; set; }
        public virtual DbSet<Student> Students { get; set; }
        public virtual DbSet<Teacher> Teachers { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Log>()
                .Property(e => e.Notes)
                .IsUnicode(false);
        }
    }
}
