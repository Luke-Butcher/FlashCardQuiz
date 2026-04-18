namespace FlashCardQuiz.Models;

public class QuizRun
{
    public int Id { get; set; }
    public int QuizId { get; set; }
    public DateTime CompletedAt { get; set; } = DateTime.UtcNow;
    public int Correct { get; set; }
    public int Wrong { get; set; }
    public int Skipped { get; set; }
    public int TotalCards { get; set; }
}
