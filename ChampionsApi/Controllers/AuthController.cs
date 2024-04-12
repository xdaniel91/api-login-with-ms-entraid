using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ChampionsApi.Controllers;

[Route("[controller]")]
[ApiController]
[AllowAnonymous]
public class AuthController : ControllerBase
{
    [HttpPost("login")]
    public IActionResult Login()
    {
        // Lógica para lidar com o login personalizado

        string clientId = "48bd4bdd-b47c-4081-96c4-cf49de0f10c2";
        string redirectUrl = "https://localhost:7149/Ping";
        string scope = "access_as_user";

        string microsoftLoginUrl = $"https://login.microsoftonline.com/common/oauth2/v2.0/authorize?client_id={clientId}" +
                                   "&response_type=code" +
                                   $"&redirect_uri={redirectUrl}" +
                                   $"&scope={scope}";

        string script = $"<script>window.open('{microsoftLoginUrl}', '_blank');</script>";

        // Retorna o script como um ContentResult
        return Content(script, "text/html");
    }

}
