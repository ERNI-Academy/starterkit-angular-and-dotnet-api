using System.Collections.Generic;


using App.API.DataAccess.Models.Auth;
using App.API.Helpers;
using App.API.Services.Auth;
using App.API.Services.Auth.ApiModels;
using App.API.Services.CustomExceptions;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using AuthorizeAttribute = App.API.Helpers.AuthorizeAttribute;

namespace App.API.Controllers;

/// <summary>
/// The User controller hanlde the logic to manage the user accounts into the api.
/// </summary>
[Route("users")]
[AllowAnonymous]
[ApiController]
public class UsersController : BaseController
{
    /// <summary>
    /// The user service.
    /// </summary>
    private readonly IUserService userService;

    /// <summary>
    /// Initializes a new instance of the <see cref="UsersController"/> class.
    /// </summary>
    /// <param name="userService">
    /// The user service.
    /// </param>
    public UsersController(IUserService userService)
    {
        this.userService = userService;
    }

    /// <summary>
    /// Get all users registered. Only users with admin role can perform this action.
    /// </summary>
    /// <returns>The list of all users registered and its detailed information.</returns>
    /// <response code="200">Returns list of all users registered and its detailed information.</response>
    /// <response code="401">If the requester user is unauthorized to perform this action.</response>  
    /// <response code="500">An internal error occurred.</response>  
    [Authorize(Role.Admin)]
    [HttpGet]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
    public ActionResult<IEnumerable<UserResponse>> GetAll()
    {
        var users = userService.GetAll();
        return Ok(users);
    }

    /// <summary>
    /// Get the user information by its identifier. Only authenticated users can perform this action.
    /// </summary>
    /// <param name="id" example="1">The user identifier.</param>
    /// <returns>The detailed information about the specified user.</returns>
    /// <response code="200">he detailed information about the specified user.</response>
    /// <response code="401">If the requester user is unauthorized to perform this action.</response>  
    /// <response code="404">If the user id was not found.</response>  
    /// <response code="500">An internal error occurred.</response>  
    [Authorize]
    [HttpGet("{id:int}")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
    public ActionResult<UserResponse> GetById(int id)
    {
        var requester = userService.GetUser(RequesterId);

        // users can get their own user and admins can get any user
        if (id != requester.Id && requester.Role != Role.Admin)
        {
            throw new UnauthorizedException();
        }

        var user = userService.GetUserById(id);
        return Ok(user);
    }

    /// <summary>
    /// Register a new user into the system, only users with admin role can create new users.
    /// </summary>
    /// <param name="requestData">The user information to create.</param>
    /// <remarks>
    /// Sample request:
    /// 
    ///     POST /users/
    ///     {
    ///         "username": "dojo",
    ///         "firstName": "John",
    ///         "lastName": "Doe",
    ///         "role":  "Operator",
    ///         "password": "Test123."
    ///     }     
    /// </remarks>
    /// <returns>The  information for the new user.</returns>
    /// <response code="201">If the user was created.</response>
    /// <response code="400">If the username its already registered.</response>
    /// <response code="401">If the requester user is unauthorized to perform this action.</response>  
    /// <response code="500">An internal error occurred.</response>  
    [Authorize(Role.Admin)]
    [HttpPost]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
    public ActionResult<UserResponse> Create(CreateUserRequest requestData)
    {
        var user = userService.Create(requestData);
        return Created(string.Empty, user);
    }

    /// <summary>
    /// Update the information of a specified user. Only authenticated users can perform this action.
    /// </summary> 
    /// <remarks>
    /// Sample request:
    /// 
    ///     PUT /users/{id}
    ///     {
    ///         "username": "dojo",
    ///         "firstName": "John",
    ///         "lastName": "Doe",
    ///         "role": "Operator",
    ///         "password": "Test123."
    ///     }     
    /// </remarks>
    /// <param name="id" example="1">The user identifier.</param>
    /// <param name="requestData">The user information to update.</param>
    /// <returns>The user information updated</returns>
    /// <response code="202">If the user was update successfully.</response>
    /// <response code="400">If the username its already registered.</response>
    /// <response code="401">If the requester user is unauthorized to perform this action.</response>  
    /// <response code="404">If user id was not found.</response>  
    /// <response code="403">If is changing the username for a Default user.</response>  
    /// <response code="500">An internal error occurred.</response>  
    [Authorize]
    [HttpPut("{id:int}")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status202Accepted)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
    public ActionResult<UserResponse> Update(int id, UpdateUserRequest requestData)
    {
        var requester = userService.GetUser(RequesterId);

        // users can update their own user and admins can update any user
        if (id != requester.Id && requester.Role != Role.Admin)
        {
            throw new ForbiddenException("Only users with admin role are allowed to change the information for other users.");
        }

        // only admins can update role
        if (requester.Role != Role.Admin)
        {
            requestData.Role = userService.GetUser(id).Role;
        }

        var user = userService.Update(id, requestData);
        return Accepted(user);
    }

    /// <summary>
    /// Reset the user password. Only authenticated users can perform this action.
    /// </summary>
    /// <remarks>
    /// Sample request:
    /// 
    ///     POST /users/reset-password
    ///     {
    ///         "oldPassword": "Test123."
    ///         "newPassword": "NewPassword"
    ///     }
    /// </remarks>
    /// <param name="requestData">The passeord reset data.</param>
    /// <returns>If the user was update successfully.</returns>
    /// <response code="202">If password was update successfully.</response>
    /// <response code="400">If old password does not match.</response>
    /// <response code="401">If the requester user is unauthorized to perform this action.</response>
    /// <response code="404">If user id was not found.</response>  
    /// <response code="500">An internal error occurred.</response>  
    [Authorize]
    [HttpPost("reset-password")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status202Accepted)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
    public IActionResult ResetPassword(ResetPasswordRequest requestData)
    {
        userService.ResetPassword(RequesterId, requestData);
        return Accepted();
    }

    /// <summary>
    /// Reset the user password, only users with admin role can create new users. 
    /// </summary>
    /// <remarks>
    /// Sample request:
    /// 
    ///     POST /users/reset-password
    ///     {
    ///         "username": "dojo",
    ///         "newPassword": "NewPassword"
    ///     }
    /// </remarks>
    /// <param name="requestData">The passeord reset data.</param>
    /// <returns>If the user was update successfully.</returns>
    /// <response code="202">If the user was update successfully.</response>
    /// <response code="401">If the requester user is unauthorized to perform this action.</response>  
    /// <response code="404">If the user name  was not found.</response>  
    /// <response code="500">An internal error occurred.</response>  
    [Authorize(Role.Admin)]
    [HttpPost("reset-password-admin")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status202Accepted)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
    public IActionResult ResetPassword(ResetPasswordAdminRequest requestData)
    {
        userService.ResetPassword(requestData);
        return Accepted();
    }

    /// <summary>
    /// Delete the current logged user or if the current user has admin role, it deletes the specified user.
    /// </summary>
    /// <param name="id" example="1">The user identifier.</param>
    /// <returns>If the user was deleted successfully.</returns>
    /// <response code="202">If the user was deleted successfully.</response>
    /// <response code="401">If the requester user is unauthorized to perform this action.</response>  
    /// <response code="403">If deleting default users or a different user than the logged.</response>  
    /// <response code="404">If the user id was not found.</response>  
    /// <response code="500">An internal error occurred.</response>  
    [Authorize]
    [HttpDelete("{id:int}")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status202Accepted)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
    public IActionResult Delete(int id)
    {
        var requester = userService.GetUser(RequesterId);

        // users can delete their own user and admins can delete any user
        if (id != requester.Id && requester.Role != Role.Admin)
        {
            throw new ForbiddenException("Only admin users can delete other users.");
        }

        userService.Delete(id);
        return Accepted();
    }
}
