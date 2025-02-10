-- Ensure the necessary schema exists
CREATE SCHEMA IF NOT EXISTS mail;

CREATE TABLE IF NOT EXISTS mail.imap_configuration  (
   id UUID PRIMARY KEY DEFAULT uuid_generate_v4(),
   code VARCHAR(50) NOT NULL,
   name VARCHAR(255) NOT NULL,
   imap VARCHAR(255) NOT NULL,
   ssl BOOLEAN NOT NULL,
   port INT NOT NULL,
   username VARCHAR(255) NOT NULL,
   password VARCHAR(50) NOT NULL,
   is_active BOOLEAN DEFAULT TRUE,
   created_at TIMESTAMP NOT NULL DEFAULT NOW(),
   created_by VARCHAR(255) NOT NULL DEFAULT 'Mail Service Api',
   modified_at TIMESTAMP NULL,
   modified_by VARCHAR(255) NULL

   CONSTRAINT unique_code_imap UNIQUE (code)  
);

CREATE TABLE IF NOT EXISTS mail.smtp_configuration (
   id UUID PRIMARY KEY DEFAULT uuid_generate_v4(),
   code VARCHAR(20) NOT NULL,
   name VARCHAR(200) NOT NULL,
   smtp VARCHAR(200) NOT NULL,
   ssl BOOLEAN NOT NULL,
   port INT NOT NULL,
   email_address VARCHAR(200) NOT NULL,
   username VARCHAR(200) NOT NULL,
   password VARCHAR(200) NOT NULL,
   from_name VARCHAR(200) NOT NULL,
   is_active BOOLEAN DEFAULT TRUE,
   created_at TIMESTAMP NOT NULL DEFAULT NOW(),
   created_by VARCHAR(255) NOT NULL DEFAULT 'Mail Service Api',
   modified_at TIMESTAMP NULL,
   modified_by VARCHAR(255) NULL,

   is_google BOOLEAN,
   is_outlook BOOLEAN,
   app_id VARCHAR(500),
   tenant_id VARCHAR(500),
   secret_id VARCHAR(500),
   use_access_token BOOLEAN,
   access_token TEXT,
   code_challange TEXT,
   refresh_token TEXT,

   CONSTRAINT unique_code_smtp UNIQUE (code)  
);

CREATE TABLE IF NOT EXISTS mail.mailbox (
   id UUID PRIMARY KEY DEFAULT uuid_generate_v4(),
   name VARCHAR(200) NOT NULL,
   smtp_id UUID,
   imap_id UUID,
   is_active BOOLEAN DEFAULT TRUE,   
   created_at TIMESTAMP NOT NULL DEFAULT NOW(),
   created_by VARCHAR(255) NOT NULL DEFAULT 'Mail Service Api',
   modified_at TIMESTAMP NULL,
   modified_by VARCHAR(255) NULL,

   CONSTRAINT fk_mailbox_smtp FOREIGN KEY (smtp_id) REFERENCES mail.smtp_configuration (id) ON DELETE SET NULL,
   CONSTRAINT fk_mailbox_imap FOREIGN KEY (imap_id) REFERENCES mail.imap_configuration (id) ON DELETE SET NULL
);

CREATE TABLE IF NOT EXISTS mail.message (
    id UUID PRIMARY KEY DEFAULT uuid_generate_v4(),
    mailbox_id UUID NULL,
    subject VARCHAR(2000) NULL,
    text_plain TEXT NULL,
    text_html TEXT NULL,
    format_text_html TEXT NULL,
    date_sent TIMESTAMP NULL,
    cc_field TEXT NULL,
    bcc_field TEXT NULL,
    sent_to TEXT NULL,
    sent_from VARCHAR(500) NULL,
    sent_from_display_name VARCHAR(500) NULL,
    sent_from_raw VARCHAR(500) NULL,
    imap_message_id VARCHAR(500) NULL,
    mime_version VARCHAR(255) NULL,
    return_path TEXT NULL,
    created_by VARCHAR(255) NOT NULL DEFAULT 'Mail Service Api',
    created_at TIMESTAMP NOT NULL DEFAULT NOW(),
    is_deleted BOOLEAN NOT NULL DEFAULT false,
    -- Add foreign key constraint for mailbox
    CONSTRAINT fk_message_mailbox FOREIGN KEY (mailbox_id) REFERENCES mail.mailbox(id)
);

CREATE TABLE IF NOT EXISTS mail.in_reply_to (
    id UUID PRIMARY KEY DEFAULT uuid_generate_v4(),
    mail_message_id UUID,
    in_reply_to_message_id VARCHAR(100) NOT NULL,
    created_at TIMESTAMP NOT NULL DEFAULT NOW(),
    -- Add foreign key constraint for mail_message_id
    CONSTRAINT fk_in_reply_to_mail_message FOREIGN KEY (mail_message_id) REFERENCES mail.message(id)
);

CREATE TABLE IF NOT EXISTS mail.reference (
    id UUID PRIMARY KEY DEFAULT uuid_generate_v4(),
    mail_message_id UUID,
    reference_message_id VARCHAR(500) NOT NULL,
    created_at TIMESTAMP NOT NULL DEFAULT NOW(),
    -- Add foreign key constraint for mail_message_id
    CONSTRAINT fk_reference_mail_message FOREIGN KEY (mail_message_id) REFERENCES mail.message(id)
);
