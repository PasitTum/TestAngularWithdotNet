using Microsoft.EntityFrameworkCore;
using TestTCCBackEnd.Data;
using TestTCCBackEnd.DTOs;
using TestTCCBackEnd.Models;

namespace TestTCCBackEnd.Services;

public class PostService : IPostService
{
    private readonly AppDbContext _db;

    public PostService(AppDbContext db) => _db = db;

    public async Task<PostResponse?> GetPostWithCommentsAsync(int postId)
    {
        var post = await _db.Posts
            .Include(p => p.Comments.OrderBy(c => c.CreatedAt))
            .FirstOrDefaultAsync(p => p.Id == postId);

        return post is null ? null : ToPostResponse(post);
    }

    public async Task<CommentResponse> AddCommentAsync(int postId, CommentRequest request)
    {
        var postExists = await _db.Posts.AnyAsync(p => p.Id == postId);
        if (!postExists)
            throw new KeyNotFoundException($"ไม่พบ Post ID {postId}");

        var comment = new Comment
        {
            PostId      = postId,
            Username    = request.Username,
            CommentText = request.CommentText,
            CreatedAt   = DateTime.Now
        };

        _db.Comments.Add(comment);
        await _db.SaveChangesAsync();

        return ToCommentResponse(comment);
    }

    private static PostResponse ToPostResponse(Post p) => new()
    {
        Id          = p.Id,
        Username    = p.Username,
        ImageBase64 = p.ImageBase64,
        Content     = p.Content,
        CreatedAt   = p.CreatedAt,
        Comments    = p.Comments.Select(ToCommentResponse).ToList()
    };

    private static CommentResponse ToCommentResponse(Comment c) => new()
    {
        Id          = c.Id,
        PostId      = c.PostId,
        Username    = c.Username,
        CommentText = c.CommentText,
        CreatedAt   = c.CreatedAt
    };
}
