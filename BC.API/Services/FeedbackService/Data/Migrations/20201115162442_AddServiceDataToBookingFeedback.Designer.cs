﻿// <auto-generated />
using System;
using BC.API.Services.FeedbackService.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace BC.API.Services.FeedbackService.Data.Migrations
{
    [DbContext(typeof(FeedbackContext))]
    [Migration("20201115162442_AddServiceDataToBookingFeedback")]
    partial class AddServiceDataToBookingFeedback
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("feedback")
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.0-rc.1.20451.13");

            modelBuilder.Entity("BC.API.Services.FeedbackService.Data.BookingFeedback", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("BookingId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ClientId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ClientName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("MasterId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("MasterName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTimeOffset>("PostedAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<Guid>("ServiceId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ServiceName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Stars")
                        .HasColumnType("int");

                    b.Property<string>("Text")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("BookingFeedbacks");
                });
#pragma warning restore 612, 618
        }
    }
}
