﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Chats.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Contact", b =>
                {
                    b.Property<string>("id")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("UseridName")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("last")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("lastdate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("server")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("id");

                    b.HasIndex("UseridName");

                    b.ToTable("Contact");
                });

            modelBuilder.Entity("Message", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Contactid")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("content")
                        .HasColumnType("longtext");

                    b.Property<DateTime>("created")
                        .HasColumnType("datetime(6)");

                    b.Property<bool>("sent")
                        .HasColumnType("tinyint(1)");

                    b.HasKey("id");

                    b.HasIndex("Contactid");

                    b.ToTable("Message");
                });

            modelBuilder.Entity("User", b =>
                {
                    b.Property<string>("idName")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("nickName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("password")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("idName");

                    b.ToTable("users", (string)null);
                });

            modelBuilder.Entity("Contact", b =>
                {
                    b.HasOne("User", null)
                        .WithMany("contacts")
                        .HasForeignKey("UseridName");
                });

            modelBuilder.Entity("Message", b =>
                {
                    b.HasOne("Contact", null)
                        .WithMany("messages")
                        .HasForeignKey("Contactid");
                });

            modelBuilder.Entity("Contact", b =>
                {
                    b.Navigation("messages");
                });

            modelBuilder.Entity("User", b =>
                {
                    b.Navigation("contacts");
                });
#pragma warning restore 612, 618
        }
    }
}
