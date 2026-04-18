using FlashCardQuiz.Data;
using FlashCardQuiz.Models;
using Microsoft.EntityFrameworkCore;

namespace FlashCardQuiz.Services;

public class CardService
{
    private readonly AppDbContext _context;

    public CardService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Card>> GetAllCardsAsync()
    {
        return await _context.Cards.Include(c => c.CardStat).ToListAsync();
    }

    public async Task AddCardAsync(Card card)
    {
        card.CardStat = new CardStat();
        _context.Cards.Add(card);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteCardAsync(int cardId)
    {
        var card = await _context.Cards.FindAsync(cardId);
        if (card != null)
        {
            _context.Cards.Remove(card);
            await _context.SaveChangesAsync();
        }
    }

    public async Task ImportFromJsonAsync(string json)
    {
        try
        {
            var options = new System.Text.Json.JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var importedCards = System.Text.Json.JsonSerializer.Deserialize<List<Card>>(json, options);
            if (importedCards != null)
            {
                foreach (var card in importedCards)
                {
                    card.Id = 0; // Ensure new ID
                    card.CardStat = new CardStat();
                    _context.Cards.Add(card);
                }
                await _context.SaveChangesAsync();
            }
        }
        catch
        {
            // Simple error handling for now
        }
    }
}
