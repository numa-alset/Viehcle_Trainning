
using Microsoft.AspNetCore.Identity;

namespace VehicleApi.Persistance.Auth.User
{
   
        public class UserRegisterRequest
        {
            public string Email { get; set; } = "";
            public string Password { get; set; } = "";
            public string Nickname { get; set; } = "";
            public string Phone { get; set; } = "";
        }
        public partial class UserService
        {
            public async Task<AppResponse<bool>> UserRegisterAsync(UserRegisterRequest request)
            {
                var user = new UserInfo()
                {
                    UserName = request.Email,
                    Email = request.Email,
                    NickName = request.Nickname,
                    Phone = request.Phone,

                };
                var result = await _userManager.CreateAsync(user, request.Password);
                if (result.Succeeded)
                {
                     if (!await roleManager.RoleExistsAsync(UserRole.User))
                    await roleManager.CreateAsync(new IdentityRole(UserRole.User));

                     if (await roleManager.RoleExistsAsync(UserRole.User))
                        {
                            await userManager.AddToRoleAsync(user, UserRole.User);
                        }
                return new AppResponse<bool>().SetSuccessResponse(true);
                }
                else
                {
                    return new AppResponse<bool>().SetErrorResponse(GetRegisterErrors(result));
                }
            }

            private Dictionary<string, string[]> GetRegisterErrors(IdentityResult result)
            {
                var errorDictionary = new Dictionary<string, string[]>(1);

                foreach (var error in result.Errors)
                {
                    string[] newDescriptions;

                    if (errorDictionary.TryGetValue(error.Code, out var descriptions))
                    {
                        newDescriptions = new string[descriptions.Length + 1];
                        Array.Copy(descriptions, newDescriptions, descriptions.Length);
                        newDescriptions[descriptions.Length] = error.Description;
                    }
                    else
                    {
                        newDescriptions = [error.Description];
                    }

                    errorDictionary[error.Code] = newDescriptions;
                }

                return errorDictionary;
            }

        }
    }

