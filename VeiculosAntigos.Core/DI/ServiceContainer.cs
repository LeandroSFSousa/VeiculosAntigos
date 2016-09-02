using Ninject.Modules;
using VeiculosAntigos.Service.Impl;
using VeiculosAntigos.Service.Interfaces;

namespace VeiculosAntigos.Core.DI
{
    public class ServiceContainer: NinjectModule
    {
        public override void Load()
        {
            Bind<IVeiculoService>()
                .To<VeiculoService>();
            Bind<IFabricanteService>()
                .To<FabricanteService>();
            Bind<ITipoDeVeiculoService>()
                .To<TipoDeVeiculoService>();
        }
    }
}
