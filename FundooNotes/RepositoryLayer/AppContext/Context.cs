﻿using Microsoft.EntityFrameworkCore;
using RepositoryLayer.Entites;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.AppContext
{
  
        public class Context : DbContext
        {
            public Context(DbContextOptions options)
                : base(options)
            {
            }
            public DbSet<User> Users { get; set; }
        }
    
}
