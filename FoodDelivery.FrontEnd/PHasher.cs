using System.Security.Cryptography;
using System.Text;


namespace FoodDelivery.FrontEnd;

public static class PHasher
{

    public static string HashPassword(string password)
    {
        SHA256 hash = SHA256.Create();
        var passwordBtytes = Encoding.Default.GetBytes(password);
        var hashedPassword = hash.ComputeHash(passwordBtytes);

        return Convert.ToHexString(hashedPassword);
    }

}