using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ConsumerApi.Migrations
{
    /// <inheritdoc />
    public partial class Migration1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Consumer",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Dob = table.Column<DateTime>(type: "datetime2", maxLength: 255, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Pan = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    BusinessName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Validity = table.Column<bool>(type: "bit", maxLength: 255, nullable: false),
                    AgentName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    AgentId = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Consumer", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ConsumerPolicies",
                columns: table => new
                {
                    PolicyId = table.Column<string>(type: "nvarchar(450)", nullable: false, defaultValueSql: "NEWID()"),
                    BusinessId = table.Column<long>(type: "bigint", nullable: false),
                    ConsumerId = table.Column<long>(type: "bigint", nullable: false),
                    AcceptanceStatus = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    AcceptedQuote = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    CoveredSum = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Duration = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    EffectiveDate = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    PaymentDetails = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    PolicyStatus = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    PropertyType = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ConsumerType = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    AssuredSum = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Tenure = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    BusinessValue = table.Column<long>(type: "bigint", nullable: true),
                    PropertyValue = table.Column<long>(type: "bigint", nullable: true),
                    BaseLocation = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Type = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConsumerPolicies", x => x.PolicyId);
                });

            migrationBuilder.CreateTable(
                name: "quotes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BusinessValue = table.Column<long>(type: "bigint", nullable: false),
                    PropertyValue = table.Column<long>(type: "bigint", nullable: false),
                    PropertyType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Quote = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_quotes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Business",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ConsumerId = table.Column<long>(type: "bigint", nullable: false),
                    BusinessName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    BusinessType = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    BusinessAge = table.Column<int>(type: "int", nullable: false),
                    TotalEmployees = table.Column<int>(type: "int", nullable: false),
                    BusinessValue = table.Column<int>(type: "int", nullable: false),
                    CapitalInvested = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    BusinessTurnover = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Business", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Business_Consumer_ConsumerId",
                        column: x => x.ConsumerId,
                        principalTable: "Consumer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Property",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BusinessId = table.Column<long>(type: "bigint", nullable: false),
                    ConsumerId = table.Column<long>(type: "bigint", nullable: false),
                    BuildingSqFt = table.Column<int>(type: "int", maxLength: 255, nullable: false),
                    BuildingType = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    BuildingStoreys = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    BuildingAge = table.Column<long>(type: "bigint", nullable: false),
                    PropertyValue = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CostOfTheAsset = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SalvageValue = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    UsefulLifeOfAsset = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Property", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Property_Business_BusinessId",
                        column: x => x.BusinessId,
                        principalTable: "Business",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Property_Consumer_ConsumerId",
                        column: x => x.ConsumerId,
                        principalTable: "Consumer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Business_ConsumerId",
                table: "Business",
                column: "ConsumerId");

            migrationBuilder.CreateIndex(
                name: "IX_Property_BusinessId",
                table: "Property",
                column: "BusinessId");

            migrationBuilder.CreateIndex(
                name: "IX_Property_ConsumerId",
                table: "Property",
                column: "ConsumerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ConsumerPolicies");

            migrationBuilder.DropTable(
                name: "Property");

            migrationBuilder.DropTable(
                name: "quotes");

            migrationBuilder.DropTable(
                name: "Business");

            migrationBuilder.DropTable(
                name: "Consumer");
        }
    }
}
