using Abs.Application.Abstractions.Data;
using Abs.Application.Abstractions.Email;
using Abs.Application.Abstractions.Time;
using Abs.Domain.Abstractions;
using Abs.Domain.Apartments;
using Abs.Domain.Bookings;
using Abs.Domain.Users;
using Abs.Infrastructure.Data;
using Abs.Infrastructure.Email;
using Abs.Infrastructure.Repositories;
using Abs.Infrastructure.Time;
using Dapper;
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

            services.AddScoped<IUserRepository, UserRepository>();

            services.AddScoped<IApartmentRepository, ApartmentRepository>();

            services.AddScoped<IBookingRepository, BookingRepository>();

            services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<ApplicationDbContext>());

            services.AddSingleton<ISqlConnectionFactory>(sp=> new SqlConnectionFactory(conntectionString));

            SqlMapper.AddTypeHandler(new DateOnlyTypeHandler());

            return services;
        }
    }
}
