namespace api.Models;

public class Comment
{
    public int Id { get; set; }

    public string Title { get; set; } = string.Empty;
    
    public string Content { get; set; } = string.Empty;
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public int? StockId { get; set; }
    
    //navigation property
    public Stock? Stock { get; set; }
    
}