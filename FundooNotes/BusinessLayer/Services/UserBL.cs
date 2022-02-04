using BusinessLayer.Interfaces;
using CommonLayer.Models;
using RepositoryLayer.Entites;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
    public class UserBL : IUserBL
    {
        IUserRL userRL;
        public UserBL(IUserRL userRL)
        {
            this.userRL = userRL;
        }
        public User Registration(UserRegistration user)
        {
            try
            {
                return userRL.Registration(user);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public string Login(UserLogin userLogin)
        {
            try
            {
                return userRL.Login(userLogin);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public string GenerateJwtToken(string email)
        {
            try
            {
                return userRL.GenerateJwtToken(email);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public string ForgetPassword(string email)
        {
            try
            {
                return userRL.ForgetPassword(email);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool ResetPassword(string email,string password, string confirmPassword)
        {
            try
            {
                return userRL.ResetPassword(email,password, confirmPassword);
            }
            catch (Exception)
            {

                throw;
            }
        }


    }
}
