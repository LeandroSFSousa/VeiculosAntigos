using VeiculosAntigos.Data.EF.Context;
using VeiculosAntigos.Model;
using VeiculosAntigos.Repository.Base;
using VeiculosAntigos.Repository.Interfaces;

namespace VeiculosAntigos.Repository.RepositoryEF
{
    public class FabricanteRepositoryEF : GenericRepositoryEF<Fabricante>, IFabricanteRepository
    {
        public FabricanteRepositoryEF(VeiculosAntigosEFModel context): base(context)
        { }
    }
}
