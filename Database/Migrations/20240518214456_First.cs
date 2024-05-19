using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Database.Migrations
{
    /// <inheritdoc />
    public partial class First : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Authorization",
                columns: table => new
                {
                    ID_aut = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Login = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FIO = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PassHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Authorization", x => x.ID_aut);
                });

            migrationBuilder.CreateTable(
                name: "Certificate",
                columns: table => new
                {
                    IdQuaCertificate = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StandardPerMark = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Manufacturer = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProductStandard = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Min = table.Column<int>(type: "int", nullable: false),
                    Max = table.Column<int>(type: "int", nullable: false),
                    Units = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    properties_cert = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateAddCertificate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Certificate", x => x.IdQuaCertificate);
                });

            migrationBuilder.CreateTable(
                name: "Company",
                columns: table => new
                {
                    IdCompany = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Company", x => x.IdCompany);
                });

            migrationBuilder.CreateTable(
                name: "Container",
                columns: table => new
                {
                    IdContainer = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdOrder = table.Column<int>(type: "int", nullable: false),
                    TypeModel = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MarkContainer = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DTContainer = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Container", x => x.IdContainer);
                });

            migrationBuilder.CreateTable(
                name: "ContainerPackage",
                columns: table => new
                {
                    IdContainer = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TypeModel = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MarkContainer = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContainerPackage", x => x.IdContainer);
                });

            migrationBuilder.CreateTable(
                name: "Defects",
                columns: table => new
                {
                    IdDefect = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdOrder = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Reasons = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProductSending = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FIO = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Defects", x => x.IdDefect);
                });

            migrationBuilder.CreateTable(
                name: "Delivery",
                columns: table => new
                {
                    IdDelivery = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdOrder = table.Column<int>(type: "int", nullable: false),
                    Consignee = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateOfDelivery = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Delivery", x => x.IdDelivery);
                });

            migrationBuilder.CreateTable(
                name: "Payer",
                columns: table => new
                {
                    IdPayer = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FIO = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    phone = table.Column<string>(type: "nvarchar(12)", maxLength: 12, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payer", x => x.IdPayer);
                });

            migrationBuilder.CreateTable(
                name: "Shipment",
                columns: table => new
                {
                    IdShipment = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdOrder = table.Column<int>(type: "int", nullable: true),
                    Consignee = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DTShipments = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ShipmentTotalAmountTons = table.Column<int>(type: "int", nullable: true),
                    IdTransport = table.Column<int>(type: "int", nullable: true),
                    NumberOfShipmentsPerMonthTons = table.Column<float>(type: "real", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shipment", x => x.IdShipment);
                });

            migrationBuilder.CreateTable(
                name: "Transport",
                columns: table => new
                {
                    IdTransport = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VehicleRegistration = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transport", x => x.IdTransport);
                });

            migrationBuilder.CreateTable(
                name: "Consignee",
                columns: table => new
                {
                    IdConsignee = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FIO = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdPayer = table.Column<int>(type: "int", nullable: true),
                    phone = table.Column<string>(type: "nvarchar(12)", maxLength: 12, nullable: false),
                    email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdCompany = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Consignee", x => x.IdConsignee);
                    table.ForeignKey(
                        name: "FK_Consignee_Company_IdCompany",
                        column: x => x.IdCompany,
                        principalTable: "Company",
                        principalColumn: "IdCompany",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Storage",
                columns: table => new
                {
                    IdStorage = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(12)", maxLength: 12, nullable: false),
                    FIOResponsible = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateAddStorage = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IdCompany = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Storage", x => x.IdStorage);
                    table.ForeignKey(
                        name: "FK_Storage_Company_IdCompany",
                        column: x => x.IdCompany,
                        principalTable: "Company",
                        principalColumn: "IdCompany",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    IdOrder = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SystC3 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LogC3 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdPayer = table.Column<int>(type: "int", nullable: true),
                    IdConsignee = table.Column<int>(type: "int", nullable: true),
                    DTDelivery = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DTReceived = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DTAdoption = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ThicknessMm = table.Column<double>(type: "float", nullable: false),
                    WidthMm = table.Column<double>(type: "float", nullable: false),
                    LengthMm = table.Column<double>(type: "float", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdCompany = table.Column<int>(type: "int", nullable: false),
                    StatusOrder = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Mark = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdQuaCertificate = table.Column<int>(type: "int", nullable: true),
                    AccessStandart = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdStorage = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.IdOrder);
                    table.ForeignKey(
                        name: "FK_Orders_Company_IdCompany",
                        column: x => x.IdCompany,
                        principalTable: "Company",
                        principalColumn: "IdCompany",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Orders_Consignee_IdConsignee",
                        column: x => x.IdConsignee,
                        principalTable: "Consignee",
                        principalColumn: "IdConsignee");
                    table.ForeignKey(
                        name: "FK_Orders_Payer_IdPayer",
                        column: x => x.IdPayer,
                        principalTable: "Payer",
                        principalColumn: "IdPayer");
                    table.ForeignKey(
                        name: "FK_Orders_Storage_IdStorage",
                        column: x => x.IdStorage,
                        principalTable: "Storage",
                        principalColumn: "IdStorage");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Consignee_IdCompany",
                table: "Consignee",
                column: "IdCompany");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_IdCompany",
                table: "Orders",
                column: "IdCompany");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_IdConsignee",
                table: "Orders",
                column: "IdConsignee");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_IdPayer",
                table: "Orders",
                column: "IdPayer");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_IdStorage",
                table: "Orders",
                column: "IdStorage");

            migrationBuilder.CreateIndex(
                name: "IX_Storage_IdCompany",
                table: "Storage",
                column: "IdCompany");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Authorization");

            migrationBuilder.DropTable(
                name: "Certificate");

            migrationBuilder.DropTable(
                name: "Container");

            migrationBuilder.DropTable(
                name: "ContainerPackage");

            migrationBuilder.DropTable(
                name: "Defects");

            migrationBuilder.DropTable(
                name: "Delivery");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Shipment");

            migrationBuilder.DropTable(
                name: "Transport");

            migrationBuilder.DropTable(
                name: "Consignee");

            migrationBuilder.DropTable(
                name: "Payer");

            migrationBuilder.DropTable(
                name: "Storage");

            migrationBuilder.DropTable(
                name: "Company");
        }
    }
}
