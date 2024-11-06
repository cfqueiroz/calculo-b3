using System.Web.Http;
using Unity;
using Unity.WebApi;
using calculo_b3.Services;

namespace calculo_b3
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
            var container = new UnityContainer();

            // Registre ICdbService para que ele seja resolvido como CdbService
            container.RegisterType<ICdbService, CdbService>();

            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}
