using Microsoft.Extensions.DependencyInjection.Extensions;
using MinimalAPIExample.Endpoints;
using System.Reflection;

namespace MinimalAPIExample
{
    public static class DependencyInjection
    {
        private readonly static Assembly[] Assemblies = AppDomain.CurrentDomain.GetAssemblies();

        public static IServiceCollection AddEndpoints(this IServiceCollection services)
        {
            var assembly = typeof(IAssemblyMarker).Assembly;

            ServiceDescriptor[] serviceDescriptors = assembly
                .DefinedTypes
                .Where(type => type is { IsAbstract: false, IsInterface: false } &&
                               type.IsAssignableTo(typeof(IEndpoint)))
                .Select(type => ServiceDescriptor.Transient(typeof(IEndpoint), type))
                .ToArray();

            services.TryAddEnumerable(serviceDescriptors);

            return services;
        }

        public static IApplicationBuilder MapEndpoints(this WebApplication app)
        {
            IEnumerable<IEndpoint> endpoints = app.Services.GetRequiredService<IEnumerable<IEndpoint>>();

            foreach (IEndpoint endpoint in endpoints)
            {
                endpoint.MapEndpoint(app);
            }

            return app;
        }
    }

    public interface IAssemblyMarker
    {

    }
}
