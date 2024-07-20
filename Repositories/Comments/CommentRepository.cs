using api.Dtos.Comments;
using api.EntityFrameworkCore;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repositories.Comments;

public class CommentRepository : ICommentRepository
{
    private readonly ApplicationDbContext _dbContext;
    private readonly IStockRepository _stockRepository;

    public CommentRepository(ApplicationDbContext dbContext, IStockRepository stockRepository)
    {
        _dbContext = dbContext;
        _stockRepository = stockRepository;
    }
    
    
    public async Task<List<Comment>> GetAllCommentsAsync()
    {
        var comments = await _dbContext.Comments.ToListAsync();
        return comments;
    }

    public async Task<Comment> GetCommentByIdAsync(int id)
    {
        var comment = await _dbContext.Comments.FirstOrDefaultAsync(x => x.Id == id);

        if (comment == null)
        {
            throw new InvalidOperationException("Comment not found.");
        }
        
        return comment;
    }

    public async Task CreateCommentForStockAsync(Comment comment)
    {
        try
        {
            await _dbContext.Comments.AddAsync(comment);
            await _dbContext.SaveChangesAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<Comment> UpdateCommentForStockAsync(Comment comment)
    {
        try
        {
            _dbContext.Comments.Update(comment);
            await _dbContext.SaveChangesAsync();
            return comment;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task DeleteCommentForStockAsync(Comment comment)
    {
        try
        {
            _dbContext.Comments.Remove(comment);
            await _dbContext.SaveChangesAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<bool> CommentExistsAsync(int id)
    {
        return await _dbContext.Comments.AnyAsync(x => x.Id == id);
    }
}