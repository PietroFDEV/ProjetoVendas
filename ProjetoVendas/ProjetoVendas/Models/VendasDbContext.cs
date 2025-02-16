using Microsoft.EntityFrameworkCore;
using ProjetoVenda.Models;

public class VendasDbContext : DbContext
{
    public VendasDbContext(DbContextOptions<VendasDbContext> options) : base(options) { }

    public DbSet<Cliente> Cliente { get; set; }
    public DbSet<Produto> Produto { get; set; }
    public DbSet<Venda> Venda { get; set; }
}
