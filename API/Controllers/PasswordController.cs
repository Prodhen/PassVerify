using System.Threading.Tasks;
using API.Models;
using API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PasswordController : ControllerBase
    {

     private readonly IPasswordService _passwordService;

    public PasswordController(IPasswordService passwordService)
    {
        _passwordService = passwordService;
    }

   [HttpPost("verify")]
public async Task<ActionResult<PasswordResponseModel>> VerifyPassword([FromBody] PasswordRequestModel request)
{
    var strength = _passwordService.GetStrengthFeedback(request.Password);
    var isCompromised = await _passwordService.CheckCompromised(request.Password);

    var response = new PasswordResponseModel
    {
  
        StrengthFeedback = strength,
        IsCompromised = isCompromised,
        CompromisedFeedback = isCompromised 
            ? "❌ This password has appeared in known data breaches. Please choose a more secure one."
            : "✅ This password has not been found in common breaches."
    };
    return Ok(response);
}

    // This could be expanded with more logic for strength checks and compromised checks
    private bool CheckPasswordStrength(string password)
    {
        // Implement your strength check logic (e.g., minimum length, complexity, etc.)
        return password.Length >= 8; // Sample simple check
    }

    private bool CheckIfCompromised(string password)
    {
        // Simulate checking the password against a compromised list or API
        // (You could integrate with a service like "Have I Been Pwned")
        return false; // Simulated check
    }

    private string GetPasswordStrengthFeedback(string password)
    {
        // Provide feedback based on strength check
        if (password.Length < 8) return "Password is too short.";
        return "Password is strong.";
    }
}
}