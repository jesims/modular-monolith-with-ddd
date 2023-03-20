CREATE TABLE IF NOT EXISTS sss_meetings.countries
(
    code char(2)     NOT NULL PRIMARY KEY,
    name varchar(50) NOT NULL
);

DROP VIEW IF EXISTS sss_meetings.v_countries;

CREATE VIEW sss_meetings.v_countries
AS
SELECT country.code,
       country.name
FROM sss_meetings.Countries AS country;
