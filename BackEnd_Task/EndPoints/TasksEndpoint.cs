using Application.Dto;
using Application.Services;
using Microsoft.AspNetCore.Authorization;

namespace BackEnd_Task.EndPoints
{
    public static class TasksEndPoint
    {
        public static void MapTasksEndPoints(this WebApplication app)
        {
            app.MapGet("/tasks", [Authorize(AuthenticationSchemes = "Bearer")] async ( ITasksService tasksService, int order) =>
            {
                var rs = await tasksService.GetAsync(order);
                return Results.Ok(rs);
            });
        }

        public static void MapTasksCreateEndPoints(this WebApplication app)
        {
            app.MapPost("/tasks", [Authorize(AuthenticationSchemes = "Bearer")] async (ITasksService tasksService, TaskRequestDto task) =>
            {
                var rs = await tasksService.CreateAsync(task);
                return Results.Ok(rs);
            });
        }

        public static void MapTasksUpdateEndPoints(this WebApplication app)
        {
            app.MapPut("/tasks", [Authorize(AuthenticationSchemes = "Bearer")] async (ITasksService tasksService, TaskRequestDto task) =>
            {
                var rs = await tasksService.UpdateAsync(task);
                return Results.Ok(rs);
            });
        }

        public static void MapTasksUpdateStatusEndPoints(this WebApplication app)
        {
            app.MapPut("/tasks/status", [Authorize(AuthenticationSchemes = "Bearer")] async (ITasksService tasksService, TaskUpdateStatusRequestDto task) =>
            {
                var rs = await tasksService.UpdateTaskStatusAsync(task);
                return Results.Ok(rs);
            });
        }

        public static void MapTasksDeleteEndPoints(this WebApplication app)
        {
            app.MapDelete("/tasks/{id}", [Authorize(AuthenticationSchemes = "Bearer")] async (ITasksService tasksService, Guid id) =>
            {
                var rs = await tasksService.DeleteAsync(id);
                return Results.Ok(rs);
            });
        }
    }
}
