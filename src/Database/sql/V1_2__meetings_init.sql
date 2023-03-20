CREATE SCHEMA IF NOT EXISTS sss_meetings;

CREATE TABLE IF NOT EXISTS sss_meetings.meeting_waitlist_members
(
    meeting_id              uuid      NOT NULL,
    member_id               uuid      NOT NULL,
    sign_up_date            timestamp NOT NULL,
    is_signed_off           boolean   NOT NULL,
    sign_off_date           timestamp NULL,
    is_moved_to_attendees   boolean   NOT NULL,
    moved_to_attendees_date timestamp NULL
);

DROP INDEX IF EXISTS idx_meeting_waitlist_members_meeting_id_member_id_sign_up_date;
CREATE UNIQUE INDEX idx_meeting_waitlist_members_meeting_id_member_id_sign_up_date
    ON sss_meetings.meeting_waitlist_members (meeting_id, member_id, sign_up_date);

CREATE TABLE IF NOT EXISTS sss_meetings.meeting_not_attendees
(
    meeting_id           uuid      NOT NULL,
    member_id            uuid      NOT NULL,
    decision_date        timestamp NOT NULL,
    decision_changed     boolean   NOT NULL,
    decision_change_date timestamp NULL
);

DROP INDEX IF EXISTS idx_meeting_not_attendees_meeting_id_member_id_decision_date;
CREATE UNIQUE INDEX idx_meeting_not_attendees_meeting_id_member_id_decision_date
    ON sss_meetings.meeting_not_attendees (meeting_id, member_id, decision_date);

CREATE TABLE IF NOT EXISTS sss_meetings.meetings
(
    id                   uuid          NOT NULL PRIMARY KEY,
    meeting_group_id     uuid          NOT NULL,
    creator_id           uuid          NOT NULL,
    create_date          timestamp     NOT NULL,
    title                varchar(200)  NOT NULL,
    description          varchar(4000) NOT NULL,
    term_start_date      timestamp     NOT NULL,
    term_end_date        timestamp     NOT NULL,
    location_name        varchar(200)  NOT NULL,
    location_address     varchar(200)  NOT NULL,
    location_postal_code varchar(200)  NULL,
    location_city        varchar(50)   NOT NULL,
    attendees_limit      int           NULL,
    guests_limit         int           NOT NULL,
    rsvpterm_start_date  timestamp     NULL,
    rsvpterm_end_date    timestamp     NULL,
    event_fee_value      decimal(5)    NULL,
    event_fee_currency   varchar(3)    NULL,
    change_date          timestamp     NULL,
    change_member_id     uuid          NULL,
    cancel_date          timestamp     NULL,
    cancel_member_id     uuid          NULL,
    is_canceled          boolean       NOT NULL
);

CREATE TABLE IF NOT EXISTS sss_meetings.meeting_group_members
(
    meeting_group_id uuid        NOT NULL,
    member_id        uuid        NOT NULL,
    joined_date      timestamp   NOT NULL,
    role_code        varchar(50) NOT NULL,
    is_active        boolean     NOT NULL,
    leave_date       timestamp   NULL
);

DROP INDEX IF EXISTS idx_meeting_group_members_meeting_group_id_member_id_joined_date;
CREATE UNIQUE INDEX idx_meeting_group_members_meeting_group_id_member_id_joined_date
    ON sss_meetings.meeting_group_members (meeting_group_id, member_id, joined_date);

CREATE TABLE IF NOT EXISTS sss_meetings.meeting_groups
(
    id                    uuid         NOT NULL PRIMARY KEY,
    name                  varchar(255) NOT NULL,
    description           varchar(200) NULL,
    location_city         varchar(50)  NOT NULL,
    location_country_code varchar(3)   NOT NULL,
    creator_id            uuid         NOT NULL,
    create_date           timestamp    NOT NULL,
    payment_date_to       date         NULL
);

CREATE TABLE IF NOT EXISTS sss_meetings.members
(
    id         uuid         NOT NULL PRIMARY KEY,
    login      varchar(100) NOT NULL,
    email      varchar(255) NOT NULL,
    first_name varchar(50)  NOT NULL,
    last_name  varchar(50)  NOT NULL,
    name       varchar(255) NOT NULL
);

CREATE TABLE IF NOT EXISTS sss_meetings.meeting_group_proposals
(
    id                    uuid         NOT NULL PRIMARY KEY,
    name                  varchar(255) NOT NULL,
    description           varchar(200) NULL,
    location_city         varchar(50)  NOT NULL,
    location_country_code varchar(3)   NOT NULL,
    proposal_user_id      uuid         NOT NULL,
    proposal_date         timestamp    NOT NULL,
    status_code           varchar(50)  NOT NULL
);

CREATE TABLE IF NOT EXISTS sss_meetings.outbox_messages
(
    id             uuid         NOT NULL PRIMARY KEY,
    occurred_on    timestamp    NOT NULL,
    type           varchar(255) NOT NULL,
    data           text         NOT NULL,
    processed_date timestamp    NULL
);

CREATE TABLE IF NOT EXISTS sss_meetings.internal_commands
(
    id             uuid         NOT NULL PRIMARY KEY,
    enqueue_date   timestamp    NOT NULL,
    type           varchar(255) NOT NULL,
    data           text         NOT NULL,
    processed_date timestamp    NULL,
    error          text         NULL
);

CREATE TABLE IF NOT EXISTS sss_meetings.inbox_messages
(
    id             uuid         NOT NULL PRIMARY KEY,
    occurred_on    timestamp    NOT NULL,
    type           varchar(255) NOT NULL,
    data           text         NOT NULL,
    processed_date timestamp    NULL
);

CREATE TABLE IF NOT EXISTS sss_meetings.member_subscriptions
(
    id              uuid      NOT NULL PRIMARY KEY,
    expiration_date timestamp NOT NULL
);


CREATE TABLE IF NOT EXISTS sss_meetings.meeting_attendees
(
    meeting_id               uuid         NOT NULL,
    attendee_id              uuid         NOT NULL,
    decision_date            timestamp    NOT NULL,
    role_code                varchar(50)  NULL,
    guests_number            int          NULL,
    decision_changed         boolean      NOT NULL,
    decision_change_date     timestamp    NULL,
    is_removed               boolean      NOT NULL,
    removing_member_id       uuid         NULL,
    removing_reason          varchar(500) NULL,
    removed_date             timestamp    NULL,
    became_not_attendee_date timestamp    NULL,
    fee_value                decimal(5)   NULL,
    fee_currency             varchar(3)   NULL,
    is_fee_paid              boolean      NOT NULL
);

DROP INDEX IF EXISTS idx_meeting_attendees_meeting_id_attendee_id_decision_date;

CREATE UNIQUE INDEX idx_meeting_attendees_meeting_id_attendee_id_decision_date ON
    sss_meetings.meeting_attendees (meeting_id, attendee_id, decision_date);

CREATE TABLE IF NOT EXISTS sss_meetings.meeting_comments
(
    id                     uuid         NOT NULL PRIMARY KEY,
    meeting_id             uuid         NOT NULL,
    author_id              uuid         NOT NULL,
    in_reply_to_comment_id uuid         NULL,
    comment                varchar(300) NULL,
    is_removed             boolean      NOT NULL,
    removed_by_reason      varchar(300) NULL,
    create_date            timestamp    NOT NULL,
    edit_date              timestamp    NULL
);