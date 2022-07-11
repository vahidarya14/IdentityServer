// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Mappers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Linq;
using System.Reflection;

namespace IdentityServer
{
    public class Startup
    {
        public IWebHostEnvironment Environment { get; }

        public Startup(IWebHostEnvironment environment)
        {
            Environment = environment;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            // uncomment, if you want to add an MVC-based UI
            services.AddControllersWithViews().AddRazorRuntimeCompilation();

            //////////////////اگر تو مموری نگه میداری دیتا رو
            ////var builder = services.AddIdentityServer(options =>
            ////{
            ////    // see https://identityserver4.readthedocs.io/en/latest/topics/resources.html
            ////    options.EmitStaticAudienceClaim = true;
            ////})
            ////    .AddInMemoryIdentityResources(Config.IdentityResources)
            ////    .AddInMemoryApiScopes(Config.ApiScopes)
            ////    .AddInMemoryClients(Config.Clients);
            //////////////////////////////////////////////////////////////////////////////////

            //////////////////////برای نگهداری دیتابیس در دیتابیس با انتیتی فرمیورک
            var migrationsAssembly = typeof(Startup).GetTypeInfo().Assembly.GetName().Name;
            const string connectionString = @"Data Source=(LocalDb)\MSSQLLocalDB;database=IdentityServer4.Quickstart.EntityFramework-3.0.0;trusted_connection=yes;";

            var builder = services.AddIdentityServer()
                .AddTestUsers(Config.TestUsers)
                .AddConfigurationStore(options =>
                {
                    options.ConfigureDbContext = b => b.UseSqlServer(connectionString,
                        sql => sql.MigrationsAssembly(migrationsAssembly));
                })
                .AddOperationalStore(options =>
                {
                    options.ConfigureDbContext = b => b.UseSqlServer(connectionString,
                        sql => sql.MigrationsAssembly(migrationsAssembly));
                });
            ///////////////////////////////////////////////////////////////////////////////

            // not recommended for production - you need to store your key material somewhere secure
            builder.AddDeveloperSigningCredential();
        }

        public void Configure(IApplicationBuilder app)
        {
            ///////////////////////برای نگهداری دیتابیس در دیتابیس با انتیتی فرمیورک
            InitializeDatabase(app);

            if (Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // uncomment if you want to add MVC
            app.UseStaticFiles();
            app.UseRouting();

            app.UseIdentityServer();

            // uncomment, if you want to add MVC
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });
        }

        void InitializeDatabase(IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope();
            serviceScope.ServiceProvider.GetRequiredService<PersistedGrantDbContext>().Database.Migrate();

            var context = serviceScope.ServiceProvider.GetRequiredService<ConfigurationDbContext>();
            context.Database.Migrate();
            if (!context.Clients.Any())
            {
                foreach (var client in Config.Clients)
                {
                    context.Clients.Add(client.ToEntity());
                }
                context.SaveChanges();
            }

            if (!context.IdentityResources.Any())
            {
                foreach (var resource in Config.IdentityResources)
                {
                    context.IdentityResources.Add(resource.ToEntity());
                }
                context.SaveChanges();
            }

            if (!context.ApiResources.Any())
            {
                foreach (var resource in Config.ApiResources)
                {
                    context.ApiResources.Add(resource.ToEntity());
                }
                context.SaveChanges();
            }


            if (!context.ApiScopes.Any())
            {
                context.ApiScopes.Add(new IdentityServer4.EntityFramework.Entities.ApiScope
                {
                    Name = "api1",
                    DisplayName = "api1",
                    Required = true,
                    Emphasize = true,
                    ShowInDiscoveryDocument = true,
                });
                context.SaveChanges();
            }
        }

    } 
}
