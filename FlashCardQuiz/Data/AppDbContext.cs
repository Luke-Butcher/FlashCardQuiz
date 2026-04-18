using FlashCardQuiz.Models;
using Microsoft.EntityFrameworkCore;

namespace FlashCardQuiz.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Card> Cards { get; set; }
    public DbSet<CardStat> CardStats { get; set; }
    public DbSet<Quiz> Quizzes { get; set; }
    public DbSet<QuizCard> QuizCards { get; set; }
    public DbSet<QuizRun> QuizRuns { get; set; }
}
