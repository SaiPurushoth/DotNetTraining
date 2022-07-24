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
    [Migration("20220721112253_Secon DbUpdation")]
    partial class SeconDbUpdation
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("MutualFundManagement.Models.Customers", b =>
                {
                    b.Property<int>("CustomerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("CustomerName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("FundId")
                        .HasColumnType("int");

                    b.Property<float>("InvestedAmount")
                        .HasColumnType("float");

                    b.Property<float>("InvestedUnits")
                        .HasColumnType("float");

                    b.Property<int>("mutualFundFundId")
                        .HasColumnType("int");

                    b.HasKey("CustomerId");

                    b.HasIndex("mutualFundFundId");

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

            modelBuilder.Entity("MutualFundManagement.Models.Customers", b =>
                {
                    b.HasOne("MutualFundManagement.Models.MutualFundBank", "mutualFund")
                        .WithMany()
                        .HasForeignKey("mutualFundFundId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("mutualFund");
                });
#pragma warning restore 612, 618
        }
    }
}