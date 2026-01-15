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
        public virtual DbSet<LK_PaymentMethod> LK_PaymentMethod { get; set; }
        public virtual DbSet<LK_ExpenseCategory> LK_ExpenseCategory { get; set; }
        public virtual DbSet<LK_Designation> LK_Designation { get; set; }
        public virtual DbSet<LK_Bank> LK_Bank { get; set; }
        public virtual DbSet<LK_EmploymentType> LK_EmploymentType { get; set; }
        public virtual DbSet<Branch> Branch { get; set; }
        public virtual DbSet<Company> Company { get; set; }
        public virtual DbSet<RightSetting> RightSetting { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<UserRight> UserRight { get; set; }
        public virtual DbSet<PCRIncome> PCRIncome { get; set; }
        public virtual DbSet<PCRExpense> PCRExpense { get; set; }
        public virtual DbSet<HREmployee> HREmployee { get; set; }




    }
}
