CREATE TABLE IF NOT EXISTS sss_meetings.meeting_commenting_configurations
(
    id                    uuid    NOT NULL PRIMARY KEY,
    meeting_id            uuid    NOT NULL references sss_meetings.meetings (id),
    is_commenting_enabled boolean NOT NULL
);