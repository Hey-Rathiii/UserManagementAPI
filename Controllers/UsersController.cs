using Microsoft.AspNetCore.Mvc;
using UserManagementAPI.Models;

namespace UserManagementAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private static readonly List<User> Users =
    [
        new User { Id = 1, Name = "Ayush", Email = "ayush@example.com" }
    ];

    [HttpGet]
    public IActionResult GetUsers() => Ok(Users);

    [HttpGet("{id}")]
    public IActionResult GetUser(int id)
    {
        var user = Users.FirstOrDefault(x => x.Id == id);
        return user is null ? NotFound() : Ok(user);
    }

    [HttpPost]
    public IActionResult CreateUser(User user)
    {
        user.Id = Users.Max(x => x.Id) + 1;
        Users.Add(user);
        return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateUser(int id, User updatedUser)
    {
        var user = Users.FirstOrDefault(x => x.Id == id);
        if (user is null) return NotFound();

        user.Name = updatedUser.Name;
        user.Email = updatedUser.Email;

        return Ok(user);
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteUser(int id)
    {
        var user = Users.FirstOrDefault(x => x.Id == id);
        if (user is null) return NotFound();

        Users.Remove(user);
        return NoContent();
    }
}
