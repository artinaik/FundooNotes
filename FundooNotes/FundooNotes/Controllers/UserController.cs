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
                if(userBL.Registration(user))
                {
                    return this.Ok(new { Success = true, message = "Registration successfull" });
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
                if (userBL.Login(login))
                {
                    var tokenString = userBL.GenerateJwtToken(login.Email);
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
        public IActionResult ForgetPassword(UserLogin login)
        {
            try
            {
                return userBL.ForgetPassword(login.Email);
            }
            catch (Exception)
            {

                throw;
            }
        }


    }
}
