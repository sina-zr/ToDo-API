﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ToDoMinimalAPI.Endpoints;

public static class AuthenticationEndpoints
{
    public record AuthenticationData(string? Username, string? Password);
    public record UserData(int Id, string FirstName, string LastName, string Username);
        

    public static void AddAuthenicationEndpoint(this WebApplication app)
    {
        app.MapPost("/api/Authenticate", [AllowAnonymous] (IConfiguration config, [FromBody] AuthenticationData data) =>
        {
            var user = ValidateCredentials(data);

            if (user is null)
            {
                return Results.Unauthorized();
            }

            var token = GenerateToken(user, config);

            return Results.Ok(token);
        });
    }

    private static string GenerateToken(UserData user, IConfiguration config)
    {
        var secretKey = new SymmetricSecurityKey(
            Encoding.ASCII.GetBytes(
                config.GetValue<string>("Authentication:SecretKey")));

        var signingCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

        List<Claim> claims = new List<Claim>();
        claims.Add(new(JwtRegisteredClaimNames.Sub, user.Id.ToString()));
        claims.Add(new(JwtRegisteredClaimNames.UniqueName, user.Username));
        claims.Add(new(JwtRegisteredClaimNames.GivenName, user.FirstName));
        claims.Add(new(JwtRegisteredClaimNames.FamilyName, user.LastName));

        var token = new JwtSecurityToken(
            config.GetValue<string>("Authentication:Issuer"),
            config.GetValue<string>("Authentication:Audience"),
            claims,
            DateTime.UtcNow,
            DateTime.UtcNow.AddMinutes(1),
            signingCredentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    private static UserData? ValidateCredentials(AuthenticationData data)
    {
        // This is not Production Code - Replace with a call to your Auth system
        if (CompareValues(data.Username, "tcorey") &&
            CompareValues(data.Password, "1234"))
        {
            return new UserData(1, "Tim", "Corey", data.Username!);
        }
        if (CompareValues(data.Username, "sstorm") &&
            CompareValues(data.Password, "1234"))
        {
            return new UserData(2, "Sue", "Storm", data.Username!);
        }

        return null;
    }

    private static bool CompareValues(string? actual, string? expected)
    {
        if (actual is not null)
        {
            if (actual.Equals(expected))
            {
                return true;
            }
        }
        return false;
    }
}
