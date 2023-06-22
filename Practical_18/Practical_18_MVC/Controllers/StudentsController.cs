using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Practical18_MVC.ViewModels;
using System.Net.Http.Headers;
using System.Text;

namespace Practical18_MVC.Controllers;

[Route("students")]
public class StudentsController : Controller
{
    private readonly HttpClient _httpClient = new HttpClient();
    private readonly IMapper _mapper;

    public StudentsController(IConfiguration configuration, IMapper mapper)
    {
        _httpClient.BaseAddress = new Uri(configuration["AppSettings:BaseUrl"]);
        _mapper = mapper;
    }

    [HttpGet("")]
    public async Task<IActionResult> Index()
    {
        _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        HttpResponseMessage responseMessage = await _httpClient.GetAsync("api/v1/students");
        if (responseMessage.IsSuccessStatusCode)
        {
            var students = await responseMessage.Content.ReadFromJsonAsync<IEnumerable<StudentViewModel>>();
            return View(students);
        }
        return View();
    }

    [HttpGet("create")]
    public ActionResult Create()
    {
        return View();
    }

    [HttpPost("create")]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Create(CreateStudentViewModel studentViewModel)
    {
        if (!ModelState.IsValid)
        {
            return View();
        }
        HttpResponseMessage response = await _httpClient.PostAsJsonAsync<CreateStudentViewModel>("api/v1/students", studentViewModel);
        if (response.IsSuccessStatusCode)
        {
            if(response.Headers.TryGetValues("Location", out var locations))
            {
                if (locations is not null && locations.FirstOrDefault() is not null)
                {
                    var location = locations.FirstOrDefault();
                    var studentId = location?.Substring(location.LastIndexOf('/')+1);
                    return LocalRedirect($"/students/details/{studentId}");
                }
            }
        }
        return View();
    }

    [HttpGet("details/{id}")]
    public async Task<IActionResult> Details(Guid id)
    {
        _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        HttpResponseMessage responseMessage = await _httpClient.GetAsync($"api/v1/students/{id}");
        if (responseMessage.IsSuccessStatusCode)
        {
            var student = await responseMessage.Content.ReadFromJsonAsync<StudentViewModel>();
            return View(student);
        }
        return View("_NotFound", new ErrorViewModel { ErrorMessage = "No student found with this id" });
    }

    [HttpGet("edit/{id}")]
    public async Task<IActionResult> Edit(Guid id)
    {
        _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        HttpResponseMessage responseMessage = await _httpClient.GetAsync($"api/v1/students/{id}");
        if (responseMessage.IsSuccessStatusCode)
        {
            var student = await responseMessage.Content.ReadFromJsonAsync<StudentViewModel>();
            var studentViewModel = _mapper.Map<EditStudentViewModel>(student);
            return View(studentViewModel);
        }
        return View("_NotFound", new ErrorViewModel { ErrorMessage = "No student found with this id" });
    }

    [HttpPost("edit/{id}")]
    public async Task<IActionResult> Edit(Guid id, EditStudentViewModel editStudentViewModel)
    {
        if (!ModelState.IsValid)
        {
            return View();
        }
        _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        HttpResponseMessage responseMessage = await _httpClient.PutAsJsonAsync<EditStudentViewModel>($"api/v1/students/{id}",editStudentViewModel);
        if (responseMessage.IsSuccessStatusCode)
        {
            return LocalRedirect($"/students/details/{id}");
        }
        return View();
    }

    [HttpGet("delete/{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        HttpResponseMessage responseMessage = await _httpClient.GetAsync($"api/v1/students/{id}");
        if (responseMessage.IsSuccessStatusCode)
        {
            var student = await responseMessage.Content.ReadFromJsonAsync<StudentViewModel>();
            return View(student);
        }
        return View("_NotFound", new ErrorViewModel { ErrorMessage = "No student found with this id" });
    }

    [HttpPost("delete/{id}")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(Guid id, IFormCollection keyValuePairs)
    {
        _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        HttpResponseMessage responseMessage = await _httpClient.DeleteAsync($"api/v1/students/{id}");
        if (responseMessage.IsSuccessStatusCode)
        {
            return RedirectToAction("Index");
        }
        return View();
    }
}
