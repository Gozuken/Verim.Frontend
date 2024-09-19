namespace Verim.Frontend.Models;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using System.Text;

public class RegisterCredentials
{
    [Required(ErrorMessage ="Mail Boş Bırakılamaz.")][StringLength(50)]
    public string? Mail {get; set;}
    [Required(ErrorMessage ="Kullanıcı adı Boş Bırakılamaz.")][StringLength(50)]
    public string? Username {get; set;}
    [Required(ErrorMessage ="Şifre Boş Bırakılamaz.")][StringLength(50)]
    public string? Password {get; set;}

    public string Hash(string str)
    {
        using var sha256 = SHA256.Create();
        var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(str));
        return BitConverter.ToString(bytes).Replace("-", "").ToLowerInvariant();
    }
}

public class LoginCredentials
{
    [Required(ErrorMessage ="Kullanıcı adı Boş Bırakılamaz.")][StringLength(50)]
    public string? Username {get; set;}
    [Required(ErrorMessage ="Şifre Boş Bırakılamaz.")][StringLength(50)]
    public string? Password {get; set;}

    public string Hash(string str)
    {
        using var sha256 = SHA256.Create();
        var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(str));
        return BitConverter.ToString(bytes).Replace("-", "").ToLowerInvariant();
    }
}
