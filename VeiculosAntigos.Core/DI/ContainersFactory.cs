using Ninject;
using Ninject.Modules;
using Ninject.Parameters;
using System;
using System.Collections.Concurrent;
using VeiculosAntigos.Data.EF.Context;

namespace VeiculosAntigos.Core.DI
{
    public class ContainersFactory
    {
        private static readonly ConcurrentDictionary<string, StandardKernel> _modules = new ConcurrentDictionary<string, StandardKernel>();

        public static class RepositoryFactory
        {
            public static TRepositoy GetInstance<TRepositoy>()
            {
                IParameter paramContext = new ConstructorArgument("context", new VeiculosAntigosEFModel());
                return getInstance<RepositoryContainer, TRepositoy>(paramContext);
            }
        }

        public static class ServiceFactory
        {
            public static TService GetInstance<TService, TRepository>()
            {
                var rep = RepositoryFactory.GetInstance<TRepository>();
                IParameter paramRepository = new ConstructorArgument("repository", rep);
                return getInstance<ServiceContainer, TService>(paramRepository);
            }
        }

        private static T2 getInstance<T1, T2>(params IParameter[] parameters)
            where T1 : NinjectModule, new()
        {
            Type type = typeof(T1);

            NinjectModule nModule = new T1();
            StandardKernel kernel = null;

            if (!_modules.TryGetValue(type.Name, out kernel))
            {
                kernel = new StandardKernel(nModule);
                _modules.TryAdd(type.Name, kernel);
            }

            return kernel.Get<T2>(parameters);
        }
    }
}
