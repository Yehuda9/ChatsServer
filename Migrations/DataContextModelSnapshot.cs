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

            modelBuilder.Entity("Chat", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("user1Id")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("user2Id")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("chats");
                });

            modelBuilder.Entity("ChatUser", b =>
                {
                    b.Property<int>("userMessagesId")
                        .HasColumnType("int");

                    b.Property<string>("usersuserId")
                        .HasColumnType("varchar(255)");

                    b.HasKey("userMessagesId", "usersuserId");

                    b.HasIndex("usersuserId");

                    b.ToTable("ChatUser");
                });

            modelBuilder.Entity("Message", b =>
                {
                    b.Property<string>("MessageId")
                        .HasColumnType("varchar(255)");

                    b.Property<int?>("ChatId")
                        .HasColumnType("int");

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

                    b.HasIndex("ChatId");

                    b.ToTable("Message");
                });

            modelBuilder.Entity("User", b =>
                {
                    b.Property<string>("userId")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("longtext");

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
                        .HasForeignKey("userMessagesId")
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
                        .HasForeignKey("ChatId");
                });

            modelBuilder.Entity("Chat", b =>
                {
                    b.Navigation("messages");
                });
#pragma warning restore 612, 618
        }
    }
}
