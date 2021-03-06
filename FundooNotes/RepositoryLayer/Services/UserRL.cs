using CommonLayer.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RepositoryLayer.AppContext;
using RepositoryLayer.Entites;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace RepositoryLayer.Services
{
    public class UserRL:IUserRL
    {
        Context context;
       
        private readonly IConfiguration configuration;
        
        public UserRL(Context context, IConfiguration config)
        {
            this.context = context;//appcontext to for api
            this.configuration = config;//for startup file instance
        }
        /// <summary>
        /// This method add new user in application and save in databse
        /// </summary>
        /// <param name="user">It is object of model class for new user entry</param>
        /// <returns>It returns newly created user from database</returns>
        public User Registration(UserRegistration user)
        {
            try
            {
                User newuser = new User();
                newuser.FirstName = user.FirstName;
                newuser.LastName = user.LastName;
                newuser.Email = user.Email;
                newuser.Password = EncryptPassword(user.Password);
                context.Users.Add(newuser);
                int result = context.SaveChanges();
                if (result > 0)
                    return newuser;
                else
                    return null;

            }
            catch (Exception)
            {

                throw;
            }
        }
        /// <summary>
        /// this method converts the user given password into encoded format
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        public string EncryptPassword(string password)
        {
            try
            {
                byte[] encode = new byte[password.Length];
                encode = Encoding.UTF8.GetBytes(password);
                string encPassword = Convert.ToBase64String(encode);
                return encPassword;
            }
            catch (Exception)
            {
                throw;
            }         
        }
        public string DecryptPassword(string encryptpwd)
        {
            try
            {
                UTF8Encoding encodepwd = new UTF8Encoding();
                Decoder Decode = encodepwd.GetDecoder();
                byte[] todecode_byte = Convert.FromBase64String(encryptpwd);
                int charCount = Decode.GetCharCount(todecode_byte, 0, todecode_byte.Length);
                char[] decoded_char = new char[charCount];
                Decode.GetChars(todecode_byte, 0, todecode_byte.Length, decoded_char, 0);
                string decryptpwd = new String(decoded_char);
                return decryptpwd;
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
                User user = new User();               
                user = context.Users.Where(x => x.Email == userLogin.Email).FirstOrDefault();
                string decPass = DecryptPassword(user.Password);
                var id = user.Id;
                if(decPass==userLogin.Password&&user!=null)
                    return ClaimTokenByID(id);
                else
                    return null;

            }
            catch (Exception)
            {

                throw;
            }
        }
        public string ClaimTokenByID(long Id)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(configuration["Jwt:key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("Id", Id.ToString()) }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
        public string GenerateJwtToken(string email)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(configuration["Jwt:key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("Email", email)}),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public string ForgetPassword(string email)
        {
            try
            {
                var checkemail = context.Users.FirstOrDefault(e => e.Email == email);
                if(checkemail!=null)
                {
                    var token = GenerateJwtToken(email);
                    new MSMQModel().MSMQSender(token);
                    return token;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool ResetPassword(string email,string password,string confirmPassword)
        {
            try
            {
                
                if (password.Equals(confirmPassword))
                {
                    User user = context.Users.Where(e => e.Email==email).FirstOrDefault();
                    user.Password = EncryptPassword(confirmPassword);
                    context.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
