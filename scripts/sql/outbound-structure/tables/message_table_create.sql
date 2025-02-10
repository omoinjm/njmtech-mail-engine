-- If you need to restart
-- DROP SCHEMA mail CASCADE;

-- Create extension if it doesn't exist (needed for UUID generation)
CREATE EXTENSION IF NOT EXISTS "uuid-ossp";

-- Create schema if it doesn't exist
CREATE SCHEMA IF NOT EXISTS mail;

-- Create tables
CREATE TABLE IF NOT EXISTS mail.error_log (
    id UUID PRIMARY KEY DEFAULT uuid_generate_v4(),
    
    message_log_id UUID NULL,
    message_id UUID NULL,
    mailbox_id UUID NULL,
    smtp_id UUID NULL,
    imap_id UUID NULL,
    
    message TEXT NULL,
    stack_trace TEXT NULL,
    area VARCHAR(200) NULL,
    function VARCHAR(200) NULL,
    source TEXT NULL,
    help_link VARCHAR(500) NULL,
    engine VARCHAR(50) NULL,
    step VARCHAR(200) NULL,

    created_at TIMESTAMP NOT NULL DEFAULT NOW()
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

CREATE TABLE IF NOT EXISTS mail.message_log_status (
    id UUID PRIMARY KEY DEFAULT uuid_generate_v4(),
    code VARCHAR(20) NOT NULL,
    name VARCHAR(100) NOT NULL,
    CONSTRAINT unique_code_status UNIQUE (code)
);

CREATE TABLE IF NOT EXISTS mail.message_log_type (
    id UUID PRIMARY KEY DEFAULT uuid_generate_v4(),
    code VARCHAR(20) NOT NULL,
    name VARCHAR(100) NOT NULL,
    CONSTRAINT unique_code_type UNIQUE (code)
);

CREATE TABLE IF NOT EXISTS mail.message_log_header (
    id UUID PRIMARY KEY DEFAULT uuid_generate_v4(),
    
    table_name VARCHAR(100) NULL,
    primary_key INT NULL,
    subject VARCHAR(200) NULL,

    created_at TIMESTAMP NOT NULL DEFAULT NOW(),
    created_by VARCHAR(255) NOT NULL DEFAULT 'Mail Service Api',
    modified_at TIMESTAMP NULL,
    modified_by VARCHAR(255) NULL
);

CREATE TABLE IF NOT EXISTS mail.message_log (
    id UUID PRIMARY KEY DEFAULT uuid_generate_v4(),
    
    message_log_header_id UUID NOT NULL,
    message_log_type_id UUID NOT NULL,
    message_log_status_id UUID NOT NULL,
    smtp_id UUID NOT NULL,

    from_field VARCHAR(200) NULL,
    from_name VARCHAR(200) NULL,
    to_field TEXT NULL,
    cc_field TEXT NULL,
    bcc_field TEXT NULL,
    subject VARCHAR(1000) NULL,
    body TEXT NULL,
    date_sent TIMESTAMP NULL,
    status_message TEXT NULL,

    created_at TIMESTAMP NOT NULL DEFAULT NOW(),
    created_by VARCHAR(255) NOT NULL DEFAULT 'Mail Service Api',

    CONSTRAINT fk_message_log_status FOREIGN KEY (message_log_status_id)
        REFERENCES mail.message_log_status (id),
    CONSTRAINT fk_message_log_type FOREIGN KEY (message_log_type_id)
        REFERENCES mail.message_log_type (id),
    CONSTRAINT fk_message_log_smtp_id FOREIGN KEY (smtp_id)
        REFERENCES mail.smtp_configuration (id),
    CONSTRAINT fk_message_log_header FOREIGN KEY (message_log_header_id)
        REFERENCES mail.message_log_header (id)
);

CREATE TABLE IF NOT EXISTS mail.message_log_attachment (
    id UUID PRIMARY KEY DEFAULT uuid_generate_v4(),

    message_log_id UUID NOT NULL,
    attachment_url TEXT NULL,
    file_name VARCHAR(200) NULL,

    content_id VARCHAR(200) NULL,
    is_inline_image BOOLEAN NULL,
    
    is_processed BOOLEAN DEFAULT FALSE,
    
    created_at TIMESTAMP NOT NULL DEFAULT NOW(),
    created_by VARCHAR(255) NOT NULL DEFAULT 'Mail Service Api',

    azure_uri VARCHAR(500) NULL,
    azure_path VARCHAR(500) NULL,
    extension VARCHAR(50) NULL,
    file_size_kb NUMERIC(19,6) NULL,
    attachment_data BYTEA NULL,

    CONSTRAINT fk_message_log_attachment FOREIGN KEY (message_log_id)
        REFERENCES mail.message_log (id)
);
