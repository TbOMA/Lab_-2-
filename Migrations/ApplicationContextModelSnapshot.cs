﻿// <auto-generated />
using Lab__2_.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Lab__2_.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    partial class ApplicationContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Lab_2.Models.AdministratorVm", b =>
                {
                    b.Property<int>("AdministratorID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AdministratorID"));

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("AdministratorID");

                    b.ToTable("Administrators");
                });

            modelBuilder.Entity("Lab_2.Models.CarsVm", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsAvailable")
                        .HasColumnType("bit");

                    b.Property<bool>("IsDamaged")
                        .HasColumnType("bit");

                    b.Property<decimal>("RentPrice")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.ToTable("Cars");

                    b.HasDiscriminator<string>("Discriminator").HasValue("CarsVm");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("Lab_2.Models.ClientVm", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<decimal>("Balance")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("PassportNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Clients");
                });

            modelBuilder.Entity("Lab_2.Models.RentalFormVm", b =>
                {
                    b.Property<int>("RentalFormID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RentalFormID"));

                    b.Property<int>("CarID")
                        .HasColumnType("int");

                    b.Property<int>("ClientID")
                        .HasColumnType("int");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsApproved")
                        .HasColumnType("bit");

                    b.Property<bool>("IsConsidered")
                        .HasColumnType("bit");

                    b.Property<string>("RejectionReason")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("RentalFormID");

                    b.HasIndex("CarID");

                    b.ToTable("RentalForm");

                    b.HasDiscriminator<string>("Discriminator").HasValue("RentalFormVm");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("Lab_2.Models.RentalCarVm", b =>
                {
                    b.HasBaseType("Lab_2.Models.CarsVm");

                    b.Property<string>("CarImagePath")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CarName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.HasDiscriminator().HasValue("RentalCarVm");
                });

            modelBuilder.Entity("Lab_2.Models.OrderVm", b =>
                {
                    b.HasBaseType("Lab_2.Models.RentalFormVm");

                    b.Property<bool>("IsPaid")
                        .HasColumnType("bit");

                    b.Property<int>("RentalTime")
                        .HasColumnType("int");

                    b.Property<decimal>("TotalAmount")
                        .HasColumnType("decimal(18,2)");

                    b.HasIndex("ClientID");

                    b.HasDiscriminator().HasValue("OrderVm");
                });

            modelBuilder.Entity("Lab_2.Models.RentalFormVm", b =>
                {
                    b.HasOne("Lab_2.Models.CarsVm", "Car")
                        .WithMany()
                        .HasForeignKey("CarID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Car");
                });

            modelBuilder.Entity("Lab_2.Models.OrderVm", b =>
                {
                    b.HasOne("Lab_2.Models.ClientVm", "Client")
                        .WithMany("ClientOrders")
                        .HasForeignKey("ClientID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Client");
                });

            modelBuilder.Entity("Lab_2.Models.ClientVm", b =>
                {
                    b.Navigation("ClientOrders");
                });
#pragma warning restore 612, 618
        }
    }
}
