using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Guides.Backend.Domain;
using Microsoft.EntityFrameworkCore;

namespace Guides.Backend.Data
{
    public class GuidesContext: DbContext
    {
        public GuidesContext(DbContextOptions<GuidesContext> options) : base(options)
        {
            
        }

        //  Auth
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        
        //  Domain
        public DbSet<Respondent> Respondents { get; set; }
        public DbSet<SocioDemographic> SocioDemographics { get; set; }
        public DbSet<PregnancyAndGdmRiskFactors> PregnancyAndGdmRiskFactorsCollection { get; set; }
        public DbSet<TobaccoAndAlcoholUse> TobaccoAndAlcoholUseCollection { get; set; }
        public DbSet<PhysicalActivity> PhysicalActivityCollection { get; set; }
        public DbSet<DietaryBehaviour> DietaryBehaviourCollection { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //  Auth
            modelBuilder.Entity<UserRole>()
                .HasOne(ur => ur.Role)
                .WithMany(r => r.UserRoles)
                .HasForeignKey(ur => ur.RoleId);
            
            modelBuilder.Entity<UserRole>()
                .HasOne(ur => ur.User)
                .WithMany(u => u.UserRoles)
                .HasForeignKey(ur => ur.UserId);

            modelBuilder.Entity<UserRole>()
                .HasKey(ur => new {ur.UserId, ur.RoleId});
            
            //  Domain
            modelBuilder.Entity<Respondent>()
                .HasOne(r => r.User)
                .WithMany(u => u.Respondents)
                .HasForeignKey(r => r.UserId);

            modelBuilder.Entity<Respondent>()
                .HasOne(r => r.SocioDemographic)
                .WithOne(sd => sd.Respondent)
                .HasForeignKey<SocioDemographic>(sd => sd.RespondentId);
            
            modelBuilder.Entity<Respondent>()
                .HasOne(r => r.PregnancyAndGdmRiskFactors)
                .WithOne(p => p.Respondent)
                .HasForeignKey<PregnancyAndGdmRiskFactors>(p => p.RespondentId);
            
            modelBuilder.Entity<Respondent>()
                .HasOne(r => r.TobaccoAndAlcoholUse)
                .WithOne(t => t.Respondent)
                .HasForeignKey<TobaccoAndAlcoholUse>(t => t.RespondentId)
                .OnDelete(DeleteBehavior.Cascade);
            
            modelBuilder.Entity<Respondent>()
                .HasOne(r => r.PhysicalActivity)
                .WithOne(p => p.Respondent)
                .HasForeignKey<PhysicalActivity>(p => p.RespondentId);
            
            modelBuilder.Entity<Respondent>()
                .HasOne(r => r.DietaryBehaviour)
                .WithOne(d => d.Respondent)
                .HasForeignKey<DietaryBehaviour>(d => d.RespondentId);
        }
    }
}
