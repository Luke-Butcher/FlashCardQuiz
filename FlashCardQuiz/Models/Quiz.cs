namespace FlashCardQuiz.Models;

public class Quiz
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    public List<QuizCard> QuizCards { get; set; } = new();
    public List<QuizRun> QuizRuns { get; set; } = new();
}
