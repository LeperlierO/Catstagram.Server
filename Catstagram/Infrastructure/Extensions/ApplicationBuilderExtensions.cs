using Catstagram.Data;
using Catstagram.Server.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Catstagram.Server.Infrastructure.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseSwaggerUI(this IApplicationBuilder app)
            => app.UseSwagger()
                    .UseSwaggerUI(options =>
                    {
                        options.SwaggerEndpoint("/swagger/v1/swagger.json", "My Catstagram API");
                        options.RoutePrefix = string.Empty;
                    });

        public static void ApplyMigrations(this IApplicationBuilder app)
        {
            using var services = app.ApplicationServices.CreateScope();

            var dbContext = services.ServiceProvider.GetService<CatstagramDbContext>();

            dbContext.Database.Migrate();
        }
    }
}
