using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using VeiculosAntigos.Data.EF.Context;
using VeiculosAntigos.Repository.Interfaces;
using VeiculosAntigos.Repository.RepositoryEF;
using VeiculosAntigos.Service.Impl;
using VeiculosAntigos.Service.Interfaces;

namespace VeiculosAntigos.Service.Tests
{
    [TestClass]
    public class TipoDeVeiculoServiceTests
    {
        private readonly ITipoDeVeiculoService _service;
        private Guid idHatch = Guid.Parse("BA21C5EE-8638-40E3-8ADC-AA3559753E4D");
        private Guid idSuv = Guid.Parse("91FAC4B4-E874-4730-8143-47913F8BD3BF");
        private Guid idSedan = Guid.Parse("C119D399-0E88-4C45-960E-F466D4C1048D");

        public TipoDeVeiculoServiceTests()
        {
            var context = new VeiculosAntigosEFModel();
            ITipoDeVeiculoRepository rep = new TipoDeVeiculoRepositoryEF(context);
            _service = new TipoDeVeiculoService(rep);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void TipoDeVeiculoService_InsertIdExistente()
        {
            var success = _service.Insert(new Model.TipoDeVeiculo() { Id = idHatch, Tipo = "Hatch" });
            Assert.IsTrue(success);

            success = _service.Insert(new Model.TipoDeVeiculo() { Id = idSuv, Tipo = "SUV" });
            Assert.IsTrue(success);

            success = _service.Insert(new Model.TipoDeVeiculo() { Id = idHatch, Tipo = "Sedan" });
            Assert.IsTrue(success);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void TipoDeVeiculoService_InsertTipoExistente()
        {
            var success = _service.Insert(new Model.TipoDeVeiculo() { Id = idSedan, Tipo = "Sedan" });
            Assert.IsTrue(success);

            success = _service.Insert(new Model.TipoDeVeiculo() { Id = Guid.NewGuid(), Tipo = "Sedan" });
            Assert.IsTrue(success);
        }

        [TestMethod]
        public void TipoDeVeiculoService_UpdateSemAlteracoes()
        {
            var hatch = _service.Get(idHatch);
            Assert.IsNotNull(hatch);

            var success = _service.Update(hatch);
            Assert.IsTrue(success);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void TipoDeVeiculoService_UpdateTipoExistente()
        {
            var hatch = _service.Get(idHatch);
            Assert.IsNotNull(hatch);

            hatch.Tipo = "SUV";
            var success = _service.Update(hatch);
            Assert.IsTrue(success);
        }

        [TestMethod]
        public void TipoDeVeiculoService_Get()
        {
            var items = _service.Get();
            Assert.IsNotNull(items);
            Assert.IsTrue(items.Count() > 0);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void TipoDeVeiculoService_DeleteInexistente()
        {
            var success = _service.Delete(Guid.NewGuid());
            Assert.IsTrue(success);
        }

        [TestMethod]
        public void TipoDeVeiculoService_DeleteExistente()
        {
            var success = _service.Delete(idSedan);
            Assert.IsTrue(success);
        }
    }
}
