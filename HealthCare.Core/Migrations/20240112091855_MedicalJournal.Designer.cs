﻿// <auto-generated />
using System;
using HealthCare.Core.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace HealthCare.Core.Migrations
{
    [DbContext(typeof(HealthcareContext))]
    [Migration("20240112091855_MedicalJournal")]
    partial class MedicalJournal
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.14")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("HealthCare.Core.Models.Appointment.Feedback", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("FeedbackText")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("FeedbackTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("PatientId")
                        .HasColumnType("int");

                    b.Property<int>("Stars")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PatientId");

                    b.ToTable("Feedback");
                });

            modelBuilder.Entity("HealthCare.Core.Models.AppointmentModels.Appointment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CareGiverId")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Feedback")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("PatientId")
                        .HasColumnType("int");

                    b.Property<int?>("Rating")
                        .HasColumnType("int");

                    b.Property<int?>("Service")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CareGiverId");

                    b.HasIndex("PatientId");

                    b.ToTable("Appointment");
                });

            modelBuilder.Entity("HealthCare.Core.Models.UserModels.CareGiver", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Gender")
                        .HasColumnType("int");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Role")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("CareGiver");
                });

            modelBuilder.Entity("HealthCare.Core.Models.UserModels.MedicalJournalEntry", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CareGiverId")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Entry")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PatientId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CareGiverId");

                    b.HasIndex("PatientId");

                    b.ToTable("MedicalJournal");
                });

            modelBuilder.Entity("HealthCare.Core.Models.UserModels.Patient", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Gender")
                        .HasColumnType("int");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Role")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Patient");
                });

            modelBuilder.Entity("HealthCare.Core.Models.Appointment.Feedback", b =>
                {
                    b.HasOne("HealthCare.Core.Models.UserModels.Patient", "Patient")
                        .WithMany()
                        .HasForeignKey("PatientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Patient");
                });

            modelBuilder.Entity("HealthCare.Core.Models.AppointmentModels.Appointment", b =>
                {
                    b.HasOne("HealthCare.Core.Models.UserModels.CareGiver", "CareGiver")
                        .WithMany()
                        .HasForeignKey("CareGiverId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HealthCare.Core.Models.UserModels.Patient", "Patient")
                        .WithMany()
                        .HasForeignKey("PatientId");

                    b.Navigation("CareGiver");

                    b.Navigation("Patient");
                });

            modelBuilder.Entity("HealthCare.Core.Models.UserModels.MedicalJournalEntry", b =>
                {
                    b.HasOne("HealthCare.Core.Models.UserModels.CareGiver", "CareGiver")
                        .WithMany()
                        .HasForeignKey("CareGiverId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HealthCare.Core.Models.UserModels.Patient", "Patient")
                        .WithMany("JournalEntries")
                        .HasForeignKey("PatientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CareGiver");

                    b.Navigation("Patient");
                });

            modelBuilder.Entity("HealthCare.Core.Models.UserModels.Patient", b =>
                {
                    b.Navigation("JournalEntries");
                });
#pragma warning restore 612, 618
        }
    }
}
