using FlashCardQuiz.Data;
using Microsoft.EntityFrameworkCore;

namespace FlashCardQuiz.Services;

public class StatService
{
    private readonly AppDbContext _context;

    public StatService(AppDbContext context)
    {
        _context = context;
    }

    public async Task RecordCorrectAsync(int cardId)
    {
        var stat = await _context.CardStats.FirstOrDefaultAsync(s => s.CardId == cardId);
        if (stat != null)
        {
            stat.Correct++;
            await _context.SaveChangesAsync();
        }
    }

    public async Task RecordWrongAsync(int cardId)
    {
        var stat = await _context.CardStats.FirstOrDefaultAsync(s => s.CardId == cardId);
        if (stat != null)
        {
            stat.Wrong++;
            await _context.SaveChangesAsync();
        }
    }

    public async Task RecordSkippedAsync(int cardId)
    {
        var stat = await _context.CardStats.FirstOrDefaultAsync(s => s.CardId == cardId);
        if (stat != null)
        {
            stat.Skipped++;
            await _context.SaveChangesAsync();
        }
    }
}
