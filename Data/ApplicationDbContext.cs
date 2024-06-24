using CodeBuilder.Models;
using CodeBuilder.Models.Briefs;
using Microsoft.EntityFrameworkCore;

namespace CodeBuilder.Data
{
    public class ApplicationDbContext : DbContext
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

            builder.Entity<VariableInputModel>().HasKey(x => new { x.CalculationId, x.InputId });

            builder.Entity<VariableGroupVariableModel>().HasKey(x => new { x.GroupId, x.VariableId });

            builder.Entity<VariableInputModel>().HasOne(x => x.Input).WithMany().OnDelete(DeleteBehavior.Restrict);

            // Seed data

            // Variable ids
            var vid1 = Guid.NewGuid();
            var vid2 = Guid.NewGuid();
            var vid3 = Guid.NewGuid();

            // Block ids
            var bid0 = Guid.NewGuid();
            var bid1 = Guid.NewGuid();
            var bid2 = Guid.NewGuid();
            var bid3 = Guid.NewGuid();

            // Check ids
            var cid1 = Guid.NewGuid();

            // Condition ids
            var cnid1 = Guid.NewGuid();
            var cnid2 = Guid.NewGuid();

            // Standard ids
            var sid1 = Guid.NewGuid();

            // Brief check ids
            var bcid1 = Guid.NewGuid();

            // Brief ids
            var brid1 = Guid.NewGuid();


            builder.Entity<StandardModel>().HasData(
                new StandardModel { Id = sid1, StandardType = StandardType.EC, State = StateType.Current, Published = DateTime.Now, Code = "EN 2024-1-1:2024", Name = "Example Standard", Desc = "An example Eurocode standard" }
            );

            builder.Entity<VariableModel>().HasData(
                new VariableModel { Id = vid1, StandardId = sid1, Math = "A", Code = "a", Value = "6", Storage = StorageType.Real, Name = "Area Rectangle", Desc = "The area of a rectangle", Unit = UnitType.MillimetersSquared, MathCalculation = "W \\times H", CodeCalculation = "w * h" },
                new VariableModel { Id = vid2, StandardId = sid1, Math = "H", Code = "h", Value = "2", Storage = StorageType.Real, Name = "Height", Desc = "The height of an object", Unit = UnitType.Millimeters },
                new VariableModel { Id = vid3, StandardId = sid1, Math = "W", Code = "w", Value = "3", Storage = StorageType.Real, Name = "Width", Desc = "The width of an object", Unit = UnitType.Millimeters }
            );

            builder.Entity<VariableInputModel>().HasData(
                new VariableInputModel { CalculationId = vid1, InputId = vid2, },
                new VariableInputModel { CalculationId = vid1, InputId = vid3, }
            );

            builder.Entity<ConditionModel>().HasData(
                new ConditionModel { Id = cnid1, LhsId = vid2, ConditionType = ConditionType.If, OperationType = OperationType.IsGreaterThan, LhsType = ConditionItemType.IsVariable, RhsType = ConditionItemType.IsNumber, RhsValue = "0" },
                new ConditionModel { Id = cnid2, LhsId = vid3, ConditionType = ConditionType.If, OperationType = OperationType.IsGreaterThan, LhsType = ConditionItemType.IsVariable, RhsType = ConditionItemType.IsNumber, RhsValue = "0" }
            );

            builder.Entity<BlockModel>().HasData(
                new BlockModel { Id = bid0, ParentId = null, CheckId = cid1, }, // Root block
                new BlockModel { Id = bid1, ParentId = bid0, CalculationId = vid2, ConditionId = cnid1, },
                new BlockModel { Id = bid2, ParentId = bid1, CalculationId = vid3, ConditionId = cnid2, },
                new BlockModel { Id = bid3, ParentId = bid2, CalculationId = vid1 }
            );

            builder.Entity<CheckModel>().HasData(
                new CheckModel { Id = cid1, StandardId = sid1, Name = "Area Check", Desc = "An example of a check", }
            );

            builder.Entity<BriefCheckModel>().HasData(
                new BriefCheckModel { Id = bcid1, BriefId = brid1, CheckId = cid1, }
            );

            builder.Entity<BriefModel>().HasData(
                new BriefModel { Id = brid1, Name = "Brief Example", Desc = "An example brief object", StandardId = sid1, }
            );
        }


    }
}