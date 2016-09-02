using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using VeiculosAntigos.Core.DI;
using VeiculosAntigos.Model;
using VeiculosAntigos.Repository.Interfaces;
using VeiculosAntigos.Service.Interfaces;

namespace VeiculosAntigos.WebApi.Controllers
{
    public class TipoDeVeiculoController : ApiController
    {
        private readonly ITipoDeVeiculoService _service;

        public TipoDeVeiculoController()
        {
            _service = ContainersFactory.ServiceFactory.GetInstance<ITipoDeVeiculoService, ITipoDeVeiculoRepository>();
        }

        [ResponseType(typeof(TipoDeVeiculoDTO))]
        public IHttpActionResult Get()
        {
            var entities = _service.Get().Select(x => convertToDTO(x));
            return Ok(entities);
        }

        [ResponseType(typeof(TipoDeVeiculoDTO))]
        public IHttpActionResult Get(Guid id)
        {
            var entity = _service.Get(id);
            return Ok(entity);
        }

        [ResponseType(typeof(TipoDeVeiculoDTO))]
        public IHttpActionResult Post([FromBody]TipoDeVeiculoDTO model)
        {
            bool success;
            try
            {
                var entity = new TipoDeVeiculo() { Id = Guid.NewGuid(), Tipo = model.Tipo };
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
        [Route("api/TipoDeVeiculo/Put/{id}")]
        public IHttpActionResult Put(Guid id, [FromBody]TipoDeVeiculoDTO model)
        {
            bool success;
            try
            {
                var entity = new TipoDeVeiculo() { Id = id, Tipo = model.Tipo };
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
        [Route("api/TipoDeVeiculo/Delete/{id}")]
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

        private TipoDeVeiculoDTO convertToDTO(TipoDeVeiculo entity)
        {
            return new TipoDeVeiculoDTO() { Id = entity.Id, Tipo = entity.Tipo };
        }
    }
}
