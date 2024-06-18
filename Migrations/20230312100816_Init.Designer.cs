﻿// <auto-generated />
using System;
using CodeBuilder.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CodeBuilder.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20230312100816_Init")]
    partial class Init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("CodeBuilder.Models.BlockModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("CalculationId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("CheckId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("ConditionId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Index")
                        .HasColumnType("int");

                    b.Property<Guid?>("ParentId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("CalculationId");

                    b.HasIndex("CheckId")
                        .IsUnique()
                        .HasFilter("[CheckId] IS NOT NULL");

                    b.HasIndex("ConditionId");

                    b.HasIndex("ParentId");

                    b.ToTable("Blocks");
                });

            modelBuilder.Entity("CodeBuilder.Models.BriefModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Code")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("Desc")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Reference")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("StandardId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("State")
                        .HasColumnType("int");

                    b.Property<DateTime>("Updated")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("StandardId");

                    b.ToTable("Briefs");
                });

            modelBuilder.Entity("CodeBuilder.Models.CalculationModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("CalculationModelId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Code")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CodeCalculation")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("Desc")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Math")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MathCalculation")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Reference")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("StandardId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("State")
                        .HasColumnType("int");

                    b.Property<int>("Storage")
                        .HasColumnType("int");

                    b.Property<int>("Unit")
                        .HasColumnType("int");

                    b.Property<DateTime>("Updated")
                        .HasColumnType("datetime2");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CalculationModelId");

                    b.HasIndex("StandardId");

                    b.ToTable("Calculations");
                });

            modelBuilder.Entity("CodeBuilder.Models.CalculationVariableModel", b =>
                {
                    b.Property<Guid>("CalculationId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("VariableId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("CalculationId", "VariableId");

                    b.HasIndex("VariableId");

                    b.ToTable("CalculationVariables");
                });

            modelBuilder.Entity("CodeBuilder.Models.CheckModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("BriefModelId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Code")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("Desc")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Reference")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("StandardId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("State")
                        .HasColumnType("int");

                    b.Property<DateTime>("Updated")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("BriefModelId");

                    b.HasIndex("StandardId");

                    b.ToTable("Checks");
                });

            modelBuilder.Entity("CodeBuilder.Models.ConditionModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("ConditionType")
                        .HasColumnType("int");

                    b.Property<Guid?>("LhsId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("LhsType")
                        .HasColumnType("int");

                    b.Property<string>("LhsValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("OperationType")
                        .HasColumnType("int");

                    b.Property<Guid?>("RhsId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("RhsType")
                        .HasColumnType("int");

                    b.Property<string>("RhsValue")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("LhsId");

                    b.HasIndex("RhsId");

                    b.ToTable("Conditions");
                });

            modelBuilder.Entity("CodeBuilder.Models.StandardModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Code")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("Desc")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Isbn")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Link")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Published")
                        .HasColumnType("datetime2");

                    b.Property<string>("Reference")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("StandardId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("StandardType")
                        .HasColumnType("int");

                    b.Property<int>("State")
                        .HasColumnType("int");

                    b.Property<DateTime>("Updated")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("StandardId");

                    b.ToTable("Standards");
                });

            modelBuilder.Entity("CodeBuilder.Models.TableDataModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ColId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("RowId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("TableId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("TableId");

                    b.ToTable("TableData");
                });

            modelBuilder.Entity("CodeBuilder.Models.TableModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Code")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("ColId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("ColType")
                        .HasColumnType("int");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("Desc")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Reference")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("RowId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("RowType")
                        .HasColumnType("int");

                    b.Property<Guid?>("StandardId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("State")
                        .HasColumnType("int");

                    b.Property<int>("Storage")
                        .HasColumnType("int");

                    b.Property<int>("Unit")
                        .HasColumnType("int");

                    b.Property<DateTime>("Updated")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("StandardId");

                    b.ToTable("Tables");
                });

            modelBuilder.Entity("CodeBuilder.Models.VariableGroupModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Code")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("Desc")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Reference")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("StandardId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("State")
                        .HasColumnType("int");

                    b.Property<DateTime>("Updated")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("StandardId");

                    b.ToTable("VariableGroups");
                });

            modelBuilder.Entity("CodeBuilder.Models.VariableGroupVariableModel", b =>
                {
                    b.Property<Guid>("GroupId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("VariableId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("GroupId", "VariableId");

                    b.HasIndex("VariableId");

                    b.ToTable("VariableGroupVariables");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderKey")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Name")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("CodeBuilder.Models.BlockModel", b =>
                {
                    b.HasOne("CodeBuilder.Models.CalculationModel", "Calculation")
                        .WithMany()
                        .HasForeignKey("CalculationId");

                    b.HasOne("CodeBuilder.Models.CheckModel", "Check")
                        .WithOne("Block")
                        .HasForeignKey("CodeBuilder.Models.BlockModel", "CheckId");

                    b.HasOne("CodeBuilder.Models.ConditionModel", "Condition")
                        .WithMany()
                        .HasForeignKey("ConditionId");

                    b.HasOne("CodeBuilder.Models.BlockModel", "Parent")
                        .WithMany("Children")
                        .HasForeignKey("ParentId");

                    b.Navigation("Calculation");

                    b.Navigation("Check");

                    b.Navigation("Condition");

                    b.Navigation("Parent");
                });

            modelBuilder.Entity("CodeBuilder.Models.BriefModel", b =>
                {
                    b.HasOne("CodeBuilder.Models.StandardModel", "Standard")
                        .WithMany()
                        .HasForeignKey("StandardId");

                    b.Navigation("Standard");
                });

            modelBuilder.Entity("CodeBuilder.Models.CalculationModel", b =>
                {
                    b.HasOne("CodeBuilder.Models.CalculationModel", null)
                        .WithMany("Calculations")
                        .HasForeignKey("CalculationModelId");

                    b.HasOne("CodeBuilder.Models.StandardModel", "Standard")
                        .WithMany()
                        .HasForeignKey("StandardId");

                    b.Navigation("Standard");
                });

            modelBuilder.Entity("CodeBuilder.Models.CalculationVariableModel", b =>
                {
                    b.HasOne("CodeBuilder.Models.CalculationModel", "Calculation")
                        .WithMany()
                        .HasForeignKey("CalculationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CodeBuilder.Models.CalculationModel", "Variable")
                        .WithMany()
                        .HasForeignKey("VariableId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Calculation");

                    b.Navigation("Variable");
                });

            modelBuilder.Entity("CodeBuilder.Models.CheckModel", b =>
                {
                    b.HasOne("CodeBuilder.Models.BriefModel", null)
                        .WithMany("Checks")
                        .HasForeignKey("BriefModelId");

                    b.HasOne("CodeBuilder.Models.StandardModel", "Standard")
                        .WithMany()
                        .HasForeignKey("StandardId");

                    b.Navigation("Standard");
                });

            modelBuilder.Entity("CodeBuilder.Models.ConditionModel", b =>
                {
                    b.HasOne("CodeBuilder.Models.CalculationModel", "Lhs")
                        .WithMany()
                        .HasForeignKey("LhsId");

                    b.HasOne("CodeBuilder.Models.CalculationModel", "Rhs")
                        .WithMany()
                        .HasForeignKey("RhsId");

                    b.Navigation("Lhs");

                    b.Navigation("Rhs");
                });

            modelBuilder.Entity("CodeBuilder.Models.StandardModel", b =>
                {
                    b.HasOne("CodeBuilder.Models.StandardModel", "Standard")
                        .WithMany("Children")
                        .HasForeignKey("StandardId");

                    b.Navigation("Standard");
                });

            modelBuilder.Entity("CodeBuilder.Models.TableDataModel", b =>
                {
                    b.HasOne("CodeBuilder.Models.TableModel", "Table")
                        .WithMany("Children")
                        .HasForeignKey("TableId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Table");
                });

            modelBuilder.Entity("CodeBuilder.Models.TableModel", b =>
                {
                    b.HasOne("CodeBuilder.Models.StandardModel", "Standard")
                        .WithMany()
                        .HasForeignKey("StandardId");

                    b.Navigation("Standard");
                });

            modelBuilder.Entity("CodeBuilder.Models.VariableGroupModel", b =>
                {
                    b.HasOne("CodeBuilder.Models.StandardModel", "Standard")
                        .WithMany()
                        .HasForeignKey("StandardId");

                    b.Navigation("Standard");
                });

            modelBuilder.Entity("CodeBuilder.Models.VariableGroupVariableModel", b =>
                {
                    b.HasOne("CodeBuilder.Models.VariableGroupModel", "Group")
                        .WithMany("Children")
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CodeBuilder.Models.CalculationModel", "Variable")
                        .WithMany()
                        .HasForeignKey("VariableId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Group");

                    b.Navigation("Variable");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CodeBuilder.Models.BlockModel", b =>
                {
                    b.Navigation("Children");
                });

            modelBuilder.Entity("CodeBuilder.Models.BriefModel", b =>
                {
                    b.Navigation("Checks");
                });

            modelBuilder.Entity("CodeBuilder.Models.CalculationModel", b =>
                {
                    b.Navigation("Calculations");
                });

            modelBuilder.Entity("CodeBuilder.Models.CheckModel", b =>
                {
                    b.Navigation("Block");
                });

            modelBuilder.Entity("CodeBuilder.Models.StandardModel", b =>
                {
                    b.Navigation("Children");
                });

            modelBuilder.Entity("CodeBuilder.Models.TableModel", b =>
                {
                    b.Navigation("Children");
                });

            modelBuilder.Entity("CodeBuilder.Models.VariableGroupModel", b =>
                {
                    b.Navigation("Children");
                });
#pragma warning restore 612, 618
        }
    }
}
