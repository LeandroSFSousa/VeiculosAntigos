using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using VeiculosAntigos.Data.EF.Context;
using VeiculosAntigos.Model;
using VeiculosAntigos.Repository.Interfaces;
using VeiculosAntigos.Repository.RepositoryEF;

namespace VeiculosAntigos.Repository.Tests
{
    [TestClass]
    public class TipoDeVeiculoRepositoryEFTests
    {
        private readonly ITipoDeVeiculoRepository rep;

        private Guid idHatch = Guid.Parse("BA21C5EE-8638-40E3-8ADC-AA3559753E4D");
        private Guid idSuv = Guid.Parse("91FAC4B4-E874-4730-8143-47913F8BD3BF");
        private Guid idSedan = Guid.Parse("BDAF7733-A88E-45EE-8733-BACEB354B29E");

        public TipoDeVeiculoRepositoryEFTests()
        {
            var context = new VeiculosAntigosEFModel();
            rep = new TipoDeVeiculoRepositoryEF(context);
        }

        [TestMethod]
        public void TipoDeVeiculoRepositoryEF_InsertAndGetById()
        {
            rep.Insert(new TipoDeVeiculo() { Id = idSuv, Tipo = "SUV" });
            var i = rep.Save();
            Assert.IsTrue(i > 0);

            rep.Insert(new TipoDeVeiculo() { Id = idHatch, Tipo = "Hatch" });
            i = rep.Save();
            Assert.IsTrue(i > 0);

            var hatch = rep.Get(idHatch);
            var suv = rep.Get(idSuv);
            Assert.IsNotNull(hatch);
            Assert.IsNotNull(suv);
        }

        [TestMethod]
        public void TipoDeVeiculoRepositoryEF_Get()
        {
            var items = rep.Get().ToList();
            Assert.IsNotNull(items);
            Assert.IsTrue(items.Count > 0);
        }

        [TestMethod]
        public void TipoDeVeiculoRepositoryEF_Delete()
        {
            rep.Delete(idSuv);
            rep.Save();
            var suv = rep.Get(idSuv);
            Assert.IsNull(suv);

            var hatch = rep.Get(idHatch);
            Assert.IsNotNull(hatch);
            rep.Delete(hatch);
            rep.Save();

            hatch = rep.Get(idHatch);
            Assert.IsNull(hatch);
        }

        [TestMethod]
        public void TipoDeVeiculoRepositoryEF_InsertAndUpdate()
        {
            string incorreto = "Sedan";
            string correto = "Sedã";

            rep.Insert(new TipoDeVeiculo() { Id = idSedan, Tipo = incorreto });
            var i = rep.Save();
            Assert.IsTrue(i > 0);

            var sedan = rep.Get(idSedan);
            Assert.IsNotNull(sedan);

            sedan.Tipo = correto;
            rep.Update(sedan);
            i = rep.Save();
            Assert.IsTrue(i > 0);

            var entityUpdated = rep.Get(idSedan);
            Assert.IsNotNull(entityUpdated);
            Assert.AreEqual(entityUpdated.Tipo, correto);
        }
    }
}
