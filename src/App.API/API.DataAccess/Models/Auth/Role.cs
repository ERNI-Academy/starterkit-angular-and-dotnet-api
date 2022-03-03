using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

using System.Runtime.Serialization;

namespace App.API.DataAccess.Models.Auth;
/// <summary>
/// The roles a user can have.
/// </summary>
[JsonConverter(typeof(StringEnumConverter))]
public enum Role
{
    /// <summary>
    /// Operator Role.
    /// </summary>
    [EnumMember(Value = "Operator")]
    Operator,

    /// <summary>
    /// Maintenance Role.
    /// </summary>
    [EnumMember(Value = "Maintenance")]
    Maintenance,

    /// <summary>
    /// Commissioning Role.
    /// </summary>
    [EnumMember(Value = "Commissioning")]
    Commissioning,

    /// <summary>
    /// Admininistrator Role.
    /// </summary>
    [EnumMember(Value = "Admin")]
    Admin,
}
