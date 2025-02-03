CREATE OR REPLACE PROCEDURE mail.message_insert(
    OUT p_new_mail_message_id UUID,  -- OUT parameter must be first
    IN p_mailbox_id UUID DEFAULT NULL,
    IN p_subject VARCHAR(255) DEFAULT NULL,
    IN p_text_plain TEXT DEFAULT NULL,
    IN p_text_html TEXT DEFAULT NULL,
    IN p_format_text_html TEXT DEFAULT NULL,
    IN p_date_sent TIMESTAMP DEFAULT NULL,
    IN p_cc_field VARCHAR(500) DEFAULT NULL,
    IN p_bcc_field VARCHAR(500) DEFAULT NULL,
    IN p_sent_to VARCHAR(500) DEFAULT NULL,
    IN p_sent_from VARCHAR(255) DEFAULT NULL,
    IN p_sent_from_display_name VARCHAR(255) DEFAULT NULL,
    IN p_sent_from_raw VARCHAR(255) DEFAULT NULL,
    IN p_imap_message_id VARCHAR(1000) DEFAULT NULL,
    IN p_mime_version VARCHAR(255) DEFAULT NULL,
    IN p_return_path VARCHAR(255) DEFAULT NULL,
    IN p_logged_in_user VARCHAR(255) DEFAULT NULL
)
LANGUAGE plpgsql AS $$
-- =============================================
-- Author:		Nhlanhla Malaza
-- Create date: YYYY-MM-DD
-- Description:	Inserts received mail messages from mailboxes
-- =============================================
DECLARE
    v_now TIMESTAMP := NOW();
BEGIN
    INSERT INTO mail.message (
        subject,
        text_plain,
        text_html,
        format_text_html,
        date_sent,
        cc_field,
        bcc_field,
        sent_to,
        sent_from,
        sent_from_display_name,
        sent_from_raw,
        imap_message_id,
        mime_version,
        return_path,
        created_by,
        created_at,
        is_deleted
    )
    VALUES (
        p_subject,
        p_text_plain,
        p_text_html,
        p_format_text_html,
        p_date_sent,
        p_cc_field,
        p_bcc_field,
        p_sent_to,
        p_sent_from,
        p_sent_from_display_name,
        p_sent_from_raw,
        p_imap_message_id,
        p_mime_version,
        p_return_path,
        p_logged_in_user,
        v_now,
        FALSE
    )
    RETURNING id INTO p_new_mail_message_id;

EXCEPTION
    WHEN OTHERS THEN
        RAISE EXCEPTION 'Error: %', SQLERRM;
END;
$$;
