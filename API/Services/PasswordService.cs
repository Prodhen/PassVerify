using System;
using System.Security.Cryptography;
using System.Text;

namespace API.Services;

public class PasswordService:IPasswordService
{
 
 private readonly HttpClient _httpClient;

    public PasswordService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<bool> CheckCompromised(string password)
    {
        if (string.IsNullOrWhiteSpace(password))
            return false;

        // Check from most reliable to least reliable sources
        var checks = new List<Task<bool>>
        {
            IsPasswordPwnedAsync(password),
            IsInSecListsTop10kAsync(password),
            IsInPwnedPasswordsMirrorAsync(password)
        };

   
        var results = await Task.WhenAll(checks);
        return results.Any(result => result);
    }

    private async Task<bool> IsPasswordPwnedAsync(string password)
    {
        try
        {
            string hash = GetSHA1(password);
            string prefix = hash[..5];
            string suffix = hash[5..];

    
            await Task.Delay(1500);
            
            var response = await _httpClient.GetStringAsync($"https://api.pwnedpasswords.com/range/{prefix}");
            return response.Split('\n', StringSplitOptions.RemoveEmptyEntries)
                          .Any(line => line.StartsWith(suffix, StringComparison.OrdinalIgnoreCase));
        }
        catch
        {
            return false;
        }
    }

    private async Task<bool> IsInSecListsTop10kAsync(string password)
    {
        try
        {
            var response = await _httpClient.GetStringAsync(
                "https://raw.githubusercontent.com/danielmiessler/SecLists/master/Passwords/Common-Credentials/10-million-password-list-top-10000.txt");
            
            return response.Split('\n', StringSplitOptions.RemoveEmptyEntries)
                          .Select(p => p.Trim())
                          .Contains(password, StringComparer.OrdinalIgnoreCase);
        }
        catch
        {
            return false;
        }
    }

    private async Task<bool> IsInPwnedPasswordsMirrorAsync(string password)
    {
        try
        {
            var response = await _httpClient.GetStringAsync(
                "https://raw.githubusercontent.com/OWASP/passfault/master/wordlists/wordlists/10k-worst-passwords.txt");
            
            return response.Split('\n', StringSplitOptions.RemoveEmptyEntries)
                          .Select(p => p.Trim())
                          .Contains(password, StringComparer.OrdinalIgnoreCase);
        }
        catch
        {
            return false;
        }
    }

    private static string GetSHA1(string input)
    {
        using var sha1 = SHA1.Create();
        byte[] bytes = sha1.ComputeHash(Encoding.UTF8.GetBytes(input));
        return BitConverter.ToString(bytes).Replace("-", "").ToUpper();
    }
    public bool CheckStrength(string password)
    {
        return password.Length >= 8 &&
               password.Any(char.IsUpper) &&
               password.Any(char.IsLower) &&
               password.Any(char.IsDigit) &&
               password.Any(c => !char.IsLetterOrDigit(c));
    }

    public string GetStrengthFeedback(string password)
    {
   var feedback = new List<string>();

    if (password.Length < 8)
        feedback.Add("at least 8 characters");
    if (!password.Any(char.IsUpper))
        feedback.Add("an uppercase letter");
    if (!password.Any(char.IsLower))
        feedback.Add("a lowercase letter");
    if (!password.Any(char.IsDigit))
        feedback.Add("a number");
    if (!password.Any(ch => "!@#$%^&*()_+-=[]{}|;:,.<>?".Contains(ch)))
        feedback.Add("a special character");

    if (feedback.Count == 0)
        return "Your password is strong.";

    return "Your password is weak. Please include " + string.Join(", ", feedback) + ".";
    }


  

}
