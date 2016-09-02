using System;

namespace VeiculosAntigos.Model
{

    public partial class VeiculoDTO
    {
        public Guid Id { get; set; }
        public string Descricao { get; set; }
        public int Ano { get; set; }
        public Guid IdFabricante { get; set; }
        public string Fabricante { get; set; }
        public Guid IdTipo { get; set; }
        public string Tipo { get; set; }
    }
}
