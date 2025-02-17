using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjetoVenda.Models;
using System;
using System.Diagnostics;
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
            return View("~/Views/Importacao/Venda.cshtml");
        }

        [HttpGet]
        public JsonResult GetVendas(string search)
        {
            try
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
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Erro ao buscar vendas. Verifique a conexão ou tente novamente mais tarde.";
                Console.WriteLine($"Erro ao buscar vendas: {ex.Message}");
                return Json(new { error = TempData["ErrorMessage"] });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Venda venda)
        {
            try
            {
                if (venda == null || venda.idCliente == 0 || venda.idProduto == 0 || venda.qtdVenda <= 0)
                    return BadRequest("Todos os campos são obrigatórios.");

                if (venda.dthVenda < new DateTime(1753, 1, 1) || venda.dthVenda > new DateTime(9999, 12, 31))
                {
                    TempData["ErrorMessage"] = "A data da venda está fora do intervalo permitido para o banco de dados.";
                    return RedirectToAction("Index");
                }

                var produto = await _context.Produto.FindAsync(venda.idProduto);
                if (produto == null)
                    return NotFound("Produto não encontrado.");

                venda.vlrUnitarioVenda = produto.vlrUnitario;
                venda.vlrTotalVenda = produto.vlrUnitario * venda.qtdVenda;

                _context.Venda.Add(venda);
                int affectedRows = await _context.SaveChangesAsync();

                if (affectedRows == 0)
                    return StatusCode(500, "Nenhuma venda foi salva.");

                return Ok();
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Erro ao salvar venda. Tente novamente mais tarde.";
                Console.WriteLine($"Erro ao salvar venda: {ex.Message}");
                return StatusCode(500, TempData["ErrorMessage"]);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit([FromBody] Venda venda)
        {
            try
            {
                if (venda == null || venda.idVenda == 0)
                    return BadRequest("Dados inválidos");

                var existingVenda = _context.Venda.Find(venda.idVenda);
                if (existingVenda == null)
                    return NotFound("Venda não encontrada.");

                var produto = await _context.Produto.FindAsync(venda.idProduto);
                if (produto == null)
                    return NotFound("Produto não encontrado.");

                existingVenda.idCliente = venda.idCliente;
                existingVenda.idProduto = venda.idProduto;
                existingVenda.qtdVenda = venda.qtdVenda;
                existingVenda.vlrUnitarioVenda = produto.vlrUnitario;
                existingVenda.vlrTotalVenda = produto.vlrUnitario * venda.qtdVenda;

                _context.Update(existingVenda);
                int affectedRows = await _context.SaveChangesAsync();

                if (affectedRows == 0)
                    return StatusCode(500, "Nenhuma venda foi atualizada.");

                return Ok();
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Erro ao editar venda. Tente novamente mais tarde.";
                Console.WriteLine($"Erro ao editar venda: {ex.Message}");
                return StatusCode(500, TempData["ErrorMessage"]);
            }
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var venda = await _context.Venda.FindAsync(id);
                if (venda == null)
                    return NotFound("Venda não encontrada.");

                _context.Venda.Remove(venda);
                int affectedRows = await _context.SaveChangesAsync();

                if (affectedRows == 0)
                    return StatusCode(500, "Erro ao excluir a venda.");

                return Ok();
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Erro ao excluir venda. Tente novamente mais tarde.";
                Console.WriteLine($"Erro ao excluir venda: {ex.Message}");
                return StatusCode(500, TempData["ErrorMessage"]);
            }
        }
    }
}
