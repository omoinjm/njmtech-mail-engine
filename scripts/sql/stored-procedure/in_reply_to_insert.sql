CREATE OR REPLACE PROCEDURE mail.in_reply_to_insert(
    IN p_mail_message_id INT, 
    IN p_in_reply_to_message_id TEXT
)
LANGUAGE plpgsql AS $$
-- =============================================
-- Author:      Nhlanhla Malaza
-- Create date: YYYY-MM-DD
-- Description: Insert In Reply To Emails. This will link mails to replies
-- =============================================
DECLARE
    v_now TIMESTAMP := NOW();
BEGIN
    -- Insert statement for procedure here
    INSERT INTO mail.in_reply_to (
        mail_message_id,
        in_reply_to_message_id,
        created_at
    )
    VALUES (
        p_mail_message_id,
        p_in_reply_to_message_id,
        v_now
    );
END;
$$;