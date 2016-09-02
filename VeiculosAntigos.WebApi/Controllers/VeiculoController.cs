using System;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;
using VeiculosAntigos.Core.DI;
using VeiculosAntigos.Model;
using VeiculosAntigos.Repository.Interfaces;
using VeiculosAntigos.Service.Interfaces;

namespace VeiculosAntigos.WebApi.Controllers
{
    public class VeiculoController : ApiController
    {
        private readonly IVeiculoService _service;

        public VeiculoController()
        {
            _service = ContainersFactory.ServiceFactory.GetInstance<IVeiculoService, IVeiculoRepository>();
        }

        [ResponseType(typeof(VeiculoDTO))]
        public IHttpActionResult Get()
        {
            var entities = _service.Get().ToList();
            var result = entities.Select(x => convertToDTO(x));
            return Ok(result);
        }

        [ResponseType(typeof(VeiculoDTO))]
        public IHttpActionResult Get(Guid id)
        {
            var entity = _service.Get(id);
            return Ok(entity);
        }

        [ResponseType(typeof(VeiculoDTO))]
        public IHttpActionResult Post([FromBody]VeiculoDTO model)
        {
            bool success;
            try
            {
                var entity = new Veiculo() { Id = Guid.NewGuid(), Descricao = model.Descricao, Ano = model.Ano, IdFabricante = model.IdFabricante, IdTipo = model.IdTipo };
                success = _service.Insert(entity);
                if (success)
                    return Ok(convertToDTO(entity));
                else
                    return InternalServerError(new Exception("Não foi possível incluir registro."));
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpPost]
        [ResponseType(typeof(bool))]
        [Route("api/Veiculo/Put/{id}")]
        public IHttpActionResult Put(Guid id, [FromBody]VeiculoDTO model)
        {
            bool success;
            try
            {
                var entity = new Veiculo() { Id = id, Descricao = model.Descricao, Ano = model.Ano, IdFabricante = model.IdFabricante, IdTipo = model.IdTipo };
                success = _service.Update(id, entity);
                return Ok(success);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpPost]
        [ResponseType(typeof(bool))]
        [Route("api/Veiculo/Delete/{id}")]
        public IHttpActionResult Delete(Guid id)
        {
            bool success;
            try
            {
                success = _service.Delete(id);
                return Ok(success);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        private VeiculoDTO convertToDTO(Veiculo entity)
        {
            return new VeiculoDTO() { Id = entity.Id, Descricao = entity.Descricao,
                Ano = entity.Ano, IdFabricante = entity.IdFabricante, IdTipo = entity.IdTipo,
                Fabricante = entity.Fabricante != null ? entity.Fabricante.NomeFabricante : string.Empty,
                Tipo = entity.TipoDeVeiculo != null ? entity.TipoDeVeiculo.Tipo : string.Empty};
        }
    }
}
