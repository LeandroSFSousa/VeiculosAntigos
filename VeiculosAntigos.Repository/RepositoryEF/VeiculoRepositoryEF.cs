using VeiculosAntigos.Data.EF.Context;
using VeiculosAntigos.Model;
using VeiculosAntigos.Repository.Base;
using VeiculosAntigos.Repository.Interfaces;

namespace VeiculosAntigos.Repository.RepositoryEF
{
    public class VeiculoRepositoryEF : GenericRepositoryEF<Veiculo>, IVeiculoRepository
    {
        public VeiculoRepositoryEF(VeiculosAntigosEFModel context): base(context)
        { }
    }
}
