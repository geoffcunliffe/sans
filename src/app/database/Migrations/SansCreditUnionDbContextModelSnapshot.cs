﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Sans.CreditUnion.Database.Context;

namespace Sans.CreditUnion.Database.Migrations
{
    [DbContext(typeof(SansCreditUnionDbContext))]
    partial class SansCreditUnionDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.1-rtm-30846")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Sans.CreditUnion.Database.Models.Account", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AccountNumber");

                    b.Property<int>("AccountTypeId");

                    b.Property<decimal>("Balance");

                    b.Property<string>("Guid");

                    b.Property<decimal>("InterestRate");

                    b.Property<long>("MemberId");

                    b.HasKey("Id");

                    b.HasIndex("AccountTypeId");

                    b.HasIndex("MemberId");

                    b.ToTable("Accounts");
                });

            modelBuilder.Entity("Sans.CreditUnion.Database.Models.AccountType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.HasKey("Id");

                    b.ToTable("AccountTypes");
                });

            modelBuilder.Entity("Sans.CreditUnion.Database.Models.BillPay", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<decimal>("Amount");

                    b.Property<string>("Frequency");

                    b.Property<string>("Guid");

                    b.Property<bool>("IsRecurring");

                    b.Property<DateTime?>("LastPaymentDate");

                    b.Property<long>("MemberId");

                    b.Property<DateTime?>("NextPaymentDate");

                    b.Property<long>("PayeeId");

                    b.HasKey("Id");

                    b.HasIndex("MemberId");

                    b.HasIndex("PayeeId");

                    b.ToTable("BillPays");
                });

            modelBuilder.Entity("Sans.CreditUnion.Database.Models.BrokerageAccount", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Guid");

                    b.Property<long>("MemberId");

                    b.HasKey("Id");

                    b.HasIndex("MemberId");

                    b.ToTable("BrokerageAccounts");
                });

            modelBuilder.Entity("Sans.CreditUnion.Database.Models.BrokerageStock", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CompanyName");

                    b.Property<decimal>("PreviousDayClose");

                    b.Property<string>("Ticker");

                    b.HasKey("Id");

                    b.ToTable("BrokerageStocks");
                });

            modelBuilder.Entity("Sans.CreditUnion.Database.Models.BrokerageStockPriceHistory", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<decimal>("Price");

                    b.Property<long>("StockId");

                    b.Property<DateTime>("Timestamp");

                    b.HasKey("Id");

                    b.HasIndex("StockId");

                    b.ToTable("BrokerageStockPriceHistory");
                });

            modelBuilder.Entity("Sans.CreditUnion.Database.Models.BrokerageTrade", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<long>("AccountId");

                    b.Property<long>("BrokerageAccountId");

                    b.Property<int>("BrokerageTradeType");

                    b.Property<string>("Guid");

                    b.Property<DateTime?>("PostedDateTime");

                    b.Property<decimal>("Price");

                    b.Property<int>("Quantity");

                    b.Property<long>("StockId");

                    b.Property<DateTime>("TransactionDate");

                    b.HasKey("Id");

                    b.HasIndex("BrokerageAccountId");

                    b.HasIndex("StockId");

                    b.ToTable("BrokerageTrades");
                });

            modelBuilder.Entity("Sans.CreditUnion.Database.Models.CheckOrder", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<long>("AccountId");

                    b.Property<string>("Guid");

                    b.Property<DateTime>("OrderedDate");

                    b.HasKey("Id");

                    b.HasIndex("AccountId");

                    b.ToTable("CheckOrders");
                });

            modelBuilder.Entity("Sans.CreditUnion.Database.Models.CreditUnionRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex");

                    b.ToTable("CreditUnionRoles");
                });

            modelBuilder.Entity("Sans.CreditUnion.Database.Models.CreditUnionRoleClaim", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("CreditUnionRoleClaims");
                });

            modelBuilder.Entity("Sans.CreditUnion.Database.Models.CreditUnionUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<long>("MemberId");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("MemberId")
                        .IsUnique();

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex");

                    b.ToTable("CreditUnionUsers");
                });

            modelBuilder.Entity("Sans.CreditUnion.Database.Models.CreditUnionUserClaim", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("CreditUnionUserClaims");
                });

            modelBuilder.Entity("Sans.CreditUnion.Database.Models.CreditUnionUserLogin", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("CreditUnionUserLogins");
                });

            modelBuilder.Entity("Sans.CreditUnion.Database.Models.CreditUnionUserRole", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("CreditUnionUserRoles");
                });

            modelBuilder.Entity("Sans.CreditUnion.Database.Models.CreditUnionUserToken", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("CreditUnionUserTokens");
                });

            modelBuilder.Entity("Sans.CreditUnion.Database.Models.Member", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("City");

                    b.Property<string>("FirstName");

                    b.Property<string>("Guid");

                    b.Property<string>("LastName");

                    b.Property<string>("State");

                    b.Property<string>("Street");

                    b.Property<DateTime?>("TravelingAbroadEnd");

                    b.Property<DateTime?>("TravelingAbroadStart");

                    b.Property<string>("ZipCode");

                    b.HasKey("Id");

                    b.ToTable("Members");
                });

            modelBuilder.Entity("Sans.CreditUnion.Database.Models.Payee", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("City");

                    b.Property<string>("Guid");

                    b.Property<long>("MemberIdWhoAdded");

                    b.Property<string>("Name");

                    b.Property<string>("State");

                    b.Property<string>("Street1");

                    b.Property<string>("Street2");

                    b.Property<string>("Zip4");

                    b.Property<string>("Zip5");

                    b.HasKey("Id");

                    b.ToTable("Payees");
                });

            modelBuilder.Entity("Sans.CreditUnion.Database.Models.Transaction", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<long?>("AccountId");

                    b.Property<decimal>("Amount");

                    b.Property<string>("Description");

                    b.Property<DateTime?>("PostedDateTime");

                    b.Property<long>("ReceivingAccountId");

                    b.Property<long>("SendingAccountId");

                    b.Property<DateTime>("TransactionDateTime");

                    b.HasKey("Id");

                    b.HasIndex("AccountId");

                    b.ToTable("Transactions");
                });

            modelBuilder.Entity("Sans.CreditUnion.Database.Models.Account", b =>
                {
                    b.HasOne("Sans.CreditUnion.Database.Models.AccountType", "AccountType")
                        .WithMany()
                        .HasForeignKey("AccountTypeId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Sans.CreditUnion.Database.Models.Member", "Member")
                        .WithMany("Accounts")
                        .HasForeignKey("MemberId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Sans.CreditUnion.Database.Models.BillPay", b =>
                {
                    b.HasOne("Sans.CreditUnion.Database.Models.Member", "Member")
                        .WithMany()
                        .HasForeignKey("MemberId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Sans.CreditUnion.Database.Models.Payee", "Payee")
                        .WithMany()
                        .HasForeignKey("PayeeId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Sans.CreditUnion.Database.Models.BrokerageAccount", b =>
                {
                    b.HasOne("Sans.CreditUnion.Database.Models.Member", "Member")
                        .WithMany()
                        .HasForeignKey("MemberId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Sans.CreditUnion.Database.Models.BrokerageStockPriceHistory", b =>
                {
                    b.HasOne("Sans.CreditUnion.Database.Models.BrokerageStock", "Stock")
                        .WithMany("PriceHistories")
                        .HasForeignKey("StockId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Sans.CreditUnion.Database.Models.BrokerageTrade", b =>
                {
                    b.HasOne("Sans.CreditUnion.Database.Models.BrokerageAccount", "BrokerageAccount")
                        .WithMany()
                        .HasForeignKey("BrokerageAccountId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Sans.CreditUnion.Database.Models.BrokerageStock", "Stock")
                        .WithMany()
                        .HasForeignKey("StockId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Sans.CreditUnion.Database.Models.CheckOrder", b =>
                {
                    b.HasOne("Sans.CreditUnion.Database.Models.Account", "Account")
                        .WithMany()
                        .HasForeignKey("AccountId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Sans.CreditUnion.Database.Models.CreditUnionRoleClaim", b =>
                {
                    b.HasOne("Sans.CreditUnion.Database.Models.CreditUnionRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Sans.CreditUnion.Database.Models.CreditUnionUser", b =>
                {
                    b.HasOne("Sans.CreditUnion.Database.Models.Member", "Member")
                        .WithOne("User")
                        .HasForeignKey("Sans.CreditUnion.Database.Models.CreditUnionUser", "MemberId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Sans.CreditUnion.Database.Models.CreditUnionUserClaim", b =>
                {
                    b.HasOne("Sans.CreditUnion.Database.Models.CreditUnionUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Sans.CreditUnion.Database.Models.CreditUnionUserLogin", b =>
                {
                    b.HasOne("Sans.CreditUnion.Database.Models.CreditUnionUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Sans.CreditUnion.Database.Models.CreditUnionUserRole", b =>
                {
                    b.HasOne("Sans.CreditUnion.Database.Models.CreditUnionRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Sans.CreditUnion.Database.Models.CreditUnionUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Sans.CreditUnion.Database.Models.CreditUnionUserToken", b =>
                {
                    b.HasOne("Sans.CreditUnion.Database.Models.CreditUnionUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Sans.CreditUnion.Database.Models.Transaction", b =>
                {
                    b.HasOne("Sans.CreditUnion.Database.Models.Account", "Account")
                        .WithMany("Transactions")
                        .HasForeignKey("AccountId");
                });
#pragma warning restore 612, 618
        }
    }
}
