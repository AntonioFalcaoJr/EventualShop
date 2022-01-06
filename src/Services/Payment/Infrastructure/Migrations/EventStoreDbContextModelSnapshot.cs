﻿// <auto-generated />
using System;
using Infrastructure.EventSourcing.EventStore.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Infrastructure.Migrations
{
    [DbContext(typeof(EventStoreDbContext))]
    partial class EventStoreDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseCollation("SQL_Latin1_General_CP1_CS_AS")
                .HasAnnotation("ProductVersion", "6.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Application.EventSourcing.EventStore.Events.PaymentSnapshot", b =>
                {
                    b.Property<int>("AggregateVersion")
                        .HasColumnType("int");

                    b.Property<Guid>("AggregateId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("AggregateName")
                        .IsRequired()
                        .HasMaxLength(30)
                        .IsUnicode(false)
                        .HasColumnType("varchar(30)");

                    b.Property<string>("AggregateState")
                        .IsRequired()
                        .HasMaxLength(2048)
                        .IsUnicode(false)
                        .HasColumnType("varchar(2048)");

                    b.HasKey("AggregateVersion", "AggregateId");

                    b.ToTable("PaymentSnapshots");
                });

            modelBuilder.Entity("Application.EventSourcing.EventStore.Events.PaymentStoreEvent", b =>
                {
                    b.Property<int>("Version")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Version"), 1L, 1);

                    b.Property<Guid>("AggregateId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("AggregateName")
                        .IsRequired()
                        .HasMaxLength(30)
                        .IsUnicode(false)
                        .HasColumnType("varchar(30)");

                    b.Property<string>("Event")
                        .IsRequired()
                        .HasMaxLength(2048)
                        .IsUnicode(false)
                        .HasColumnType("varchar(2048)");

                    b.Property<string>("EventName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)");

                    b.HasKey("Version");

                    b.ToTable("PaymentStoreEvents");
                });
#pragma warning restore 612, 618
        }
    }
}
