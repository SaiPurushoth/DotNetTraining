﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MutualFundManagement.Models;

#nullable disable

namespace MutualFundManagement.Migrations
{
    [DbContext(typeof(MutualFundDbContext))]
    [Migration("20220723063038_fourth DbUpdation")]
    partial class fourthDbUpdation
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("MutualFundManagement.Models.CustomerFunds", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("CustomerId")
                        .HasColumnType("int");

                    b.Property<int>("FundId")
                        .HasColumnType("int");

                    b.Property<float>("InvestedAmount")
                        .HasColumnType("float");

                    b.Property<float>("InvestedUnits")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.ToTable("CustomerFunds");
                });

            modelBuilder.Entity("MutualFundManagement.Models.Customers", b =>
                {
                    b.Property<int>("CustomerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("CustomerName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<bool>("IsAdmin")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("CustomerId");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("MutualFundManagement.Models.MutualFundBank", b =>
                {
                    b.Property<int>("FundId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("FundName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<float>("NAV")
                        .HasColumnType("float");

                    b.Property<float>("TotalInvestment")
                        .HasColumnType("float");

                    b.Property<float>("TotalUnits")
                        .HasColumnType("float");

                    b.HasKey("FundId");

                    b.ToTable("MutualFundBanks");
                });
#pragma warning restore 612, 618
        }
    }
}
