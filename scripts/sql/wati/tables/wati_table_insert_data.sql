INSERT INTO wati.message_template
(code, name, json_payload)
VALUES
(
    'RM',
    'response_message_v16',
    '{
        "template_name": "response_message_v16",
        "broadcast_name": "response_message_v16_broadcast",
        "parameters": [
            {
                "name": "notificationmessage",
                "value": "{message}"
            }
        ]
    }'
),
(
    'UCD',
    'update_customer_details_v5',
    '{
        "template_name": "update_customer_details_v5",
        "broadcast_name": "update_customer_details_v5_broadcast",
        "parameters": [
            {
            "name": "key",
            "value": "{ENCRYPTED_ID}"
            }
        ]
    }'
),
(
    'NCW',
    'new_customer_welcome_v10',
    '{
        "template_name": "new_customer_welcome_v10",
        "broadcast_name": "new_customer_welcome_v10_broadcast",
        "parameters": [
            {
            "name": "firstname",
            "value": "{firstName}"
            },
            {
            "name": "surname",
            "value": "{surname}"
            }
        ]
    }'
),
(
    'WS',
    'wallety_secure_v4',
    '{
        "template_name": "wallety_secure_v4",
        "broadcast_name": "wallety_secure_v4_broadcast",
        "parameters": [
            {
            "name": "name",
            "value": "{firstName}"
            }
        ]
    }'
),
(
    'UL',
    'user_login_v5',
    '{
        "template_name": "user_login_v5",
        "broadcast_name": "user_login_v5_broadcast",
        "parameters": [
            {
            "name": "loginkey",
            "value": "{ENCRYPTED_ID}"
            }
        ]
    }'
)
