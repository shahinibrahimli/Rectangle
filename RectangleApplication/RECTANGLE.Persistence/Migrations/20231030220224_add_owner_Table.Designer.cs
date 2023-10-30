﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Rectangle.Persistence;

#nullable disable

namespace Rectangle.Persistence.Migrations
{
    [DbContext(typeof(RectangleDbContext))]
    [Migration("20231030220224_add_owner_Table")]
    partial class add_owner_Table
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Rectangle.Domain.Entities.OwnedRectangle", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<Guid>("CreatedById")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("LastModifiedById")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("LastModifiedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("OwnedRectangle");
                });

            modelBuilder.Entity("Rectangle.Domain.Entities.ShapeRectangle", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<Guid>("CreatedById")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("LastModifiedById")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("LastModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("Length")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int?>("OwnedRectangleId")
                        .HasColumnType("int");

                    b.Property<decimal>("Width")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("OwnedRectangleId");

                    b.ToTable("Rectangles");
                });

            modelBuilder.Entity("Rectangle.Domain.Entities.ShapeRectangle", b =>
                {
                    b.HasOne("Rectangle.Domain.Entities.OwnedRectangle", "OwnedRectangle")
                        .WithMany("Rectangles")
                        .HasForeignKey("OwnedRectangleId");

                    b.Navigation("OwnedRectangle");
                });

            modelBuilder.Entity("Rectangle.Domain.Entities.OwnedRectangle", b =>
                {
                    b.Navigation("Rectangles");
                });
#pragma warning restore 612, 618
        }
    }
}