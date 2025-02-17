using Microsoft.AspNetCore.Mvc;
using ProjetoVenda.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoVendas.Controllers
{
    public class ClienteController : Controller
    {
        private readonly VendasDbContext _context;

        public ClienteController(VendasDbContext context)
        {
            _context = context;
        }

        public IActionResult Index(string search)
        {
            return View("~/Views/Importacao/Cliente.cshtml");
        }

        [HttpGet]
        public JsonResult GetClientes(string search)
        {
            try
            {
                var clientes = string.IsNullOrEmpty(search)
                    ? _context.Cliente.ToList()
                    : _context.Cliente.Where(c => c.nmCliente.Contains(search)).ToList();

                return Json(clientes);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Erro ao buscar clientes. Verifique a conexão ou tente novamente mais tarde.";
                Console.WriteLine($"Erro ao buscar clientes: {ex.Message}");
                return Json(new { error = TempData["ErrorMessage"] });
            }
        }

        [HttpPost]
        public IActionResult Create([FromBody] Cliente cliente)
        {
            try
            {
                if (cliente == null || string.IsNullOrWhiteSpace(cliente.nmCliente) || string.IsNullOrWhiteSpace(cliente.cidade))
                    return BadRequest("Nome e Cidade são obrigatórios.");

                _context.Cliente.Add(cliente);
                _context.SaveChanges();
                return Ok();
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Erro ao criar cliente. Tente novamente mais tarde.";
                Console.WriteLine($"Erro ao criar cliente: {ex.Message}");
                return StatusCode(500, TempData["ErrorMessage"]);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit([FromBody] Cliente cliente)
        {
            try
            {
                if (cliente == null || cliente.idCliente == 0) return BadRequest("Dados inválidos");

                var existingCliente = _context.Cliente.Find(cliente.idCliente);
                if (existingCliente == null) return NotFound();

                existingCliente.nmCliente = cliente.nmCliente;
                existingCliente.cidade = cliente.cidade;

                _context.Update(existingCliente);
                await _context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Erro ao editar cliente. Tente novamente mais tarde.";
                Console.WriteLine($"Erro ao editar cliente: {ex.Message}");
                return StatusCode(500, TempData["ErrorMessage"]);
            }
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var cliente = _context.Cliente.Find(id);
                if (cliente == null)
                    return NotFound();

                _context.Cliente.Remove(cliente);
                await _context.SaveChangesAsync();

                return Ok();
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Erro ao excluir cliente. Tente novamente mais tarde.";
                Console.WriteLine($"Erro ao excluir cliente: {ex.Message}");
                return StatusCode(500, TempData["ErrorMessage"]);
            }
        }
    }
}
