﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Chats.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20220513200341_b")]
    partial class b
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Chat", b =>
                {
                    b.Property<string>("id")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("user1Id")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("user2Id")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("id");

                    b.ToTable("chats");
                });

            modelBuilder.Entity("ChatUser", b =>
                {
                    b.Property<string>("userMessagesid")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("usersuserId")
                        .HasColumnType("varchar(255)");

                    b.HasKey("userMessagesid", "usersuserId");

                    b.HasIndex("usersuserId");

                    b.ToTable("ChatUser");
                });

            modelBuilder.Entity("Message", b =>
                {
                    b.Property<string>("MessageId")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("chatId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.Property<string>("content")
                        .HasColumnType("longtext");

                    b.Property<DateTime>("created")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("fromId")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<bool>("sent")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("toId")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("MessageId");

                    b.HasIndex("chatId");

                    b.ToTable("messages");
                });

            modelBuilder.Entity("User", b =>
                {
                    b.Property<string>("userId")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("fullName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("last")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("lastDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("nickName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("password")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("server")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("userId");

                    b.ToTable("users");
                });

            modelBuilder.Entity("ChatUser", b =>
                {
                    b.HasOne("Chat", null)
                        .WithMany()
                        .HasForeignKey("userMessagesid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("User", null)
                        .WithMany()
                        .HasForeignKey("usersuserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Message", b =>
                {
                    b.HasOne("Chat", null)
                        .WithMany("messages")
                        .HasForeignKey("chatId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Chat", b =>
                {
                    b.Navigation("messages");
                });
#pragma warning restore 612, 618
        }
    }
}
