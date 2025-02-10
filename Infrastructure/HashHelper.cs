public static class HashHelper
{
    public static string HashPassword(string password)
    {
        using var sha512 = System.Security.Cryptography.SHA512.Create();
        var hashBytes = sha512.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
        return Convert.ToBase64String(hashBytes);
    }
}