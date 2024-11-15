using Microsoft.AspNetCore.Routing;
using Microsoft.OpenApi.Models;
using BlogApi.Entities;
using BlogApi.Services;
using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Identity;
            

namespace BlogApi.Endpoints;

public static class PostsEndpoints
{
    public static void PostEndpoints(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapPost("/Posts", async (Post Post, IPostService _PostService) =>
        {
            await _PostService.AddPost(Post);
            return Results.Created($"{Post.Id}", Post);
        })
        .WithName("AddPost")
        .RequireAuthorization()
        .WithOpenApi(x => new Microsoft.OpenApi.Models.OpenApiOperation(x)
        {
            Summary = "Add Post",
            Description = "Add a new Post to the library",
            Tags = new List<OpenApiTag> { new OpenApiTag { Name = "Post" } }
        });

        endpoints.MapGet("/Posts", async (IPostService _PostService) =>
                   TypedResults.Ok(await _PostService.GetPosts()))
                   .WithName("GetPosts")
                   .WithOpenApi(x => new OpenApiOperation(x)
                   {
                       Summary = "Get All Posts",
                       Description = "Get all Posts from the library.",
                       Tags = new List<OpenApiTag> { new() { Name = "Post" } }
                   });

        endpoints.MapGet("/Posts/{id}", async (IPostService _PostService, int id) =>
        {
            var Post = await _PostService.GetPost(id);

            if (Post != null)
                return Results.Ok(Post);
            else
                return Results.NotFound();
        })
            .WithName("GetPostbyId")
            .RequireAuthorization()
            .WithOpenApi(x => new OpenApiOperation(x)
            {
                Summary = "Get Post By Id",
                Description = "Returns information about a Post.",
                Tags = new List<OpenApiTag> { new OpenApiTag { Name = "Post" } }
            });

        endpoints.MapDelete("/Posts/{id}", async (int id, IPostService _PostService) =>
        {
            await _PostService.DeletePost(id);
            return Results.Ok($"Post de id={id} deletado");
        })
        .WithName("DeletePostbyId")
        .WithOpenApi(x => new OpenApiOperation(x)
        {
            Summary = "Delete Post",
            Description = "Deletes a Post from the library.",
            Tags = new List<OpenApiTag> { new OpenApiTag { Name = "Post" } }
        });

        endpoints.MapPut("/Posts/{id}", async (int id, Post Post, IPostService _PostService) =>
        {
            if (Post is null)
                return Results.BadRequest("Dados inválidos");

            if (id != Post.Id)
                return Results.BadRequest();

            await _PostService.UpdatePost(Post);

            return Results.Ok(Post);
        })
        .WithName("UpdatePost")
        .WithOpenApi(x => new OpenApiOperation(x)
        {
            Summary = "Alter Post",
            Description = "Alter a Post from the Library",
            Tags = new List<OpenApiTag> { new OpenApiTag { Name = "Post" } }
        });
    }
}
