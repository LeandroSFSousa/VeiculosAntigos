using VeiculosAntigos.Data.EF.Context;
using VeiculosAntigos.Model;
using VeiculosAntigos.Repository.Base;
using VeiculosAntigos.Repository.Interfaces;

namespace VeiculosAntigos.Repository.RepositoryEF
{
    public class TipoDeVeiculoRepositoryEF : GenericRepositoryEF<TipoDeVeiculo>, ITipoDeVeiculoRepository
    {
        public TipoDeVeiculoRepositoryEF(VeiculosAntigosEFModel context): base(context)
        { }
    }
}
