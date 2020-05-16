namespace SikkimGov.Platform.Common.Security.Contracts
{
    public interface ICryptoService
    {
        string Encrypt(string input);

        string Decrypt(string input);
    }
}
