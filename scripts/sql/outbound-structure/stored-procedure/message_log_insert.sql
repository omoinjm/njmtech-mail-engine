CREATE OR REPLACE PROCEDURE mail.message_log_insert (
    OUT p_is_error BOOLEAN,
    OUT p_return_record_id INTEGER,
    OUT p_result_message TEXT,
    OUT p_object_name VARCHAR(100),
    IN p_message_log_id INTEGER DEFAULT NULL,
    IN p_table_name VARCHAR(100) DEFAULT NULL,
    IN p_primary_key INTEGER DEFAULT NULL,
    IN p_subject VARCHAR(200) DEFAULT NULL,
    IN p_user VARCHAR(200) DEFAULT NULL,
    IN p_smtp_id INTEGER DEFAULT NULL,
    IN p_to_field TEXT DEFAULT NULL,
    IN p_cc_field TEXT DEFAULT NULL,
    IN p_bcc_field TEXT DEFAULT NULL,
    IN p_body TEXT DEFAULT NULL,
    IN p_message_log_type_id INTEGER DEFAULT NULL,
    IN p_message_log_status_id INTEGER DEFAULT NULL,
    IN p_in_reply_to_id VARCHAR(500) DEFAULT NULL,
    IN p_from_name VARCHAR(50) DEFAULT NULL,
    IN p_text_plain TEXT DEFAULT NULL
)
LANGUAGE plpgsql
AS $$
DECLARE
    v_message_log_header_id INTEGER;
BEGIN
    -- Set default object name
    p_object_name := 'mail.message_log_insert';
    
    -- Handle NULL message_log_id
    IF p_message_log_id = 0 THEN
        p_message_log_id := NULL;
    END IF;

    -- Set default message log status
    IF p_message_log_status_id IS NULL OR p_message_log_status_id = 0 THEN
        SELECT id INTO p_message_log_status_id
        FROM mail.message_log_status
        WHERE code = 'P';
    END IF;

    -- Update existing message log
    IF p_message_log_id IS NOT NULL THEN
        UPDATE mail.message_log
        SET to_field = p_to_field,
            cc_field = p_cc_field,
            subject = p_subject,
            from_field = p_from_name,
            from_name = p_from_name,
            body = p_body,
            message_log_status_id = p_message_log_status_id,
            text_plain = p_text_plain,
            in_reply_to_id = p_in_reply_to_id
        WHERE id = p_message_log_id;

        p_is_error := FALSE;
        p_return_record_id := p_message_log_id;
        p_result_message := 'Update successful';
        RETURN;
    END IF;

    -- Insert new message log
    IF p_message_log_id IS NULL THEN
        -- Insert message log header
        INSERT INTO mail.message_log_header (
            table_name,
            primary_key,
            subject,
            created_at,
            created_by
        ) VALUES (
            p_table_name,
            p_primary_key,
            p_subject,
            CURRENT_TIMESTAMP,
            p_user
        ) RETURNING id INTO v_message_log_header_id;

        IF v_message_log_header_id IS NOT NULL THEN
            -- Insert message log
            INSERT INTO mail.message_log (
                message_log_header_id,
                to_field,
                cc_field,
                bcc_field,
                subject,
                body,
                message_log_type_id,
                message_log_status_id,
                created_at,
                created_by,
                smtp_id,
                from_name,
                text_plain,
                in_reply_to_id
            ) VALUES (
                v_message_log_header_id,
                p_to_field,
                p_cc_field,
                p_bcc_field,
                p_subject,
                p_body,
                p_message_log_type_id,
                p_message_log_status_id,
                CURRENT_TIMESTAMP,
                p_user,
                p_smtp_id,
                p_from_name,
                p_text_plain,
                p_in_reply_to_id
            ) RETURNING id INTO p_message_log_id;

            p_is_error := FALSE;
            p_return_record_id := p_message_log_id;
            p_result_message := 'Send successful';
            RETURN;
        END IF;
    END IF;

    -- If we get here, something went wrong
    p_is_error := TRUE;
    p_return_record_id := NULL;
    p_result_message := 'Operation failed';
END;
$$;
