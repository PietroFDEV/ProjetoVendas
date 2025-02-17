using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjetoVenda.Models
{
    public class Venda
    {
        [Key]
        public int idVenda { get; set; }

        [ForeignKey("Cliente")]
        public int idCliente { get; set; }
        public Cliente Cliente { get; set; }

        [ForeignKey("Produto")]
        public int idProduto { get; set; }
        public Produto Produto { get; set; }

        public int qtdVenda { get; set; }
        public double vlrUnitarioVenda { get; set; }
        public DateTime dthVenda { get; set; }
        public double vlrTotalVenda { get; set; }
    }
}
