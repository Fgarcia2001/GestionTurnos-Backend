/*using GestionTurnos.Application.Abstraction.Infrastructure;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class TestController : ControllerBase
{
    private readonly IEmailService _emailService;

    public TestController(IEmailService emailService)
    {
        _emailService = emailService;
    }

    [HttpGet("send-test-email")]
    public async Task<IActionResult> SendTest()
    {
        await _emailService.SendAsync(
            "salinas.cristian15@gmail.com",
            "Prueba de Integración SendGrid",
            "Si recibes este mail, quiero que sepas q te fuiste dodgeado padreeeeeeeeeeeee", // plainText
            "<strong>SSi recibes este mail, quiero que sepas q te fuiste dodgeado padreeeeeeeeeeeee.</strong>" // htmlContent
        );

        return Ok("Correo enviado. ¡Revisa tu bandeja de entrada!");
    }
}*/