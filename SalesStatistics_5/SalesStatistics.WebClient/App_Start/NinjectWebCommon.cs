using SalesStatistics.DataAccessLayer;
using SalesStatistics.DataAccessLayer.EFUnitOfWork;
using SalesStatistics.DataAccessLayer.EntityFrameworkContext;
using SalesStatistics.DataAccessLayer.Repository;
using SalesStatistics.WebClient.Services;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(SalesStatistics.WebClient.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(SalesStatistics.WebClient.NinjectWebCommon), "Stop")]

namespace SalesStatistics.WebClient
{
    using System;
    using System.Web;
    using Microsoft.Web.Infrastructure.DynamicModuleHelper;
    using Ninject;
    using Ninject.Web.Common;
    using Ninject.Web.Common.WebHost;

    public static class NinjectWebCommon
    {
        private static readonly Bootstrapper Bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start()
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            Bootstrapper.Initialize(CreateKernel);
        }

        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            Bootstrapper.ShutDown();
        }

        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
            kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

            RegisterServices(kernel);
            return kernel;
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            SampleContextFactory contextFactory = new SampleContextFactory();
            kernel.Bind<IUnitOfWork>()
                .To<UnitOfWork>()
                .WithConstructorArgument("contextFactory", contextFactory);
            
            kernel.Bind<IMapperConfig>()
                .To<MapperConfig>();
            
            kernel.Bind<IRepository>().To<GenericRepository>()
                .WithConstructorArgument("context", contextFactory.Create());
        }
    }
}