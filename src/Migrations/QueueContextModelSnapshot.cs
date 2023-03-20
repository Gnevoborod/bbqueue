﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using bbqueue.Database;

#nullable disable

namespace bbqueue.Migrations
{
    [DbContext(typeof(QueueContext))]
    partial class QueueContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("bbqueue.Database.Entities.EmployeeEntity", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("employee_id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<bool>("Active")
                        .HasColumnType("boolean")
                        .HasColumnName("active");

                    b.Property<string>("ExternalSystemIdentity")
                        .IsRequired()
                        .HasMaxLength(16)
                        .HasColumnType("character varying(16)")
                        .HasColumnName("external_system_id");

                    b.Property<string>("Name")
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("name");

                    b.Property<int>("Role")
                        .HasColumnType("integer")
                        .HasColumnName("role");

                    b.HasKey("Id");

                    b.ToTable("employee");
                });

            modelBuilder.Entity("bbqueue.Database.Entities.GroupEntity", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("group_id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("Description")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)")
                        .HasColumnName("description");

                    b.Property<long?>("GroupLinkId")
                        .HasColumnType("bigint")
                        .HasColumnName("group_link_id");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("character varying(64)")
                        .HasColumnName("name");

                    b.HasKey("Id");

                    b.HasIndex("GroupLinkId");

                    b.ToTable("group");
                });

            modelBuilder.Entity("bbqueue.Database.Entities.TargetEntity", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("target_id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("Description")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)")
                        .HasColumnName("description");

                    b.Property<long?>("GroupLinkId")
                        .HasColumnType("bigint")
                        .HasColumnName("group_link_id");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("character varying(64)")
                        .HasColumnName("name");

                    b.Property<char>("Prefix")
                        .HasColumnType("character(1)")
                        .HasColumnName("prefix");

                    b.HasKey("Id");

                    b.HasIndex("GroupLinkId");

                    b.HasIndex("Prefix")
                        .IsUnique();

                    b.ToTable("target");
                });

            modelBuilder.Entity("bbqueue.Database.Entities.TicketAmountEntity", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("ticket_amount_id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<int>("Number")
                        .HasColumnType("integer")
                        .HasColumnName("number");

                    b.Property<char>("Prefix")
                        .HasColumnType("character(1)")
                        .HasColumnName("prefix");

                    b.HasKey("Id");

                    b.HasIndex("Prefix")
                        .IsUnique();

                    b.ToTable("ticket_amount");
                });

            modelBuilder.Entity("bbqueue.Database.Entities.TicketEntity", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("ticket_id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<DateTime>("Closed")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("closed");

                    b.Property<DateTime>("Created")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created");

                    b.Property<int>("Number")
                        .HasColumnType("integer")
                        .HasColumnName("number");

                    b.Property<string>("PublicNumber")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("public_number");

                    b.Property<int>("State")
                        .HasColumnType("integer")
                        .HasColumnName("state");

                    b.Property<long>("TargetId")
                        .HasColumnType("bigint")
                        .HasColumnName("target_id");

                    b.HasKey("Id");

                    b.ToTable("ticket");
                });

            modelBuilder.Entity("bbqueue.Database.Entities.TicketOperationEntity", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("ticket_opearation_id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<long?>("EmployeeId")
                        .HasColumnType("bigint")
                        .HasColumnName("employee_id");

                    b.Property<DateTime>("Processed")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("processed");

                    b.Property<int>("State")
                        .HasColumnType("integer")
                        .HasColumnName("state");

                    b.Property<long?>("TargetId")
                        .HasColumnType("bigint")
                        .HasColumnName("target_id");

                    b.Property<long>("TicketId")
                        .HasColumnType("bigint")
                        .HasColumnName("ticket_id");

                    b.Property<long?>("WindowId")
                        .HasColumnType("bigint")
                        .HasColumnName("window_id");

                    b.HasKey("Id");

                    b.HasIndex("EmployeeId");

                    b.HasIndex("TicketId");

                    b.HasIndex("WindowId");

                    b.ToTable("ticket_operation");
                });

            modelBuilder.Entity("bbqueue.Database.Entities.WindowEntity", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("window_id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("Description")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)")
                        .HasColumnName("description");

                    b.Property<long?>("EmployeeId")
                        .HasColumnType("bigint")
                        .HasColumnName("employee_id");

                    b.Property<string>("Number")
                        .IsRequired()
                        .HasMaxLength(6)
                        .HasColumnType("character varying(6)")
                        .HasColumnName("number");

                    b.Property<int>("WindowWorkState")
                        .HasColumnType("integer")
                        .HasColumnName("window_work_state");

                    b.HasKey("Id");

                    b.HasIndex("EmployeeId");

                    b.ToTable("window");
                });

            modelBuilder.Entity("bbqueue.Database.Entities.WindowTargetEntity", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("window_target_id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<long>("TargetId")
                        .HasColumnType("bigint")
                        .HasColumnName("target_id");

                    b.Property<long>("WindowId")
                        .HasColumnType("bigint")
                        .HasColumnName("window_id");

                    b.HasKey("Id");

                    b.HasIndex("WindowId");

                    b.ToTable("window_target");
                });

            modelBuilder.Entity("bbqueue.Database.Entities.GroupEntity", b =>
                {
                    b.HasOne("bbqueue.Database.Entities.GroupEntity", "GroupLink")
                        .WithMany()
                        .HasForeignKey("GroupLinkId");

                    b.Navigation("GroupLink");
                });

            modelBuilder.Entity("bbqueue.Database.Entities.TargetEntity", b =>
                {
                    b.HasOne("bbqueue.Database.Entities.GroupEntity", "GroupLink")
                        .WithMany()
                        .HasForeignKey("GroupLinkId");

                    b.Navigation("GroupLink");
                });

            modelBuilder.Entity("bbqueue.Database.Entities.TicketOperationEntity", b =>
                {
                    b.HasOne("bbqueue.Database.Entities.EmployeeEntity", "Employee")
                        .WithMany()
                        .HasForeignKey("EmployeeId");

                    b.HasOne("bbqueue.Database.Entities.TicketEntity", "Ticket")
                        .WithMany()
                        .HasForeignKey("TicketId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("bbqueue.Database.Entities.WindowEntity", "Window")
                        .WithMany()
                        .HasForeignKey("WindowId");

                    b.Navigation("Employee");

                    b.Navigation("Ticket");

                    b.Navigation("Window");
                });

            modelBuilder.Entity("bbqueue.Database.Entities.WindowEntity", b =>
                {
                    b.HasOne("bbqueue.Database.Entities.EmployeeEntity", "Employee")
                        .WithMany()
                        .HasForeignKey("EmployeeId");

                    b.Navigation("Employee");
                });

            modelBuilder.Entity("bbqueue.Database.Entities.WindowTargetEntity", b =>
                {
                    b.HasOne("bbqueue.Database.Entities.WindowEntity", "Window")
                        .WithMany()
                        .HasForeignKey("WindowId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Window");
                });
#pragma warning restore 612, 618
        }
    }
}
