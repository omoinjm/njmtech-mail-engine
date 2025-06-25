using Mail.Engine.Service.Core.Enum;

namespace Mail.Engine.Service.Infrastructure.DbQueries
{
    public class WatiQuery
    {
        #region Select Queries
        public static string GetWatiConfigQuery()
        {
            var query = $@"
                
                SELECT

                    ""RecordId"",
                    ""Bearer"",
                    ""BaseUrl"",
                    ""ClientID"" AS ClientId

                FROM ""WatiConfig""

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


                WHERE t.code = '{EnumMessageTypeLog.WATI}' AND s.code = '{EnumMessageStatusLog.Pending}'

                ORDER BY m.created_at DESC
            ";

            return query;
        }
        #endregion

        #region Insert Queries
        public static string InsertJsonData()
        {
            var query = @" 

                INSERT INTO mail.message_log_wati_response (
                    message_log_id,
                    response_json
                )
                VALUES (
                    @p_message_log_id,
                    @p_response_json
                )

			";

            return query;
        }
        #endregion

    }
}
