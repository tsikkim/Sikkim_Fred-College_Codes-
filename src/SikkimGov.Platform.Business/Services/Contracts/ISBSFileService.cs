namespace SikkimGov.Platform.Business.Services.Contracts
{
    public interface ISBSFileService
    {
        void ProcessSBSFile(SBSFileType fileType, string filePath);
    }
}
