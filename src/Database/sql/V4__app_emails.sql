CREATE SCHEMA IF NOT EXISTS sss_app;
CREATE TABLE sss_app.emails
(
    id      uuid         NOT NULL PRIMARY KEY,
    "from"  varchar(255) NOT NULL,
    "to"    varchar(255) NOT NULL,
    subject varchar(255) NOT NULL,
    content text         NOT NULL,
    date    timestamp    NOT NULL
);
