﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Repository;

#nullable disable

namespace SmartAttendance.Migrations
{
    [DbContext(typeof(RepositoryContext))]
    [Migration("20241220105048_InitialData")]
    partial class InitialData
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Entities.Models.Department", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("DepartmentId");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<Guid>("FacultyId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(60)
                        .HasColumnType("nvarchar(60)");

                    b.HasKey("Id");

                    b.HasIndex("FacultyId");

                    b.ToTable("Department");

                    b.HasData(
                        new
                        {
                            Id = new Guid("3d490a70-94ce-4d15-9494-5248280c2ce3"),
                            Code = "CSC",
                            FacultyId = new Guid("b4e2e3f2-3b3d-4b5d-8b0b-2b2b2b2b2b2b"),
                            Name = "Computer Science"
                        },
                        new
                        {
                            Id = new Guid("80abbca8-664d-4b20-b5de-024705497d4a"),
                            Code = "EEE",
                            FacultyId = new Guid("b4e2e3f2-3b3d-4b5d-8b0b-1b1b1b1b1b1b"),
                            Name = "Electrical Engineering"
                        });
                });

            modelBuilder.Entity("Entities.Models.Faculty", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("FacultyId");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(60)
                        .HasColumnType("nvarchar(60)");

                    b.HasKey("Id");

                    b.ToTable("Faculty");

                    b.HasData(
                        new
                        {
                            Id = new Guid("b4e2e3f2-3b3d-4b5d-8b0b-1b1b1b1b1b1b"),
                            Code = "FOE",
                            Name = "Faculty of Engineering"
                        },
                        new
                        {
                            Id = new Guid("b4e2e3f2-3b3d-4b5d-8b0b-2b2b2b2b2b2b"),
                            Code = "FOS",
                            Name = "Faculty of Science"
                        });
                });

            modelBuilder.Entity("Entities.Models.Department", b =>
                {
                    b.HasOne("Entities.Models.Faculty", "Faculty")
                        .WithMany("Departments")
                        .HasForeignKey("FacultyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Faculty");
                });

            modelBuilder.Entity("Entities.Models.Faculty", b =>
                {
                    b.Navigation("Departments");
                });
#pragma warning restore 612, 618
        }
    }
}
