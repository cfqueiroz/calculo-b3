using calculo_b3.Services;
using System.Web.Http;
using Unity;
using Unity.Lifetime;
using Unity.WebApi;

namespace calculo_b3
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
            var container = new UnityContainer();

            // Registrar ICdbService com ciclo de vida do contêiner (Singleton)
            container.RegisterType<ICdbService, CdbService>(new HierarchicalLifetimeManager());

            // Configurar Unity como DependencyResolver para Web API
            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}
