﻿using Microsoft.EntityFrameworkCore;

namespace WesleyCore.Infrastructure
{
    public static class DbContextOptionsConfigurer
    {
        public static void Configure(
            DbContextOptionsBuilder<UserContext> dbContextOptions,
            string connectionString
            )
        {
            /* This is the single point to configure DbContextOptions for WesleyPoolDbContext */
            dbContextOptions.UseSqlServer(connectionString);
        }
    }
}