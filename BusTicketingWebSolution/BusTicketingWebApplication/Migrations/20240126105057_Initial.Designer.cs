﻿// <auto-generated />
using System;
using BusTicketingWebApplication.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BusTicketingWebApplication.Migrations
{
    [DbContext(typeof(TicketingContext))]
    [Migration("20240126105057_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.25")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("BusModelLibrary.Bus", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<float>("Cost")
                        .HasColumnType("real");

                    b.Property<int>("DriverAge")
                        .HasColumnType("int");

                    b.Property<string>("DriverName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DriverPhone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<float>("DriverRating")
                        .HasColumnType("real");

                    b.Property<string>("Duration")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("End")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Start")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StartTime")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Buses");
                });

            modelBuilder.Entity("BusTicketingWebApplication.Models.BookedSeat", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("AvailableSeats")
                        .HasColumnType("int");

                    b.Property<int>("BookedSeatCount")
                        .HasColumnType("int");

                    b.Property<string>("BookedSeats")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("BusId")
                        .HasColumnType("int");

                    b.Property<string>("Date")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("BookedSeats");
                });

            modelBuilder.Entity("BusTicketingWebApplication.Models.Booking", b =>
                {
                    b.Property<int>("BookingId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("BookingId"), 1L, 1);

                    b.Property<int>("BusId")
                        .HasColumnType("int");

                    b.Property<string>("Date")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SelectedSeats")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<float>("TotalFare")
                        .HasColumnType("real");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("BookingId");

                    b.ToTable("Bookings");
                });

            modelBuilder.Entity("BusTicketingWebApplication.Models.BusRoute", b =>
                {
                    b.Property<int>("RouteId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RouteId"), 1L, 1);

                    b.Property<int>("Bus")
                        .HasColumnType("int");

                    b.Property<string>("End")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Start")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("RouteId");

                    b.ToTable("BusRoutes");
                });

            modelBuilder.Entity("BusTicketingWebApplication.Models.CancelledBooking", b =>
                {
                    b.Property<int>("CancellationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CancellationId"), 1L, 1);

                    b.Property<int>("BookingId")
                        .HasColumnType("int");

                    b.Property<int>("BusId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CancelledDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("CancelledSeats")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Date")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<float>("TotalFare")
                        .HasColumnType("real");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CancellationId");

                    b.ToTable("CancelledBookings");
                });

            modelBuilder.Entity("BusTicketingWebApplication.Models.User", b =>
                {
                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("Key")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<byte[]>("Password")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Pincode")
                        .HasColumnType("int");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserName");

                    b.ToTable("Users");
                });
#pragma warning restore 612, 618
        }
    }
}
