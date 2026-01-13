using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using office360.Models.DAL.DTO;

namespace office360.Models.DAL
{
    public class EMSIntegCubeEntities : DbContext
    {
        public EMSIntegCubeEntities()
            : base("name=EMSIntegCubeEntities")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<System.Data.Entity.ModelConfiguration.Conventions.PluralizingTableNameConvention>();
            base.OnModelCreating(modelBuilder);
        }
        public virtual DbSet<PaymentMethod> PaymentMethod { get; set; }
        public virtual DbSet<ExpenseCategory> ExpenseCategory { get; set; }
        public virtual DbSet<Branch> Branch { get; set; }
        public virtual DbSet<Company> Company { get; set; }
        public virtual DbSet<RightSetting> RightSetting { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<UserRight> UserRight { get; set; }
        public virtual DbSet<PCRIncome> PCRIncome { get; set; }
        public virtual DbSet<PCRExpense> PCRExpense { get; set; }



    }
}
