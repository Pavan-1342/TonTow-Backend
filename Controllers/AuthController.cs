using Azure.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Mail;
using System.Net;
using System.Security.Claims;
using System.Security.Cryptography;
using TonTow.API.Data;
using TonTow.API.Models;
using TonTow.API.Services.UserService;
using System;

namespace TonTow.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IUserService _userService;
        private readonly TonTowDbContext _tonTowDbContext;


        public AuthController(IConfiguration configuration, IUserService userService, TonTowDbContext tonTowDbContext)
        {
            _configuration = configuration;
            _userService = userService;
            _tonTowDbContext = tonTowDbContext;
        }

        //[HttpGet, Authorize]
        //public ActionResult<string> GetMe()
        //{
        //    var userName = _userService.GetMyName();
        //    return Ok(userName);
        //}

        [HttpPost("register")]
        public async Task<ActionResult<TonTowUser>> Register(LoginRequest request)
        {
            TonTowUser userLoginInfo = new TonTowUser();
            CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);

            userLoginInfo.Username = request.Username;
            userLoginInfo.PasswordHash = passwordHash;
            userLoginInfo.PasswordSalt = passwordSalt;
            userLoginInfo.Role = "A";
            userLoginInfo.EmailId = "TonTowAdmin@gmail.com";
            userLoginInfo.Phone = "";
            userLoginInfo.RefreshToken = CreateToken(userLoginInfo);
            var refreshToken = GenerateRefreshToken();
            SetRefreshToken(refreshToken);
            userLoginInfo.TokenCreated = refreshToken.Created;
            userLoginInfo.TokenExpires = refreshToken.Expires;
            await _tonTowDbContext.TonTowUser.AddAsync(userLoginInfo);
            await _tonTowDbContext.SaveChangesAsync();
            return Ok(request);
        }

        //[HttpPost("ChangePassword")]
        //public async Task<ActionResult<string>> ChangePassword(string oldPassword, string newPassword)
        //{
        //    if (string.IsNullOrEmpty(oldPassword))
        //    {
        //        return BadRequest("");
        //    }
        //    return Ok();
        //}
        [HttpPost("ChangePassword")]
        public async Task<ActionResult> ChangePassword(ChangePasswordRequest PasswordRequest)
        {
            bool blnStatus = false;
            var UserFound = _tonTowDbContext.TonTowUser.SingleOrDefault(
                  p => p.Username == PasswordRequest.Username
                  );
            if (UserFound == null)
            {
                return StatusCode(StatusCodes.Status404NotFound, "User Not Found");
            }
            if (!VerifyPasswordHash(PasswordRequest.OldPassword, UserFound.PasswordHash, UserFound.PasswordSalt))
            {
                return BadRequest("Old password does not match.");
            }
            if (string.Compare(PasswordRequest.NewPassword, PasswordRequest.ConfirmPassword) != 0)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "The new password and confirmed passwod doesn't match");
            }
            CreatePasswordHash(PasswordRequest.NewPassword, out byte[] passwordHash, out byte[] passwordSalt);
            UserFound.PasswordHash = passwordHash;
            UserFound.PasswordSalt = passwordSalt;
            _tonTowDbContext.TonTowUser.UpdateRange(UserFound);
            await _tonTowDbContext.SaveChangesAsync();
            return Ok(new { Result="Success" });;
        }
        [HttpPost("CreateUser")]
        public async Task<ActionResult<string>> CreateUser(UserCreationRequest request)
        {
            var UserFound = _tonTowDbContext.TonTowUser.SingleOrDefault(
                  p => p.Username == request.JobNum
                  );
            if (UserFound == null)
            {
                TonTowUser userLoginInfo = new TonTowUser();
                CreatePasswordHash(request.JobNum, out byte[] passwordHash, out byte[] passwordSalt);

                userLoginInfo.Username = request.JobNum;
                userLoginInfo.TonTowRptId = request.TonTowRptId;
                userLoginInfo.PasswordHash = passwordHash;
                userLoginInfo.PasswordSalt = passwordSalt;
                userLoginInfo.Role = "U";
                await _tonTowDbContext.TonTowUser.AddAsync(userLoginInfo);
                await _tonTowDbContext.SaveChangesAsync();
                return Ok(request);
            }
            return BadRequest("User Already Exist");
        }
        [HttpPost("login")]
        public async Task<ActionResult<string>> Login(LoginRequest request)
        {
            try
            {
                string token;
                TonTowUser userLoginInfo = new TonTowUser();
                LoginResponse loginResponce = new LoginResponse();
                var UserData = _tonTowDbContext.TonTowUser.SingleOrDefault(
                  p => p.Username == request.Username
                  );

                if (UserData==null || UserData.Username != request.Username)
                {
                    return BadRequest("User not found.");
                }
                else
                {
                    userLoginInfo.Username = UserData.Username;
                    userLoginInfo.Role = UserData.Role;
                    userLoginInfo.PasswordHash = UserData.PasswordHash;
                    userLoginInfo.PasswordSalt = UserData.PasswordSalt;

                    if (!VerifyPasswordHash(request.Password, userLoginInfo.PasswordHash, userLoginInfo.PasswordSalt))
                    {
                        return BadRequest("Wrong password.");
                    }

                    token = CreateToken(userLoginInfo);
                    var refreshToken = GenerateRefreshToken();
                    SetRefreshToken(refreshToken);
                    loginResponce.Id = UserData.UserId;
                    loginResponce.Role = UserData.Role;
                    loginResponce.Token = token;
                    loginResponce.Created = userLoginInfo.TokenCreated;
                    loginResponce.Expires = userLoginInfo.TokenExpires;
                    loginResponce.TonTowRptId = UserData.TonTowRptId;
                }
                return Ok(loginResponce);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return BadRequest(ex);
            }            
        }

        [HttpPost("refresh-token")]
        public async Task<ActionResult<string>> RefreshToken(TonTowUser userLoginInfo)
        {
            var refreshToken = Request.Cookies["refreshToken"];

            if (!userLoginInfo.RefreshToken.Equals(refreshToken))
            {
                return Unauthorized("Invalid Refresh Token.");
            }
            else if (userLoginInfo.TokenExpires < DateTime.Now)
            {
                return Unauthorized("Token expired.");
            }

            string token = CreateToken(userLoginInfo);
            var newRefreshToken = GenerateRefreshToken();
            SetRefreshToken(newRefreshToken);

            return Ok(token);
        }

        private RefreshToken GenerateRefreshToken()
        {
            var refreshToken = new RefreshToken
            {
                Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
                Expires = DateTime.Now.AddDays(7),
                Created = DateTime.Now
            };

            return refreshToken;
        }

        private void SetRefreshToken(RefreshToken newRefreshToken)
        {
            TonTowUser userLoginInfo = new TonTowUser();
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = newRefreshToken.Expires
            };
            Response.Cookies.Append("refreshToken", newRefreshToken.Token, cookieOptions);

            userLoginInfo.RefreshToken = newRefreshToken.Token;
            userLoginInfo.TokenCreated = newRefreshToken.Created;
            userLoginInfo.TokenExpires = newRefreshToken.Expires;
        }

        private string CreateToken(TonTowUser user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, user.Role)
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
                _configuration.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }

        [HttpGet("ForgetPassword")]
        public async Task<IActionResult> GetUserPassword(string userName)
        {
            try
            {
                var UserFound = _tonTowDbContext.TonTowUser.SingleOrDefault(
                  p => p.Username == userName
                  );
                if (UserFound == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, "User Not Found");
                }
                if(UserFound.Role=="A")
                {
                    return StatusCode(StatusCodes.Status404NotFound, "Please contact adminstrator for Admin users.");
                }
                if (UserFound.TonTowRptId != 0)
                {

                    var policeReportVehicle1 = _tonTowDbContext.PoliceReportVehicleDtls.SingleOrDefault(
                                p => p.TonTowRptId == UserFound.TonTowRptId && p.VehicleNo==1
                            );

                    var OprDob = policeReportVehicle1?.DOBAge;

                    if (OprDob == null)
                    {
                        return StatusCode(StatusCodes.Status404NotFound, "Dob Not Found");
                    }

                    if (OprDob != null)
                    {
                        var sPass = UserFound.Username + OprDob.ToString().Split(' ')[0].Replace("/", "");
                        CreatePasswordHash(sPass, out byte[] passwordHash, out byte[] passwordSalt);
                        UserFound.PasswordHash = passwordHash;
                        UserFound.PasswordSalt = passwordSalt;
                        _tonTowDbContext.TonTowUser.UpdateRange(UserFound);
                        int res = await _tonTowDbContext.SaveChangesAsync();
                        if (res > 0)
                        {
                            try
                            {

                                var host = _configuration.GetSection("MailSettings:Host").Value;
                                var mailId = _configuration.GetSection("MailSettings:MailId").Value;
                                var pwd = _configuration.GetSection("MailSettings:Pwd").Value;
                                int port = Convert.ToInt16(_configuration.GetSection("MailSettings:Port").Value);
                                SendEmail(mailId, UserFound.EmailId, sPass, pwd, host, Convert.ToUInt16(port));
                                //var smtpClient = new SmtpClient(host, port)
                                //{
                                //    UseDefaultCredentials = false,
                                //    EnableSsl = true,
                                //    Credentials = new NetworkCredential(mailId, pwd)
                                //};

                                //string msgBody = "Dear User" + Environment.NewLine
                                //    + "Your Password has beem reset to - " + sPass
                                //    + Environment.NewLine + "Kindly change the password after login"
                                //    + Environment.NewLine + Environment.NewLine
                                //    + "Admin";
                                //var mailMessage = new MailMessage
                                //{
                                //    From = new MailAddress(mailId),
                                //    Subject = "Password Reset",
                                //    Body = msgBody
                                //};
                                //mailMessage.To.Add(UserFound.EmailId);

                                //await smtpClient.SendMailAsync(mailMessage);

                                Console.WriteLine("Email sent successfully.");
                            }
                            catch (Exception ex)
                            {
                                return StatusCode(StatusCodes.Status400BadRequest, ex.Message);

                            }

                        }
                    }

                }
                return Ok(new { Result = "Success" });
            }
            catch (Exception)
            {

                throw;
            }
            
        }

        public static void SendEmail(string emailFromAddress,string emailToAddress, string changepassword,string emailpassword,string host,int portNumber)
        {
            try
            {               
                string msgBody = "Dear User" + Environment.NewLine
                                    + "Your Password has beem reset to - " + changepassword
                                    + Environment.NewLine + "Kindly change the password after login"
                                    + Environment.NewLine + Environment.NewLine
                                    + "Admin";
                using (MailMessage mail = new MailMessage())
                {
                    mail.From = new MailAddress(emailFromAddress);
                    mail.To.Add(emailToAddress);
                    mail.Subject = "Password Reset";
                    mail.Body = msgBody;
                    mail.IsBodyHtml = true;
                    using (SmtpClient smtp = new SmtpClient(host, portNumber))
                    {
                        smtp.Credentials = new NetworkCredential(emailFromAddress, emailpassword);
                        smtp.EnableSsl = true;
                        smtp.Send(mail);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
           
        }
    }
}
