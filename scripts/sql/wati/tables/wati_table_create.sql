CREATE SCHEMA IF NOT EXISTS wati;

DROP TABLE IF EXISTS wati.message_template;

-- Create tables
CREATE TABLE IF NOT EXISTS wati.message_template (
    id UUID PRIMARY KEY DEFAULT uuid_generate_v4(),

    code TEXT NOT NULL UNIQUE CHECK (code ~ '^[A-Z]{2,5}$'),
    name TEXT NOT NULL,

    json_payload TEXT NOT NULL,
    
    created_at TIMESTAMP NOT NULL DEFAULT NOW()
);
