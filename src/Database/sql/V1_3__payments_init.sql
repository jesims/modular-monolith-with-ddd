CREATE SCHEMA IF NOT EXISTS sss_payments;

CREATE TABLE sss_payments.payers
(
    id         uuid         NOT NULL PRIMARY KEY,
    login      varchar(100) NOT NULL,
    email      varchar(255) NOT NULL,
    first_name varchar(50)  NOT NULL,
    last_name  varchar(50)  NOT NULL,
    name       varchar(255) NOT NULL
);

CREATE TABLE sss_payments.outbox_messages
(
    id             uuid         NOT NULL PRIMARY KEY,
    occurred_on    timestamp    NOT NULL,
    type           varchar(255) NOT NULL,
    data           text         NOT NULL,
    processed_date timestamp    NULL
);

CREATE TABLE sss_payments.internal_commands
(
    id             uuid         NOT NULL PRIMARY KEY,
    enqueue_date   timestamp    NOT NULL,
    type           varchar(255) NOT NULL,
    data           text         NOT NULL,
    processed_date timestamp    NULL,
    error          text         NULL
);

CREATE TABLE sss_payments.inbox_messages
(
    id             uuid         NOT NULL PRIMARY KEY,
    occurred_on    timestamp    NOT NULL,
    type           varchar(255) NOT NULL,
    data           text         NOT NULL,
    processed_date timestamp    NULL
);


CREATE TABLE sss_payments.meeting_fees
(
    meeting_fee_id uuid           NOT NULL PRIMARY KEY,
    payer_id       uuid           NOT NULL,
    meeting_id     uuid           NOT NULL,
    fee_value      decimal(18, 2) NOT NULL,
    fee_currency   varchar(50)    NOT NULL,
    status         varchar(50)    NOT NULL
);

CREATE TABLE sss_payments.price_list_items
(
    id                       uuid           NOT NULL PRIMARY KEY,
    subscription_period_code varchar(50)    NOT NULL,
    category_code            varchar(50)    NOT NULL,
    country_code             varchar(50)    NOT NULL,
    money_value              decimal(18, 2) NOT NULL,
    money_currency           varchar(50)    NOT NULL,
    is_active                bit            NOT NULL
);

CREATE TABLE sss_payments.subscription_payments
(
    payment_id      uuid           NOT NULL,
    payer_id        uuid           NOT NULL,
    type            varchar(50)    NOT NULL,
    status          varchar(50)    NOT NULL,
    period          varchar(50)    NOT NULL,
    date            timestamp      NOT NULL,
    subscription_id uuid           NULL,
    money_value     decimal(18, 2) NOT NULL,
    money_currency  varchar(50)    NOT NULL
);

CREATE TABLE sss_payments.subscription_checkpoints
(
    code     VARCHAR(50) NOT NULL,
    position BIGINT      NOT NULL
);

CREATE TABLE sss_payments.subscription_details
(
    id              uuid        NOT NULL PRIMARY KEY,
    period          varchar(50) NOT NULL,
    status          varchar(50) NOT NULL,
    country_code    varchar(50) NOT NULL,
    expiration_date timestamp   NOT NULL,
    payer_id        uuid        NOT NULL
);

CREATE TABLE sss_payments.streams
(
    id          char(42)      NOT NULL,
    id_original varchar(1000) NOT NULL,
    id_internal serial        NOT NULL PRIMARY KEY,
    version     int           NOT NULL DEFAULT -1,
    position    bigint        NOT NULL DEFAULT -1
);

DROP INDEX IF EXISTS streams_id;
CREATE UNIQUE INDEX streams_id
    ON sss_payments.streams (id);

CREATE TABLE sss_payments.messages
(
    stream_id_internal int          NOT NULL references sss_payments.streams (id_internal),
    stream_version     int          NOT NULL,
    position           serial       NOT NULL PRIMARY KEY,
    id                 uuid         NOT NULL,
    created            timestamp    NOT NULL,
    type               varchar(128) NOT NULL,
    json_data          text         NOT NULL,
    json_metadata      text         NULL
);

DROP INDEX IF EXISTS idx_messages_stream_id_internal_id;
CREATE UNIQUE INDEX idx_messages_stream_id_internal_id
    ON sss_payments.messages (stream_id_internal, id);

DROP INDEX IF EXISTS idx_messages_stream_id_internal_stream_version;
CREATE UNIQUE INDEX idx_messages_stream_id_internal_stream_version
    ON sss_payments.messages (stream_id_internal, stream_version);

DROP INDEX IF EXISTS idx_messages_stream_id_internal_created;
CREATE INDEX idx_messages_stream_id_internal_created
    ON sss_payments.messages (stream_id_internal, created);
