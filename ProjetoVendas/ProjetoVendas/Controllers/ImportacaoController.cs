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

            TempData["SuccessMessage"] = "Importação concluída com sucesso!";
        }
        catch (HttpRequestException ex)
        {
            TempData["ErrorMessage"] = "Erro ao conectar à API externa. Verifique sua conexão.";
            Console.WriteLine($"Erro de conexão: {ex.Message}");
        }
        catch (Exception ex)
        {
            TempData["ErrorMessage"] = "Ocorreu um erro inesperado durante a importação.";
            Console.WriteLine($"Erro inesperado: {ex.Message}");
        }

        return RedirectToAction("Index");
    }

    public IActionResult Index()
    {
        return View();
    }
}
