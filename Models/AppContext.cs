﻿using Microsoft.EntityFrameworkCore;

namespace SistemaDeViagens.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        
    }
}