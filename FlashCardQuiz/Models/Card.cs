namespace FlashCardQuiz.Models;

public class Card
{
    public int Id { get; set; }
    public string Front { get; set; } = string.Empty;
    public string Back { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    public CardStat? CardStat { get; set; }
}
