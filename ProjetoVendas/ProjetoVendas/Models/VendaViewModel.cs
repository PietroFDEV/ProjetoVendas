using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjetoVenda.Models
{
    public class Venda
    {
        [Key]
        public int idVenda { get; set; }
        public int idCliente { get; set; }
        public int idProduto { get; set; }
        public int qtdVenda { get; set; }
        public double vleUnitarioVenda { get; set; }
        public DateTime dthVenda { get; set; }
        public double vlrTotalVenda { get; set; }

        [NotMapped]
        public Cliente Cliente { get; set; }
        [NotMapped]
        public Produto Produto { get; set; }
    }
}
