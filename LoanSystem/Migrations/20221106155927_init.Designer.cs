﻿// <auto-generated />
using System;
using LoanSystem.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace LoanSystem.Migrations
{
    [DbContext(typeof(LoanDbContext))]
    [Migration("20221106155927_init")]
    partial class init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("LoanSystem.Models.Customer", b =>
                {
                    b.Property<int>("CustomerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CustomerId"), 1L, 1);

                    b.Property<long>("AadharNumber")
                        .HasColumnType("bigint");

                    b.Property<string>("CustomerName")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("EmailId")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("MobileNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.HasKey("CustomerId");

                    b.HasIndex("RoleId");

                    b.ToTable("Customer");
                });

            modelBuilder.Entity("LoanSystem.Models.Loan", b =>
                {
                    b.Property<int>("LoanId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("LoanId"), 1L, 1);

                    b.Property<int>("CustomerId")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateOfSanction")
                        .HasColumnType("datetime2");

                    b.Property<int>("LoanAmount")
                        .HasColumnType("int");

                    b.Property<int>("LoanTypeId")
                        .HasColumnType("int");

                    b.HasKey("LoanId");

                    b.HasIndex("CustomerId");

                    b.HasIndex("LoanTypeId");

                    b.ToTable("Loan");
                });

            modelBuilder.Entity("LoanSystem.Models.LoanDetails", b =>
                {
                    b.Property<int>("LoanDetailsId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("LoanDetailsId"), 1L, 1);

                    b.Property<int>("BalanceAmount")
                        .HasColumnType("int");

                    b.Property<int>("BalanceDuration")
                        .HasColumnType("int");

                    b.Property<int>("LoanId")
                        .HasColumnType("int");

                    b.Property<int>("PaidAmount")
                        .HasColumnType("int");

                    b.Property<int>("TotalPaidAmount")
                        .HasColumnType("int");

                    b.HasKey("LoanDetailsId");

                    b.HasIndex("LoanId");

                    b.ToTable("LoanDetails");
                });

            modelBuilder.Entity("LoanSystem.Models.LoanType", b =>
                {
                    b.Property<int>("LoanTypeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("LoanTypeId"), 1L, 1);

                    b.Property<int>("Duration")
                        .HasColumnType("int");

                    b.Property<string>("LoanName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("LoanTypeId");

                    b.ToTable("LoanType");
                });

            modelBuilder.Entity("LoanSystem.Models.Role", b =>
                {
                    b.Property<int>("RoleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RoleId"), 1L, 1);

                    b.Property<string>("RoleName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("RoleId");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("LoanSystem.Models.Customer", b =>
                {
                    b.HasOne("LoanSystem.Models.Role", "Roles")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Roles");
                });

            modelBuilder.Entity("LoanSystem.Models.Loan", b =>
                {
                    b.HasOne("LoanSystem.Models.Customer", "Customers")
                        .WithMany()
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LoanSystem.Models.LoanType", "LoanTypes")
                        .WithMany()
                        .HasForeignKey("LoanTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customers");

                    b.Navigation("LoanTypes");
                });

            modelBuilder.Entity("LoanSystem.Models.LoanDetails", b =>
                {
                    b.HasOne("LoanSystem.Models.Loan", "Loans")
                        .WithMany()
                        .HasForeignKey("LoanId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Loans");
                });
#pragma warning restore 612, 618
        }
    }
}
