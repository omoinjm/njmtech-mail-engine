-- Insert Message Log Statuses
INSERT INTO mail.message_log_status (code, name)
VALUES
    ('C', 'Created'),
    ('P', 'Pending'),
    ('F', 'Failed'),
    ('S', 'Sent')
ON CONFLICT (code) DO NOTHING;

-- Insert Message Log Types
INSERT INTO mail.message_log_type (code, name)
VALUES
    ('E', 'Email'),
    ('S', 'SMS')
ON CONFLICT (code) DO NOTHING;

-- Insert Message Log Header
INSERT INTO mail.message_log_header (subject)
VALUES
    ('Testing Mails');

-- Insert Message Log Entry
INSERT INTO mail.message_log (message_log_header_id, from_name, to_field, subject, body, message_log_type_id, message_log_status_id, smtp_id)
VALUES
    ((SELECT id FROM mail.message_log_header WHERE subject = 'Testing Mails'), 'Wallety', 'njmcloud@gmail.com', 'Testing Mails', 
    '<table style="width:100%; background-color:#f3f3f3; border-radius:5px" cellspacing="50"> 
        <tr> 
            <td style="width:auto"></td> 
            <td style="width: 600px; background-color: #fff; border-radius: 5px; margin-top: 20px; margin-bottom: 20px; color: #2a2a2a; border-style: solid; border-width: 1px; border-color: #dedede; font-size: 14px; font-family: Verdana; padding: 20px;"> 
                <div style="min-height: 500px;"> 
                    <div style="width:100%; text-align:center; padding:10px; margin-bottom:20px"> 
                        <img src="https://visibill.co.za/wp-content/uploads/Visibill.png" width="400" /> 
                    </div> 
                    <div style="width:100%; text-align:center; padding:5px;"> 
                        <h1>Reset your Visibill Password</h1> 
                    </div> 
                    <p>Hi there,</p> 
                    <p>A password reset request was made for account <strong>gabriella.degiovanni@rysis.co.za</strong>. Please click the button below to reset your password.</p> 
                    <br /><br /><br /><br /> 
                    <div style="text-align:center"> 
                        <a href="https://cellusave.app.visibill.co.za/account/reset-password/9497fd49-72cf-44c0-a5d0-8ce779d1c08e" style="background-color:#023047; color:#fff; border:0; padding:14px; font-size:18px; border-radius:5px; text-decoration:none;">Click here to reset your password</a> 
                    </div> 
                    <br /><br /> 
                </div> 
            </td> 
            <td style="width:auto"></td> 
        </tr> 
    </table>',
    (SELECT id FROM mail.message_log_type WHERE code = 'E'),
    (SELECT id FROM mail.message_log_status WHERE code = 'P'),
    (SELECT id FROM mail.smtp_configuration WHERE code = 'SNE'));
