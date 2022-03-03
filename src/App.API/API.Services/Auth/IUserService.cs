using System.Collections.Generic;

using App.API.DataAccess.Models.Auth;
using App.API.Services.Auth.ApiModels;

namespace App.API.Services.Auth;

/// <summary>
/// The UserService interface.
/// </summary>
public interface IUserService
{
    /// <summary>
    /// Get all users.
    /// </summary>
    /// <returns> The list of all users.</returns>
    IEnumerable<UserResponse> GetAll();

    /// <summary>
    /// Get a user.
    /// </summary>
    /// <param name="id">The id.</param>
    /// <returns>The <see cref="User"/>.</returns>
    User GetUser(int id);

    /// <summary>
    /// Get user by id.
    /// </summary>
    /// <param name="id">The id.</param>
    /// <returns>The <see cref="UserResponse"/>.</returns>
    UserResponse GetUserById(int id);

    /// <summary>
    /// Create a new user.
    /// </summary>
    /// <param name="model">The model.</param>
    /// <returns>The <see cref="UserResponse"/>.</returns>
    UserResponse Create(CreateUserRequest model);

    /// <summary>
    /// Update user data.
    /// </summary>
    /// <param name="id">The id.</param>
    /// <param name="model">The model.</param>
    /// <returns>The <see cref="UserResponse"/>.</returns>
    UserResponse Update(int id, UpdateUserRequest model);

    /// <summary>
    /// Reset password.
    /// </summary>
    /// <param name="requesterId">The requester id.</param>
    /// <param name="model">The model.</param>
    void ResetPassword(int requesterId, ResetPasswordRequest model);

    /// <summary>
    /// Reset password.
    /// </summary>
    /// <param name="model">The model.</param>
    void ResetPassword(ResetPasswordAdminRequest model);

    /// <summary>
    /// Delete a user.
    /// </summary>
    /// <param name="id">The id.</param>
    void Delete(int id);
}
