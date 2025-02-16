using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjetoVenda.Models
{
    public class Cliente
    {
        [Key]
        public int idCliente { get; set; }
        public string nmCliente { get; set; }
        public string cidade { get; set; }
    }
}
