using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using api.db.model;
using api.db;

namespace api
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();


            var options = new DbContextOptionsBuilder<WordListContext>()
                .UseSqlite(@"Filename=data.db")
                .Options;
            using (var context = new WordListContext(options))
            {
                if (context.Database.EnsureCreated())
                {
                    context.Entries.AddRange(new Entry
                    {
                        Word = "aap",
                        Translation = "monkey"
                    },
                    new Entry
                    {
                        Word = "ezel",
                        Translation = "donkey"
                    });

                    context.SaveChanges();
                }

            }
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddCors();
            services.AddMvc();
            services.AddDbContext<WordListContext>(options => options.UseSqlite(@"Filename=data.db"));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseCors(builder =>
            {
                builder.AllowAnyHeader();
                builder.AllowAnyMethod();
                builder.AllowCredentials();
                builder.AllowAnyOrigin();
            });

            app.UseMvc();
        }
    }
}
