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
            modelBuilder.HasAnnotation("ProductVersion", "6.0.5");

            modelBuilder.Entity("Chat", b =>
                {
                    b.Property<string>("id")
                        .HasColumnType("TEXT");

                    b.Property<string>("user1Id")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("user2Id")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("id");

                    b.ToTable("chats");
                });

            modelBuilder.Entity("ChatUser", b =>
                {
                    b.Property<string>("userMessagesid")
                        .HasColumnType("TEXT");

                    b.Property<string>("usersuserId")
                        .HasColumnType("TEXT");

                    b.HasKey("userMessagesid", "usersuserId");

                    b.HasIndex("usersuserId");

                    b.ToTable("ChatUser");
                });

            modelBuilder.Entity("FileModel", b =>
                {
                    b.Property<string>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("contentType")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<byte[]>("data")
                        .IsRequired()
                        .HasColumnType("BLOB");

                    b.Property<long>("length")
                        .HasColumnType("INTEGER");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("id");

                    b.ToTable("FileModel");
                });

            modelBuilder.Entity("Img", b =>
                {
                    b.Property<string>("id")
                        .HasColumnType("TEXT");

                    b.Property<byte[]>("image")
                        .IsRequired()
                        .HasColumnType("BLOB");

                    b.HasKey("id");

                    b.ToTable("Img");
                });

            modelBuilder.Entity("Message", b =>
                {
                    b.Property<string>("MessageId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("chatId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("content")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("created")
                        .HasColumnType("TEXT");

                    b.Property<string>("formFileid")
                        .HasColumnType("TEXT");

                    b.Property<string>("fromId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<bool>("sent")
                        .HasColumnType("INTEGER");

                    b.Property<string>("toId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("MessageId");

                    b.HasIndex("chatId");

                    b.HasIndex("formFileid");

                    b.ToTable("messages");
                });

            modelBuilder.Entity("User", b =>
                {
                    b.Property<string>("userId")
                        .HasColumnType("TEXT");

                    b.Property<string>("fullName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("last")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("lastDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("nickName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("password")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("profileImgid")
                        .HasColumnType("TEXT");

                    b.Property<string>("server")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("userId");

                    b.HasIndex("profileImgid");

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

                    b.HasOne("FileModel", "formFile")
                        .WithMany()
                        .HasForeignKey("formFileid");

                    b.Navigation("formFile");
                });

            modelBuilder.Entity("User", b =>
                {
                    b.HasOne("Img", "profileImg")
                        .WithMany()
                        .HasForeignKey("profileImgid");

                    b.Navigation("profileImg");
                });

            modelBuilder.Entity("Chat", b =>
                {
                    b.Navigation("messages");
                });
#pragma warning restore 612, 618
        }
    }
}
