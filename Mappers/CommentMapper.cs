using api.Dtos.Comments;
using api.Models;

namespace api.Mappers;

public static class CommentMapper
{
    public static CommentDto ToCommentDto(this Comment model)
    {
        return new CommentDto
        {
            Id = model.Id,
            Title = model.Title,
            Content = model.Content,
            CreatedAt = model.CreatedAt,
            StockId = model.StockId
        };
    }

    public static Comment CreateCommentRequestDto(this CreateCommentDto commentDto)
    {
        return new Comment
        {
            Title = commentDto.Title,
            Content = commentDto.Content,
            StockId = commentDto.StockId
        };
    }
}