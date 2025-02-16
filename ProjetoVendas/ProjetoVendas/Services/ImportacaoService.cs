using Newtonsoft.Json;
using ProjetoVenda.Models;

namespace ProjetoVenda.Services
{
    public class ImportacaoService
    {
        private readonly VendasDbContext _context;

        public ImportacaoService(VendasDbContext context)
        {
            _context = context;
        }

        public async Task ImportClienteAsync()
        {
            using (HttpClient client = new HttpClient())
            {
                string url = "https://camposdealer.dev/Sites/TesteAPI/cliente";

                try
                {
                    var response = await client.GetStringAsync(url);

                    var firstDeserialization = JsonConvert.DeserializeObject<string>(response);

                    var Cliente = JsonConvert.DeserializeObject<List<Cliente>>(firstDeserialization);

                    if (Cliente != null && Cliente.Count > 0) {
                        foreach (var cliente in Cliente)
                        {
                            cliente.idCliente = 0;
                        }

                        _context.Cliente.AddRange(Cliente);
                        await _context.SaveChangesAsync();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                    throw;
                }
            }
        }

        public async Task ImportProdutoAsync()
        {
            using (HttpClient client = new HttpClient())
            {
                string url = "https://camposdealer.dev/Sites/TesteAPI/produto";
                
                try
                {
                    var response = await client.GetStringAsync(url);

                    var firstDeserialization = JsonConvert.DeserializeObject<string>(response);

                    var Produto = JsonConvert.DeserializeObject<List<Produto>>(firstDeserialization);

                    if (Produto != null && Produto.Count > 0)
                    {
                        foreach (var produto in Produto)
                        {
                            produto.idProduto = 0;
                        }

                        _context.Produto.AddRange(Produto);
                        await _context.SaveChangesAsync();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                    throw;
                }
            }
        }

        public async Task ImportVendaAsync()
        {
            using (HttpClient client = new HttpClient())
            {
                string url = "https://camposdealer.dev/Sites/TesteAPI/venda";
                
                try
                {
                    var response = await client.GetStringAsync(url);

                    var firstDeserialization = JsonConvert.DeserializeObject<string>(response);

                    var Venda = JsonConvert.DeserializeObject<List<Venda>>(firstDeserialization);

                    if (Venda != null && Venda.Count > 0)
                    {
                        foreach (var venda in Venda)
                        {
                            venda.idVenda = 0;
                        }

                        _context.Venda.AddRange(Venda);
                        await _context.SaveChangesAsync();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                    throw;
                }
            }
        }
    }
}
