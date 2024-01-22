using Catstagram.Data;

namespace Catstagram.Server.Infrastructure
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseSwaggerUI(this IApplicationBuilder app)
            => app.UseSwagger()
                    .UseSwaggerUI(options =>
                    {
                        options.SwaggerEndpoint("/swagger/v1/swagger.json", "My Catstagram API");
                        options.RoutePrefix = String.Empty;
                    });

        public static void ApplyMigrations(this IApplicationBuilder app)
        {
            using var services = app.ApplicationServices.CreateScope();

            var dbContext = services.ServiceProvider.GetService<CatstagramDbContext>();

            dbContext.Database.Migrate();
        }
    }
}
