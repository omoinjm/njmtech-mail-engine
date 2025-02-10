-- Insert IMAP configurations
INSERT INTO mail.imap_configuration (code, name, imap, ssl, port, username, password)
VALUES
    ('INW', 'imap.gmail.com', 'imap.gmail.com', TRUE, 993, 'nhlanhla@wallety.cash', 'zktz hpkc hyez itzn'),
    ('INE', 'imap.gmail.com', 'imap.gmail.com', TRUE, 993, 'nodeexam@gmail.com', 'vwmp xzyf qhqr zddp')
ON CONFLICT (code) DO NOTHING;

-- Insert SMTP configurations
INSERT INTO mail.smtp_configuration (code, name, smtp, ssl, port, email_address, username, password, from_name, is_google)
VALUES
    ('SNW', 'smtp.gmail.com', 'smtp.gmail.com', TRUE, 587, 'nhlanhla@wallety.cash', 'nhlanhla@wallety.cash', 'zktz hpkc hyez itzn', 'Nhlanhla Malaza (Wallety)', TRUE),
    ('SNE', 'smtp.gmail.com', 'smtp.gmail.com', TRUE, 587, 'nodeexam@gmail.com', 'nodeexam@gmail.com', 'zktz hpkc hyez itzn', 'Node Example', TRUE)
ON CONFLICT (code) DO NOTHING;

-- Insert Mailbox records
INSERT INTO mail.mailbox (name, smtp_id, imap_id)
VALUES
    ('nodeexam@gmail.com', 
        (SELECT id FROM mail.smtp_configuration WHERE code = 'SNE'), 
        (SELECT id FROM mail.imap_configuration WHERE code = 'INE')),
    ('nhlanhla@wallety.cash', 
        (SELECT id FROM mail.smtp_configuration WHERE code = 'SNW'), 
        (SELECT id FROM mail.imap_configuration WHERE code = 'INW'))
ON CONFLICT (name) DO NOTHING;
