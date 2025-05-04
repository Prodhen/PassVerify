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

 
   
}
}