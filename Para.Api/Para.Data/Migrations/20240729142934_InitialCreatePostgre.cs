using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Para.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreatePostgre : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.CreateTable(
                name: "Country",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CountyCode = table.Column<string>(type: "character varying(3)", maxLength: 3, nullable: false),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    InsertUser = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    InsertDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Country", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Customer",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FirstName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    IdentityNumber = table.Column<string>(type: "character varying(11)", maxLength: 11, nullable: false),
                    Email = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    CustomerNumber = table.Column<int>(type: "integer", nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    InsertUser = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    InsertDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customer", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Account",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CustomerId = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    AccountNumber = table.Column<int>(type: "integer", nullable: false),
                    IBAN = table.Column<string>(type: "character varying(34)", maxLength: 34, nullable: false),
                    Balance = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false, defaultValue: 0m),
                    CurrencyCode = table.Column<string>(type: "character varying(3)", maxLength: 3, nullable: false),
                    OpenDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    InsertUser = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    InsertDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Account", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Account_Customer_CustomerId",
                        column: x => x.CustomerId,
                        principalSchema: "dbo",
                        principalTable: "Customer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CustomerAddress",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CustomerId = table.Column<long>(type: "bigint", nullable: false),
                    Country = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    City = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    AddressLine = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
                    ZipCode = table.Column<string>(type: "character varying(6)", maxLength: 6, nullable: true),
                    IsDefault = table.Column<bool>(type: "boolean", nullable: false),
                    InsertUser = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    InsertDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerAddress", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CustomerAddress_Customer_CustomerId",
                        column: x => x.CustomerId,
                        principalSchema: "dbo",
                        principalTable: "Customer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CustomerDetail",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CustomerId = table.Column<long>(type: "bigint", nullable: false),
                    FatherName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    MotherName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    EducationStatus = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    MontlyIncome = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Occupation = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    InsertUser = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    InsertDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CustomerDetail_Customer_CustomerId",
                        column: x => x.CustomerId,
                        principalSchema: "dbo",
                        principalTable: "Customer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CustomerPhone",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CustomerId = table.Column<long>(type: "bigint", nullable: false),
                    CountyCode = table.Column<string>(type: "character varying(3)", maxLength: 3, nullable: false),
                    Phone = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    IsDefault = table.Column<bool>(type: "boolean", nullable: false),
                    InsertUser = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    InsertDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerPhone", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CustomerPhone_Customer_CustomerId",
                        column: x => x.CustomerId,
                        principalSchema: "dbo",
                        principalTable: "Customer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "User",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CustomerId = table.Column<long>(type: "bigint", nullable: true),
                    UserName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Password = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Role = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    FirstName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    LastLoginDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    InsertUser = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    InsertDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                    table.ForeignKey(
                        name: "FK_User_Customer_CustomerId",
                        column: x => x.CustomerId,
                        principalSchema: "dbo",
                        principalTable: "Customer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AccountTransaction",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AccountId = table.Column<long>(type: "bigint", nullable: false),
                    ReferenceNumber = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    DebitAmount = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false, defaultValue: 0m),
                    CreditAmount = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false, defaultValue: 0m),
                    Description = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    TransactionDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    TransactionCode = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    InsertUser = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    InsertDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountTransaction", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AccountTransaction_Account_AccountId",
                        column: x => x.AccountId,
                        principalSchema: "dbo",
                        principalTable: "Account",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Account_CustomerId",
                schema: "dbo",
                table: "Account",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_AccountTransaction_AccountId",
                schema: "dbo",
                table: "AccountTransaction",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Country_CountyCode",
                schema: "dbo",
                table: "Country",
                column: "CountyCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Customer_CustomerNumber",
                schema: "dbo",
                table: "Customer",
                column: "CustomerNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Customer_Email",
                schema: "dbo",
                table: "Customer",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Customer_IdentityNumber",
                schema: "dbo",
                table: "Customer",
                column: "IdentityNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CustomerAddress_CustomerId",
                schema: "dbo",
                table: "CustomerAddress",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerDetail_CustomerId",
                schema: "dbo",
                table: "CustomerDetail",
                column: "CustomerId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CustomerPhone_CountyCode_Phone",
                schema: "dbo",
                table: "CustomerPhone",
                columns: new[] { "CountyCode", "Phone" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CustomerPhone_CustomerId",
                schema: "dbo",
                table: "CustomerPhone",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_User_CustomerId",
                schema: "dbo",
                table: "User",
                column: "CustomerId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_User_UserName",
                schema: "dbo",
                table: "User",
                column: "UserName",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccountTransaction",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Country",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "CustomerAddress",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "CustomerDetail",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "CustomerPhone",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "User",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Account",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Customer",
                schema: "dbo");
        }
    }
}
