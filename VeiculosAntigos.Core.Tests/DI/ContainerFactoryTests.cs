using Microsoft.VisualStudio.TestTools.UnitTesting;
using VeiculosAntigos.Core.DI;
using VeiculosAntigos.Repository.Interfaces;
using VeiculosAntigos.Service.Interfaces;
using System.Linq;

namespace VeiculosAntigos.Core.Tests
{
    [TestClass]
    public class ContainerFactoryTests
    {
        [TestMethod]
        public void ContainerFactory_Service_TipoDeVeiculo_Get()
        {
            var service = ContainersFactory.ServiceFactory.GetInstance<ITipoDeVeiculoService, ITipoDeVeiculoRepository>();
            Assert.IsNotNull(service);

            var items = service.Get();
            Assert.IsNotNull(items);
            Assert.IsTrue(items.Count() > 0);
        }
    }
}
