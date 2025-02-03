-- Insert data
insert into mail.smtp_configuration (
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

insert into mail.message_log_status
(code, name)
values
('C', 'Created'),
('P', 'Pending'),
('F', 'Failed'),
('S', 'Sent');

insert into mail.message_log_type
(code, name)
values
('E', 'Email'),
('S', 'Sms');

-- insert into mail.message_log_header
-- (subject, created_at, created_by)
-- values
-- ('Testing Mails', NOW(), 'Nhlanhla Malaza');
-- select * from mail.message_log_header;

insert into mail.message_log
(message_log_header_id, from_name, to_field, subject, body, message_log_type_id, message_log_type_code, message_log_status_id, message_log_status_code, smtp_configuration_id, smtp_configuration_code, created_at, created_by)
values
('5ad8d789-69d9-4038-8050-cd33f05cfd0d', 'Wallety', 'njmcloud@gmail.com', 'Testing Mails', '      <table style="width:100%; background-color:#f3f3f3; border-radius:5px" cellspacing="50">     <tr>         <td style="width:auto">         </td>         <td style="width: 600px;         background-color: #fff;         border-radius: 5px;         margin-top: 20px;         margin-bottom: 20px;         color: #2a2a2a;         border-style: solid;         border-width: 1px;         border-color: #dedede;         font-size: 14px;         font-family: Verdana;         padding: 20px;">             <div style="min-height: 500px;">                 <div style="width:100%; text-align:center; padding:10px; margin-bottom:20px">                     <img src="https://visibill.co.za/wp-content/uploads/Visibill.png" width="400" />                 </div>                  <div style="width:100%; text-align:center; padding:5px;">                     <h1>                         Reset your Visibill Password                     </h1>                 </div>                 <p>                     Hi there,                 </p>                 <p>                     A password reset request was made to reset your password for account <strong>gabriella.degiovanni@rysis.co.za </strong>.  Please click on the button below to reset your password.                 </p>                 <br />                 <br />                 <br />                 <br />                 <div style="text-align:center">                     <a href="https://cellusave.app.visibill.co.za/account/reset-password/9497fd49-72cf-44c0-a5d0-8ce779d1c08e" style="background-color:#023047; color:#fff; border:0; padding:14px; font-size:18px; border-radius:5px; text-decoration:none;">Click here to reset your password</a>                 </div>                 <br />                 <br />             </div>          </td>         <td style="width:auto">         </td>     </tr> </table>',
'c95accda-53da-42b6-acf4-a46de19ff13e', 'S', 'fddd183b-e1b1-450b-878d-4162908ddf59', 'P', 'c0f244b9-1fa2-4c33-8ea9-360575719f72', 'NW', NOW(), 'Nhlanhla Malaza');

select * from mail.message_log;


