using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;
using VeiculosAntigos.Core.DI;
using VeiculosAntigos.Model;
using VeiculosAntigos.Repository.Interfaces;
using VeiculosAntigos.Service.Interfaces;

namespace VeiculosAntigos.WebApi.Controllers
{
    public class FabricanteController : ApiController
    {
        private readonly IFabricanteService _service;

        public FabricanteController()
        {
            _service = ContainersFactory.ServiceFactory.GetInstance<IFabricanteService, IFabricanteRepository>();
        }

        [ResponseType(typeof(FabricanteDTO))]
        public IHttpActionResult Get()
        {
            var entities = _service.Get().Select(x => convertToDTO(x));
            return Ok(entities);
        }

        [ResponseType(typeof(FabricanteDTO))]
        public IHttpActionResult Get(Guid id)
        {
            var entity = _service.Get(id);
            return Ok(entity);
        }

        [ResponseType(typeof(FabricanteDTO))]
        public IHttpActionResult Post([FromBody]FabricanteDTO model)
        {
            bool success;
            try
            {
                var entity = new Fabricante() { Id = Guid.NewGuid(), NomeFabricante = model.NomeFabricante };
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
        [Route("api/Fabricante/Put/{id}")]
        public IHttpActionResult Put(Guid id, [FromBody]FabricanteDTO model)
        {
            bool success;
            try
            {
                var entity = new Fabricante() { Id = id, NomeFabricante = model.NomeFabricante };
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
        [Route("api/Fabricante/Delete/{id}")]
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

        private FabricanteDTO convertToDTO(Fabricante entity)
        {
            return new FabricanteDTO() { Id = entity.Id, NomeFabricante = entity.NomeFabricante };
        }
    }
}
