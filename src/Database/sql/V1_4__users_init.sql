CREATE SCHEMA IF NOT EXISTS sss_users;

CREATE TABLE sss_users.inbox_messages
(
    id             uuid         NOT NULL PRIMARY KEY,
    occurred_on    timestamp    NOT NULL,
    type           varchar(255) NOT NULL,
    data           text         NOT NULL,
    processed_date timestamp    NULL
);

CREATE TABLE sss_users.user_roles
(
    user_id   uuid        NOT NULL,
    role_code varchar(50) NULL
);

CREATE TABLE sss_users.user_registrations
(
    id             uuid         NOT NULL PRIMARY KEY,
    login          varchar(100) NOT NULL,
    email          varchar(255) NOT NULL,
    password       varchar(255) NOT NULL,
    first_name     varchar(50)  NOT NULL,
    last_name      varchar(50)  NOT NULL,
    name           varchar(255) NOT NULL,
    status_code    varchar(50)  NOT NULL,
    register_date  timestamp    NOT NULL,
    confirmed_date timestamp    NULL
);

CREATE TABLE sss_users.users
(
    id         uuid         NOT NULL PRIMARY KEY,
    login      varchar(100) NOT NULL,
    email      varchar(255) NOT NULL,
    password   varchar(255) NOT NULL,
    is_active  boolean      NOT NULL,
    first_name varchar(50)  NOT NULL,
    last_name  varchar(50)  NOT NULL,
    name       varchar(255) NOT NULL
);

CREATE TABLE sss_users.roles_to_permissions
(
    role_code       varchar(50) NOT NULL,
    permission_code varchar(50) NOT NULL
);

DROP INDEX IF EXISTS idx_roles_to_permissions_role_code_permission_code;
CREATE UNIQUE INDEX idx_roles_to_permissions_role_code_permission_code
    on sss_users.roles_to_permissions (role_code, permission_code);

CREATE TABLE sss_users.permissions
(
    code        varchar(50)  NOT NULL PRIMARY KEY,
    name        varchar(100) NOT NULL,
    description varchar(255) NULL
);

CREATE TABLE sss_users.internal_commands
(
    id             uuid         NOT NULL PRIMARY KEY,
    enqueue_date   timestamp    NOT NULL,
    type           varchar(255) NOT NULL,
    data           text         NOT NULL,
    processed_date timestamp    NULL,
    error          text         NULL
);

CREATE TABLE sss_users.outbox_messages
(
    id             uuid         NOT NULL PRIMARY KEY,
    occurred_on    timestamp    NOT NULL,
    type           varchar(255) NOT NULL,
    data           text         NOT NULL,
    processed_date timestamp    NULL
);