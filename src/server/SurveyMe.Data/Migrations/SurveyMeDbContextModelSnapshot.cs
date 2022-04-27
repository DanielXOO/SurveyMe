﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SurveyMe.Data;

#nullable disable

namespace SurveyMe.Data.Migrations
{
    [DbContext(typeof(SurveyMeDbContext))]
    partial class SurveyMeDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("RoleUser", b =>
                {
                    b.Property<Guid>("RolesId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("UsersId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("RolesId", "UsersId");

                    b.HasIndex("UsersId");

                    b.ToTable("RoleUser");
                });

            modelBuilder.Entity("SurveyMe.DomainModels.FileAnswer", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("FileInfoId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("QuestionAnswerId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("FileInfoId");

                    b.HasIndex("QuestionAnswerId")
                        .IsUnique();

                    b.ToTable("FileAnswer");
                });

            modelBuilder.Entity("SurveyMe.DomainModels.FileInfo", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ContentType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("FileInfo");
                });

            modelBuilder.Entity("SurveyMe.DomainModels.Question", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("SurveyId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("SurveyId");

                    b.ToTable("Question");
                });

            modelBuilder.Entity("SurveyMe.DomainModels.QuestionAnswer", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("FileAnswerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("QuestionId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<double>("RateAnswer")
                        .HasColumnType("float");

                    b.Property<double>("ScaleAnswer")
                        .HasColumnType("float");

                    b.Property<Guid>("SurveyAnswerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("TextAnswer")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("SurveyAnswerId");

                    b.ToTable("QuestionAnswer");
                });

            modelBuilder.Entity("SurveyMe.DomainModels.QuestionAnswerOption", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("QuestionAnswerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("QuestionOptionId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("QuestionAnswerId");

                    b.ToTable("QuestionAnswerOption");
                });

            modelBuilder.Entity("SurveyMe.DomainModels.QuestionOption", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("QuestionId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("QuestionId");

                    b.ToTable("QuestionOption");
                });

            modelBuilder.Entity("SurveyMe.DomainModels.Role", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("SurveyMe.DomainModels.Survey", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AuthorId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("UpdateDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("AuthorId");

                    b.ToTable("Survey");
                });

            modelBuilder.Entity("SurveyMe.DomainModels.SurveyAnswer", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("SurveyId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("SurveyId");

                    b.HasIndex("UserId");

                    b.ToTable("SurveyAnswer");
                });

            modelBuilder.Entity("SurveyMe.DomainModels.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreationTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("DisplayName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("RoleUser", b =>
                {
                    b.HasOne("SurveyMe.DomainModels.Role", null)
                        .WithMany()
                        .HasForeignKey("RolesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SurveyMe.DomainModels.User", null)
                        .WithMany()
                        .HasForeignKey("UsersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("SurveyMe.DomainModels.FileAnswer", b =>
                {
                    b.HasOne("SurveyMe.DomainModels.FileInfo", "FileInfo")
                        .WithMany()
                        .HasForeignKey("FileInfoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SurveyMe.DomainModels.QuestionAnswer", "QuestionAnswer")
                        .WithOne("FileAnswer")
                        .HasForeignKey("SurveyMe.DomainModels.FileAnswer", "QuestionAnswerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("FileInfo");

                    b.Navigation("QuestionAnswer");
                });

            modelBuilder.Entity("SurveyMe.DomainModels.Question", b =>
                {
                    b.HasOne("SurveyMe.DomainModels.Survey", "Survey")
                        .WithMany("Questions")
                        .HasForeignKey("SurveyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Survey");
                });

            modelBuilder.Entity("SurveyMe.DomainModels.QuestionAnswer", b =>
                {
                    b.HasOne("SurveyMe.DomainModels.SurveyAnswer", "SurveyAnswer")
                        .WithMany("QuestionAnswers")
                        .HasForeignKey("SurveyAnswerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("SurveyAnswer");
                });

            modelBuilder.Entity("SurveyMe.DomainModels.QuestionAnswerOption", b =>
                {
                    b.HasOne("SurveyMe.DomainModels.QuestionAnswer", "QuestionAnswer")
                        .WithMany("Options")
                        .HasForeignKey("QuestionAnswerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("QuestionAnswer");
                });

            modelBuilder.Entity("SurveyMe.DomainModels.QuestionOption", b =>
                {
                    b.HasOne("SurveyMe.DomainModels.Question", "Question")
                        .WithMany("Options")
                        .HasForeignKey("QuestionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Question");
                });

            modelBuilder.Entity("SurveyMe.DomainModels.Survey", b =>
                {
                    b.HasOne("SurveyMe.DomainModels.User", "Author")
                        .WithMany("Surveys")
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Author");
                });

            modelBuilder.Entity("SurveyMe.DomainModels.SurveyAnswer", b =>
                {
                    b.HasOne("SurveyMe.DomainModels.Survey", "Survey")
                        .WithMany("Answers")
                        .HasForeignKey("SurveyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SurveyMe.DomainModels.User", "User")
                        .WithMany("Answers")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Survey");

                    b.Navigation("User");
                });

            modelBuilder.Entity("SurveyMe.DomainModels.Question", b =>
                {
                    b.Navigation("Options");
                });

            modelBuilder.Entity("SurveyMe.DomainModels.QuestionAnswer", b =>
                {
                    b.Navigation("FileAnswer")
                        .IsRequired();

                    b.Navigation("Options");
                });

            modelBuilder.Entity("SurveyMe.DomainModels.Survey", b =>
                {
                    b.Navigation("Answers");

                    b.Navigation("Questions");
                });

            modelBuilder.Entity("SurveyMe.DomainModels.SurveyAnswer", b =>
                {
                    b.Navigation("QuestionAnswers");
                });

            modelBuilder.Entity("SurveyMe.DomainModels.User", b =>
                {
                    b.Navigation("Answers");

                    b.Navigation("Surveys");
                });
#pragma warning restore 612, 618
        }
    }
}
