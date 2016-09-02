using Ninject.Modules;
using VeiculosAntigos.Data.EF.Context;
using VeiculosAntigos.Repository.Interfaces;
using VeiculosAntigos.Repository.RepositoryEF;

namespace VeiculosAntigos.Core.DI
{
    public class RepositoryContainer: NinjectModule
    {
        public override void Load()
        {
            Bind<IVeiculoRepository>()
                .To<VeiculoRepositoryEF>()
                //.WithConstructorArgument("context", new VeiculosAntigosEFModel())
                ;
            Bind<IFabricanteRepository>()
                .To<FabricanteRepositoryEF>()
                //.WithConstructorArgument("context", new VeiculosAntigosEFModel())
                ;
            Bind<ITipoDeVeiculoRepository>()
                .To<TipoDeVeiculoRepositoryEF>()
                //.WithConstructorArgument("context", new VeiculosAntigosEFModel())
                ;
        }
    }
}
