CREATE OR REPLACE PROCEDURE mail.reference_insert(
    IN p_mail_message_id INT, 
    IN p_reference_message_id TEXT
)
LANGUAGE plpgsql AS $$
-- =============================================
-- Author:      Nhlanhla Malaza
-- Create date: 2025-01-31
-- Description: Insert Reference Emails to Mails
-- =============================================
DECLARE
    v_now TIMESTAMP := NOW();
BEGIN
    -- Insert statement for procedure here
    INSERT INTO mail.reference (
        mail_message_id,
        reference_message_id,
        created_at
    )
    VALUES (
        p_mail_message_id,
        p_reference_message_id,
        v_now
    );
END;
$$;