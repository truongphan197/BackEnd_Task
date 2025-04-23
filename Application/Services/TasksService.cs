using Application.Dto;
using Domain.Entities;
using Application.Abstractions;
using Application.Common.AppResults;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace Application.Services
{
    public interface ITasksService
    {
        Task<AppResult<IEnumerable<Tasks>>> GetAsync(int order);
        Task<Tasks?> GetByIdAsync(Guid Id);
        Task<bool> CreateAsync(TaskRequestDto request);
        Task<bool> UpdateAsync(TaskRequestDto request);
        Task<bool> UpdateTaskStatusAsync(TaskUpdateStatusRequestDto request);
        Task<bool> DeleteAsync(Guid Id);
    }

    public class TasksService : ITasksService
    {
        public ITasksRepository _tasksRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public TasksService(ITasksRepository tasksRepository, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
        {
            _tasksRepository = tasksRepository;
            _unitOfWork = unitOfWork;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<AppResult<IEnumerable<Tasks>>> GetAsync(int order)
        {
            var userId = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
                return AppResult<IEnumerable<Tasks>>.Error("Authen");
            var rs = (await _tasksRepository.GetAsync()).Where(x => x.UserId == Guid.Parse(userId));

            rs = order == 1 ? rs.OrderByDescending(x => x.DueDate) : rs.OrderBy(x => x.DueDate);

            return AppResult<IEnumerable<Tasks>>.Success(rs);
        }

        public async Task<Tasks?> GetByIdAsync(Guid Id)
        {
            var task = await _tasksRepository.GetByIdAsync(Id);

            return task;
        }

        public async Task<bool> CreateAsync(TaskRequestDto request)
        {
            try
            {
                var userId = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                var task = new Tasks
                {
                    Title = request.Title,
                    Description = request.Description,
                    Status = request.Status,
                    UserId = Guid.Parse(userId),
                    DueDate = request.DueDate.ToUniversalTime(),
                    CreatedDate = DateTime.UtcNow,
                    CreatedById = Guid.Parse(userId),
                };
                await _tasksRepository.CreateAsync(task);

                var rsSaveChange = await _unitOfWork.SaveChangesAsync();

                return rsSaveChange;
            }
            catch (Exception ex)
            {
                return false;
                throw;
            }
        }

        public async Task<bool> UpdateAsync(TaskRequestDto request)
        {
            if (request.Id == null)
                return false;
            var userId = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var task = await _tasksRepository.GetByIdAsync(request.Id);
            if(task != null)
            {
                task.Title = request.Title;
                task.Description = request.Description;
                task.Status = request.Status;
                task.UserId = Guid.Parse(userId);
                task.DueDate = request.DueDate.ToUniversalTime();    

                await _tasksRepository.UpdateAsync(task);
                await _unitOfWork.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public async Task<bool> UpdateTaskStatusAsync(TaskUpdateStatusRequestDto request)
        {
            if (request.Id == null)
                return false;
            var userId = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var task = await _tasksRepository.GetByIdAsync(request.Id);
            if (task != null)
            {
                task.Status = request.Status;

                await _tasksRepository.UpdateAsync(task);

                await _unitOfWork.SaveChangesAsync();
                
                return true;
            }

            return false;
        }

        public async Task<bool> DeleteAsync(Guid Id)
        {
            var task = await _tasksRepository.GetByIdAsync(Id);
            if (task == null)
                return false;

            await _tasksRepository.DeleteAsync(task);
            var rsSaveChange = await _unitOfWork.SaveChangesAsync();

            return rsSaveChange;
        }
    }
}
