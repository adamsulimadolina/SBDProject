﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Project.Data;

namespace Project.Migrations
{
    [DbContext(typeof(ProjectContext))]
    partial class ProjectContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.2-servicing-10034")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Project.Models.AdvertisementModel", b =>
                {
                    b.Property<int>("AdvertisementID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AdvertisementType")
                        .HasMaxLength(40);

                    b.Property<int>("FlatID");

                    b.Property<int>("OwnerID");

                    b.HasKey("AdvertisementID");

                    b.HasIndex("FlatID");

                    b.HasIndex("OwnerID");

                    b.ToTable("Ogłoszenie");
                });

            modelBuilder.Entity("Project.Models.CityModel", b =>
                {
                    b.Property<int>("CityID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CityName")
                        .HasMaxLength(50);

                    b.HasKey("CityID");

                    b.ToTable("Miasto");
                });

            modelBuilder.Entity("Project.Models.FlatModel", b =>
                {
                    b.Property<int>("FlatID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("BathroomCount");

                    b.Property<int>("CityID");

                    b.Property<string>("KitchenType")
                        .HasMaxLength(20);

                    b.Property<int>("RoomsCount");

                    b.Property<double>("Surface");

                    b.HasKey("FlatID");

                    b.HasIndex("CityID");

                    b.ToTable("Mieszkanie");
                });

            modelBuilder.Entity("Project.Models.LogsModel", b =>
                {
                    b.Property<int>("LogID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Log")
                        .HasMaxLength(50);

                    b.Property<DateTime?>("MessageDate");

                    b.Property<int>("UserID");

                    b.HasKey("LogID");

                    b.HasIndex("UserID");

                    b.ToTable("Logi");
                });

            modelBuilder.Entity("Project.Models.MessagesModel", b =>
                {
                    b.Property<int>("MessageID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Message")
                        .HasMaxLength(256);

                    b.Property<DateTime?>("MessageDate");

                    b.Property<int?>("UserRUserID");

                    b.Property<int>("UserReceiverID");

                    b.Property<int?>("UserSUserID");

                    b.Property<int>("UserSenderID");

                    b.HasKey("MessageID");

                    b.HasIndex("UserRUserID");

                    b.HasIndex("UserSUserID");

                    b.ToTable("Wiadomości");
                });

            modelBuilder.Entity("Project.Models.OpinionModel", b =>
                {
                    b.Property<int>("OpinionID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Opinion")
                        .HasMaxLength(250);

                    b.Property<int>("OwnerID");

                    b.Property<int?>("UserID");

                    b.HasKey("OpinionID");

                    b.HasIndex("OwnerID");

                    b.HasIndex("UserID");

                    b.ToTable("Opinie");
                });

            modelBuilder.Entity("Project.Models.OwnerModel", b =>
                {
                    b.Property<int>("OwnerID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasMaxLength(20);

                    b.Property<string>("Surname")
                        .HasMaxLength(30);

                    b.Property<int>("UserID");

                    b.HasKey("OwnerID");

                    b.HasIndex("UserID");

                    b.ToTable("Wlasciciel");
                });

            modelBuilder.Entity("Project.Models.PairsModel", b =>
                {
                    b.Property<int>("PairID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<double>("PairCompatibility");

                    b.Property<int>("TenantID_1");

                    b.Property<int>("TenantID_2");

                    b.Property<int?>("Tenant_1TenantID");

                    b.Property<int?>("Tenant_2TenantID");

                    b.HasKey("PairID");

                    b.HasIndex("Tenant_1TenantID");

                    b.HasIndex("Tenant_2TenantID");

                    b.ToTable("Parowanie");
                });

            modelBuilder.Entity("Project.Models.RoomModel", b =>
                {
                    b.Property<int>("RoomID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AdditionalInfo")
                        .HasMaxLength(40);

                    b.Property<bool>("Balcony");

                    b.Property<bool>("Bed");

                    b.Property<int>("FlatID");

                    b.Property<double>("Rent");

                    b.Property<double>("Surface");

                    b.Property<bool>("Wardrobe");

                    b.HasKey("RoomID");

                    b.HasIndex("FlatID");

                    b.ToTable("Pokoj");
                });

            modelBuilder.Entity("Project.Models.TenantModel", b =>
                {
                    b.Property<int>("TenantID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Age");

                    b.Property<bool>("IsSmoking");

                    b.Property<bool>("IsVege");

                    b.Property<string>("Name")
                        .HasMaxLength(20);

                    b.Property<string>("Status");

                    b.Property<string>("Surname")
                        .HasMaxLength(30);

                    b.Property<int>("UserID");

                    b.HasKey("TenantID");

                    b.HasIndex("UserID");

                    b.ToTable("Najemca");
                });

            modelBuilder.Entity("Project.Models.UserModel", b =>
                {
                    b.Property<int>("UserID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Login")
                        .IsRequired();

                    b.Property<string>("Password")
                        .IsRequired();

                    b.Property<string>("VerifyPassword");

                    b.HasKey("UserID");

                    b.ToTable("User");
                });

            modelBuilder.Entity("Project.Models.AdvertisementModel", b =>
                {
                    b.HasOne("Project.Models.FlatModel", "Flat")
                        .WithMany()
                        .HasForeignKey("FlatID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Project.Models.OwnerModel", "Owner")
                        .WithMany()
                        .HasForeignKey("OwnerID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Project.Models.FlatModel", b =>
                {
                    b.HasOne("Project.Models.CityModel", "City")
                        .WithMany()
                        .HasForeignKey("CityID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Project.Models.LogsModel", b =>
                {
                    b.HasOne("Project.Models.UserModel", "UserR")
                        .WithMany()
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Project.Models.MessagesModel", b =>
                {
                    b.HasOne("Project.Models.UserModel", "UserR")
                        .WithMany()
                        .HasForeignKey("UserRUserID");

                    b.HasOne("Project.Models.UserModel", "UserS")
                        .WithMany()
                        .HasForeignKey("UserSUserID");
                });

            modelBuilder.Entity("Project.Models.OpinionModel", b =>
                {
                    b.HasOne("Project.Models.OwnerModel", "Owner")
                        .WithMany()
                        .HasForeignKey("OwnerID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Project.Models.UserModel", "User")
                        .WithMany()
                        .HasForeignKey("UserID");
                });

            modelBuilder.Entity("Project.Models.OwnerModel", b =>
                {
                    b.HasOne("Project.Models.UserModel", "User")
                        .WithMany()
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Project.Models.PairsModel", b =>
                {
                    b.HasOne("Project.Models.TenantModel", "Tenant_1")
                        .WithMany()
                        .HasForeignKey("Tenant_1TenantID");

                    b.HasOne("Project.Models.TenantModel", "Tenant_2")
                        .WithMany()
                        .HasForeignKey("Tenant_2TenantID");
                });

            modelBuilder.Entity("Project.Models.RoomModel", b =>
                {
                    b.HasOne("Project.Models.FlatModel", "Flat")
                        .WithMany()
                        .HasForeignKey("FlatID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Project.Models.TenantModel", b =>
                {
                    b.HasOne("Project.Models.UserModel", "User")
                        .WithMany()
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
