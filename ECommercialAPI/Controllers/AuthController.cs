using ApplicationCore;
using ECommercialAPI.Constants;
using Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;

namespace ECommercialAPI.Controllers;

[Route("auth")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IUnitOfWork<EcommercialContext> _unitOfWork;
    public AuthController(IUnitOfWork<EcommercialContext> unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    [HttpGet(ApiEndPointConstant.Authentiction.AuthenticationEndpoint)]
    public async Task<IActionResult> Login()
    {
        return Ok(await _unitOfWork.GetRepository<Company>().GetListAsync());
    }
}