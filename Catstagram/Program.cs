using Catstagram.Data;
using Catstagram.Server.Authorization;
using Catstagram.Server.Infrastructure.Extensions;
using Catstagram.Server.Infrastructure.Filters;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// ================
// === SERVICES ===
// ================

builder.Services.AddDatabase(builder.Configuration)
                .AddIdentity()
                .AddDatabaseDeveloperPageExceptionFilter()
                .AddJwtAuthentication(builder.Configuration)
                .AddApplicationServices()
                .AddSwagger()
                .AddApiControllers();

// ===========
// === APP ===
// ===========

var app = builder.Build();

app.UseCors(x => x
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDatabaseErrorPage();
}

app.UseRouting()
    .UseAuthorization()
    .UseAuthentication()
    .UseSwaggerUI()
    .UseMiddleware<JwtMiddleware>()
    .UseEndpoints(endpoints =>
    {
        endpoints.MapControllers();
    })
    .ApplyMigrations();
    
app.Run();
