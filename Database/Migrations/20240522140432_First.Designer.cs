﻿// <auto-generated />
using System;
using Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Database.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    [Migration("20240522140432_First")]
    partial class First
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Database.MDLS.Authorization", b =>
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

            modelBuilder.Entity("Database.MDLS.Certificate", b =>
                {
                    b.Property<int>("IdQuaCertificate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdQuaCertificate"));

                    b.Property<DateTime>("DTCertificate")
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

                    b.Property<string>("PropertiesCert")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StandardPerMark")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Units")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdQuaCertificate");

                    b.ToTable("Certificate");
                });

            modelBuilder.Entity("Database.MDLS.Company", b =>
                {
                    b.Property<int>("IdCompany")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdCompany"));

                    b.Property<string>("NameCompany")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdCompany");

                    b.ToTable("Company");
                });

            modelBuilder.Entity("Database.MDLS.Consignee", b =>
                {
                    b.Property<int>("IdConsignee")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdConsignee"));

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FIOConsignee")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("IdCompany")
                        .HasColumnType("int");

                    b.Property<int?>("IdPayer")
                        .HasColumnType("int");

                    b.Property<string>("PhoneCons")
                        .IsRequired()
                        .HasMaxLength(12)
                        .HasColumnType("nvarchar(12)");

                    b.HasKey("IdConsignee");

                    b.HasIndex("IdCompany");

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

            modelBuilder.Entity("Database.MDLS.ContainerPackage", b =>
                {
                    b.Property<int>("IdContainerPackage")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdContainerPackage"));

                    b.Property<string>("MarkContainer")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TypeModel")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdContainerPackage");

                    b.ToTable("ContainerPackage");
                });

            modelBuilder.Entity("Database.MDLS.Defects", b =>
                {
                    b.Property<int>("IdDefect")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdDefect"));

                    b.Property<DateTime?>("DTProductSending")
                        .HasColumnType("datetime2");

                    b.Property<string>("FIOSend")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("IDSend")
                        .HasColumnType("int");

                    b.Property<int>("IdOrder")
                        .HasColumnType("int");

                    b.Property<string>("Reasons")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdDefect");

                    b.HasIndex("IdOrder");

                    b.ToTable("Defects");
                });

            modelBuilder.Entity("Database.MDLS.Delivery", b =>
                {
                    b.Property<int>("IdDelivery")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdDelivery"));

                    b.Property<DateTime?>("DTDelivery")
                        .HasColumnType("datetime2");

                    b.Property<string>("EarlyDelivery")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("IdOrder")
                        .HasColumnType("int");

                    b.HasKey("IdDelivery");

                    b.ToTable("Delivery");
                });

            modelBuilder.Entity("Database.MDLS.Orders", b =>
                {
                    b.Property<int>("IdOrder")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdOrder"));

                    b.Property<DateTime?>("DTAdoption")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DTAttestation")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DTReceived")
                        .HasColumnType("datetime2");

                    b.Property<int>("IdCompany")
                        .HasColumnType("int");

                    b.Property<int?>("IdConsignee")
                        .HasColumnType("int");

                    b.Property<int?>("IdPayer")
                        .HasColumnType("int");

                    b.Property<int?>("IdQuaCertificate")
                        .HasColumnType("int");

                    b.Property<int?>("IdStorage")
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

                    b.HasIndex("IdCompany");

                    b.HasIndex("IdConsignee");

                    b.HasIndex("IdPayer");

                    b.HasIndex("IdStorage");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("Database.MDLS.Payer", b =>
                {
                    b.Property<int>("IdPayer")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdPayer"));

                    b.Property<string>("FIOPayer")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneP")
                        .IsRequired()
                        .HasMaxLength(12)
                        .HasColumnType("nvarchar(12)");

                    b.HasKey("IdPayer");

                    b.ToTable("Payer");
                });

            modelBuilder.Entity("Database.MDLS.Shipment", b =>
                {
                    b.Property<int>("IdShipment")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdShipment"));

                    b.Property<DateTime?>("DTShipments")
                        .HasColumnType("datetime2");

                    b.Property<int>("IdOrder")
                        .HasColumnType("int");

                    b.Property<int?>("IdTransport")
                        .HasColumnType("int");

                    b.Property<int?>("ShipmentTotalAmountTons")
                        .HasColumnType("int");

                    b.HasKey("IdShipment");

                    b.ToTable("Shipment");
                });

            modelBuilder.Entity("Database.MDLS.Storage", b =>
                {
                    b.Property<int>("IdStorage")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdStorage"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("DateAddStorage")
                        .HasColumnType("datetime2");

                    b.Property<string>("FIOResponsible")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("IdCompany")
                        .HasColumnType("int");

                    b.Property<string>("NameStorage")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasMaxLength(12)
                        .HasColumnType("nvarchar(12)");

                    b.HasKey("IdStorage");

                    b.HasIndex("IdCompany");

                    b.ToTable("Storage");
                });

            modelBuilder.Entity("Database.MDLS.Transport", b =>
                {
                    b.Property<int>("IdTransport")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdTransport"));

                    b.Property<string>("NameTransport")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("VehicleRegistration")
                        .HasMaxLength(8)
                        .HasColumnType("nvarchar(8)");

                    b.HasKey("IdTransport");

                    b.ToTable("Transport");
                });

            modelBuilder.Entity("Database.MDLS.Consignee", b =>
                {
                    b.HasOne("Database.MDLS.Company", "Company")
                        .WithMany("Consignee")
                        .HasForeignKey("IdCompany")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Company");
                });

            modelBuilder.Entity("Database.MDLS.Defects", b =>
                {
                    b.HasOne("Database.MDLS.Orders", "Orders")
                        .WithMany()
                        .HasForeignKey("IdOrder")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Orders");
                });

            modelBuilder.Entity("Database.MDLS.Orders", b =>
                {
                    b.HasOne("Database.MDLS.Company", "Company")
                        .WithMany()
                        .HasForeignKey("IdCompany")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Database.MDLS.Consignee", "Consignee")
                        .WithMany()
                        .HasForeignKey("IdConsignee");

                    b.HasOne("Database.MDLS.Payer", "Payer")
                        .WithMany()
                        .HasForeignKey("IdPayer");

                    b.HasOne("Database.MDLS.Storage", "Storage")
                        .WithMany()
                        .HasForeignKey("IdStorage");

                    b.Navigation("Company");

                    b.Navigation("Consignee");

                    b.Navigation("Payer");

                    b.Navigation("Storage");
                });

            modelBuilder.Entity("Database.MDLS.Storage", b =>
                {
                    b.HasOne("Database.MDLS.Company", "Company")
                        .WithMany("Storage")
                        .HasForeignKey("IdCompany")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Company");
                });

            modelBuilder.Entity("Database.MDLS.Company", b =>
                {
                    b.Navigation("Consignee");

                    b.Navigation("Storage");
                });
#pragma warning restore 612, 618
        }
    }
}
