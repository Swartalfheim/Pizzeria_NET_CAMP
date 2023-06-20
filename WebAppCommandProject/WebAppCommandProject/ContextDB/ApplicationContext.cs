using Microsoft.EntityFrameworkCore;
using WebAppCommandProject.Models;

namespace WebAppCommandProject.ContextDB
{
    public class ApplicationContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
            //Database.EnsureDeleted();
            Database.EnsureCreated();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            string adminRoleName = "admin";
            string managerRoleName = "manager";
            string userRoleName = "user";

            string adminEmail = "admin@gmail.com";
            string adminPassword = "123456";

            string managerEmail = "manager@gmail.com";
            string managerPassword = "123456";

            string userEmail = "user@gmail.com";
            string userPassword = "123456";

            // add password
            Role adminRole = new Role { Id = 1, Name = adminRoleName };
            Role managerRole = new Role { Id = 2, Name = managerRoleName };
            Role userRole = new Role { Id = 3, Name = userRoleName };

            User adminUser = new User { Id = 1, Email = adminEmail, Password = adminPassword, RoleId = adminRole.Id };
            User managerUser = new User { Id = 2, Email = managerEmail, Password = managerPassword, RoleId = managerRole.Id };
            User userUser = new User { Id = 3, Email = userEmail, Password = userPassword, RoleId = userRole.Id };

            modelBuilder.Entity<Role>().HasData(new Role[] { adminRole, managerRole, userRole });
            modelBuilder.Entity<User>().HasData(new User[] { adminUser, userUser, managerUser });
            base.OnModelCreating(modelBuilder);
        }
    }
}
