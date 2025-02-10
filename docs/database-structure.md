# Database Design Structure

```mermaid
erDiagram
    IMAP_CONFIGURATION {
        UUID id PK
        VARCHAR code
        VARCHAR name
        VARCHAR imap
        BOOLEAN ssl
        INT port
        VARCHAR username
        VARCHAR password
        BOOLEAN is_active
        TIMESTAMP created_at
        VARCHAR created_by
        TIMESTAMP modified_at
        VARCHAR modified_by
    }

    SMTP_CONFIGURATION {
        UUID id PK
        VARCHAR code
        VARCHAR name
        VARCHAR smtp
        BOOLEAN ssl
        INT port
        VARCHAR email_address
        VARCHAR username
        VARCHAR password
        VARCHAR from_name
        BOOLEAN is_active
        TIMESTAMP created_at
        VARCHAR created_by
        TIMESTAMP modified_at
        VARCHAR modified_by
        BOOLEAN is_google
        BOOLEAN is_outlook
        VARCHAR app_id
        VARCHAR tenant_id
        VARCHAR secret_id
        BOOLEAN use_access_token
        TEXT access_token
        TEXT code_challange
        TEXT refresh_token
    }

    MAILBOX {
        UUID id PK
        VARCHAR name
        UUID smtp_id FK
        UUID imap_id FK
        BOOLEAN is_active
        TIMESTAMP created_at
        VARCHAR created_by
        TIMESTAMP modified_at
        VARCHAR modified_by
    }

    MESSAGE {
        UUID id PK
        UUID mailbox_id FK
        VARCHAR subject
        TEXT text_plain
        TEXT text_html
        TEXT format_text_html
        TIMESTAMP date_sent
        TEXT cc_field
        TEXT bcc_field
        TEXT sent_to
        VARCHAR sent_from
        VARCHAR sent_from_display_name
        VARCHAR sent_from_raw
        VARCHAR imap_message_id
        VARCHAR mime_version
        TEXT return_path
        TIMESTAMP created_at
        VARCHAR created_by
        BOOLEAN is_deleted
    }

    IN_REPLY_TO {
        UUID id PK
        UUID mail_message_id FK
        VARCHAR in_reply_to_message_id
        TIMESTAMP created_at
    }

    REFERENCE {
        UUID id PK
        UUID mail_message_id FK
        VARCHAR reference_message_id
        TIMESTAMP created_at
    }

    ERROR_LOG {
        UUID id PK
        UUID message_log_id FK
        UUID message_id FK
        UUID mailbox_id FK
        UUID smtp_id FK
        UUID imap_id FK
        TEXT message
        TEXT stack_trace
        VARCHAR area
        VARCHAR function
        TEXT source
        VARCHAR help_link
        VARCHAR engine
        VARCHAR step
        TIMESTAMP created_at
    }

    MESSAGE_LOG_STATUS {
        UUID id PK
        VARCHAR code
        VARCHAR name
    }

    MESSAGE_LOG_TYPE {
        UUID id PK
        VARCHAR code
        VARCHAR name
    }

    MESSAGE_LOG_HEADER {
        UUID id PK
        VARCHAR table_name
        INT primary_key
        VARCHAR subject
        TIMESTAMP created_at
        VARCHAR created_by
        TIMESTAMP modified_at
        VARCHAR modified_by
    }

    MESSAGE_LOG {
        UUID id PK
        UUID message_log_header_id FK
        UUID message_log_type_id FK
        UUID message_log_status_id FK
        UUID smtp_id FK
        VARCHAR from_field
        VARCHAR from_name
        TEXT to_field
        TEXT cc_field
        TEXT bcc_field
        VARCHAR subject
        TEXT body
        TIMESTAMP date_sent
        TEXT status_message
        TIMESTAMP created_at
        VARCHAR created_by
    }

    MESSAGE_LOG_ATTACHMENT {
        UUID id PK
        UUID message_log_id FK
        TEXT attachment_url
        VARCHAR file_name
        BOOLEAN is_processed
        TIMESTAMP created_at
        VARCHAR created_by
    }

    %% Relationships
    IMAP_CONFIGURATION ||--o| MAILBOX : "imap_id"
    SMTP_CONFIGURATION ||--o| MAILBOX : "smtp_id"
    MAILBOX ||--o| MESSAGE : "mailbox_id"
    MESSAGE ||--o| IN_REPLY_TO : "mail_message_id"
    MESSAGE ||--o| REFERENCE : "mail_message_id"
    MESSAGE_LOG_STATUS ||--o| MESSAGE_LOG : "message_log_status_id"
    MESSAGE_LOG_TYPE ||--o| MESSAGE_LOG : "message_log_type_id"
    SMTP_CONFIGURATION ||--o| MESSAGE_LOG : "smtp_id"
    MESSAGE_LOG_HEADER ||--o| MESSAGE_LOG : "message_log_header_id"
    MESSAGE_LOG ||--o| MESSAGE_LOG_ATTACHMENT : "message_log_id"
```

<!--
    ERROR_LOG ||--o| MESSAGE_LOG : "message_log_id"
    ERROR_LOG ||--o| MESSAGE : "message_id"
    ERROR_LOG ||--o| MAILBOX : "mailbox_id"
    ERROR_LOG ||--o| SMTP_CONFIGURATION : "smtp_id"
    ERROR_LOG ||--o| IMAP_CONFIGURATION : "imap_id" -->
