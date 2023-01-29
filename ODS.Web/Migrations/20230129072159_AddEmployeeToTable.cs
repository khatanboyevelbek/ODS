using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ODS.Web.Migrations
{
    /// <inheritdoc />
    public partial class AddEmployeeToTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PayrollNumber = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DateOfBirth = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Address1 = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Address2 = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PostalCode = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    StartedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    UpdatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Employees_Id_PayrollNumber_FirstName_LastName_DateOfBirth_PhoneNumber_Address1_Address2_PostalCode_Email_StartedDate_Created~",
                table: "Employees",
                columns: new[] { "Id", "PayrollNumber", "FirstName", "LastName", "DateOfBirth", "PhoneNumber", "Address1", "Address2", "PostalCode", "Email", "StartedDate", "CreatedDate", "UpdatedDate" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Employees");
        }
    }
}
