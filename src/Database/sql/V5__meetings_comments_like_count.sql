ALTER TABLE IF EXISTS sss_meetings.meeting_comments
    ADD COLUMN IF NOT EXISTS likes_count INT DEFAULT 0;

CREATE TABLE IF NOT EXISTS sss_meetings.meeting_member_comment_likes
(
    id                 uuid NOT NULL PRIMARY KEY,
    member_id          uuid NOT NULL REFERENCES sss_meetings.members (id),
    meeting_comment_id uuid NOT NULL REFERENCES sss_meetings.meeting_comments (id)
);
