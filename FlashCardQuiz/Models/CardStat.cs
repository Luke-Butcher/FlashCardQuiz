namespace FlashCardQuiz.Models;

public class CardStat
{
    public int Id { get; set; }
    public int CardId { get; set; }
    public int Correct { get; set; }
    public int Wrong { get; set; }
    public int Skipped { get; set; }
    
    public Card Card { get; set; } = null!;
}
