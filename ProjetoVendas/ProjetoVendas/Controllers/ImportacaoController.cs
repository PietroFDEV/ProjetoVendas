using Microsoft.AspNetCore.Mvc;
using ProjetoVenda.Models;
using ProjetoVenda.Services;
using System.Diagnostics;

public class ImportacaoController : Controller
{
    private readonly ImportacaoService _dataImportService;

    public ImportacaoController(ImportacaoService dataImportService)
    {
        _dataImportService = dataImportService;
    }

    public async Task<ActionResult> ImportData()
    {
        try
        {
            await _dataImportService.ImportClienteAsync();
            await _dataImportService.ImportProdutoAsync();
            await _dataImportService.ImportVendaAsync();

            TempData["SuccessMessage"] = "Importa��o conclu�da com sucesso!";
        }
        catch (HttpRequestException ex)
        {
            TempData["ErrorMessage"] = "Erro ao conectar � API externa. Verifique sua conex�o.";
            Console.WriteLine($"Erro de conex�o: {ex.Message}");
        }
        catch (Exception ex)
        {
            TempData["ErrorMessage"] = "Ocorreu um erro inesperado durante a importa��o.";
            Console.WriteLine($"Erro inesperado: {ex.Message}");
        }

        return RedirectToAction("Index");
    }

    public IActionResult Index()
    {
        return View();
    }
}
