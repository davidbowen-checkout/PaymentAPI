﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Payment.Gateway.Api.Database;
using Payment.Gateway.Api.Interface;

namespace Payment.Gateway.Api.Migrations
{
    [DbContext(typeof(PaymentsContext))]
    [Migration("20200802123029_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.6");

            modelBuilder.Entity("Payment.Gateway.Api.DTO.PaymentData", b =>
                {
                    b.Property<string>("PaymentId")
                        .HasColumnType("TEXT");

                    b.Property<int>("BankAccountNumber")
                        .HasColumnType("INTEGER");

                    b.Property<double>("PaymentValue")
                        .HasColumnType("REAL");

                    b.Property<int>("SortCode")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Status")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("TransactionDate")
                        .HasColumnType("TEXT");

                    b.HasKey("PaymentId");

                    b.ToTable("PaymentData");
                });
#pragma warning restore 612, 618
        }
    }
}
