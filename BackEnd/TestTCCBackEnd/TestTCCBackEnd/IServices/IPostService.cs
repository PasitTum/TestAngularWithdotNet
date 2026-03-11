using TestTCCBackEnd.DTOs;

namespace TestTCCBackEnd.Services;

public interface IPostService
{
    Task<PostResponse?> GetPostWithCommentsAsync(int postId);
    Task<CommentResponse> AddCommentAsync(int postId, CommentRequest request);
}
