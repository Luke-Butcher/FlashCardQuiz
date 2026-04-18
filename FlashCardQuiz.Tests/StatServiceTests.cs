using System.Linq;
using System.Threading.Tasks;
using FlashCardQuiz.Models;
using FlashCardQuiz.Services;
using Xunit;

namespace FlashCardQuiz.Tests;

public class StatServiceTests : TestBase
{
    [Fact]
    public async Task RecordCorrect_IncrementsCorrectCount()
    {
        // Arrange
        using var context = GetTestDbContext();
        var cardService = new CardService(context);
        var statService = new StatService(context);
        
        var card = new Card { Front = "Q", Back = "A" };
        await cardService.AddCardAsync(card);

        // Act
        await statService.RecordCorrectAsync(card.Id);

        // Assert
        var stat = context.CardStats.First(s => s.CardId == card.Id);
        Assert.Equal(1, stat.Correct);
        Assert.Equal(0, stat.Wrong);
    }

    [Fact]
    public async Task RecordWrong_IncrementsWrongCount()
    {
        // Arrange
        using var context = GetTestDbContext();
        var cardService = new CardService(context);
        var statService = new StatService(context);
        
        var card = new Card { Front = "Q", Back = "A" };
        await cardService.AddCardAsync(card);

        // Act
        await statService.RecordWrongAsync(card.Id);

        // Assert
        var stat = context.CardStats.First(s => s.CardId == card.Id);
        Assert.Equal(0, stat.Correct);
        Assert.Equal(1, stat.Wrong);
    }
}
