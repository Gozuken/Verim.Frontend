using Microsoft.AspNetCore.Http.HttpResults;
using Verim.Frontend.Models;

namespace Verim.Frontend.Clients;

public class CredentialsClient(HttpClient httpClient)
{

    public async Task<AuthenticationResult> Authenticate(LoginCredentials credentials)
    {
        try
        {
            var response = await httpClient.PostAsJsonAsync("http://localhost:5197/login", credentials);

            if (response.IsSuccessStatusCode)
            {
                // Extract the 'Set-Cookie' header
                if (response.Headers.TryGetValues("Set-Cookie", out var setCookieValues))
                {
                    var authTokenCookie = setCookieValues.FirstOrDefault(cookie => cookie.StartsWith("AuthToken="));
                    if (authTokenCookie != null)
                    {
                        var token = authTokenCookie.Split(';').First().Split('=')[1];
                        return new AuthenticationResult
                        {
                            IsValid = true,
                            Token = token
                        };
                    }
                    else
                    {
                        return new AuthenticationResult
                        {
                            IsValid = false,
                            Token = string.Empty,
                            ErrorMessage = "Failed to split token"
                        };
                    }
                }
                else
                {
                    return new AuthenticationResult
                    {
                        IsValid = false,
                        Token = string.Empty,
                        ErrorMessage = "Failed to access token from header."
                    };
                }

            }            
            else
            {
                return new AuthenticationResult 
                    { IsValid = false, Token = string.Empty};
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error on Authenticate: {ex.Message}");
            return new AuthenticationResult 
                    { IsValid = false,Token = string.Empty};
        }
    }

    public async Task<AuthenticationResult> Register(RegisterCredentials credentials)
    {
        try
        {
            var response = await httpClient.PostAsJsonAsync("http://localhost:5197/register", credentials);

            if (response.IsSuccessStatusCode)
            {
                return new AuthenticationResult 
                    { IsValid = true, Token = string.Empty };
            }                               //must add token from the, var token = httpContext.Request.Cookies["AuthToken"];
            else
            {
                return new AuthenticationResult 
                    { IsValid = false,Token = string.Empty};
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error on Register: {ex.Message}");
            return new AuthenticationResult 
                    { IsValid = false,Token = string.Empty};
        }
    }

}

public class AuthenticationResult
{
    public bool IsValid { get; set; }
    public required string Token { get; set; }
    public string? ErrorMessage {get; set;}
}
