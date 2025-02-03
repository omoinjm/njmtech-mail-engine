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
    smtp_code VARCHAR(20) NULL,
    imap_id UUID NULL,
    imap_code VARCHAR(20) NULL,
    message TEXT NULL,
    stack_trace TEXT NULL,
    created_at TIMESTAMP NOT NULL DEFAULT NOW(),
    area VARCHAR(200) NULL,
    function VARCHAR(200) NULL,
    source TEXT NULL,
    help_link VARCHAR(500) NULL,
    engine VARCHAR(50) NULL,
    step VARCHAR(200) NULL
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
    created_by VARCHAR(255) NOT NULL DEFAULT 'System',
    modified_at TIMESTAMP NULL,
    modified_by VARCHAR(255) NULL
);

CREATE TABLE IF NOT EXISTS mail.message_log (
    id UUID PRIMARY KEY DEFAULT uuid_generate_v4(),
    message_log_header_id UUID NOT NULL,
    from_field VARCHAR(200) NULL,
    from_name VARCHAR(200) NULL,
    to_field TEXT NULL,
    cc_field TEXT NULL,
    bcc_field TEXT NULL,
    subject VARCHAR(1000) NULL,
    body TEXT NULL,
    message_log_type_id UUID NOT NULL,
    message_log_status_id UUID NOT NULL,
    smtp_configuration_id UUID NOT NULL,
    date_sent TIMESTAMP NULL,
    status_message TEXT NULL,

    created_at TIMESTAMP NOT NULL DEFAULT NOW(),
    created_by VARCHAR(255) NOT NULL DEFAULT 'System',

    CONSTRAINT fk_message_log_status FOREIGN KEY (message_log_status_id)
        REFERENCES mail.message_log_status (id),
    CONSTRAINT fk_message_log_type FOREIGN KEY (message_log_type_id)
        REFERENCES mail.message_log_type (id),
    CONSTRAINT fk_message_log_status_code FOREIGN KEY (message_log_status_code)
        REFERENCES mail.message_log_status (code),
    CONSTRAINT fk_message_log_type_code FOREIGN KEY (message_log_type_code)
        REFERENCES mail.message_log_type (code),
    CONSTRAINT fk_message_log_smtp_id FOREIGN KEY (smtp_configuration_id)
        REFERENCES mail.smtp_configuration (id),
    CONSTRAINT fk_message_log_smtp_code FOREIGN KEY (smtp_configuration_code)
        REFERENCES mail.smtp_configuration (code),
    CONSTRAINT fk_message_log_header FOREIGN KEY (message_log_header_id)
        REFERENCES mail.message_log_header (id)
);

CREATE TABLE IF NOT EXISTS mail.message_log_attachment (
    id UUID PRIMARY KEY DEFAULT uuid_generate_v4(),
    message_log_id UUID NOT NULL,
    attachment_url TEXT NULL,
    file_name VARCHAR(200) NULL,
    is_processed BOOLEAN DEFAULT FALSE,
    
    created_at TIMESTAMP NOT NULL DEFAULT NOW(),
    created_by VARCHAR(255) NOT NULL DEFAULT 'System',

    CONSTRAINT fk_message_log_attachment FOREIGN KEY (message_log_id)
        REFERENCES mail.message_log (id)
);
