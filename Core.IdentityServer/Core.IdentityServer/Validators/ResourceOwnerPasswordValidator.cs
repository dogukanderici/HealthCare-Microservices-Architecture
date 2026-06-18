using Duende.IdentityServer.Models;
using Duende.IdentityServer.Validation;
using Core.IdentityServer.Models;
using Microsoft.AspNetCore.Identity;

namespace Core.IdentityServer.Validators
{
    public class ResourceOwnerPasswordValidator : IResourceOwnerPasswordValidator
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public ResourceOwnerPasswordValidator(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            ApplicationUser user = await _userManager.FindByNameAsync(context.UserName);
            if (user != null)
            {
                bool check = await _userManager.CheckPasswordAsync(user, context.Password);
                if (check)
                {
                    context.Result = new GrantValidationResult(
                        subject: user.Id.ToString(),
                        authenticationMethod: "password"
                        );

                    return;
                }
            }

            context.Result = new GrantValidationResult(
                TokenRequestErrors.InvalidGrant, "Invalid username or password"
                );
        }
    }
}
