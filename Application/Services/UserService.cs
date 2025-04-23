using Application.Abstractions;
using Application.Common.AppResults;
using Application.Dto;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace Application.Services
{
    public interface IUserService
    {
        Task<IEnumerable<ApplicationUser>> GetAsync();
        Task<bool> CreateAsync(UserRegisterDto request);
        Task<AppResult<string>> SignInAsync(UserRequestDto request);
    }

    public class UserService : IUserService
    {
        private readonly IJwtProvider _jwtProvider;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public UserService( SignInManager<ApplicationUser> signInManager, IJwtProvider jwtProvider, UserManager<ApplicationUser> userManager)
        {
            _signInManager = signInManager;
            _jwtProvider = jwtProvider;
            _userManager = userManager;
        }

        public async Task<IEnumerable<ApplicationUser>> GetAsync()
        {
            var rs = _userManager.Users.ToList();
            return rs;
        }

        public async Task<bool> CreateAsync(UserRegisterDto request)
        {
            var applicationUser = new ApplicationUser
            {
                UserName = request.UserName,
                Email = request.Email,
                PasswordHash = request.Password
            };
            var rs =await _userManager.CreateAsync(applicationUser, request.Password);
            
            return rs.Succeeded;
        }

        public async Task<AppResult<string>> SignInAsync(UserRequestDto request)
        {
            var user = await _userManager.FindByNameAsync(request.UserName).ConfigureAwait(false);

            if (user == null)
                return AppResult<string>.Error("User name or password not match!");

            var checkPass = await _userManager.CheckPasswordAsync(user, request.Password).ConfigureAwait(false);
            if (!checkPass)
                return AppResult<string>.Error("User name or password not match!");

            var token = _jwtProvider.Genereate(user!);

            return AppResult<string>.Success(token);
        }
    }
}
