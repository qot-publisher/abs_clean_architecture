﻿using Abs.Application.Abstractions.Behaviors;
using Abs.Domain.Bookings;
using Microsoft.Extensions.DependencyInjection;

namespace Abs.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(configuration =>
            {
                configuration.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly);

                configuration.AddOpenBehavior(typeof(LoggingBehavior<,>));
            });

            services.AddTransient<PricingService>();


            return services;
        }
    }
}
