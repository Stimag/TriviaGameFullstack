using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using TriviaGame.Shared.Models;

namespace TriviaGame.Server.Data;

public partial class DatabaseContext : DbContext
{
    public DatabaseContext()
    {
    }

    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
    {
    }

    public virtual DbSet<TriviaChoice> TriviaChoices { get; set; }

    public virtual DbSet<TriviaQuestion> TriviaQuestions { get; set; }

    public virtual DbSet<TriviaTopic> TriviaTopics { get; set; }

    public virtual DbSet<UserAccount> UserAccounts { get; set; }

    public virtual DbSet<UserLife> UserLives { get; set; }

    public virtual DbSet<UserScore> UserScores { get; set; }


    

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Server=.\\SQLEXPRESS;Database=Trivia Game Database;Trusted_Connection=true;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TriviaChoice>(entity =>
        {
            entity.HasKey(e => e.ChoiceId).HasName("PK__Quiz_Cho__33CAF83AD6E1CDDB");

            entity.ToTable("TriviaChoice");

            entity.Property(e => e.ChoiceId)
                .ValueGeneratedNever()
                .HasColumnName("choice_id");
            entity.Property(e => e.ChoiceText)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("choice_text");
            entity.Property(e => e.IsCorrect)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("is_correct");
            entity.Property(e => e.QuestionId).HasColumnName("question_id");

            entity.HasOne(d => d.Question).WithMany(p => p.TriviaChoices)
                .HasForeignKey(d => d.QuestionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Quiz_Choi__quest__59063A47");
        });

        modelBuilder.Entity<TriviaQuestion>(entity =>
        {
            entity.HasKey(e => e.QuestionId).HasName("PK__Quiz_Que__2EC2154918910693");

            entity.ToTable("TriviaQuestion");

            entity.Property(e => e.QuestionId)
                .ValueGeneratedNever()
                .HasColumnName("question_id");
            entity.Property(e => e.QuestionDifficulty).HasColumnName("question_difficulty");
            entity.Property(e => e.QuestionText)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("question_text");
            entity.Property(e => e.TopicId).HasColumnName("topic_id");

            entity.HasOne(d => d.Topic).WithMany(p => p.TriviaQuestions)
                .HasForeignKey(d => d.TopicId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Trivia_Question_Trivia_Topic");
        });

        modelBuilder.Entity<TriviaTopic>(entity =>
        {
            entity.HasKey(e => e.TopicId).HasName("PK__Quiz_Top__D5DAA3E9F254D5FC");

            entity.ToTable("TriviaTopic");

            entity.Property(e => e.TopicId)
                .ValueGeneratedNever()
                .HasColumnName("topic_id");
            entity.Property(e => e.TopicName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("topic_name");
        });

        modelBuilder.Entity<UserAccount>(entity =>
        {
            entity.HasKey(e => e.UserAccountId).HasName("PK__User_Acc__1918BBDA44280699");

            entity.ToTable("UserAccount");

            entity.Property(e => e.UserAccountId)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("user_account_id");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .HasColumnName("email");
            entity.Property(e => e.PasswordHash)
                .HasMaxLength(255)
                .HasColumnName("password_hash");
            entity.Property(e => e.Username)
                .HasMaxLength(255)
                .HasColumnName("username");
        });

        modelBuilder.Entity<UserLife>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("UserLife");

            entity.Property(e => e.RemainingLives).HasColumnName("remaining_lives");
            entity.Property(e => e.UserAccountId)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("user_account_id");

            entity.HasOne(d => d.UserAccount).WithMany()
                .HasForeignKey(d => d.UserAccountId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Lives__user_acco__5165187F");
        });

        modelBuilder.Entity<UserScore>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("UserScore");

            entity.Property(e => e.Highscore).HasColumnName("highscore");
            entity.Property(e => e.Score).HasColumnName("score");
            entity.Property(e => e.UserAccountId)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("user_account_id");

            entity.HasOne(d => d.UserAccount).WithMany()
                .HasForeignKey(d => d.UserAccountId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Score__user_acco__6FE99F9F");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
