using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations.Schema;
using System;

namespace ReCountant.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class MyUser : IdentityUser<long, MyLogin, MyUserRole, MyClaim>

    {
        public string Name { get; set; }
        public string CNIC_Number { get; set; }
        public string Address { get; set; }
        public string Gender { get; set; }
        public string NTN { get; set; }
        public string STRN { get; set; }
        public string Residential_Status { get; set; }
        public string Industry { get; set; }
        public string Supply_Chain_Role { get; set; }
        public string Filer_NonFiler { get; set; }
        public string Individual_AOP_Company { get; set; }
        public string Business { get; set; }
        public string EPZ { get; set; }
    }
    public class MyUserRole : IdentityUserRole<long> { }
    public class MyRole : IdentityRole<long, MyUserRole> { }
    public class MyClaim : IdentityUserClaim<long> { }
    public class MyLogin : IdentityUserLogin<long> { }

    public class ApplicationDbContext : IdentityDbContext<MyUser, MyRole, long, MyLogin, MyUserRole, MyClaim>
    {
        public ApplicationDbContext()
            : base("MynewIdentity")
        {
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //Map entities to there table
            modelBuilder.Entity<MyUser>().ToTable("Users");
            modelBuilder.Entity<MyUserRole>().ToTable("UserRoles");
            modelBuilder.Entity<MyRole>().ToTable("Roles");
            modelBuilder.Entity<MyClaim>().ToTable("UserClaims");
            modelBuilder.Entity<MyLogin>().ToTable("UserLogins");
            //Set auto incerament property
            //modelBuilder.Entity<MyUser>.prop
            modelBuilder.Entity<MyUser>().Property(r => r.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<MyRole>().Property(r => r.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<MyClaim>().Property(r => r.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
        }
        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public System.Data.Entity.DbSet<ReCountant.Models.MyUserRole> MyUserRoles { get; set; }



    }
}