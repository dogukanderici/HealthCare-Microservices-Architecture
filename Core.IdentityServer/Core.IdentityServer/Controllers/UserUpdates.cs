using AutoMapper;
using Core.IdentityServer.Dtos.UserDtos;
using Core.IdentityServer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Core.IdentityServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserUpdates : BaseController
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;
        private readonly ILogger<UserUpdates> _logger;

        public UserUpdates(UserManager<ApplicationUser> userManager, IMapper mapper, ILogger<UserUpdates> logger)
        {
            _userManager = userManager;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpPut("{email}")]
        [Authorize(AuthenticationSchemes = "IdentityServerAccessToken", Roles = "Admin")]
        public async Task<IActionResult> UserUpdate(UserUpdateDto userUpdateDto)
        {
            try
            {
                string oldEmail = RouteData.Values["email"]?.ToString();

                if (string.IsNullOrEmpty(oldEmail))
                {
                    _logger.LogError("Message: No E-Mail data found for update process!");

                    return StatusCode(404, "No E-Mail data found for update process!");
                }

                ApplicationUser userCheck = await _userManager.FindByEmailAsync(oldEmail);

                if (userCheck == null)
                {
                    _logger.LogError($"Email: {oldEmail} Message: No user found for E-Mail!");

                    return StatusCode(404, "No user found for E-Mail!");
                }

                _mapper.Map(userUpdateDto, userCheck);

                IdentityResult updatedUser = await _userManager.UpdateAsync(userCheck);

                if (updatedUser.Succeeded)
                {
                    _logger.LogInformation($"Email: {oldEmail} Message: User updated successfully!");

                    return StatusCode(200, "User updated successfully!");
                }

                _logger.LogError($"Email: {oldEmail} Message: User updating is failed! Errors: {updatedUser.Errors?.ToList()}");

                return StatusCode(400, "User updating is failed!");
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occured while user updating. Message: {ex.Message}");

                return StatusCode(500, "An error occured while user updating!");
            }
        }
    }
}