using FlashCardQuiz.Data;
using FlashCardQuiz.Models;
using Microsoft.EntityFrameworkCore;

namespace FlashCardQuiz.Services;

public class QuizService
{
    private readonly AppDbContext _context;

    public QuizService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Quiz>> GetQuizzesAsync()
    {
        return await _context.Quizzes
            .Include(q => q.QuizCards)
            .Include(q => q.QuizRuns)
            .ToListAsync();
    }

    public async Task SaveQuizAsync(Quiz quiz)
    {
        _context.Quizzes.Add(quiz);
        await _context.SaveChangesAsync();
    }

    public async Task SaveRunAsync(QuizRun run)
    {
        _context.QuizRuns.Add(run);
        await _context.SaveChangesAsync();
    }
}
