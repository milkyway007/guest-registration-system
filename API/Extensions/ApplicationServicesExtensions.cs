using Application.Core;
using Application.Events.Queries;
using Application.Interfaces.Core;
using MediatR;
using Microsoft.OpenApi.Models;
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
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "API", Version = "v1" });
            });
            services.AddDbContext<DataContext>();
            services.AddScoped<IDataContext, DataContext>(
                provider => provider.GetService<DataContext>());
            services.AddMediatR(typeof(List.Handler).Assembly);
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            services.AddAutoMapper(typeof(MappingProfiles).Assembly);
            services.AddScoped
                <IEntityFrameworkQueryableExtensionsAbstraction, EntityFrameworkQueryableExtensionsAbstraction>();
            services.AddScoped<IQueryableExtensionsAbstraction, QueryableExtensionsAbstraction>();

            return services;
        }
    }
}
