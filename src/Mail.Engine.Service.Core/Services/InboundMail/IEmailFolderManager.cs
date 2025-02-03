using MailKit;

namespace Mail.Engine.Service.Core.Services.InboundMail
{
    public interface IEmailFolderManager
    {
        Task<IMailFolder> GetOrCreateFolderAsync(IMailFolder root, string folderName);

    }
}