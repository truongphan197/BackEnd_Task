using Application.Dto;
using Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace BackEnd_Task.EndPoints
{
    public static class UserEndpoint
    {
        public static void MapUserEndPoints(this WebApplication app)
        {
            app.MapGet("/users", async (IUserService userService) =>
            {
                var rs = await userService.GetAsync();
                return Results.Ok(rs);
            });
        }

        public static void MapUserCreateEndPoints(this WebApplication app)
        {
            app.MapPost("/users", async (IUserService userService,[FromBody] UserRegisterDto request) =>
            {
                var rs = await userService.CreateAsync(request);
                return Results.Ok(rs);
            });
        }

        public static void MapUserSignInEndPoints(this WebApplication app)
        {
            app.MapPost("/users/signin", async (IUserService userService, UserRequestDto request) =>
            {
                var rs = await userService.SignInAsync(request);
                return Results.Ok(rs);
            });
        }
    }
}
