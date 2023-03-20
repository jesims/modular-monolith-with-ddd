CREATE SCHEMA IF NOT EXISTS sss_administration;

CREATE TABLE IF NOT EXISTS sss_administration.internal_commands
(
    id             uuid         NOT NULL PRIMARY KEY,
    enqueue_date   timestamp    NOT NULL,
    type           varchar(255) NOT NULL,
    data           text         NOT NULL,
    processed_date timestamp    NULL,
    error          text         NULL
);

CREATE TABLE IF NOT EXISTS sss_administration.inbox_messages
(
    id             uuid         NOT NULL PRIMARY KEY,
    occurred_on    timestamp    NOT NULL,
    type           varchar(255) NOT NULL,
    data           text         NOT NULL,
    processed_date timestamp    NULL
);

CREATE TABLE IF NOT EXISTS sss_administration.outbox_messages
(
    id             uuid         NOT NULL PRIMARY KEY,
    occurred_on    timestamp    NOT NULL,
    type           varchar(255) NOT NULL,
    data           text         NOT NULL,
    processed_date timestamp    NULL
);

CREATE TABLE IF NOT EXISTS sss_administration.members
(
    id         uuid         NOT NULL PRIMARY KEY,
    login      varchar(100) NOT NULL,
    email      varchar(255) NOT NULL,
    first_name varchar(50)  NOT NULL,
    last_name  varchar(50)  NOT NULL,
    name       varchar(255) NOT NULL
);

CREATE TABLE IF NOT EXISTS sss_administration.meeting_group_proposals
(
    id                     uuid         NOT NULL PRIMARY KEY,
    name                   varchar(255) NOT NULL,
    description            varchar(200) NULL,
    location_city          varchar(50)  NOT NULL,
    location_country_code  varchar(3)   NOT NULL,
    proposal_user_id       uuid         NOT NULL,
    proposal_date          timestamp    NOT NULL,
    status_code            varchar(50)  NOT NULL,
    decision_date          timestamp    NULL,
    decision_user_id       uuid         NULL,
    decision_code          varchar(50)  NULL,
    decision_reject_reason varchar(250) NULL
);