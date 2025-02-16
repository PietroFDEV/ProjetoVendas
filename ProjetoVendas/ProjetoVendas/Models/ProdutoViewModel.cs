using System.ComponentModel.DataAnnotations;

namespace ProjetoVenda.Models
{
    public class Produto
    {
        [Key]
        public int idProduto { get; set; }
        public string dscProduto { get; set; }
        public double vlrUnitario { get; set; }
    }
}
