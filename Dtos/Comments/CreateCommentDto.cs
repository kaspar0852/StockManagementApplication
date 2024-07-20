namespace api.Dtos.Comments;

public class CreateCommentDto
{
    public string Title { get; set; } = string.Empty;
    
    public string Content { get; set; } = string.Empty;
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    public int StockId { get; set; }
}
