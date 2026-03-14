using Microsoft.AspNetCore.Mvc;
using TodoApi.Models;
using TodoApi.Services;
using Microsoft.AspNetCore.Authorization;

namespace TodoApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TodoController : ControllerBase
{
    private readonly TodoService _todoService;

    public TodoController(TodoService todoService)
    {
        _todoService = todoService;
    }

    // 🔹 Récupérer tous les todos (utilisateur connecté)
    [Authorize]
    [HttpGet]
    public ActionResult<List<Todo>> Get()
    {
        return _todoService.Get();
    }

    // 🔹 Ajouter un todo (admin seulement)
    [Authorize(Roles = "admin")]
    [HttpPost]
    public IActionResult Create(Todo todo)
    {
        _todoService.Create(todo);
        return Ok(todo);
    }

    // 🔹 Supprimer un todo (admin seulement)
    [Authorize(Roles = "admin")]
    [HttpDelete("{id}")]
    public IActionResult Delete(string id)
    {
        _todoService.Delete(id);
        return Ok(new { message = "Todo supprimé avec succès" });
    }
}