﻿using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using AppCitas.Data;
using AppCitas.Interfaces;
using AppCitas.Services;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using AppCitas.Extensions;
using AppCitas.Middleware;

namespace AppCitas;

public class Startup
{
    private readonly IConfiguration _config;
    public Startup(IConfiguration config)
    {
        _config = config;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddApplicationServices(_config);
        services.AddControllers();
        services.AddCors();
        services.AddIdentityServices(_config);

        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "WebAPIv5", Version = "v1" });
        });
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        app.UseMiddleware<ExceptionMiddleware>();

        app.UseHttpsRedirection();

        app.UseRouting();

        app.UseCors(p => p.AllowAnyHeader().AllowAnyMethod().WithOrigins("https://localhost:4200"));

        app.UseAuthentication();

        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}