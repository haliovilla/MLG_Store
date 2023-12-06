﻿// <auto-generated />
using System;
using MLGStore.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace MLGStore.Data.Migrations
{
    [DbContext(typeof(StoreDbContext))]
    partial class StoreDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.25")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("MLGStore.Entities.Article", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"), 1L, 1);

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Image")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("Stock")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Articles");
                });

            modelBuilder.Entity("MLGStore.Entities.ArticleStore", b =>
                {
                    b.Property<long>("ArticleId")
                        .HasColumnType("bigint");

                    b.Property<long>("StoreId")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.HasKey("ArticleId", "StoreId");

                    b.HasIndex("StoreId");

                    b.ToTable("ArticlesStores");
                });

            modelBuilder.Entity("MLGStore.Entities.Customer", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"), 1L, 1);

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Surnames")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("MLGStore.Entities.CustomerArticle", b =>
                {
                    b.Property<long>("CustomerId")
                        .HasColumnType("bigint");

                    b.Property<long>("ArticleId")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.HasKey("CustomerId", "ArticleId");

                    b.HasIndex("ArticleId");

                    b.ToTable("CustomersArticles");
                });

            modelBuilder.Entity("MLGStore.Entities.Store", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"), 1L, 1);

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Branch")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Stores");
                });

            modelBuilder.Entity("MLGStore.Entities.ArticleStore", b =>
                {
                    b.HasOne("MLGStore.Entities.Article", null)
                        .WithMany("ArticlesStores")
                        .HasForeignKey("ArticleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MLGStore.Entities.Store", null)
                        .WithMany("ArticlesStores")
                        .HasForeignKey("StoreId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("MLGStore.Entities.CustomerArticle", b =>
                {
                    b.HasOne("MLGStore.Entities.Article", null)
                        .WithMany("CustomersArticles")
                        .HasForeignKey("ArticleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MLGStore.Entities.Customer", null)
                        .WithMany("CustomersArticles")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("MLGStore.Entities.Article", b =>
                {
                    b.Navigation("ArticlesStores");

                    b.Navigation("CustomersArticles");
                });

            modelBuilder.Entity("MLGStore.Entities.Customer", b =>
                {
                    b.Navigation("CustomersArticles");
                });

            modelBuilder.Entity("MLGStore.Entities.Store", b =>
                {
                    b.Navigation("ArticlesStores");
                });
#pragma warning restore 612, 618
        }
    }
}
