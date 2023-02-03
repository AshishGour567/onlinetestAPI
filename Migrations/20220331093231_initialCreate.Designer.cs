﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using OnlineTest.Data;

#nullable disable

namespace OnlineTest.Migrations
{
    [DbContext(typeof(OnlineTestDbContext))]
    [Migration("20220331093231_initialCreate")]
    partial class initialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("OnlineTest.Models.Answer", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<string>("answer")
                        .HasColumnType("longtext");

                    b.Property<long>("createdBy")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("createdDate")
                        .HasColumnType("datetime(6)");

                    b.Property<long>("optionId")
                        .HasColumnType("bigint");

                    b.Property<long>("questionId")
                        .HasColumnType("bigint");

                    b.Property<long>("updatedBy")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("updatedDate")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.HasIndex("optionId");

                    b.HasIndex("questionId");

                    b.ToTable("Answers");
                });

            modelBuilder.Entity("OnlineTest.Models.Exam", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<long>("createdBy")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("createdDate")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("endDateTime")
                        .HasColumnType("datetime(6)");

                    b.Property<long>("examTypeId")
                        .HasColumnType("bigint");

                    b.Property<bool>("isPublic")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("name")
                        .HasColumnType("longtext");

                    b.Property<long>("orgId")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("startDateTime")
                        .HasColumnType("datetime(6)");

                    b.Property<long>("updatedBy")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("updatedDate")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.HasIndex("examTypeId");

                    b.ToTable("Exams");
                });

            modelBuilder.Entity("OnlineTest.Models.ExamType", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<long>("createdBy")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("createdDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("type")
                        .HasColumnType("longtext");

                    b.Property<long>("updatedBy")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("updatedDate")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.ToTable("ExamTypes");

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            createdBy = 0L,
                            createdDate = new DateTime(2022, 3, 31, 15, 2, 0, 0, DateTimeKind.Unspecified),
                            type = "Developer",
                            updatedBy = 0L,
                            updatedDate = new DateTime(2022, 3, 31, 15, 2, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 2L,
                            createdBy = 0L,
                            createdDate = new DateTime(2022, 3, 31, 15, 2, 0, 0, DateTimeKind.Unspecified),
                            type = "Freshers",
                            updatedBy = 0L,
                            updatedDate = new DateTime(2022, 3, 31, 15, 2, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 3L,
                            createdBy = 0L,
                            createdDate = new DateTime(2022, 3, 31, 15, 2, 0, 0, DateTimeKind.Unspecified),
                            type = "Demo",
                            updatedBy = 0L,
                            updatedDate = new DateTime(2022, 3, 31, 15, 2, 0, 0, DateTimeKind.Unspecified)
                        });
                });

            modelBuilder.Entity("OnlineTest.Models.Option", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<long>("createdBy")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("createdDate")
                        .HasColumnType("datetime(6)");

                    b.Property<bool>("isAnswer")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("option")
                        .HasColumnType("longtext");

                    b.Property<long>("questionId")
                        .HasColumnType("bigint");

                    b.Property<long>("updatedBy")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("updatedDate")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.HasIndex("questionId");

                    b.ToTable("Options");
                });

            modelBuilder.Entity("OnlineTest.Models.Question", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<long>("createdBy")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("createdDate")
                        .HasColumnType("datetime(6)");

                    b.Property<long>("examId")
                        .HasColumnType("bigint");

                    b.Property<string>("question")
                        .HasColumnType("longtext");

                    b.Property<long>("questionTypeId")
                        .HasColumnType("bigint");

                    b.Property<long>("updatedBy")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("updatedDate")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.HasIndex("examId");

                    b.HasIndex("questionTypeId");

                    b.ToTable("Questions");
                });

            modelBuilder.Entity("OnlineTest.Models.QuestionType", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<long>("createdBy")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("createdDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("type")
                        .HasColumnType("longtext");

                    b.Property<long>("updatedBy")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("updatedDate")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.ToTable("QuestionTypes");

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            createdBy = 0L,
                            createdDate = new DateTime(2022, 3, 31, 15, 2, 0, 0, DateTimeKind.Unspecified),
                            type = "Coding",
                            updatedBy = 0L,
                            updatedDate = new DateTime(2022, 3, 31, 15, 2, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 2L,
                            createdBy = 0L,
                            createdDate = new DateTime(2022, 3, 31, 15, 2, 0, 0, DateTimeKind.Unspecified),
                            type = "MCQs",
                            updatedBy = 0L,
                            updatedDate = new DateTime(2022, 3, 31, 15, 2, 0, 0, DateTimeKind.Unspecified)
                        });
                });

            modelBuilder.Entity("OnlineTest.Models.Answer", b =>
                {
                    b.HasOne("OnlineTest.Models.Option", "option")
                        .WithMany()
                        .HasForeignKey("optionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("OnlineTest.Models.Question", "question")
                        .WithMany()
                        .HasForeignKey("questionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("option");

                    b.Navigation("question");
                });

            modelBuilder.Entity("OnlineTest.Models.Exam", b =>
                {
                    b.HasOne("OnlineTest.Models.ExamType", "examType")
                        .WithMany()
                        .HasForeignKey("examTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("examType");
                });

            modelBuilder.Entity("OnlineTest.Models.Option", b =>
                {
                    b.HasOne("OnlineTest.Models.Question", "question")
                        .WithMany("options")
                        .HasForeignKey("questionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("question");
                });

            modelBuilder.Entity("OnlineTest.Models.Question", b =>
                {
                    b.HasOne("OnlineTest.Models.Exam", "exam")
                        .WithMany()
                        .HasForeignKey("examId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("OnlineTest.Models.QuestionType", "questionType")
                        .WithMany()
                        .HasForeignKey("questionTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("exam");

                    b.Navigation("questionType");
                });

            modelBuilder.Entity("OnlineTest.Models.Question", b =>
                {
                    b.Navigation("options");
                });
#pragma warning restore 612, 618
        }
    }
}
