namespace SikkimGov.Platform.Business.Common.Contracts
{
    public interface ICryptoService
    {
        string Encrypt(string input);

        string Decrypt(string input);
    }
}
