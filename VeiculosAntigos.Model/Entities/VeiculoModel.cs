using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VeiculosAntigos.Model
{

    [Table("Veiculo")]
    public partial class Veiculo
    {
        public Guid Id { get; set; }

        [Required]
        [StringLength(500)]
        public string Descricao { get; set; }

        public int Ano { get; set; }

        public Guid IdFabricante { get; set; }

        public Guid IdTipo { get; set; }

        public virtual Fabricante Fabricante { get; set; }

        public virtual TipoDeVeiculo TipoDeVeiculo { get; set; }
    }
}
