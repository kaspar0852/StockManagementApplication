using api.Dtos;
using api.Dtos.Comments;
using api.Extensions;
using api.Interfaces;
using api.Mappers;
using api.Repositories.Comments;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers;

[Route("api/Comments")]
[ApiController]
public class CommentController : ControllerBase
{
    private readonly ICommentRepository _commentRepository;
    private readonly IStockRepository _stockRepository;

    public CommentController(ICommentRepository commentRepository, 
        IStockRepository stockRepository)
    {
        _commentRepository = commentRepository;
        _stockRepository = stockRepository;
    }
    
    [HttpGet]
    public async Task<ResponseDto<List<CommentDto>>> GetAllComments()
    {
        try
        {
            var comments = await _commentRepository.GetAllCommentsAsync();

            var commentDto = comments.Select(x => x.ToCommentDto()).ToList();
                
            return this.SendSuccess(data: commentDto, message: "Success");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    [HttpGet("{id}")]
    public async Task<ResponseDto<CommentDto>> GetCommentById(int id)
    {
        try
        {
            var comment = await _commentRepository.GetCommentByIdAsync(id);

            var commentDto = comment.ToCommentDto();
                
            return this.SendSuccess(data: commentDto, message: "Success");

        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    [HttpPost]
    public async Task<ResponseDto<string>> CreateCommentForStock(CreateCommentDto input)
    {
        try
        {
            var stockExist = await _stockRepository.StockExists(input.StockId);

            if (!stockExist)
            {
                return this.SendFailure<string>(null, "Failed");
            }

            var comment = input.CreateCommentRequestDto();


            await _commentRepository.CreateCommentForStockAsync(comment);

            return this.SendSuccess<string>("Created successfully", "Created Success");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    

    [HttpPut]
    [Route("{id}")]
    public async Task<ResponseDto<CommentDto>> UpdateCommentForStock(int id, UpdateCommentDto input)
    {
        try
        {
            var comment = await _commentRepository.GetCommentByIdAsync(id);
            
            comment.Title = input.Title;
            comment.Content = input.Content;

            var updatedComment = await _commentRepository.UpdateCommentForStockAsync(comment);

            var result = updatedComment.ToCommentDto();

            return this.SendSuccess(result, "Success");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    [HttpDelete]
    [Route("{id}")]
    public async Task<ResponseDto<string>> DeleteCommentForStock(int id)
    {
        try
        {
            var comment = await _commentRepository.GetCommentByIdAsync(id);
        
            await _commentRepository.DeleteCommentForStockAsync(comment);

            return this.SendSuccess("Comment deleted", "Success");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

}