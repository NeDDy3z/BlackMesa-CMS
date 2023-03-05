using System.Security.Cryptography;
using System.Text;

namespace DB_Projekt;

//I will not be doing this.
//So here's the sós, bb.
//source = https://stackoverflow.com/questions/9031537/really-simple-encryption-with-c-sharp-and-symmetricalgorithm
public class PasswordEncryption
{
    //key
    private static readonly byte[] key = new byte[8] { 1, 2, 3, 4, 5, 6, 7, 8 };
    private static readonly byte[] iv = new byte[8] { 1, 2, 3, 4, 5, 6, 7, 8 };

    /// <summary>
    /// Encrypts string
    /// </summary>
    /// <returns>
    /// Encrypted string of characters
    /// </returns>
    public static string Enrypt(string text)
    {
        SymmetricAlgorithm algorithm = DES.Create();
        var transform = algorithm.CreateEncryptor(key, iv);
        var inputbuffer = Encoding.Unicode.GetBytes(text);
        var outputBuffer = transform.TransformFinalBlock(inputbuffer, 0, inputbuffer.Length);
        return Convert.ToBase64String(outputBuffer);
    }

    /// <summary>
    /// Decrypts string
    /// </summary>
    /// <returns>
    /// Decrypted string of characters
    /// </returns>
    public static string Decrypt(string text)
    {
        SymmetricAlgorithm algorithm = DES.Create();
        var transform = algorithm.CreateDecryptor(key, iv);
        var inputbuffer = Convert.FromBase64String(text);
        var outputBuffer = transform.TransformFinalBlock(inputbuffer, 0, inputbuffer.Length);
        return Encoding.Unicode.GetString(outputBuffer);
    }
}