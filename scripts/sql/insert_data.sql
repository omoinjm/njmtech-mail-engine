insert into public.mail_smtp_configuration (
    name,
    code,
    smtp,
    ssl,
    port,
    email_address,
    username,
    password,
    from_name,
    is_active,
    created_at,
    created_by
)
values
(
    '<name>',
    '<code>',
    'smtp.gmail.com',
    true,
    587,
    '<email>',
    '<email>',
    '<password>',
    '<name>',
    true,
    NOW(),
    '<user name surname>'
);

insert into public.mail_message_log_status
(code, name)
values
('C', 'Created'),
('P', 'Pending'),
('F', 'Failed'),
('S', 'Sent')

insert into public.mail_message_log_type
(code, name)
values
('E', 'Email'),
('S', 'Sms')


