using System.Threading.Tasks;
using System.Web.Mvc;
using VeiculosAntigos.Model;
using VeiculosAntigos.WebSite.Infra;

namespace VeiculosAntigos.WebSite.Controllers
{
    public class TipoDeVeiculoController : VeiculosAntigosBaseController<TipoDeVeiculoDTO>
    {
        public TipoDeVeiculoController(): base("TipoDeVeiculo")
        { }

        public async Task<ActionResult> Index()
        {
            ViewBag.BaseAddressApi = this.BaseAddressApi;
            return View(await this.Business.GetAllItems());
        }
    }

}