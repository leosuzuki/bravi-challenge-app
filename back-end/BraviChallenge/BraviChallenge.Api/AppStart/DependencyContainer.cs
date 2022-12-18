using BraviChallenge.Application.Interfaces;
using BraviChallenge.Application.Services;
using BraviChallenge.Core.Repositories;
using BraviChallenge.Infra.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace BraviChallenge.Api.AppStart
{
    public static class DependencyContainer
    {
        public static void ConfigureDependencyContainer(this IServiceCollection services)
        {
            services.AddScoped<IPersonService, PersonService>();

            services.AddScoped<IPersonRepository, PersonRepository>();
        }
    }
}
