using Microsoft.EntityFrameworkCore;
using mosahem.Domain.Entities;
using mosahem.Domain.Entities.Identity;
using mosahem.Domain.Entities.Location;
using mosahem.Domain.Entities.MasterData;
using mosahem.Domain.Entities.Opportunities;
using mosahem.Domain.Entities.Profiles;
using mosahem.Domain.Entities.Questions;
using System.Reflection;

namespace mosahem.Persistence
{
    public class MosahmDbContext : DbContext
    {
        public MosahmDbContext(DbContextOptions<MosahmDbContext> options)
            : base(options)
        {
        }

        #region DbSets

        // Core
        public DbSet<MosahmUser> Users => Set<MosahmUser>();

        public DbSet<Volunteer> Volunteers => Set<Volunteer>();
        public DbSet<Organization> Organizations => Set<Organization>();

        // Location
        public DbSet<Governorate> Governorates => Set<Governorate>();
        public DbSet<City> Cities => Set<City>();
        public DbSet<Address> Addresses => Set<Address>();

        // Taxonomy
        public DbSet<Field> Fields => Set<Field>();
        public DbSet<Skill> Skills => Set<Skill>();

        // Opportunities
        public DbSet<Opportunity> Opportunities => Set<Opportunity>();
        public DbSet<OpportunityApplication> OpportunityApplications => Set<OpportunityApplication>();
        public DbSet<OpportunityComment> OpportunityComments => Set<OpportunityComment>();

        // Questions
        public DbSet<Question> Questions => Set<Question>();
        public DbSet<QuestionAnswer> QuestionAnswers => Set<QuestionAnswer>();

        // Junction tables
        public DbSet<VolunteerSkill> VolunteerSkills => Set<VolunteerSkill>();
        public DbSet<VolunteerField> VolunteerFields => Set<VolunteerField>();
        public DbSet<OrganizationField> OrganizationFields => Set<OrganizationField>();
        public DbSet<OrganizationFollower> OrganizationFollowers => Set<OrganizationFollower>();

        public DbSet<OpportunitySkill> OpportunitySkills => Set<OpportunitySkill>();
        public DbSet<OpportunityField> OpportunityFields => Set<OpportunityField>();

        public DbSet<OpportunityLike> OpportunityLikes => Set<OpportunityLike>();
        public DbSet<OpportunitySave> OpportunitySaves => Set<OpportunitySave>();

        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(
                Assembly.GetExecutingAssembly()
            );
        }
    }
}
