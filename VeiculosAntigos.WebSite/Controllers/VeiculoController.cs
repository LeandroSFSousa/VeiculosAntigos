using System.Threading.Tasks;
using System.Web.Mvc;
using VeiculosAntigos.Model;
using VeiculosAntigos.WebSite.Business;
using VeiculosAntigos.WebSite.Infra;
using System.Linq;

namespace VeiculosAntigos.WebSite.Controllers
{
    public class VeiculoController : VeiculosAntigosBaseController<VeiculoDTO>
    {
        public VeiculoController() : base("Veiculo")
        { }

        public async Task<ActionResult> Index()
        {
            var businessFabricante = new VeiculosAntigosApiBusiness<FabricanteDTO>(BaseAddressApi, "Fabricante");
            var businessTipoDeVeiculo = new VeiculosAntigosApiBusiness<TipoDeVeiculoDTO>(BaseAddressApi, "TipoDeVeiculo");

            var tFabricantes = await businessFabricante.GetAllItems();
            var tTiposDeVeiculos = await businessTipoDeVeiculo.GetAllItems();
            var tVeiculos = await this.Business.GetAllItems();

            ViewBag.BaseAddressApi = this.BaseAddressApi;
            ViewBag.Fabricantes = tFabricantes.OrderBy(x => x.NomeFabricante);
            ViewBag.TiposDeVeiculos = tTiposDeVeiculos.OrderBy(x => x.Tipo);

            return View(tVeiculos);
        }
    }
}