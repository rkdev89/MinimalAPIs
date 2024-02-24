using Application.Abstractions;
using Application.Posts.Queries;
using Domain.Models;
using MediatR;

namespace Application.Posts.QueryHandlers;
public class GetPostByIdHandler : IRequestHandler<GetPostById, Post>
{
    private readonly IPostRepository _postsRepo;

    public GetPostByIdHandler(IPostRepository postsRepo)
    {
        _postsRepo = postsRepo;
    }

    public async Task<Post> Handle(GetPostById request, CancellationToken cancellationToken)
    {
        return await _postsRepo.GetPostById(request.PostId);
    }
}
