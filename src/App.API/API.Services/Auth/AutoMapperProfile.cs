
using AutoMapper;

using App.API.DataAccess.Models.Auth;
using App.API.Services.Auth.ApiModels;

namespace App.API.Services.Auth;
/// <summary>
/// The auto mapper profile.
/// </summary>
public class AutoMapperProfile : Profile
{
    /// <summary>
    /// Initializes a new instance of the <see cref="AutoMapperProfile"/> class.
    /// </summary>
    public AutoMapperProfile()
    {
        CreateMap<User, UserResponse>();

        CreateMap<User, AuthenticateResponse>();

        CreateMap<CreateUserRequest, User>();

        CreateMap<UpdateUserRequest, User>()
            .ForAllMembers(x => x.Condition(
                (src, dest, prop) =>
                {
                    // ignore null & empty string properties
                    if (prop == null)
                    {
                        return false;
                    }

                    return prop.GetType() != typeof(string) || !string.IsNullOrEmpty((string)prop);
                }));
    }
}
