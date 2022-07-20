﻿// <auto-generated />
using System;
using MedicinePlanner.Data.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace MedicinePlanner.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20220719142801_AddDailyPlanning")]
    partial class AddDailyPlanning
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.17")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("MedicinePlanner.Data.Models.DailyPlanning", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Consumed")
                        .HasColumnType("bit");

                    b.Property<DateTime>("IntakeTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("Message")
                        .HasColumnType("int");

                    b.Property<int?>("NotificationId")
                        .HasColumnType("int");

                    b.Property<int?>("PlanningId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("NotificationId");

                    b.HasIndex("PlanningId");

                    b.ToTable("DailyPlannings");
                });

            modelBuilder.Entity("MedicinePlanner.Data.Models.LoadingStock", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("LoadingDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("PillsNumber")
                        .HasColumnType("int");

                    b.Property<int?>("StockId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("StockId");

                    b.ToTable("LoadingStocks");
                });

            modelBuilder.Entity("MedicinePlanner.Data.Models.Medicine", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime?>("ExpirationDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Medicines");
                });

            modelBuilder.Entity("MedicinePlanner.Data.Models.Notification", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<int?>("MedicineId")
                        .HasColumnType("int");

                    b.Property<int?>("PlanningId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("MedicineId");

                    b.HasIndex("PlanningId");

                    b.ToTable("Notifications");
                });

            modelBuilder.Entity("MedicinePlanner.Data.Models.Planning", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Consumed")
                        .HasColumnType("bit");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("MedicineId")
                        .HasColumnType("int");

                    b.Property<int>("Message")
                        .HasColumnType("int");

                    b.Property<DateTime?>("PauseEndDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("MedicineId");

                    b.ToTable("Plannings");
                });

            modelBuilder.Entity("MedicinePlanner.Data.Models.Stock", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("MedicineId")
                        .HasColumnType("int");

                    b.Property<int>("Total")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("MedicineId")
                        .IsUnique();

                    b.ToTable("Stocks");
                });

            modelBuilder.Entity("MedicinePlanner.Data.Models.UnloadingStock", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("PillsNumber")
                        .HasColumnType("int");

                    b.Property<int?>("StockId")
                        .HasColumnType("int");

                    b.Property<DateTime>("UnloadingDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("StockId");

                    b.ToTable("UnloadingStocks");
                });

            modelBuilder.Entity("MedicinePlanner.Data.Models.DailyPlanning", b =>
                {
                    b.HasOne("MedicinePlanner.Data.Models.Notification", "Notification")
                        .WithMany()
                        .HasForeignKey("NotificationId");

                    b.HasOne("MedicinePlanner.Data.Models.Planning", "Planning")
                        .WithMany("DailyPlannings")
                        .HasForeignKey("PlanningId");

                    b.Navigation("Notification");

                    b.Navigation("Planning");
                });

            modelBuilder.Entity("MedicinePlanner.Data.Models.LoadingStock", b =>
                {
                    b.HasOne("MedicinePlanner.Data.Models.Stock", "Stock")
                        .WithMany("LoadingStocks")
                        .HasForeignKey("StockId");

                    b.Navigation("Stock");
                });

            modelBuilder.Entity("MedicinePlanner.Data.Models.Notification", b =>
                {
                    b.HasOne("MedicinePlanner.Data.Models.Medicine", "Medicine")
                        .WithMany("Notifications")
                        .HasForeignKey("MedicineId");

                    b.HasOne("MedicinePlanner.Data.Models.Planning", "Planning")
                        .WithMany()
                        .HasForeignKey("PlanningId");

                    b.Navigation("Medicine");

                    b.Navigation("Planning");
                });

            modelBuilder.Entity("MedicinePlanner.Data.Models.Planning", b =>
                {
                    b.HasOne("MedicinePlanner.Data.Models.Medicine", "Medicine")
                        .WithMany("Plannings")
                        .HasForeignKey("MedicineId");

                    b.Navigation("Medicine");
                });

            modelBuilder.Entity("MedicinePlanner.Data.Models.Stock", b =>
                {
                    b.HasOne("MedicinePlanner.Data.Models.Medicine", "Medicine")
                        .WithOne("Stock")
                        .HasForeignKey("MedicinePlanner.Data.Models.Stock", "MedicineId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Medicine");
                });

            modelBuilder.Entity("MedicinePlanner.Data.Models.UnloadingStock", b =>
                {
                    b.HasOne("MedicinePlanner.Data.Models.Stock", "Stock")
                        .WithMany("UnloadingStocks")
                        .HasForeignKey("StockId");

                    b.Navigation("Stock");
                });

            modelBuilder.Entity("MedicinePlanner.Data.Models.Medicine", b =>
                {
                    b.Navigation("Notifications");

                    b.Navigation("Plannings");

                    b.Navigation("Stock");
                });

            modelBuilder.Entity("MedicinePlanner.Data.Models.Planning", b =>
                {
                    b.Navigation("DailyPlannings");
                });

            modelBuilder.Entity("MedicinePlanner.Data.Models.Stock", b =>
                {
                    b.Navigation("LoadingStocks");

                    b.Navigation("UnloadingStocks");
                });
#pragma warning restore 612, 618
        }
    }
}
