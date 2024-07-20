using api.Models;

namespace api.Interfaces;

public interface ICommentRepository
{
    Task<List<Comment>> GetAllCommentsAsync();
    Task<Comment> GetCommentByIdAsync(int id);
    Task CreateCommentForStockAsync(Comment comment);
    Task<Comment> UpdateCommentForStockAsync(Comment comment);

    Task DeleteCommentForStockAsync(Comment comment);

    Task<bool> CommentExistsAsync(int id);


}