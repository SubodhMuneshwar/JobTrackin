using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace JobTrackin.Api.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "APPLICATION_STATUS",
                columns: table => new
                {
                    ID = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false),
                    CODE = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false),
                    DISPLAY_NAME = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: false),
                    SORT_ORDER = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    IS_ACTIVE = table.Column<bool>(type: "BOOLEAN", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_APPLICATION_STATUS", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "APPLICATION",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "RAW(16)", nullable: false),
                    TENANT_ID = table.Column<Guid>(type: "RAW(16)", nullable: false),
                    COMPANY = table.Column<string>(type: "NVARCHAR2(255)", maxLength: 255, nullable: false),
                    ROLE_TITLE = table.Column<string>(type: "NVARCHAR2(255)", maxLength: 255, nullable: false),
                    JOB_TYPE = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false),
                    WORK_MODE = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false),
                    LOCATION = table.Column<string>(type: "NVARCHAR2(255)", maxLength: 255, nullable: true),
                    APPLIED_AT = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    CURRENT_STATUS_ID = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false),
                    APPLICATION_SOURCE_ID = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: true),
                    DISCOVERY_SOURCE_ID = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false),
                    SALARY_MIN = table.Column<decimal>(type: "DECIMAL(18,2)", precision: 18, scale: 2, nullable: true),
                    SALARY_MAX = table.Column<decimal>(type: "DECIMAL(18,2)", precision: 18, scale: 2, nullable: true),
                    CURRENCY_CODE = table.Column<string>(type: "NVARCHAR2(10)", maxLength: 10, nullable: true),
                    JOB_URL = table.Column<string>(type: "NVARCHAR2(2000)", maxLength: 2000, nullable: true),
                    EMAIL_REFERENCE = table.Column<string>(type: "NVARCHAR2(1000)", maxLength: 1000, nullable: true),
                    NOTES = table.Column<string>(type: "NCLOB", maxLength: 4000, nullable: true),
                    CREATED_AT = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    UPDATED_AT = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_APPLICATION", x => x.ID);
                    table.ForeignKey(
                        name: "FK_APPLICATION_APPLICATION_STATUS_CURRENT_STATUS_ID",
                        column: x => x.CURRENT_STATUS_ID,
                        principalTable: "APPLICATION_STATUS",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "APPLICATION_STATUS",
                columns: new[] { "ID", "CODE", "DISPLAY_NAME", "IS_ACTIVE", "SORT_ORDER" },
                values: new object[,]
                {
                    { "ACCEPTED", "ACCEPTED", "Offer Accepted", true, 9 },
                    { "APPLIED", "APPLIED", "Applied", true, 2 },
                    { "ASSESSMENT", "ASSESSMENT", "Assessment", true, 5 },
                    { "INTERVIEW", "INTERVIEW", "Interview Scheduled", true, 4 },
                    { "OFFER", "OFFER", "Offer Received", true, 6 },
                    { "REJECTED", "REJECTED", "Rejected", true, 7 },
                    { "SAVED", "SAVED", "Saved", true, 1 },
                    { "UNDER_REVIEW", "UNDER_REVIEW", "Under Review", true, 3 },
                    { "UNKNOWN", "UNKNOWN", "Unknown", true, 10 },
                    { "WITHDRAWN", "WITHDRAWN", "Withdrawn", true, 8 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_APPLICATION_CURRENT_STATUS_ID",
                table: "APPLICATION",
                column: "CURRENT_STATUS_ID");

            migrationBuilder.CreateIndex(
                name: "IX_APPLICATION_TENANT_ID_APPLIED_AT",
                table: "APPLICATION",
                columns: new[] { "TENANT_ID", "APPLIED_AT" });

            migrationBuilder.CreateIndex(
                name: "IX_APPLICATION_STATUS_CODE",
                table: "APPLICATION_STATUS",
                column: "CODE",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "APPLICATION");

            migrationBuilder.DropTable(
                name: "APPLICATION_STATUS");
        }
    }
}
