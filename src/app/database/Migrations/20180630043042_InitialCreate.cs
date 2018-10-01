using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Sans.CreditUnion.Database.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AccountTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BrokerageStocks",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Ticker = table.Column<string>(nullable: true),
                    CompanyName = table.Column<string>(nullable: true),
                    PreviousDayClose = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BrokerageStocks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CreditUnionRoles",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CreditUnionRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Members",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Guid = table.Column<string>(nullable: true),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    Street = table.Column<string>(nullable: true),
                    City = table.Column<string>(nullable: true),
                    State = table.Column<string>(nullable: true),
                    ZipCode = table.Column<string>(nullable: true),
                    TravelingAbroadStart = table.Column<DateTime>(nullable: true),
                    TravelingAbroadEnd = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Members", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Payees",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Guid = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Street1 = table.Column<string>(nullable: true),
                    Street2 = table.Column<string>(nullable: true),
                    City = table.Column<string>(nullable: true),
                    State = table.Column<string>(nullable: true),
                    Zip5 = table.Column<string>(nullable: true),
                    Zip4 = table.Column<string>(nullable: true),
                    MemberIdWhoAdded = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payees", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BrokerageStockPriceHistory",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Timestamp = table.Column<DateTime>(nullable: false),
                    Price = table.Column<decimal>(nullable: false),
                    StockId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BrokerageStockPriceHistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BrokerageStockPriceHistory_BrokerageStocks_StockId",
                        column: x => x.StockId,
                        principalTable: "BrokerageStocks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CreditUnionRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    RoleId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CreditUnionRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CreditUnionRoleClaims_CreditUnionRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "CreditUnionRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Guid = table.Column<string>(nullable: true),
                    AccountNumber = table.Column<string>(nullable: true),
                    Balance = table.Column<decimal>(nullable: false),
                    InterestRate = table.Column<decimal>(nullable: false),
                    MemberId = table.Column<long>(nullable: false),
                    AccountTypeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Accounts_AccountTypes_AccountTypeId",
                        column: x => x.AccountTypeId,
                        principalTable: "AccountTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Accounts_Members_MemberId",
                        column: x => x.MemberId,
                        principalTable: "Members",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BrokerageAccounts",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Guid = table.Column<string>(nullable: true),
                    MemberId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BrokerageAccounts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BrokerageAccounts_Members_MemberId",
                        column: x => x.MemberId,
                        principalTable: "Members",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CreditUnionUsers",
                columns: table => new
                {
                    AccessFailedCount = table.Column<int>(nullable: false),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    Id = table.Column<string>(nullable: false),
                    UserName = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    MemberId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CreditUnionUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CreditUnionUsers_Members_MemberId",
                        column: x => x.MemberId,
                        principalTable: "Members",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BillPays",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Guid = table.Column<string>(nullable: true),
                    MemberId = table.Column<long>(nullable: false),
                    PayeeId = table.Column<long>(nullable: false),
                    IsRecurring = table.Column<bool>(nullable: false),
                    Frequency = table.Column<string>(nullable: true),
                    NextPaymentDate = table.Column<DateTime>(nullable: true),
                    LastPaymentDate = table.Column<DateTime>(nullable: true),
                    Amount = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BillPays", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BillPays_Members_MemberId",
                        column: x => x.MemberId,
                        principalTable: "Members",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BillPays_Payees_PayeeId",
                        column: x => x.PayeeId,
                        principalTable: "Payees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CheckOrders",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Guid = table.Column<string>(nullable: true),
                    AccountId = table.Column<long>(nullable: false),
                    OrderedDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CheckOrders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CheckOrders_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Transactions",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ReceivingAccountId = table.Column<long>(nullable: false),
                    TransactionDateTime = table.Column<DateTime>(nullable: false),
                    PostedDateTime = table.Column<DateTime>(nullable: true),
                    Amount = table.Column<decimal>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    SendingAccountId = table.Column<long>(nullable: false),
                    AccountId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transactions_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BrokerageTrades",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    TransactionDate = table.Column<DateTime>(nullable: false),
                    Quantity = table.Column<int>(nullable: false),
                    Price = table.Column<decimal>(nullable: false),
                    Guid = table.Column<string>(nullable: true),
                    PostedDateTime = table.Column<DateTime>(nullable: true),
                    BrokerageAccountId = table.Column<long>(nullable: false),
                    StockId = table.Column<long>(nullable: false),
                    AccountId = table.Column<long>(nullable: false),
                    BrokerageTradeType = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BrokerageTrades", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BrokerageTrades_BrokerageAccounts_BrokerageAccountId",
                        column: x => x.BrokerageAccountId,
                        principalTable: "BrokerageAccounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BrokerageTrades_BrokerageStocks_StockId",
                        column: x => x.StockId,
                        principalTable: "BrokerageStocks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CreditUnionUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CreditUnionUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CreditUnionUserClaims_CreditUnionUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "CreditUnionUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CreditUnionUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(nullable: false),
                    ProviderKey = table.Column<string>(nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CreditUnionUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_CreditUnionUserLogins_CreditUnionUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "CreditUnionUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CreditUnionUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    RoleId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CreditUnionUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_CreditUnionUserRoles_CreditUnionRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "CreditUnionRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CreditUnionUserRoles_CreditUnionUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "CreditUnionUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CreditUnionUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    LoginProvider = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CreditUnionUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_CreditUnionUserTokens_CreditUnionUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "CreditUnionUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_AccountTypeId",
                table: "Accounts",
                column: "AccountTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_MemberId",
                table: "Accounts",
                column: "MemberId");

            migrationBuilder.CreateIndex(
                name: "IX_BillPays_MemberId",
                table: "BillPays",
                column: "MemberId");

            migrationBuilder.CreateIndex(
                name: "IX_BillPays_PayeeId",
                table: "BillPays",
                column: "PayeeId");

            migrationBuilder.CreateIndex(
                name: "IX_BrokerageAccounts_MemberId",
                table: "BrokerageAccounts",
                column: "MemberId");

            migrationBuilder.CreateIndex(
                name: "IX_BrokerageStockPriceHistory_StockId",
                table: "BrokerageStockPriceHistory",
                column: "StockId");

            migrationBuilder.CreateIndex(
                name: "IX_BrokerageTrades_BrokerageAccountId",
                table: "BrokerageTrades",
                column: "BrokerageAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_BrokerageTrades_StockId",
                table: "BrokerageTrades",
                column: "StockId");

            migrationBuilder.CreateIndex(
                name: "IX_CheckOrders_AccountId",
                table: "CheckOrders",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_CreditUnionRoleClaims_RoleId",
                table: "CreditUnionRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "CreditUnionRoles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CreditUnionUserClaims_UserId",
                table: "CreditUnionUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_CreditUnionUserLogins_UserId",
                table: "CreditUnionUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_CreditUnionUserRoles_RoleId",
                table: "CreditUnionUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_CreditUnionUsers_MemberId",
                table: "CreditUnionUsers",
                column: "MemberId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "CreditUnionUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "CreditUnionUsers",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_AccountId",
                table: "Transactions",
                column: "AccountId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BillPays");

            migrationBuilder.DropTable(
                name: "BrokerageStockPriceHistory");

            migrationBuilder.DropTable(
                name: "BrokerageTrades");

            migrationBuilder.DropTable(
                name: "CheckOrders");

            migrationBuilder.DropTable(
                name: "CreditUnionRoleClaims");

            migrationBuilder.DropTable(
                name: "CreditUnionUserClaims");

            migrationBuilder.DropTable(
                name: "CreditUnionUserLogins");

            migrationBuilder.DropTable(
                name: "CreditUnionUserRoles");

            migrationBuilder.DropTable(
                name: "CreditUnionUserTokens");

            migrationBuilder.DropTable(
                name: "Transactions");

            migrationBuilder.DropTable(
                name: "Payees");

            migrationBuilder.DropTable(
                name: "BrokerageAccounts");

            migrationBuilder.DropTable(
                name: "BrokerageStocks");

            migrationBuilder.DropTable(
                name: "CreditUnionRoles");

            migrationBuilder.DropTable(
                name: "CreditUnionUsers");

            migrationBuilder.DropTable(
                name: "Accounts");

            migrationBuilder.DropTable(
                name: "AccountTypes");

            migrationBuilder.DropTable(
                name: "Members");
        }
    }
}
