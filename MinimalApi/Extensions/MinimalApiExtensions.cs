using Application.Abstractions;
using Application.Posts.Commands;
using DataAccess.Repositorys;
using DataAccess;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using MinimalApi.Abstractions;

namespace MinimalApi.Extensions;

public static class MinimalApiExtensions
{
    public static void RegisterServices(this WebApplicationBuilder builder)
    {
        var cs = builder.Configuration.GetConnectionString("Default");
        builder.Services.AddDbContext<SocialDbContext>(opt => opt.UseSqlServer(cs));
        builder.Services.AddScoped<IPostRepository, PostRepository>();
        builder.Services.AddMediatR(typeof(CreatePost));
    }

    public static void RegisterEndpointDefinitions(this WebApplication app)
    {
        var endpointDefinitions = typeof(Program).Assembly
            .GetTypes()
            .Where(t => t.IsAssignableTo(typeof(IEndpointDefinition))
            && !t.IsAbstract && !t.IsInterface)
            .Select(Activator.CreateInstance)
            .Cast<IEndpointDefinition>();

        foreach (var endpointDef in endpointDefinitions)
        {
            endpointDef.RegisterEndpoints(app);
        }
    }
}
