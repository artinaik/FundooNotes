using CommonLayer.Models;
using RepositoryLayer.Entites;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interfaces
{
    public interface IUserRL
    {
        public User Registration(UserRegistration user);
        public string Login(UserLogin userLogin);
        public string GenerateJwtToken(string email);
        public string ForgetPassword(string email);
        public bool ResetPassword(string email, string password, string confirmPassword);
        public string ClaimTokenByID(long Id);
        public string EncryptPassword(string password);
        public string DecryptPassword(string encryptpwd);
    }
}
