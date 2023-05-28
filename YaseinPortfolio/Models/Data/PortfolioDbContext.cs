using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace YaseinPortfolio.Models.Data
{
   public class YaseinPortofolioDbContext : IdentityDbContext
   {
      public YaseinPortofolioDbContext(DbContextOptions<YaseinPortofolioDbContext> options) : base(options)
      {

      }
      public DbSet<Service> Services { get; set; }
      public DbSet<Certificate> Certificates { get; set; }
      public DbSet<Experience> Experiences { get; set; }
      public DbSet<Skill> Skills { get; set; }
      public DbSet<MyWork> MyWorks { get; set; }
      public DbSet<TrainingCourse> TrainingCourses { get; set; }
      public DbSet<ContactMeByEmail> ContactMeByEmails { get; set; }
      public DbSet<AboutMe> AboutMes { get; set; }
      public DbSet<YaseinPortfolio.Models.ViewModels.RoleViewModel> RoleViewModel { get; set; }

   }
}
