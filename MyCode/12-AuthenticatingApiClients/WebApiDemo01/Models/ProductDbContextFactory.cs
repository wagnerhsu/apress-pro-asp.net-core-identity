// Copyright (c) xxx, 2022. All rights reserved.

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace WebApiDemo01.Models;

public class ProductDbContextFactory : IDesignTimeDbContextFactory<ProductDbContext>
{
    public ProductDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<ProductDbContext>();
        var _configuration = BuildConfiguration();
        optionsBuilder.UseSqlServer(_configuration.GetConnectionString("AppDataConnection"));
        return new ProductDbContext(optionsBuilder.Options);

        IConfiguration BuildConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false);

            return builder.Build();
        }
    }
}
