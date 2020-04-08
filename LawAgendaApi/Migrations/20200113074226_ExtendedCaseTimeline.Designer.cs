﻿// <auto-generated />
using System;
using LawAgendaApi.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace LawAgendaApi.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20200113074226_ExtendedCaseTimeline")]
    partial class ExtendedCaseTimeline
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("LawAgendaApi.Data.Entities.A1CaseType", b =>
                {
                    b.Property<short>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("smallint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Color")
                        .HasColumnType("nvarchar(10)")
                        .HasMaxLength(10);

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime2");

                    b.Property<byte>("IsDeleted")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("tinyint");

                    b.Property<string>("Type")
                        .HasColumnType("nvarchar(255)")
                        .HasMaxLength(255);

                    b.Property<DateTime>("UpdatedAt")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("A1CaseTypes");
                });

            modelBuilder.Entity("LawAgendaApi.Data.Entities.A1FileType", b =>
                {
                    b.Property<short>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("smallint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime2");

                    b.Property<byte>("IsDeleted")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("tinyint");

                    b.Property<string>("Type")
                        .HasColumnType("nvarchar(255)")
                        .HasMaxLength(255);

                    b.Property<DateTime>("UpdatedAt")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("A1FileTypes");
                });

            modelBuilder.Entity("LawAgendaApi.Data.Entities.A1UserType", b =>
                {
                    b.Property<short>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("smallint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime2");

                    b.Property<byte>("IsDeleted")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("tinyint");

                    b.Property<string>("Type")
                        .HasColumnType("nvarchar(255)")
                        .HasMaxLength(255);

                    b.Property<DateTime>("UpdatedAt")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("A1UserTypes");
                });

            modelBuilder.Entity("LawAgendaApi.Data.Entities.Case", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CourtHouse")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime2");

                    b.Property<long?>("CustomerId")
                        .HasColumnType("bigint");

                    b.Property<string>("Defendant")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EFN")
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte>("IsClosed")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("tinyint");

                    b.Property<byte>("IsDeleted")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("tinyint");

                    b.Property<byte>("IsPrivate")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("tinyint");

                    b.Property<string>("Judge")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(255)")
                        .HasMaxLength(255);

                    b.Property<DateTime?>("NextDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("NotificationDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Number")
                        .HasColumnType("nvarchar(255)")
                        .HasMaxLength(255);

                    b.Property<double?>("Price")
                        .HasColumnType("float");

                    b.Property<DateTime?>("StartingDate")
                        .HasColumnType("datetime2");

                    b.Property<short?>("TypeId")
                        .HasColumnType("smallint");

                    b.Property<DateTime>("UpdatedAt")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.HasIndex("TypeId");

                    b.ToTable("Cases");
                });

            modelBuilder.Entity("LawAgendaApi.Data.Entities.CaseTimeline", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long?>("CaseId")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime2");

                    b.Property<long?>("FileId")
                        .HasColumnType("bigint");

                    b.Property<byte>("IsDeleted")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("tinyint");

                    b.Property<byte>("IsPleading")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("tinyint");

                    b.Property<string>("Notes")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("PleadingDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("UpdatedAt")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime2");

                    b.Property<long?>("UserId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("CaseId");

                    b.HasIndex("FileId");

                    b.HasIndex("UserId");

                    b.ToTable("CaseTimelines");
                });

            modelBuilder.Entity("LawAgendaApi.Data.Entities.Customer", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime2");

                    b.Property<byte>("IsDeleted")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("tinyint");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("PhoneNumber2")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<DateTime>("UpdatedAt")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime2");

                    b.Property<string>("Username")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("LawAgendaApi.Data.Entities.File", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime2");

                    b.Property<string>("Extension")
                        .HasColumnType("nvarchar(10)")
                        .HasMaxLength(10);

                    b.Property<byte>("IsDeleted")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("tinyint");

                    b.Property<string>("Path")
                        .HasColumnType("nvarchar(512)")
                        .HasMaxLength(512);

                    b.Property<short?>("TypeId")
                        .HasColumnType("smallint");

                    b.Property<DateTime>("UpdatedAt")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("TypeId");

                    b.ToTable("Files");
                });

            modelBuilder.Entity("LawAgendaApi.Data.Entities.User", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime2");

                    b.Property<long?>("FileId")
                        .HasColumnType("bigint");

                    b.Property<byte>("IsDeleted")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("tinyint");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("PasswordAccess")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PasswordSalt")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("PhoneNumber2")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<short?>("TypeId")
                        .HasColumnType("smallint");

                    b.Property<DateTime>("UpdatedAt")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime2");

                    b.Property<string>("Username")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.HasIndex("FileId");

                    b.HasIndex("TypeId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("LawAgendaApi.Data.Entities.Case", b =>
                {
                    b.HasOne("LawAgendaApi.Data.Entities.Customer", "Customer")
                        .WithMany()
                        .HasForeignKey("CustomerId");

                    b.HasOne("LawAgendaApi.Data.Entities.A1CaseType", "Type")
                        .WithMany()
                        .HasForeignKey("TypeId");
                });

            modelBuilder.Entity("LawAgendaApi.Data.Entities.CaseTimeline", b =>
                {
                    b.HasOne("LawAgendaApi.Data.Entities.Case", "Case")
                        .WithMany()
                        .HasForeignKey("CaseId");

                    b.HasOne("LawAgendaApi.Data.Entities.File", "File")
                        .WithMany()
                        .HasForeignKey("FileId");

                    b.HasOne("LawAgendaApi.Data.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("LawAgendaApi.Data.Entities.File", b =>
                {
                    b.HasOne("LawAgendaApi.Data.Entities.A1FileType", "Type")
                        .WithMany()
                        .HasForeignKey("TypeId");
                });

            modelBuilder.Entity("LawAgendaApi.Data.Entities.User", b =>
                {
                    b.HasOne("LawAgendaApi.Data.Entities.File", "Avatar")
                        .WithMany()
                        .HasForeignKey("FileId");

                    b.HasOne("LawAgendaApi.Data.Entities.A1UserType", "Type")
                        .WithMany()
                        .HasForeignKey("TypeId");
                });
#pragma warning restore 612, 618
        }
    }
}
