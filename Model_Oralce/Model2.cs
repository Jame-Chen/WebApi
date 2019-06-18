namespace Model_Oralce
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Threading;

    public partial class Model2 : DbContext
    {
        public Model2()
            : base("name=Model2")
        {
            //将执行的sql语句记录到日志
            Database.Log = message => Console.WriteLine("[{0}]{1}-- {2}", Thread.CurrentThread.ManagedThreadId, DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff"), message.Trim());
        }

        public virtual DbSet<T_PATROL_RECODE> T_PATROL_RECODE { get; set; }
        public virtual DbSet<T_PERSON_NAME> T_PERSON_NAME { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<T_PATROL_RECODE>()
                .Property(e => e.S_RECODE_ID)
                .IsUnicode(false);

            modelBuilder.Entity<T_PATROL_RECODE>()
                .Property(e => e.N_MILEAGE)
                .HasPrecision(38, 8);

            modelBuilder.Entity<T_PERSON_NAME>()
                .Property(e => e.S_MAN_ID)
                .IsUnicode(false);

            modelBuilder.Entity<T_PERSON_NAME>()
                .Property(e => e.N_X)
                .HasPrecision(38, 8);

            modelBuilder.Entity<T_PERSON_NAME>()
                .Property(e => e.N_Y)
                .HasPrecision(38, 8);

            modelBuilder.Entity<T_PERSON_NAME>()
                .Property(e => e.S_ID)
                .IsUnicode(false);
        }
    }
}
