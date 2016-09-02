using System;
using System.Linq;
using VeiculosAntigos.Model;
using VeiculosAntigos.Repository.Interfaces;
using VeiculosAntigos.Service.Base;
using VeiculosAntigos.Service.Interfaces;

namespace VeiculosAntigos.Service.Impl
{
    public class VeiculoService : BaseService<Veiculo>, IVeiculoService
    {
        public VeiculoService(IVeiculoRepository repository): base(repository)
        { }

        public override bool Insert(Veiculo entity)
        {
            if (Exists(entity.Id))
                throw new Exception("Id Veículo já existe.");

            validarDuplicidade(entity.Descricao, entity.Id);

            return base.Insert(entity);
        }

        public override bool Update(object id, Veiculo entity)
        {
            if (!Exists(id))
                throw new Exception("Id Veículo não encontrado.");
            validarDuplicidade(entity.Descricao, entity.Id);

            return base.Update(id, entity);
        }

        public override bool Delete(object id)
        {
            if (!Exists(id))
                throw new Exception("Id Veículo não encontrado.");
            _rep.Delete(id);

            return base.Delete(id);
        }

        private void validarDuplicidade(string descricao, Guid id)
        {
            bool exists = _rep.Get(x => x.Descricao == descricao && x.Id != id).Any();
            if (exists)
                throw new Exception("Veículo já existe.");
        }
    }
}
