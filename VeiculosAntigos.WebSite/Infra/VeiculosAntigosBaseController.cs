using System.Configuration;
using System.Web.Mvc;
using VeiculosAntigos.WebSite.Business;

namespace VeiculosAntigos.WebSite.Infra
{
    public abstract class VeiculosAntigosBaseController<TEntity> : Controller 
        where TEntity : class, new()
    {
        protected VeiculosAntigosApiBusiness<TEntity> Business { get; private set; }
        protected string BaseAddressApi { get; private set; }

        public VeiculosAntigosBaseController(string controllerApi)
        {
            BaseAddressApi = ConfigurationManager.AppSettings["BaseAddressApi"];
            Business = new VeiculosAntigosApiBusiness<TEntity>(BaseAddressApi, controllerApi);
        }

    }
}