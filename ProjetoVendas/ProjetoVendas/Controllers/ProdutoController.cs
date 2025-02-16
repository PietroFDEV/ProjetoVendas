using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjetoVenda.Models;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoVendas.Controllers
{
    public class ProdutoController : Controller
    {
        private readonly VendasDbContext _context;

        public ProdutoController(VendasDbContext context)
        {
            _context = context;
        }

        public IActionResult Index(string search)
        {
            return View("~/Views/Importacao/Produto.cshtml");
        }

        [HttpGet]
        public JsonResult GetProdutos(string search)
        {
            var produtos = string.IsNullOrEmpty(search)
                ? _context.Produto.ToList()
                : _context.Produto.Where(p => p.dscProduto.Contains(search)).ToList();

            return Json(produtos);
        }

        [HttpPost]
        public IActionResult Create([FromBody] Produto produto)
        {
            if (produto == null || string.IsNullOrWhiteSpace(produto.dscProduto) || produto.vlrUnitario <= 0)
                return BadRequest("Descrição e valor unitário são obrigatórios.");

            _context.Produto.Add(produto);
            _context.SaveChanges();
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> Edit([FromBody] Produto produto)
        {
            if (produto == null || produto.idProduto == 0) return BadRequest("Dados inválidos");

            var existingProduto = await _context.Produto.FindAsync(produto.idProduto);
            if (existingProduto == null) return NotFound();

            existingProduto.dscProduto = produto.dscProduto;
            existingProduto.vlrUnitario = produto.vlrUnitario;

            _context.Update(existingProduto);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var produto = _context.Produto.Find(id);
            if (produto != null)
            {
                _context.Produto.Remove(produto);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
