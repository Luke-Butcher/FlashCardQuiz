using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlashCardQuiz.Models;
using FlashCardQuiz.Services;
using Xunit;

namespace FlashCardQuiz.Tests;

public class QuizServiceTests : TestBase
{
    [Fact]
    public async Task SaveQuiz_PersistsQuizAndCards()
    {
        // Arrange
        using var context = GetTestDbContext();
        var cardService = new CardService(context);
        var quizService = new QuizService(context);
        
        await cardService.AddCardAsync(new Card { Front = "Q1", Back = "A1" });
        await cardService.AddCardAsync(new Card { Front = "Q2", Back = "A2" });
        
        var quiz = new Quiz 
        { 
            Name = "Test Quiz",
            QuizCards = new List<QuizCard> 
            { 
                new QuizCard { CardId = 1 },
                new QuizCard { CardId = 2 }
            }
        };

        // Act
        await quizService.SaveQuizAsync(quiz);

        // Assert
        Assert.Equal(1, context.Quizzes.Count());
        Assert.Equal(2, context.QuizCards.Count());
    }

    [Fact]
    public async Task SaveRun_PersistsRun()
    {
        // Arrange
        using var context = GetTestDbContext();
        var quizService = new QuizService(context);
        
        var quiz = new Quiz { Name = "Run Test Quiz" };
        await quizService.SaveQuizAsync(quiz);
        
        var run = new QuizRun 
        { 
            QuizId = quiz.Id,
            Correct = 5,
            Wrong = 2,
            Skipped = 1,
            TotalCards = 8
        };

        // Act
        await quizService.SaveRunAsync(run);

        // Assert
        Assert.Equal(1, context.QuizRuns.Count());
        var savedRun = context.QuizRuns.First();
        Assert.Equal(quiz.Id, savedRun.QuizId);
        Assert.Equal(5, savedRun.Correct);
    }
}
