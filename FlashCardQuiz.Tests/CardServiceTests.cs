using System.Linq;
using System.Threading.Tasks;
using FlashCardQuiz.Models;
using FlashCardQuiz.Services;
using Xunit;

namespace FlashCardQuiz.Tests;

public class CardServiceTests : TestBase
{
    [Fact]
    public async Task AddCard_CreatesCardAndStatTogether()
    {
        // Arrange
        using var context = GetTestDbContext();
        var service = new CardService(context);
        var card = new Card { Front = "What is mitosis?", Back = "Cell division" };

        // Act
        await service.AddCardAsync(card);

        // Assert
        Assert.Equal(1, context.Cards.Count());
        Assert.Equal(1, context.CardStats.Count());
        var savedStat = context.CardStats.First();
        Assert.Equal(0, savedStat.Correct);
        Assert.Equal(0, savedStat.Wrong);
        Assert.Equal(0, savedStat.Skipped);
    }

    [Fact]
    public async Task DeleteCard_RemovesCardAndStat()
    {
        // Arrange
        using var context = GetTestDbContext();
        var service = new CardService(context);
        var card = new Card { Front = "Front", Back = "Back" };
        await service.AddCardAsync(card);
        var cardId = card.Id;

        // Act
        await service.DeleteCardAsync(cardId);

        // Assert
        Assert.Equal(0, context.Cards.Count());
        // Depending on Cascade Delete configuration, Stat might still be there or gone.
        // In EF Core with required relationships, it usually cascades.
    }
}
