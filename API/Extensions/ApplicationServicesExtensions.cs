using Application.Core;
using Application.Events;
using FluentValidation.AspNetCore;
using MediatR;
using Persistence;
using Persistence.Interfaces;

namespace API.Extensions
{
    public static class ApplicationServicesExtensions
    {
        public static IServiceCollection AddApplicationServices(
            this IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            services.AddDbContext<DataContext>();

            services.AddScoped<IDataContext, DataContext>(
                provider => provider.GetService<DataContext>());

            services.AddMediatR(typeof(List.Handler).Assembly);
            services.AddAutoMapper(typeof(MappingProfiles).Assembly);

            return services;
        }
    }
}
