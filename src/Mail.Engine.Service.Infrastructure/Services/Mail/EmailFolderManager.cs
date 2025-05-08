using Mail.Engine.Service.Core.Services.Mail.InboundMail;
using MailKit;

namespace Mail.Engine.Service.Infrastructure.Services.InboundMail
{
    public class EmailFolderManager() : IEmailFolderManager
    {
        public async Task<IMailFolder> GetOrCreateFolderAsync(IMailFolder root, string folderName)
        {
            try
            {
                // NM: Try get folder, might not exist.
                return await Task.FromResult(root.GetSubfolder(folderName));
            }
            catch (Exception ex)
            {
                // NM: folder might not exist, so create when it fails
                return await Task.FromResult(root.Create(folderName, true));
            }
        }
    }
}
