using System.Reflection;
using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;
using LazyLayer.Core.Providers;
using LazyLayer.Example.Core;
using LazyLayer.Serilog;

namespace LazyLayer.Example.WebApi.App_Start
{
    /// <summary>
    /// Represent Autofac configuration.
    /// </summary>
    public static class AutofacConfig
    {
        /// <summary>
        /// Configured instance of <see cref="IContainer"/>
        /// <remarks><see cref="AutofacConfig.Configure"/> must be called before trying to get Container instance.</remarks>
        /// </summary>
        public static IContainer Container;

        /// <summary>
        /// Initializes and configures instance of <see cref="IContainer"/>.
        /// </summary>
        /// <param name="configuration"></param>
        public static void Configure(HttpConfiguration configuration)
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<SerilogProvider>().As<ILogProvider>();
            builder.RegisterType<ValueService>().As<IValueService>();

            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            Container = builder.Build();

            configuration.DependencyResolver = new AutofacWebApiDependencyResolver(Container);
        }
    }
}