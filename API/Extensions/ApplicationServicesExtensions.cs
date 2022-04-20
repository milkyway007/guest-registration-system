using Application.Core;
using Application.Events.Commands;
using Application.Interfaces.Core;
using FluentValidation;
using MediatR;
using Persistence;
using Persistence.Interfaces;
using static Application.Events.Commands.Create;

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
            services.AddCors(opt =>
            {
                opt.AddPolicy(Constants.CORS_POLICY, policy =>
                {
                    policy.AllowAnyMethod().AllowAnyHeader().WithOrigins(Constants.APP_BASE_URI);
                });
            });

            services.AddScoped<IDataContext, DataContext>(
                provider => provider.GetService<DataContext>());
            services.AddMediatR(typeof(Create).Assembly);
            services.AddAutoMapper(typeof(MappingProfiles).Assembly);
            services.AddScoped
                <IEntityFrameworkQueryableExtensionsAbstraction, EntityFrameworkQueryableExtensionsAbstraction>();
            services.AddScoped<IQueryableExtensionsAbstraction, QueryableExtensionsAbstraction>();

            return services;
        }
    }
}
