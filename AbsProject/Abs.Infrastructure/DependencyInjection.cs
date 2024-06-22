using Abs.Application.Abstractions.Email;
using Abs.Application.Abstractions.Time;
using Abs.Infrastructure.Email;
using Abs.Infrastructure.Time;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace Abs.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services, 
            IConfiguration configuration)
        {
            services.AddTransient<IDateTimeProvider, DateTimeProvider>();
            services.AddTransient<IEmailService, EmailService>();

            var conntectionString = configuration.GetConnectionString("Database") 
                ?? throw new ArgumentNullException(nameof(configuration));

            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseNpgsql(conntectionString).UseSnakeCaseNamingConvention();
            });

            return services;
        }
    }
}
