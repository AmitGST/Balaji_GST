using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace BALAJI.GSP.APPLICATION.Model
{
    // You will not likely need to customize there, but it is necessary/easier to create our own 
    // project-specific implementations, so here they are:
    public class ApplicationUserLogin : IdentityUserLogin<string> { }
    public class ApplicationUserClaim : IdentityUserClaim<string> { }
    public class ApplicationUserRole : IdentityUserRole<string> 
    {
        //public ApplicationRole Role { get; set; }
        //public bool IsSysAdmin { get { return this.Role.IsSysAdmin; } }

        //public ApplicationUserRole()
        //    : base()
        //{ }

        //public bool IsPermissionInRole(string _permission)
        //{
        //    bool _retVal = false;
        //    try
        //    {
        //        _retVal = this.Role.IsPermissionInRole(_permission);
        //    }
        //    catch (Exception)
        //    {
        //    }
        //    return _retVal;
        //}

        //public bool IsSysAdmin { get { return this.Role.IsSysAdmin; } }
    }

    // Must be expressed in terms of our custom Role and other types:
    public class ApplicationUser
        : IdentityUser<string, ApplicationUserLogin,
        ApplicationUserRole, ApplicationUserClaim>
    {
        public ApplicationUser()
        {
            this.Id = Guid.NewGuid().ToString();

            // Add any custom User properties/code here
        }


       
        [Required]
        [MaxLength(100)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(100)]
        public string LastName { get; set; }

        [Required]
        [MaxLength(15)]
        public string GSTNNo { get; set; }

        [MaxLength(40)]
        public string ParentUserID { get; set; }

        [MaxLength(40)]
        public string OrganizationName { get; set; }

        [MaxLength(500)]
        public string Address { get; set; }
        
        public decimal GrossTurnOver { get; set; }

        [MaxLength(3)]
        public string StateCode { get; set; }

        [MaxLength(20)]
        public string GSTINUserId { get; set; }

        [MaxLength(20)]
        public string NameOfSignatory { get; set; }

        [MaxLength(50)]
        public string Designation { get; set; }
        public bool RegisterWithUs { get; set; }
        
        public byte? UserType { get; set; }

        public decimal? ITC { get; set; }
        public byte[] SmallImage { get; set; }
        public byte[] LargeImage { get; set; }
        public bool Flag { get; set; }


        public async Task<ClaimsIdentity>
           GenerateUserIdentityAsync(ApplicationUserManager manager)
        {
            var userIdentity = await manager
                .CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            return userIdentity;
        }

        public ClaimsIdentity GenerateUserIdentity(UserManager<ApplicationUser> manager)
        {
            var userIdentity = manager.CreateIdentity(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here

            return userIdentity;
        }
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager, string authenticationType)
        {
            var userIdentity = await manager.CreateIdentityAsync(this, authenticationType);
            // Add custom user claims here

            return userIdentity;
        }
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }

        public ICollection<ApplicationUserRole> UserRoles { get; set; }
    }

    // Must be expressed in terms of our custom UserRole:
    public class ApplicationRole : IdentityRole<string, ApplicationUserRole>
    {
        public ApplicationRole() : base() {
            this.Id = Guid.NewGuid().ToString();
        }
        //public ApplicationRole()
        //{
        //    this.Id = Guid.NewGuid().ToString();
        //}

        public ApplicationRole(string name)
            : this()
        {
            this.Name = name;
        }
        //public ApplicationRole(string name, string description)
        //    : base(name)
        //{
        //    this.Description = description;
        //}

        public virtual string Description { get; set; }

        // Add any custom Role properties/code here

       // public bool IsSysAdmin { get; set; }
    }

    // Most likely won't need to customize these either, but they were needed because we implemented
    // custom versions of all the other types:
    public class ApplicationUserStore
        : UserStore<ApplicationUser, ApplicationRole, string,
            ApplicationUserLogin, ApplicationUserRole,
            ApplicationUserClaim>, IUserStore<ApplicationUser, string>,
        IDisposable
    {
        public ApplicationUserStore()
            : this(new IdentityDbContext())
        {
            base.DisposeContext = true;
        }

        public ApplicationUserStore(DbContext context)
            : base(context)
        {
        }
    }

    public class ApplicationRoleStore
   : RoleStore<ApplicationRole, string, ApplicationUserRole>,
   IQueryableRoleStore<ApplicationRole, string>,
   IRoleStore<ApplicationRole, string>, IDisposable
    {
        public ApplicationRoleStore()
            : base(new IdentityDbContext())
        {
            base.DisposeContext = true;
        }

        public ApplicationRoleStore(DbContext context)
            : base(context)
        {
        }
    }


    public class ApplicationGroup
    {
        public ApplicationGroup()
        {
            this.Id = Guid.NewGuid().ToString();
            this.ApplicationRoles = new List<ApplicationGroupRole>();
            this.ApplicationUsers = new List<ApplicationUserGroup>();
        }

        public ApplicationGroup(string name)
            : this()
        {
            this.Name = name;
        }

        public ApplicationGroup(string name, string description)
            : this(name)
        {
            this.Description = description;
        }

        [Key]
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public virtual ICollection<ApplicationGroupRole> ApplicationRoles { get; set; }
        public virtual ICollection<ApplicationUserGroup> ApplicationUsers { get; set; }
    }


    public class ApplicationUserGroup
    {
        public string ApplicationUserId { get; set; }
        public string ApplicationGroupId { get; set; }
    }

    public class ApplicationGroupRole
    {
        public string ApplicationGroupId { get; set; }
        public string ApplicationRoleId { get; set; }
    }

    public class ApplicationSignInManager : SignInManager<ApplicationUser, string>
    {
        public ApplicationSignInManager(ApplicationUserManager userManager, IAuthenticationManager authenticationManager) :
            base(userManager, authenticationManager) { }

        public override Task<ClaimsIdentity> CreateUserIdentityAsync(ApplicationUser user)
        {
            return user.GenerateUserIdentityAsync((ApplicationUserManager)UserManager);
        }

        public static ApplicationSignInManager Create(IdentityFactoryOptions<ApplicationSignInManager> options, IOwinContext context)
        {
            return new ApplicationSignInManager(context.GetUserManager<ApplicationUserManager>(), context.Authentication);
        }
    }

}