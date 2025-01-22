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

select * from mail_smtp_configuration

