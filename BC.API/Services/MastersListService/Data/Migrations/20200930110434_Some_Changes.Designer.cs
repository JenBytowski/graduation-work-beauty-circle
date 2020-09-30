﻿// <auto-generated />
using System;
using BC.API.Services.MastersListService.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace BC.API.Services.MastersListService.Data.Migrations
{
    [DbContext(typeof(MastersContext))]
    [Migration("20200930110434_Some_Changes")]
    partial class Some_Changes
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("masters")
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.0-rc.1.20451.13");

            modelBuilder.Entity("BC.API.Services.MastersListService.Data.City", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("MapProviderId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Cities");
                });

            modelBuilder.Entity("BC.API.Services.MastersListService.Data.Master", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("About")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AvatarUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("CityId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("InstagramProfile")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("PriceListId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("ReviewsCount")
                        .HasColumnType("int");

                    b.Property<Guid>("ScheduleId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("ServiceTypeId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Skype")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("SpecialityId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<double>("Stars")
                        .HasColumnType("float");

                    b.Property<string>("Viber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("VkProfile")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CityId");

                    b.HasIndex("PriceListId");

                    b.HasIndex("ScheduleId");

                    b.HasIndex("ServiceTypeId");

                    b.HasIndex("SpecialityId");

                    b.ToTable("Masters");
                });

            modelBuilder.Entity("BC.API.Services.MastersListService.Data.PriceList", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.ToTable("PriceLists");
                });

            modelBuilder.Entity("BC.API.Services.MastersListService.Data.PriceListItem", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("DurationInMinutesMax")
                        .HasColumnType("int");

                    b.Property<Guid>("PriceListId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("PriceMax")
                        .HasColumnType("int");

                    b.Property<int>("PriceMin")
                        .HasColumnType("int");

                    b.Property<Guid>("ServiceTypeId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("PriceListId");

                    b.HasIndex("ServiceTypeId");

                    b.ToTable("PriceListItems");
                });

            modelBuilder.Entity("BC.API.Services.MastersListService.Data.Schedule", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.ToTable("Schedules");
                });

            modelBuilder.Entity("BC.API.Services.MastersListService.Data.ScheduleDay", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("DayOfWeek")
                        .HasColumnType("int");

                    b.Property<DateTime>("EndTime")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("ScheduleId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("StartTime")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("ScheduleId");

                    b.ToTable("ScheduleDays");
                });

            modelBuilder.Entity("BC.API.Services.MastersListService.Data.ScheduleDayItem", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("EndTime")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("ScheduleDayId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("StartTime")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("ScheduleDayId");

                    b.ToTable("ScheduleDayItem");

                    b.HasDiscriminator<string>("Discriminator").HasValue("ScheduleDayItem");
                });

            modelBuilder.Entity("BC.API.Services.MastersListService.Data.ServiceType", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("ServiceTypeGroupId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("ServiceTypeGroupId");

                    b.ToTable("ServiceTypes");
                });

            modelBuilder.Entity("BC.API.Services.MastersListService.Data.ServiceTypeGroup", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("ParentServiceTypeGroupId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.ToTable("ServiceTypeGroups");
                });

            modelBuilder.Entity("BC.API.Services.MastersListService.Data.ServiceTypeSubGroup", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("ServiceTypeGroupId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("ServiceTypeGroupId");

                    b.ToTable("ServiceTypeSubGroup");
                });

            modelBuilder.Entity("BC.API.Services.MastersListService.Data.Speciality", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Specialities");
                });

            modelBuilder.Entity("BC.API.Services.MastersListService.Data.Booking", b =>
                {
                    b.HasBaseType("BC.API.Services.MastersListService.Data.ScheduleDayItem");

                    b.HasDiscriminator().HasValue("Booking");
                });

            modelBuilder.Entity("BC.API.Services.MastersListService.Data.Pause", b =>
                {
                    b.HasBaseType("BC.API.Services.MastersListService.Data.ScheduleDayItem");

                    b.HasDiscriminator().HasValue("Pause");
                });

            modelBuilder.Entity("BC.API.Services.MastersListService.Data.Master", b =>
                {
                    b.HasOne("BC.API.Services.MastersListService.Data.City", "City")
                        .WithMany()
                        .HasForeignKey("CityId");

                    b.HasOne("BC.API.Services.MastersListService.Data.PriceList", "PriceList")
                        .WithMany()
                        .HasForeignKey("PriceListId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BC.API.Services.MastersListService.Data.Schedule", "Schedule")
                        .WithMany()
                        .HasForeignKey("ScheduleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BC.API.Services.MastersListService.Data.ServiceType", null)
                        .WithMany("Masters")
                        .HasForeignKey("ServiceTypeId");

                    b.HasOne("BC.API.Services.MastersListService.Data.Speciality", "Speciality")
                        .WithMany()
                        .HasForeignKey("SpecialityId");

                    b.Navigation("City");

                    b.Navigation("PriceList");

                    b.Navigation("Schedule");

                    b.Navigation("Speciality");
                });

            modelBuilder.Entity("BC.API.Services.MastersListService.Data.PriceListItem", b =>
                {
                    b.HasOne("BC.API.Services.MastersListService.Data.PriceList", "PriceList")
                        .WithMany("PriceListItems")
                        .HasForeignKey("PriceListId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BC.API.Services.MastersListService.Data.ServiceType", "ServiceType")
                        .WithMany()
                        .HasForeignKey("ServiceTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("PriceList");

                    b.Navigation("ServiceType");
                });

            modelBuilder.Entity("BC.API.Services.MastersListService.Data.ScheduleDay", b =>
                {
                    b.HasOne("BC.API.Services.MastersListService.Data.Schedule", null)
                        .WithMany("Days")
                        .HasForeignKey("ScheduleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("BC.API.Services.MastersListService.Data.ScheduleDayItem", b =>
                {
                    b.HasOne("BC.API.Services.MastersListService.Data.ScheduleDay", "ScheduleDay")
                        .WithMany("Items")
                        .HasForeignKey("ScheduleDayId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ScheduleDay");
                });

            modelBuilder.Entity("BC.API.Services.MastersListService.Data.ServiceType", b =>
                {
                    b.HasOne("BC.API.Services.MastersListService.Data.ServiceTypeGroup", "ServiceTypeGroup")
                        .WithMany()
                        .HasForeignKey("ServiceTypeGroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ServiceTypeGroup");
                });

            modelBuilder.Entity("BC.API.Services.MastersListService.Data.ServiceTypeSubGroup", b =>
                {
                    b.HasOne("BC.API.Services.MastersListService.Data.ServiceTypeGroup", "ServiceTypeGroup")
                        .WithMany("ServiceTypeSubGroupsGroups")
                        .HasForeignKey("ServiceTypeGroupId");

                    b.Navigation("ServiceTypeGroup");
                });

            modelBuilder.Entity("BC.API.Services.MastersListService.Data.PriceList", b =>
                {
                    b.Navigation("PriceListItems");
                });

            modelBuilder.Entity("BC.API.Services.MastersListService.Data.Schedule", b =>
                {
                    b.Navigation("Days");
                });

            modelBuilder.Entity("BC.API.Services.MastersListService.Data.ScheduleDay", b =>
                {
                    b.Navigation("Items");
                });

            modelBuilder.Entity("BC.API.Services.MastersListService.Data.ServiceType", b =>
                {
                    b.Navigation("Masters");
                });

            modelBuilder.Entity("BC.API.Services.MastersListService.Data.ServiceTypeGroup", b =>
                {
                    b.Navigation("ServiceTypeSubGroupsGroups");
                });
#pragma warning restore 612, 618
        }
    }
}
