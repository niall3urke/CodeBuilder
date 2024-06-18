using CodeBuilder.Models;
using CodeBuilder.Models.Briefs;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CodeBuilder.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {

        // Properties

        public DbSet<VariableGroupVariableModel> VariableGroupVariables { get; set; }

        public DbSet<VariableInputModel> VariableInputs { get; set; }

        public DbSet<VariableGroupModel> VariableGroups { get; set; }

        public DbSet<VariableModel> Variables { get; set; }


        public DbSet<TableDataModel> TableData { get; set; }

        public DbSet<TableModel> Tables { get; set; }


        public DbSet<ConditionModel> Conditions { get; set; }

        public DbSet<StandardModel> Standards { get; set; }


        public DbSet<BriefCheckModel> BriefChecks { get; set; }

        public DbSet<BriefModel> Briefs { get; set; }

        public DbSet<CheckModel> Checks { get; set; }

        public DbSet<BlockModel> Blocks { get; set; }

        // Constructors

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // Overrides

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            //builder.Entity<BriefCheckModel>().HasKey(x => new { x.BriefId, x.CheckId });

            builder.Entity<VariableInputModel>().HasKey(x => new { x.CalculationId, x.InputId });
            
            builder.Entity<VariableGroupVariableModel>().HasKey(x => new { x.GroupId, x.VariableId });

            builder.Entity<VariableInputModel>().HasOne(x => x.Input).WithMany().OnDelete(DeleteBehavior.Restrict);
        }


    }
}