using Application.Core;
using Application.Events;
using FluentValidation.AspNetCore;
using MediatR;
using Persistence;

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
            services.AddMediatR(typeof(List.Handler).Assembly);
            services.AddAutoMapper(typeof(MappingProfiles).Assembly);
            services.AddControllers().AddFluentValidation(config =>
            config.RegisterValidatorsFromAssemblyContaining<Create>());

            return services;
        }
    }
}
