using BusinessLayer.Interfaces;
using CommonLayer.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FundooNotes.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        public IUserBL userBL;
        public UserController(IUserBL userBL)
        {
            this.userBL = userBL;
        }
        [HttpPost]
        public IActionResult AddUser(UserRegistration user)
        {
            try
            {
                var result = userBL.Registration(user);
                if (result!=null)
                {
                    return this.Ok(new { success = true, message = "Registration successfull" ,Response = result });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "Registration Unsuccessfull" });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        
        [HttpPost]
        public IActionResult Login(UserLogin login)
        {
            try
            {
                string tokenString = userBL.Login(login);
                if (tokenString!=null)
                {
                    return Ok(new { Token = tokenString, Message = "Login successfull" });

                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "Invalid Username and Password" });
                }
            }
            catch (Exception)
            {
            
                throw;
            }
        }
       
        [HttpPost]
        public IActionResult ForgetPassword(string email)
        {
            try
            {
                string token = userBL.ForgetPassword(email);
                if(token!=null)
                {                  
                    return Ok(new { message = "Token sent succesfully.Please check your email for password reset" });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "Email not registered" });
                }


            }
            catch (Exception)
            {
                throw;
            }
        }
        [Authorize]
        [HttpPost]
        public IActionResult ResetPassword(string password,string confirmPassword)
        {
            try
            {
                var email = User.Claims.First(e => e.Type == "Email").Value;
                //var email1 = User.FindFirst(ClaimTypes.Email).Value.ToString();
                userBL.ResetPassword(email,password, confirmPassword);
                return Ok(new { message = "Password reset done succussfully" });
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
