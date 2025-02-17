using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjetoVenda.Models;
using System;
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
            try
            {
                var produtos = string.IsNullOrEmpty(search)
                    ? _context.Produto.ToList()
                    : _context.Produto.Where(p => p.dscProduto.Contains(search)).ToList();

                return Json(produtos);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Erro ao buscar produtos. Verifique a conexão ou tente novamente mais tarde.";
                Console.WriteLine($"Erro ao buscar produtos: {ex.Message}");
                return Json(new { error = TempData["ErrorMessage"] });
            }
        }

        [HttpPost]
        public IActionResult Create([FromBody] Produto produto)
        {
            try
            {
                if (produto == null || string.IsNullOrWhiteSpace(produto.dscProduto) || produto.vlrUnitario <= 0)
                    return BadRequest("Descrição e valor unitário são obrigatórios.");

                _context.Produto.Add(produto);
                int affectedRows = _context.SaveChanges();

                if (affectedRows == 0)
                    return StatusCode(500, "Nenhum produto foi salvo.");

                return Ok();
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Erro ao salvar produto. Tente novamente mais tarde.";
                Console.WriteLine($"Erro ao salvar produto: {ex.Message}");
                return StatusCode(500, TempData["ErrorMessage"]);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit([FromBody] Produto produto)
        {
            try
            {
                if (produto == null || produto.idProduto == 0)
                    return BadRequest("Dados inválidos");

                var existingProduto = await _context.Produto.FindAsync(produto.idProduto);
                if (existingProduto == null)
                    return NotFound("Produto não encontrado.");

                existingProduto.dscProduto = produto.dscProduto;
                existingProduto.vlrUnitario = produto.vlrUnitario;

                _context.Update(existingProduto);
                int affectedRows = await _context.SaveChangesAsync();

                if (affectedRows == 0)
                    return StatusCode(500, "Nenhum produto foi atualizado.");

                return Ok();
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Erro ao editar produto. Tente novamente mais tarde.";
                Console.WriteLine($"Erro ao editar produto: {ex.Message}");
                return StatusCode(500, TempData["ErrorMessage"]);
            }
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var produto = await _context.Produto.FindAsync(id);
                if (produto == null)
                    return NotFound("Produto não encontrado.");

                _context.Produto.Remove(produto);
                int affectedRows = await _context.SaveChangesAsync();

                if (affectedRows == 0)
                    return StatusCode(500, "Erro ao excluir produto.");

                return Ok();
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Erro ao excluir produto. Tente novamente mais tarde.";
                Console.WriteLine($"Erro ao excluir produto: {ex.Message}");
                return StatusCode(500, TempData["ErrorMessage"]);
            }
        }
    }
}
