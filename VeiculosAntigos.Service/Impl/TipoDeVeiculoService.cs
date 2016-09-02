using System;
using System.Linq;
using VeiculosAntigos.Model;
using VeiculosAntigos.Repository.Interfaces;
using VeiculosAntigos.Service.Base;
using VeiculosAntigos.Service.Interfaces;

namespace VeiculosAntigos.Service.Impl
{
    public class TipoDeVeiculoService : BaseService<TipoDeVeiculo>, ITipoDeVeiculoService
    {
        public TipoDeVeiculoService(ITipoDeVeiculoRepository repository): base(repository)
        { }

        public override bool Insert(TipoDeVeiculo entity)
        {
            if (Exists(entity.Id))
                throw new Exception("Id Tipo de Veículo já existe.");

            validarDuplicidade(entity.Tipo, entity.Id);

            return base.Insert(entity);
        }

        public override bool Update(object id, TipoDeVeiculo entity)
        {
            if (!Exists(id))
                throw new Exception("Id Tipo de Veículo não encontrado.");
            validarDuplicidade(entity.Tipo, entity.Id);

            return base.Update(id, entity);
        }

        public override bool Delete(object id)
        {
            if (!Exists((Guid)id))
                throw new Exception("Id Tipo de Veículo não encontrado.");
            _rep.Delete(id);

            return base.Delete(id);
        }

        private void validarDuplicidade(string tipo, Guid id)
        {
            bool exists = _rep.Get(x => x.Tipo == tipo && x.Id != id).Any();
            if (exists)
                throw new Exception("Tipo de Veículo já existe.");
        }
    }
}
