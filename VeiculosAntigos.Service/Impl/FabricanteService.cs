using System;
using System.Linq;
using VeiculosAntigos.Model;
using VeiculosAntigos.Repository.Interfaces;
using VeiculosAntigos.Service.Base;
using VeiculosAntigos.Service.Interfaces;

namespace VeiculosAntigos.Service.Impl
{
    public class FabricanteService : BaseService<Fabricante>, IFabricanteService
    {
        public FabricanteService(IFabricanteRepository repository): base(repository)
        { }

        public override bool Insert(Fabricante entity)
        {
            if (Exists(entity.Id))
                throw new Exception("Id Fabricante já existe.");

            validarDuplicidade(entity.NomeFabricante, entity.Id);

            return base.Insert(entity);
        }

        public override bool Update(object id, Fabricante entity)
        {
            if (!Exists(id))
                throw new Exception("Id Fabricante não encontrado.");
            validarDuplicidade(entity.NomeFabricante, entity.Id);

            return base.Update(id, entity);
        }

        public override bool Delete(object id)
        {
            if (!Exists((Guid)id))
                throw new Exception("Id Fabricante não encontrado.");
            _rep.Delete(id);

            return base.Delete(id);
        }

        private void validarDuplicidade(string nomeFabricante, Guid id)
        {
            bool exists = _rep.Get(x => x.NomeFabricante == nomeFabricante && x.Id != id).Any();
            if (exists)
                throw new Exception("Fabricante já existe.");
        }
    }
}
