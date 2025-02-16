using Microsoft.AspNetCore.Mvc;
using ProjetoVenda.Models;

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
            var clientes = string.IsNullOrEmpty(search)
                ? _context.Cliente.ToList()
                : _context.Cliente.Where(c => c.nmCliente.Contains(search)).ToList();

            return Json(clientes);
        }


        public IActionResult Create() => View();

        [HttpPost]
        public IActionResult Create([FromBody] Cliente cliente)
        {
            if (cliente == null || string.IsNullOrWhiteSpace(cliente.nmCliente) || string.IsNullOrWhiteSpace(cliente.cidade))
                return BadRequest("Nome e Cidade são obrigatórios.");

            _context.Cliente.Add(cliente);
            _context.SaveChanges();
            return Ok();
        }


        [HttpPost]
        public async Task<IActionResult> Edit([FromBody] Cliente cliente)
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


        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cliente = _context.Cliente.Find(id);
            if (cliente != null)
            {
                _context.Cliente.Remove(cliente);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
