using System;

using App.API.Helpers;
using App.API.Services.Auth;
using App.API.Services.Auth.ApiModels;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AuthorizeAttribute = App.API.Helpers.AuthorizeAttribute;

namespace App.API.Controllers;

/// <summary>
/// The authentication controller handle the logic to login and logout the users into the API. 
/// </summary>
[Route("auth")]
[ApiController]
[AllowAnonymous]
public class AuthController : BaseController
{
    /// <summary>
    /// The authorization service.
    /// </summary>
    private readonly IAuthService authService;

    /// <summary>
    /// Initializes a new instance of the <see cref="AuthController"/> class.
    /// </summary>
    /// <param name="authService">
    /// The authorization service.
    /// </param>
    public AuthController(IAuthService authService)
    {
        this.authService = authService;
    }

    /// <summary>
    /// Allows a user to authenticate into the API. 
    /// </summary>
    /// <param name="model">Information of the user who wants to authenticate.</param>
    /// <remarks> 
    /// Sample request:
    /// 
    ///     POST /authenticate
    ///     {
    ///        "username": "oper",
    ///        "password": "Test123."
    ///     }
    /// </remarks>
    /// <returns>The user information and the JWT token to use in the authorized endpoints.</returns>
    /// <response code="200">Returns the user information and the JWT token to use in the authorized endpoints.</response>
    /// <response code="400">If the username or password is incorrect.</response>  
    /// <response code="500">An internal error occurred.</response>  
    [HttpPost("authenticate")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
    public ActionResult<AuthenticateResponse> Authenticate(AuthenticateRequest model)
    {
        var response = authService.Authenticate(model, IpAddress());
        SetTokenCookie(response.RefreshToken);
        return Ok(response);
    }

    /// <summary>
    /// Refresh the token for the current user. 
    /// </summary>
    /// <returns>The user information and the JWT token to use in the authorized endpoints</returns>
    /// <response code="200">Returns the user information and the JWT token to use in the authorized endpoints</response>
    /// <response code="500">An internal error occurred.</response>  
    [HttpPost("refresh-token")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
    public ActionResult<AuthenticateResponse> RefreshToken()
    {
        var refreshToken = Request.Cookies["refreshToken"];
        var response = authService.RefreshToken(refreshToken, IpAddress());
        SetTokenCookie(response.RefreshToken);
        return Ok(response);
    }

    /// <summary>
    /// Revoke the refresh token for the current user, acts like logout. 
    /// </summary>
    /// <param name="model">The refresh token that should be revoked. if it's not specified, it will automatically look for the "refresh-token" cookie in the request. </param>
    /// <remarks> 
    /// Sample request 1:
    /// 
    ///     POST /revoke-token
    ///     {
    ///        "token": "THIS-SHOULD-BE-THE-REFRESH-TOKEN-FROM-THE-COOKIE"
    ///     }
    /// 
    /// Sample request 2:
    /// 
    ///     POST /revoke-token
    ///     {
    ///     }
    /// </remarks>
    /// <returns>If the token was revoked.</returns>
    /// <response code="202">If the token was revoked.</response>
    /// <response code="400">If the token is empty in the cookie and in the request message.</response>  
    /// <response code="401">If the requester user is unauthorized to perform this action.</response>  
    /// <response code="500">An internal error occurred.</response>  
    [Authorize]
    [HttpPost("revoke-token")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status202Accepted)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
    public IActionResult RevokeToken(RevokeTokenRequest model)
    {
        // accept token from request body or cookie
        var token = model.Token ?? Request.Cookies["refreshToken"];

        if (string.IsNullOrEmpty(token))
        {
            throw new ApiException("Token is required");
        }

        authService.RevokeToken(token, RequesterId, IpAddress());

        return Accepted("Token revoked", null);
    }

    /// <summary>
    /// The set token cookie.
    /// </summary>
    /// <param name="token">
    /// The token.
    /// </param>
    private void SetTokenCookie(string token)
    {
        var cookieOptions = new CookieOptions
        {
            HttpOnly = true,
            Expires = DateTime.UtcNow.AddDays(7)
        };
        Response.Cookies.Append("refreshToken", token, cookieOptions);
    }

    /// <summary>
    /// The ip address.
    /// </summary>
    /// <returns>
    /// The <see cref="string"/>.
    /// </returns>
    private string IpAddress()
    {
        if (Request.Headers.ContainsKey("X-Forwarded-For"))
        {
            return Request.Headers["X-Forwarded-For"];
        }
        else
        {
            return HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
        }
    }
}
