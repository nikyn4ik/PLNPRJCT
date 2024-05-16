﻿// <auto-generated />
using System;
using Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Database.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    partial class ApplicationContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Database.Authorization", b =>
                {
                    b.Property<int>("ID_aut")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID_aut"));

                    b.Property<string>("FIO")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PassHash")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID_aut");

                    b.ToTable("Authorization");
                });

            modelBuilder.Entity("Database.Certificate", b =>
                {
                    b.Property<int>("IdQuaCertificate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdQuaCertificate"));

                    b.Property<DateTime>("DateAddCertificate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Manufacturer")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Max")
                        .HasColumnType("int");

                    b.Property<int>("Min")
                        .HasColumnType("int");

                    b.Property<string>("ProductStandard")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StandardPerMark")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Units")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("properties_cert")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdQuaCertificate");

                    b.ToTable("Certificate");
                });

            modelBuilder.Entity("Database.Delivery", b =>
                {
                    b.Property<int>("IdDelivery")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdDelivery"));

                    b.Property<string>("Consignee")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("DateOfDelivery")
                        .HasColumnType("datetime2");

                    b.Property<int>("IdOrder")
                        .HasColumnType("int");

                    b.HasKey("IdDelivery");

                    b.ToTable("Delivery");
                });

            modelBuilder.Entity("Database.MDLS.Consignee", b =>
                {
                    b.Property<int>("IdConsignee")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdConsignee"));

                    b.Property<string>("Company")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FIO")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("IdPayer")
                        .HasColumnType("int");

                    b.Property<string>("email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("phone")
                        .IsRequired()
                        .HasMaxLength(12)
                        .HasColumnType("nvarchar(12)");

                    b.HasKey("IdConsignee");

                    b.ToTable("Consignee");
                });

            modelBuilder.Entity("Database.MDLS.Container", b =>
                {
                    b.Property<int>("IdContainer")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdContainer"));

                    b.Property<DateTime?>("DTContainer")
                        .HasColumnType("datetime2");

                    b.Property<int>("IdOrder")
                        .HasColumnType("int");

                    b.Property<string>("MarkContainer")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TypeModel")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdContainer");

                    b.ToTable("Container");
                });

            modelBuilder.Entity("Database.MDLS.Defects", b =>
                {
                    b.Property<int>("IdDefect")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdDefect"));

                    b.Property<string>("FIO")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("IdOrder")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("ProductSending")
                        .HasColumnType("datetime2");

                    b.Property<string>("Reasons")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdDefect");

                    b.ToTable("Defects");
                });

            modelBuilder.Entity("Database.MDLS.Payer", b =>
                {
                    b.Property<int>("IdPayer")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdPayer"));

                    b.Property<string>("FIO")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("phone")
                        .IsRequired()
                        .HasMaxLength(12)
                        .HasColumnType("nvarchar(12)");

                    b.HasKey("IdPayer");

                    b.ToTable("Payer");
                });

            modelBuilder.Entity("Database.MDLS.Transport", b =>
                {
                    b.Property<int>("IdTransport")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdTransport"));

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("VehicleRegistration")
                        .HasMaxLength(8)
                        .HasColumnType("nvarchar(8)");

                    b.HasKey("IdTransport");

                    b.ToTable("Transport");
                });

            modelBuilder.Entity("Database.Orders", b =>
                {
                    b.Property<int>("IdOrder")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdOrder"));

                    b.Property<string>("AccessStandart")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Company")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("DTAdoption")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DTDelivery")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DTReceived")
                        .HasColumnType("datetime2");

                    b.Property<int?>("IdConsignee")
                        .HasColumnType("int");

                    b.Property<int?>("IdPayer")
                        .HasColumnType("int");

                    b.Property<int?>("IdQuaCertificate")
                        .HasColumnType("int");

                    b.Property<double>("LengthMm")
                        .HasColumnType("float");

                    b.Property<string>("LogC3")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Mark")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NameStorage")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StatusOrder")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SystC3")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("ThicknessMm")
                        .HasColumnType("float");

                    b.Property<double>("WidthMm")
                        .HasColumnType("float");

                    b.HasKey("IdOrder");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("Database.Shipment", b =>
                {
                    b.Property<int>("IdShipment")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdShipment"));

                    b.Property<string>("Consignee")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("DTShipments")
                        .HasColumnType("datetime2");

                    b.Property<int?>("IdOrder")
                        .HasColumnType("int");

                    b.Property<int?>("IdTransport")
                        .HasColumnType("int");

                    b.Property<float?>("NumberOfShipmentsPerMonthTons")
                        .HasColumnType("real");

                    b.Property<int?>("ShipmentTotalAmountTons")
                        .HasColumnType("int");

                    b.HasKey("IdShipment");

                    b.ToTable("Shipment");
                });

            modelBuilder.Entity("Database.Storage", b =>
                {
                    b.Property<int>("IdStorage")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdStorage"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Company")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("DateAddStorage")
                        .HasColumnType("datetime2");

                    b.Property<string>("FIOResponsible")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasMaxLength(12)
                        .HasColumnType("nvarchar(12)");

                    b.HasKey("IdStorage");

                    b.ToTable("Storage");
                });
#pragma warning restore 612, 618
        }
    }
}
