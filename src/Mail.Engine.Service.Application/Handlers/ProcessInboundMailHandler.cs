using Mail.Engine.Service.Application.Mapper;
using Mail.Engine.Service.Application.Queries;
using Mail.Engine.Service.Application.Response;
using Mail.Engine.Service.Core.Entities;
using Mail.Engine.Service.Core.Repositories;
using Mail.Engine.Service.Core.Results;
using Mail.Engine.Service.Core.Services.InboundMail;
using MailKit;
using MailKit.Net.Imap;
using MediatR;

namespace Mail.Engine.Service.Application.Handlers
{
    public class ProcessInboundMailHandler(
        IMailRepository repository,
        IEmailProcessor processor,
        IEmailAuthenticator authenticator,
        IEmailFolderManager folderManager

    ) : IRequestHandler<GetInboundQuery, MailResponse>
    {
        private readonly IMailRepository _repository = repository;
        private readonly IEmailProcessor _processor = processor;
        private readonly IEmailAuthenticator _authenticator = authenticator;
        private readonly IEmailFolderManager _folderManager = folderManager;

        private const string MOVE_FOLDER_NAME = "WalletyRead";

        public async Task<MailResponse> Handle(GetInboundQuery request, CancellationToken cancellationToken)
        {
            var result = new MailResult();

            var mailboxes = await _repository.GetMailboxes();

            foreach (var mailbox in mailboxes)
            {
                var mailboxResult = await GetMailsAsync(mailbox);
                result.TotalMessagesProcessed += mailboxResult.TotalMessagesProcessed;
                result.TotalMessagesFailed += mailboxResult.TotalMessagesFailed;
                result.ErrorMessages.AddRange(mailboxResult.ErrorMessages);
            }

            var response = LazyMapper.Mapper.Map<MailResponse>(result);

            return response;
        }

        private async Task<MailResult> GetMailsAsync(MailboxEntity mailbox)
        {
            var result = new MailResult();

            using var client = new ImapClient();

            client.ServerCertificateValidationCallback = (s, c, h, e) => true;

            await client.ConnectAsync(mailbox.Imap, mailbox.ImapPort, mailbox.ImapSsl);

            await _authenticator.AuthenticateAsync(client, mailbox);

            try
            {
                // The Inbox folder is always available on all IMAP servers...
                var inbox = client.GetFolder(new FolderNamespace('/', "INBOX"));
                await inbox.OpenAsync(FolderAccess.ReadWrite);

                // Logic here to move to another folder.
                var root = client.GetFolder(client.PersonalNamespaces[0]);
                var destinationFolder = await _folderManager.GetOrCreateFolderAsync(root, MOVE_FOLDER_NAME);

                var messages = await inbox.FetchAsync(0, -1, MessageSummaryItems.UniqueId | MessageSummaryItems.Flags);

                foreach (var message in messages)
                {
                    try
                    {
                        bool isProcessed = await _processor.ProcessEmailAsync(message, inbox, destinationFolder, mailbox);
                        result.TotalMessagesProcessed++;

                        if (!isProcessed)
                            result.TotalMessagesFailed++;
                    }
                    catch (Exception ex)
                    {
                        result.TotalMessagesFailed++;
                        result.ErrorMessages.Add(ex.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                result.ErrorMessages.Add(ex.Message);
            }
            finally
            {
                await client.DisconnectAsync(true);
            }

            return result;
        }
    }
}