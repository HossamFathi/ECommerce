using DTO.Entities.Identity;
using DTO.Entities.Identity.Login;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceLayer.Identity.Helper
{
    public interface IUserServices
    {
        #region Services Data
        string GetCurrentUserID();
        string GetCurrentUserName();
        string GetCurrentUserRole();

        #endregion

        Task<UserMangerResonse> RegiesterUserAsync(RegiesterViewModel model);
        Task<UserMangerResonse> LoginUserAsync(LoginViewModel model);
        //Task<UserMangerResonse> GoogleLoginUserAsync(GoogleLoginViewModel model);
        //Task<UserMangerResonse> GoogleRegiesterUserAsync(GoogleLoginViewModel model);
        Task<bool> Update(IdentityUser user);
        Task<UserMangerResonse> ConfirmEmailAsync(string userID, string Token);

        Task<UserMangerResonse> DeleteuserByEmail(string email);

        Task<UserMangerResonse> ForgetPasswordAsync(string email);

        Task<UserMangerResonse> ResetPasswordAsync(ResetPasswordViewModel model);
        Task<UserMangerResonse> changePasswordAsync(ChangePasswordViewModel model);
        Task<IdentityUser> GetUser(string Email);
    }
}
