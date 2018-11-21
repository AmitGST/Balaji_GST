using BALAJI.GSP.APPLICATION.Infrastructure;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using Owin;
namespace BALAJI.GSP.APPLICATION.Model
{
        
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole,
        string, ApplicationUserLogin, ApplicationUserRole, ApplicationUserClaim>
    {
        //Add the ApplicationGroups property:
    
        //public DbSet<ApplicationRole> Roles { get; set; }

        //public ApplicationDbContext()
        //    : base("DefaultConnection", throwIfV1Schema: false)
        //{
        //    Configuration.ProxyCreationEnabled = false;
        //    Configuration.LazyLoadingEnabled = false;           
        //}
        public ApplicationDbContext()
            : base("DefaultConnection")
        {
        }

        static ApplicationDbContext()
        {
            Database.SetInitializer<ApplicationDbContext>(new ApplicationDbInitializer());
        }

        // Add the ApplicationGroups property:
        public virtual IDbSet<ApplicationGroup> ApplicationGroups { get; set; }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            if (modelBuilder == null)
            {
                throw new ArgumentNullException("ModelBuilder is NULL");
            }

            base.OnModelCreating(modelBuilder);

            //Defining the keys and relations
            modelBuilder.Entity<ApplicationUser>().ToTable("AspNetUsers");
            modelBuilder.Entity<ApplicationRole>().HasKey<string>(r => r.Id).ToTable("AspNetRoles");
            modelBuilder.Entity<ApplicationUser>().HasMany<ApplicationUserRole>((ApplicationUser u) => u.UserRoles);
            modelBuilder.Entity<ApplicationUserRole>().HasKey(r => new { UserId = r.UserId, RoleId = r.RoleId }).ToTable("AspNetUserRoles");

            // Map Users to Groups:
            modelBuilder.Entity<ApplicationGroup>()
                .HasMany<ApplicationUserGroup>((ApplicationGroup g) => g.ApplicationUsers)
                .WithRequired()
                .HasForeignKey<string>((ApplicationUserGroup ag) => ag.ApplicationGroupId);
            modelBuilder.Entity<ApplicationUserGroup>()
                .HasKey((ApplicationUserGroup r) =>
                    new
                    {
                        ApplicationUserId = r.ApplicationUserId,
                        ApplicationGroupId = r.ApplicationGroupId
                    }).ToTable("ApplicationUserGroups");

            // Map Roles to Groups:
            modelBuilder.Entity<ApplicationGroup>()
                .HasMany<ApplicationGroupRole>((ApplicationGroup g) => g.ApplicationRoles)
                .WithRequired()
                .HasForeignKey<string>((ApplicationGroupRole ap) => ap.ApplicationGroupId);
            modelBuilder.Entity<ApplicationGroupRole>().HasKey((ApplicationGroupRole gr) =>
                new
                {
                    ApplicationRoleId = gr.ApplicationRoleId,
                    ApplicationGroupId = gr.ApplicationGroupId
                }).ToTable("ApplicationGroupRoles");
        }

        public bool Seed(ApplicationDbContext context)
        {

          bool success = false;

//            ApplicationRoleManager _roleManager = new ApplicationRoleManager(new RoleStore<ApplicationRole>(context));

//            success = this.CreateRole(_roleManager, "Admin", "Global Access");
//            if (!success == true) return success;

//            success = this.CreateRole(_roleManager, "Tax-Consultant", "Edit existing records");
//            if (!success == true) return success;

//            success = this.CreateRole(_roleManager, "User", "Restricted to business domain activity");
//            if (!success) return success;

//            // Create my debug (testing) objects here

//            ApplicationUserManager userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(context));

//            ApplicationUser user = new ApplicationUser();
//            PasswordHasher passwordHasher = new PasswordHasher();

//            user.UserName = "youremail@testemail.com";
//            user.Email = "youremail@testemail.com";

//            IdentityResult result = userManager.Create(user, "Pass@123");

//            success = this.AddUserToRole(userManager, user.Id, "Admin");
//            if (!success) return success;

//            success = this.AddUserToRole(userManager, user.Id, "CanEdit");
//            if (!success) return success;

//            success = this.AddUserToRole(userManager, user.Id, "User");
//            if (!success) return success;

          return success;
        }

        public bool RoleExists(ApplicationRoleManager roleManager, string name)
        {
            return roleManager.RoleExists(name);
        }

        //public bool CreateRole(ApplicationRoleManager _roleManager, string name, string description = "")
        //{
        //    var idResult = _roleManager.Create<ApplicationRole, string>(new ApplicationRole(name, description));
        //    return idResult.Succeeded;
        //}

        public bool AddUserToRole(ApplicationUserManager _userManager, string userId, string roleName)
        {
            var idResult = _userManager.AddToRole(userId, roleName);
            return idResult.Succeeded;
        }

        //public void ClearUserRoles(ApplicationUserManager userManager, string userId)
        //{
        //    var user = userManager.FindById(userId);
        //    var currentRoles = new List<IdentityUserRole>();

        //    currentRoles.AddRange(user.UserRoles);
        //    foreach (ApplicationUserRole role in currentRoles)
        //    {
        //        userManager.RemoveFromRole(userId, role.Role.Name);
        //    }
        //}

        public bool RemoveFromRole(ApplicationUserManager userManager, string userId, string roleName)
        {
            var idResult = userManager.RemoveFromRole(userId, roleName);
            return idResult.Succeeded;
        }

        public bool ChangePassword(ApplicationUserManager userManager, string userId, string oldPassword, string newPassword)
        {
            var idResult = userManager.ChangePassword(userId,oldPassword,newPassword);
            return idResult.Succeeded;
        }

        public ApplicationUser SignInUser(ApplicationUserManager userManager, string userName, string password)
        {
            var idResult = userManager.Find(userName, password);
            return idResult;
            //if (idResult != null)
            //    return true;
            //else
            //    return false;
        }

        public bool UpdateUserProfile(ApplicationUserManager userManager, ApplicationUser applicationUser)
        {
            //var user=userManager.Find()
            var idResult = userManager.Update(applicationUser);
            if (!idResult.Succeeded)
                return false;
            else
                return true;       
        }

        public void DeleteRole(ApplicationDbContext context, ApplicationUserManager userManager, string roleId)
        {
            var roleUsers = context.Users.Where(u => u.UserRoles.Any(r => r.RoleId == roleId));
            var role = context.Roles.Find(roleId);

            foreach (var user in roleUsers)
            {
                this.RemoveFromRole(userManager, user.Id, role.Name);
            }
            context.Roles.Remove(role);
            context.SaveChanges();
        }

        /// <summary>
        /// Context Initializer
        /// </summary>
        public class DropCreateAlwaysInitializer : CreateDatabaseIfNotExists<ApplicationDbContext>
        {
            protected override void Seed(ApplicationDbContext context)
            {
                context.Seed(context);

                base.Seed(context);
            }
        }
    }
}