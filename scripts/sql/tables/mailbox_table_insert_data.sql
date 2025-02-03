insert into mail.imap_configuration
(code, name, imap, ssl, port, username, password)
values
('INW', 'imap.gmail.com', 'imap.gmail.com', true, 993, 'nhlanhla@wallety.cash', 'zktz hpkc hyez itzn'),
('INE', 'imap.gmail.com', 'imap.gmail.com', true, 993, 'nodeexam@gmail.com', 'vwmp xzyf qhqr zddp');

insert into mail.smtp_configuration
(code, name, smtp, ssl, port, email_address, username, password, from_name, is_google)
values
('SNW', 'smtp.gmail.com', 'smtp.gmail.com', true, 587, 'nhlanhla@wallety.cash', 'nhlanhla@wallety.cash', 'zktz hpkc hyez itzn', 'Nhlanhla Malaza (Wallety)', true),
('SNE', 'smtp.gmail.com', 'smtp.gmail.com', true, 587, 'nodeexam@gmail.com', 'nodeexam@gmail.com', 'zktz hpkc hyez itzn', 'Node Example', true);

insert into mail.mailbox
(name, smtp_id, imap_id)
values
('nodeexam@gmail.com', 'c10e49c7-2d39-4c5a-a2bb-b1806cca7e46', 'e2d013d8-bba5-4336-8c54-a09f2d1476d6'),
('nhlanhla@wallety.cash', '889f7ca4-dadf-4857-8a90-d3bfbcdb10f9', '71dcf0d5-a20a-439c-86b0-19e3aafed9d4');
