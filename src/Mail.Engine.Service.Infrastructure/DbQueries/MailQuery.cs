using Mail.Engine.Service.Core.Enum;

namespace Mail.Engine.Service.Infrastructure.DbQueries
{
    public class MailQuery
    {
        #region Select Queries
        public static string GetMailboxesQuery()
        {
            var query = $@"
                
                SELECT 

                    mb.id AS MailboxId,
                    mb.name,
                    mb.imap_id AS ImapId,
                    mb.smtp_id AS SmtpId,
                    mb.is_active AS IsActive,
                    
                    imap.name AS ImapName,
                    imap.imap,
                    imap.port AS ImapPort,
                    imap.ssl AS ImapSsl,
                    imap.username AS ImapUsername,
                    imap.password AS ImapPassword,
                    
                    smtp.name AS SmtpName,
                    smtp.smtp,
                    smtp.port AS SmtpPort,
                    smtp.ssl AS SmtpSsl,
                    smtp.username AS SmtpUsername,
                    smtp.password AS SmtpPassword,
                    smtp.is_outlook AS IsOutlook,
                    smtp.is_google AS IsGoogle,
                    smtp.app_id AS AppId,
                    smtp.tenant_id AS TenantId,
                    smtp.secret_id AS SecretId,
                    smtp.access_token AS AccessToken,
                    smtp.use_access_token AS UseAccessToken,
                    smtp.code_challange AS CodeChallange,
                    smtp.refresh_token AS RefreshToken

                FROM mail.mailbox mb
                LEFT JOIN mail.imap_configuration imap on imap.id = mb.imap_id
                LEFT JOIN mail.smtp_configuration smtp on smtp.id = mb.smtp_id

                WHERE mb.is_active = true

            ";

            return query;
        }

        public static string GetMessageLogsQuery()
        {
            var query = $@"
                
                SELECT 

                    m.id AS MessageLogId,
                    m.message_log_header_id AS MessageLogHeaderId,
                    m.from_field AS FromField,
                    m.from_name AS FromName,
                    m.to_field AS ToField,
                    m.cc_field AS CcField,
                    m.bcc_field AS BccField,
                    m.subject,
                    m.body,
                    m.message_log_type_id AS MessageLogTypeId,
                    m.message_log_status_id AS MessageLogStatusId,
                    m.date_sent AS DateSent,
                    m.status_message AS StatusMessage,
                    m.created_at AS CreatedAt,
                    m.created_by AS CreatedBy,

                    t.name AS MessageLogTypeName,
                    t.code AS MessageLogTypeCode,

                    s.name AS MessageLogStatusName,
                    s.code AS MessageLogStatusCode,
                    
                    sc.username AS SmtpUserName,
                    sc.id AS SmtpConfigurationId,
                    sc.smtp,
                    sc.ssl,
                    sc.port,
                    sc.email_address AS SmtpEmailAddress,
                    sc.password AS SmtpPassword,
                    sc.app_id AS AppId,
                    sc.tenant_id AS TenantId,
                    sc.secret_id AS SecretId

                FROM mail.message_log m

                LEFT JOIN mail.message_log_type t ON m.message_log_type_id = t.id
                LEFT JOIN mail.message_log_status s ON m.message_log_status_id = s.id
                LEFT JOIN mail.smtp_configuration sc ON m.smtp_id = sc.id


                WHERE t.code = '{EnumMessageTypeLog.EMAIL}' AND s.code = '{EnumMessageStatusLog.Pending}'

            ";

            return query;
        }

        public static string GetMessagesQuery()
        {
            var query = $@"
                
               SELECT

                    id AS MailMessageId,
                    mailbox_id AS MailboxId,
                    subject,
                    text_plain AS TextPlain,
                    text_html AS TextHtml,
                    format_text_html AS FormatTextHtml,
                    date_sent AS DateSent,
                    cc_field AS CcField,
                    bcc_field AS BccField,
                    sent_to AS SentTo,
                    sent_from AS SentFrom,
                    sent_from_display_name AS SentFromDisplayName,
                    sent_from_raw AS SentFromRaw,
                    imap_message_id AS ImapMessageId,
                    mime_version AS MimeVersion,
                    return_path AS ReturnPath,
                    created_by AS CreatedBy,
                    created_at AS CreatedAt,
                    is_deleted AS IsDeleted

                FROM mail.message
            ";

            return query;
        }

        public static string GetMessageAttachmentsQuery()
        {
            var query = $@"
                
                SELECT
                    
                    a.id AS MessageLogAttachmentId,
                    a.message_log_id AS MessageLogId,
                    a.attachment_url AS AttachmentUrl,
                    a.file_name AS FileName,
                    a.content_id AS ContentId,
                    a.is_inline_image AS IsInlineImage,
                    a.attachment_data AS AttachmentData

                FROM mail.message_log_attachment a
                
                LEFT JOIN mail.message_log m on a.message_log_id = m.id

                WHERE a.message_log_id = @p_mail_message_log_id
                
            ";

            return query;
        }
        #endregion

        #region Update Queries
        public static string UpdateStatusQuery()
        {
            var query = @" 

                UPDATE mail.message_log 
                SET 
                
                    message_log_status_id = sq.id,
                    date_sent = @p_mail_date_sent,
                    status_message = @p_mail_status_message,
                    from_field = @p_mail_from_field,
                    from_name = @p_mail_from_name
                
                FROM (SELECT id FROM mail.message_log_status WHERE code = @p_mail_message_log_status_code) AS sq

                WHERE mail.message_log.id = @p_mail_message_log_id;

			";

            return query;
        }
        #endregion

        #region Procedure Execution
        public static string MailboxInsertQuery()
        {
            var query = @"
               CALL mail.message_insert(
                    @p_is_error,
                    @p_return_record_id,
                    @p_result_message,
                    @p_object_name,
                    
                    @p_mailbox_id,
                    @p_subject,
                    @p_text_plain,
                    @p_text_html,
                    @p_format_text_html,
                    @p_date_sent,
                    @p_cc_field,
                    @p_bcc_field,
                    @p_sent_to,
                    @p_sent_from,
                    @p_sent_from_display_name,
                    @p_sent_from_raw,
                    @p_imap_message_id,
                    @p_mime_version,
                    @p_return_path,
                    @p_logged_in_user
                );
            ";
            return query;
        }


        public static string InReplyToInsertQuery()
        {
            var query = @"

                CALL mail.in_reply_to_insert(@p_mail_message_id, @p_in_reply_to_message_id);

            ";

            return query;
        }

        public static string ReferenceInsertQuery()
        {
            var query = @"

                CALL mail.reference_insert(@p_mail_message_id, @p_reference_message_id);

            ";

            return query;
        }
        #endregion
    }
}
