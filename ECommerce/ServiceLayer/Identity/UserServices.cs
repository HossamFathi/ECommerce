
using DTO;
using DTO.Constant;
using DTO.Entities.Identity;
using DTO.Entities.Identity.Login;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using ServiceLayer.Identity.Helper;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Identity
{
    public class UserServices : IUserServices
    {
        private IHttpContextAccessor _httpContextAccessor;
        private UserManager<IdentityUser> _userManger;
        private IConfiguration _config;
        private IServiceProvider _services;

       
        public UserServices(

            UserManager<IdentityUser> userManger,
            IConfiguration config,
            IServiceProvider serviceProvider


            )
        {

            _userManger = userManger;
            _config = config;
            _services = serviceProvider;



        }

        #region  User Data Services
        [Authorize(Roles = Roles.Admin)]
       
        public string GetCurrentUserID()
        {
            _httpContextAccessor = GetHttpContextAccessor();
            return _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
        }
        public string GetCurrentUserName()
        {
            _httpContextAccessor = GetHttpContextAccessor();
            return _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.Name);
        }
        public string GetCurrentUserRole()
        {
            _httpContextAccessor = GetHttpContextAccessor();
            return _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.Role);
        }
        private IHttpContextAccessor GetHttpContextAccessor()
        {
            return _services.GetRequiredService<IHttpContextAccessor>();
        }



        #endregion

        public async Task<UserMangerResonse> RegiesterUserAsync(RegiesterViewModel model)
        {
            // if model not have value throw exception
            if (model is null)
                throw new NullReferenceException("Regiester model is null");

            // check that password and confirm pass match
            if (model.Password != model.ConfirmPassword)
                return new UserMangerResonse
                {
                    IsSuccess = false,
                    Message = "تأكيد كلمة المرور مختلفة عن كلمة المرور, تأكد من إدخالهم بشكل صحيح",
                };

            var RoleManager = _services.GetRequiredService<RoleManager<IdentityRole>>();


            IdentityResult roleResult;
            //Adding Admin Role
            var roleCheck = await RoleManager.RoleExistsAsync(Roles.Admin);
            if (!roleCheck)
            {
                //create the roles and seed them to the database
                roleResult = await RoleManager.CreateAsync(new IdentityRole(Roles.Admin));
            }

            IdentityUser user = await GetUser(model.Email);
            if (user is null)
            {
                var IdentityUser = new IdentityUser
                {
                    Email = model.Email,
                    UserName = model.Email 
                   
                };
                var Result = await _userManger.CreateAsync(IdentityUser, model.Password);
                if (!Result.Succeeded)
                    return new UserMangerResonse(
                    "Admin Not Addedd ", false);
            }

            var roles = await _userManger.GetRolesAsync(user);
            if (roles.Contains(Roles.Admin))
                return new UserMangerResonse(
                    "Admin Addedd Successfuly", true);
            await _userManger.AddToRoleAsync(user, Roles.Admin);
             return new UserMangerResonse(
                    "Admin Addedd Successfuly", true); ;

        }



        public async Task<UserMangerResonse> LoginUserAsync(LoginViewModel model)
        {
            if (model is null)
                throw new ArgumentNullException("Login model can't be null");

            var user = await GetUser(model.Email);
            // if not found return with errors and not vailed login
            if (user is null)
                return new UserMangerResonse("Invalid email or password ", false);


            if (!await IsValiedLogin(user, model.Password))
                return new UserMangerResonse("Invalid email or password ", false);

            //if(!user.EmailConfirmed)
            //    return new UserMangerResonse("Email Not Confirmed", false);

            
            // get user Role
            var Role = await GetRoleAsync(user);
            


            // Get Security Key From Setting
            var Key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(AuthSetting.Key));

            var AccessToken = GenerateAccessToken(user, Key, Role);
            // convert token to String 
            var TokenAsString = new JwtSecurityTokenHandler().WriteToken(AccessToken);
            return new UserMangerResonse(TokenAsString, true, AccessToken.ValidTo);

        }
        public async Task<UserMangerResonse> GoogleLoginUserAsync(GoogleLoginViewModel model)
        {
            if (model is null)
                throw new ArgumentNullException("Login model can't be null");

            var user = await GetUser(model.Email);
            // if not found return with errors and not vailed login
            if (user is null)
                return new UserMangerResonse("Invalid email or password ", false);

            //EmailData email = new EmailData
            //{
            //    EmailBody = EmailForm.Draw("Active Email", "Active Email"),
            //    EmailSubject = "Active Email",
            //    EmailToId = model.Email,
            //    Sender_EMail = "hosamfathi108@gmail.com"


            //};
            //await _emailSender.SendEmailAsync(email);
            //if(!user.EmailConfirmed)
            //    return new UserMangerResonse("Email Not Confirmed", false);

            // get user Role
            var Role = await GetRoleAsync(user);
            


            // Get Security Key From Setting
            var Key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(AuthSetting.Key));

            var AccessToken = GenerateAccessToken(user, Key, Role);
            // convert token to String 
            var TokenAsString = new JwtSecurityTokenHandler().WriteToken(AccessToken);
            return new UserMangerResonse(TokenAsString, true, AccessToken.ValidTo);

        }
       
        //public async Task<UserMangerResonse> GoogleRegiesterUserAsync(GoogleLoginViewModel model)
        //{
        //    // if model not have value throw exception
        //    if (model is null)
        //        throw new NullReferenceException("Regiester model is null");

        //    // create new student 
        //    //_Student = _services.GetRequiredService<IStudentAsync>();
        //    //var student = new AddedStudentViewModel(model.Name, null,null, 0,  1, model.Email , null , "");

        //    //var AddedStudnet = await _Student.Add(student);
        //    //if(AddedStudnet == null)
        //    //    return new UserMangerResonse("تم انشاء هذا الطالب من قبل", false);

        //    // create new User Identity
        //    var IdentityUser = new ApplicationUser
        //    {
        //        Email = model.Email,
        //        UserName = model.Email,
        //        Name = model.Name, // make user name is email comapct with name
        //        LockoutEnabled = false


        //    };
        //    var Result = await _userManger.CreateAsync(IdentityUser);

        //    if (Result.Succeeded)
        //    {
        //        //var ConfigirationEmailToken = await _userManger.GenerateEmailConfirmationTokenAsync(IdentityUser);
        //        //var UserEncodingToken = Encoding.UTF8.GetBytes(ConfigirationEmailToken);
        //        //var validateEmailToken = WebEncoders.Base64UrlEncode(UserEncodingToken);

        //        //var Url = $"{_config["AppUrl"]}/api/auth/confirmEmail?userID={IdentityUser.Id}&token={validateEmailToken}";
        //        //await _emailSender.SendEmailAsync(model.Email, "Active Email", EmailForm.Draw("Active Email", "Active Email", Url));


        //        if (!await AssignToRoleStudent(IdentityUser))
        //        {

        //            return new UserMangerResonse("user is  created but not assign to role and student not created", false);
        //        }

        //        _Student = _services.GetRequiredService<IStudentAsync>();
        //        var Student = new AddedStudentViewModel(model.Name, null, null, 0, 1, model.Email, null, "");

        //        var AddedStudnet = await _Student.Add(Student);

        //        if (AddedStudnet == null)
        //            return new UserMangerResonse("Student is  not created ", false);
        //        var user = await _userManger.FindByEmailAsync(Student.Email);
        //        user.studentID = AddedStudnet.ID;
        //        user.LockoutEnabled =false;
        //        await _userManger.UpdateAsync(user);
        //        return new UserMangerResonse("user is  created successfuly", true);
        //    }

        //    return new UserMangerResonse
        //    {
        //        Message = "user is not created",
        //        IsSuccess = false,
        //        Errors = Result.Errors.Select(e => e.Description)
        //    };

        //}
        public async Task<UserMangerResonse> ConfirmEmailAsync(string userID, string Token)
        {

            var user = await _userManger.FindByIdAsync(userID);
            if (user == null)
                return new UserMangerResonse("User Not Found", false);

            var decodedToken = WebEncoders.Base64UrlDecode(Token);
            var normalToken = Encoding.UTF8.GetString(decodedToken);
            var result = await _userManger.ConfirmEmailAsync(user, normalToken);
            if (result.Succeeded)
                return new UserMangerResonse("Confirm Successfuly", true);

            var response = new UserMangerResonse("Confirm Successfuly", false);
            response.Errors = result.Errors.Select(e => e.Description);
            return response;

        }
        [Authorize]
        public async Task<UserMangerResonse> ForgetPasswordAsync(string email)
        {

            var user = await _userManger.FindByEmailAsync(email);
            if (user is null)
                return new UserMangerResonse("Not Find Email", false);
            var token = await _userManger.GeneratePasswordResetTokenAsync(user);
            var encodedToken = Encoding.UTF8.GetBytes(token);
            var ValidTaken = WebEncoders.Base64UrlEncode(encodedToken);

            ////var Url = $"{_config["AppUrl"]}/api/auth/ResetPassword?email={email}&token={ValidTaken}";
            //var Url = $"https://localhost:3000/ResetPassword?email={email}&token={ValidTaken}";
            //await _emailSender.SendEmailAsync(user.Email, "Forget Password", EmailForm.Draw("Forget Password", " Reset Password ", Url));

            return new UserMangerResonse("Reset Password Url has been send to the Email Successfuly", true);

        }

        [Authorize]
        public async Task<UserMangerResonse> ResetPasswordAsync(ResetPasswordViewModel model)
        {
            var user = await _userManger.FindByEmailAsync(model.Email);
            if (user == null)
                return new UserMangerResonse
                {
                    IsSuccess = false,
                    Message = "No user associated with email",
                };

            if (model.NewPassword != model.ConfirmPassword)
                return new UserMangerResonse
                {
                    IsSuccess = false,
                    Message = "Password doesn't match its confirmation",
                };

            var decodedToken = WebEncoders.Base64UrlDecode(model.Token);
            string normalToken = Encoding.UTF8.GetString(decodedToken);

            var result = await _userManger.ResetPasswordAsync(user, normalToken, model.NewPassword);

            if (result.Succeeded)
                return new UserMangerResonse
                {
                    Message = "Password has been reset successfully!",
                    IsSuccess = true,
                };

            return new UserMangerResonse
            {
                Message = "Something went wrong",
                IsSuccess = false,
                Errors = result.Errors.Select(e => e.Description),
            };
        }
        [Authorize]
        public async Task<UserMangerResonse> changePasswordAsync(ChangePasswordViewModel model) {
            var user = await _userManger.FindByIdAsync(model.ID);
            if (user is null)
                return new UserMangerResonse("not found user", false);
            var Result = await _userManger.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);
            if (Result.Succeeded)
                return new UserMangerResonse("user Password change successfuly ", true);

            var ResponseWithError = new UserMangerResonse("user Password not  change successfuly ", false);
            ResponseWithError.Errors = Result.Errors.Select(e => e.Description);
            return ResponseWithError;
        }

        #region Helper
        private async Task<IList<string>> GetRoleAsync(IdentityUser user)
        {

            return await _userManger.GetRolesAsync(user);


        }
        public async Task<IdentityUser> GetUser(string Email)
        {

            return await _userManger.FindByEmailAsync(Email);
        }
        private async Task<bool> IsValiedLogin(IdentityUser user, string password)
        {


            return await _userManger.CheckPasswordAsync(user, password);
        }
        private Claim[] GenerateClaims(IdentityUser user, IList<string> Roles)
        {
            

            List<Claim> Claims = new List<Claim>{
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.NameIdentifier , user.Id),
               
           };

            foreach (var role in Roles)
            {
                Claims.Add(new Claim(ClaimTypes.Role, role));
            }

            
            return Claims.ToArray();
        }
        private JwtSecurityToken GenerateAccessToken(IdentityUser user, SymmetricSecurityKey Key, IList<string> Role)
        {
            // Create Token 
            var Token = new JwtSecurityToken(
                 issuer: AuthSetting.Issuer,
                 audience: AuthSetting.Audience,
                 claims: GenerateClaims(user, Role),
                 expires: DateTime.Now.AddDays(15), // this acces will expire after 3 day
                signingCredentials: new SigningCredentials(Key, SecurityAlgorithms.HmacSha256)
                 );

            return Token;
        }

       
        public async Task<bool> Update(IdentityUser user)
        {
            var userUpdated = await _userManger.FindByIdAsync(user.Id);
            userUpdated.Email =  user.Email;
            var Result = await _userManger.UpdateAsync(userUpdated);
            return Result.Succeeded;
        }

        public async Task<UserMangerResonse> DeleteuserByEmail(string email)
        {
            var user = await _userManger.FindByEmailAsync(email);
            if (user is null)
                return new UserMangerResonse("Not Found", false);
            var result = await _userManger.DeleteAsync(user);
            if (result.Succeeded)
                return new UserMangerResonse("deleted", true);

            var res = new UserMangerResonse("Not deleted", false);
            res.Errors = result.Errors.Select(e => e.Description);
            return res;
        }



        #endregion

    }
}