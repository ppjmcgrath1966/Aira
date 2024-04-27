using Aira.Mvc.Models;
using System.Diagnostics;
using Aira.Application.Features.Continent.DTOs;
using Aira.Application.Wrappers;
using Aira.Mvc.Abstractions;

namespace Aira.Mvc.Controllers;

public class HomeController : BaseController<HomeController>
{
    private readonly ILogger<HomeController> _logger;
    private readonly IHttpClientFactory _httpClientFactory;

    public HomeController(ILogger<HomeController> logger, IHttpClientFactory httpClientFactory)
    {
        _logger = logger;
        _httpClientFactory = httpClientFactory;
    }

    public IActionResult Index()
    {
        return View();
    }

    public async Task<IActionResult> Continents()
    {
        var id = 1;
        var apiClient = _httpClientFactory.CreateClient(HttpFactoryClients.AiraApi.ToString());
        var employee = await apiClient.GetFromJsonAsync<ApiResponse<ContinentDto>>($"/Continent/{id}");
        return View(employee.Data);


    }





    public async Task<IActionResult> Privacy()
    {
        var apiClient = _httpClientFactory.CreateClient(HttpFactoryClients.AiraApi.ToString());
        var employee = await apiClient.GetFromJsonAsync<ApiResponse<List<ContinentDto>>>("Continent/GetContinents");
        var ttt = employee.Data;


        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}