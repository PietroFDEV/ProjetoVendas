using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjetoVenda.Models;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoVendas.Controllers
{
    public class VendaController : Controller
    {
        private readonly VendasDbContext _context;

        public VendaController(VendasDbContext context)
        {
            _context = context;
        }

        public IActionResult Index(string search)
        {
            var vendas = _context.Venda
                .Include(v => v.Cliente)
                .Include(v => v.Produto)
                .Where(v => string.IsNullOrEmpty(search) ||
                            v.Cliente.nmCliente.Contains(search) ||
                            v.Produto.dscProduto.Contains(search))
                .ToList();

            return View("~/Views/Importacao/Venda.cshtml", vendas);
        }

        [HttpGet]
        public JsonResult GetVendas(string search)
        {
            var vendas = _context.Venda
                .Include(v => v.Cliente)
                .Include(v => v.Produto)
                .Where(v => string.IsNullOrEmpty(search) ||
                            v.Cliente.nmCliente.Contains(search) ||
                            v.Produto.dscProduto.Contains(search))
                .Select(v => new
                {
                    v.idVenda,
                    Cliente = v.Cliente.nmCliente,
                    Produto = v.Produto.dscProduto,
                    v.qtdVenda,
                    v.vlrTotalVenda
                })
                .ToList();

            return Json(vendas);
        }

        [HttpPost]
        public IActionResult Create([FromBody] Venda venda)
        {
            if (venda == null || venda.idCliente == 0 || venda.idProduto == 0 || venda.qtdVenda <= 0)
                return BadRequest("Todos os campos são obrigatórios.");

            venda.vlrTotalVenda = _context.Produto.Find(venda.idProduto)?.vlrUnitario * venda.qtdVenda ?? 0;

            _context.Venda.Add(venda);
            _context.SaveChanges();
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> Edit([FromBody] Venda venda)
        {
            if (venda == null || venda.idVenda == 0) return BadRequest("Dados inválidos");

            var existingVenda = await _context.Venda.FindAsync(venda.idVenda);
            if (existingVenda == null) return NotFound();

            existingVenda.idCliente = venda.idCliente;
            existingVenda.idProduto = venda.idProduto;
            existingVenda.qtdVenda = venda.qtdVenda;
            existingVenda.vlrTotalVenda = _context.Produto.Find(venda.idProduto)?.vlrUnitario * venda.qtdVenda ?? 0;

            _context.Update(existingVenda);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var venda = _context.Venda.Find(id);
            if (venda != null)
            {
                _context.Venda.Remove(venda);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
