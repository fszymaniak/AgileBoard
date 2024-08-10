﻿// <auto-generated />
using System;
using AgileBoard.Infrastructure.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace AgileBoard.Infrastructure.DAL.Migrations
{
    [DbContext(typeof(AgileBoardDbContext))]
    [Migration("20240711121748_DraftEpic")]
    partial class DraftEpic
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.20")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("AgileBoard.Core.Entities.Epic", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<DateTimeOffset?>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Epics");

                    b.HasDiscriminator<string>("Type").HasValue("Epic");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("AgileBoard.Core.Entities.DraftEpic", b =>
                {
                    b.HasBaseType("AgileBoard.Core.Entities.Epic");

                    b.HasDiscriminator().HasValue("DraftEpic");
                });

            modelBuilder.Entity("AgileBoard.Core.Entities.FinalEpic", b =>
                {
                    b.HasBaseType("AgileBoard.Core.Entities.Epic");

                    b.Property<string>("AcceptanceCriteria")
                        .HasColumnType("text");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<string>("Status")
                        .HasColumnType("text");

                    b.HasDiscriminator().HasValue("FinalEpic");
                });
#pragma warning restore 612, 618
        }
    }
}