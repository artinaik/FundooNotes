﻿using CommonLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interfaces
{
    public interface IUserBL
    {
        public bool Registration(UserRegistration user);
        public bool Login(UserLogin userLogin);
        public string GenerateJwtToken(string email);

        public string ForgetPassword(string email);

    }
}