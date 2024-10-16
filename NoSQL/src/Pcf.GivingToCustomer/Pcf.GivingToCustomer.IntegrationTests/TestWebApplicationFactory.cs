﻿using System;
using System.Linq;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Pcf.GivingToCustomer.Core.Abstractions.Gateways;
using Pcf.GivingToCustomer.Core.Abstractions.Repositories;
using Pcf.GivingToCustomer.DataAccess;
using Pcf.GivingToCustomer.DataAccess.Repositories;
using Pcf.GivingToCustomer.DataAccess.Data;
using Pcf.GivingToCustomer.DataAccess.Infrastructure;
using Pcf.GivingToCustomer.Integration;
using Pcf.GivingToCustomer.IntegrationTests.Data;

namespace Pcf.GivingToCustomer.IntegrationTests
{
    public class TestWebApplicationFactory<TStartup>
        : WebApplicationFactory<TStartup> where TStartup: class
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                var descriptor = services.SingleOrDefault(
                    d => d.ServiceType ==
                         typeof(DbContextOptions<DataContext>));

                var configuration = new ConfigurationBuilder()
                    .AddEnvironmentVariables()
                    .AddJsonFile("appsettings.json")
                    .Build();

                services.Remove(descriptor);

                services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
                services.AddScoped<INotificationGateway, NotificationGateway>();
                services.AddScoped<IDbInitializer, EfTestDbInitializer>();

                services.Configure<MongoDBSettings>(configuration.GetSection("TestMongoDBSettings"));

                services.AddScoped<DbContext, MongoDBContext>();

                services.AddDbContext<MongoDBContext>();

                //services.AddDbContext<DataContext>(x =>
                //{
                //    x.UseSqlite("Filename=PromoCodeFactoryDb.sqlite");
                //    //x.UseNpgsql(Configuration.GetConnectionString("PromoCodeFactoryDb"));
                //    x.UseSnakeCaseNamingConvention();
                //    x.UseLazyLoadingProxies();
                //});

                //var sp = services.BuildServiceProvider();

                //using var scope = sp.CreateScope();
                //var scopedServices = scope.ServiceProvider;
                ////var dbContext = scopedServices.GetRequiredService<DataContext>();
                //var dbContext = scopedServices.GetRequiredService<MongoDBContext>();
                //var logger = scopedServices
                //    .GetRequiredService<ILogger<TestWebApplicationFactory<TStartup>>>();
                
                //try
                //{
                //    new EfTestDbInitializer(dbContext).InitializeDb();
                //}
                //catch (Exception ex)
                //{
                //    logger.LogError(ex, "Проблема во время заполнения тестовой базы. " +
                //                        "Ошибка: {Message}", ex.Message);
                //}
            });
        }
    }
}