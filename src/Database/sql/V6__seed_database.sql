-- Add Test Member
INSERT INTO sss_users.user_registrations VALUES 
(
	'2EBFECFC-ED13-43B8-B516-6AC89D51C510',
	'testMember@mail.com',
	'testMember@mail.com',
	'ANO7TKjxh/Dom6LG0dyoQfJciLca+e1itHQ6BZMQYs+aMbKL9MjCv6bq4gy4+MRY0w==', -- testMemberPass
	'John',
	'Doe',
	'John Doe',
	'Confirmed',
	NOW(),
	NOW()
);

INSERT INTO sss_users.users VALUES
(
	'2EBFECFC-ED13-43B8-B516-6AC89D51C510',
	'testMember@mail.com',
	'testMember@mail.com',
	'ANO7TKjxh/Dom6LG0dyoQfJciLca+e1itHQ6BZMQYs+aMbKL9MjCv6bq4gy4+MRY0w==', -- testMemberPass
	true,
	'John',
	'Doe',
	'John Doe'
);

INSERT INTO sss_meetings.members VALUES
(
	'2EBFECFC-ED13-43B8-B516-6AC89D51C510',
	'testMember@mail.com',
	'testMember@mail.com',
	'John',
	'Doe',
	'John Doe'
);

INSERT INTO sss_payments.payers VALUES
(
	'2EBFECFC-ED13-43B8-B516-6AC89D51C510',
	'testMember@mail.com',
	'testMember@mail.com',
	'John',
	'Doe',
	'John Doe'
);

INSERT INTO sss_users.user_roles VALUES
(
	'2EBFECFC-ED13-43B8-B516-6AC89D51C510', 
	'Member'
);

-- Add Test Administrator
INSERT INTO sss_users.user_registrations VALUES 
(
	'4065630E-4A4C-4F01-9142-0BACF6B8C64D',
	'testAdmin@mail.com',
	'testAdmin@mail.com',
	'AK0qplH5peUHwnCVuzW9zy0JGZTTG6/Ji88twX+nw9JdTUwqa3Wol1K4m5aCG9pE2A==', -- testAdminPass
	'Jane',
	'Doe',
	'Jane Doe',
	'Confirmed',
	NOW(),
	NOW()
);

INSERT INTO sss_users.users VALUES
(
	'4065630E-4A4C-4F01-9142-0BACF6B8C64D',
	'testAdmin@mail.com',
	'testAdmin@mail.com',
	'AK0qplH5peUHwnCVuzW9zy0JGZTTG6/Ji88twX+nw9JdTUwqa3Wol1K4m5aCG9pE2A==', -- testAdminPass
	true,
	'Jane',
	'Doe',
	'Jane Doe'
);

INSERT INTO sss_users.user_roles VALUES
('4065630E-4A4C-4F01-9142-0BACF6B8C64D', 'Administrator');

-- Roles to Permissions

INSERT INTO sss_users.permissions (code, name) VALUES
	-- Meetings
	('GetMeetingGroupProposals', 'GetMeetingGroupProposals'),
	('ProposeMeetingGroup', 'ProposeMeetingGroup'),
	('CreateNewMeeting','CreateNewMeeting'),
	('EditMeeting','EditMeeting'),
	('AddMeetingAttendee','AddMeetingAttendee'),
	('RemoveMeetingAttendee','RemoveMeetingAttendee'),
	('AddNotAttendee','AddNotAttendee'),
	('ChangeNotAttendeeDecision','ChangeNotAttendeeDecision'),
	('SignUpMemberToWaitlist','SignUpMemberToWaitlist'),
	('SignOffMemberFromWaitlist','SignOffMemberFromWaitlist'),
	('SetMeetingHostRole','SetMeetingHostRole'),
	('SetMeetingAttendeeRole','SetMeetingAttendeeRole'),
	('CancelMeeting','CancelMeeting'),
	('GetAllMeetingGroups','GetAllMeetingGroups'),
	('EditMeetingGroupGeneralAttributes','EditMeetingGroupGeneralAttributes'),
	('JoinToGroup','JoinToGroup'),
	('LeaveMeetingGroup','LeaveMeetingGroup'),
	('AddMeetingComment','AddMeetingComment'),
	('EditMeetingComment','EditMeetingComment'),
	('RemoveMeetingComment','RemoveMeetingComment'),
	('AddMeetingCommentReply','AddMeetingCommentReply'),
	('LikeMeetingComment','LikeMeetingComment'),
	('UnlikeMeetingComment','UnlikeMeetingComment'),
	('EnableMeetingCommenting','EnableMeetingCommenting'),
	('DisableMeetingCommenting','DisableMeetingCommenting'),
	('MyMeetingGroupsView','MyMeetingGroupsView'),
	('AllMeetingGroupsView','AllMeetingGroupsView'),
	('SubscriptionView','SubscriptionView'),
	('EmailsView','EmailsView'),
	('MyMeetingsView','MyMeetingsView'),
	('GetAuthenticatedMemberMeetings','GetAuthenticatedMemberMeetings'),

	-- Administration
	('AcceptMeetingGroupProposal','AcceptMeetingGroupProposal'),
	('AdministrationsView','AdministrationsView'),

	-- Payments
	('RegisterPayment','RegisterPayment'),
	('BuySubscription','BuySubscription'),
	('RenewSubscription','RenewSubscription'),
	('CreatePriceListItem','CreatePriceListItem'),
	('ActivatePriceListItem','ActivatePriceListItem'),
	('DeactivatePriceListItem','DeactivatePriceListItem'),
	('ChangePriceListItemAttributes','ChangePriceListItemAttributes'),
	('GetAuthenticatedPayerSubscription','GetAuthenticatedPayerSubscription'),
	('GetPriceListItem','GetPriceListItem');

-- Meetings
INSERT INTO sss_users.roles_to_permissions VALUES ('Member', 'GetMeetingGroupProposals');
INSERT INTO sss_users.roles_to_permissions VALUES ('Member', 'ProposeMeetingGroup');
INSERT INTO sss_users.roles_to_permissions VALUES ('Member', 'CreateNewMeeting');
INSERT INTO sss_users.roles_to_permissions VALUES ('Member', 'EditMeeting');
INSERT INTO sss_users.roles_to_permissions VALUES ('Member', 'AddMeetingAttendee');
INSERT INTO sss_users.roles_to_permissions VALUES ('Member', 'RemoveMeetingAttendee');
INSERT INTO sss_users.roles_to_permissions VALUES ('Member', 'AddNotAttendee');
INSERT INTO sss_users.roles_to_permissions VALUES ('Member', 'ChangeNotAttendeeDecision');
INSERT INTO sss_users.roles_to_permissions VALUES ('Member', 'SignUpMemberToWaitlist');
INSERT INTO sss_users.roles_to_permissions VALUES ('Member', 'SignOffMemberFromWaitlist');
INSERT INTO sss_users.roles_to_permissions VALUES ('Member', 'SetMeetingHostRole');
INSERT INTO sss_users.roles_to_permissions VALUES ('Member', 'SetMeetingAttendeeRole');
INSERT INTO sss_users.roles_to_permissions VALUES ('Member', 'CancelMeeting');
INSERT INTO sss_users.roles_to_permissions VALUES ('Member', 'GetAllMeetingGroups');
INSERT INTO sss_users.roles_to_permissions VALUES ('Member', 'EditMeetingGroupGeneralAttributes');
INSERT INTO sss_users.roles_to_permissions VALUES ('Member', 'JoinToGroup');
INSERT INTO sss_users.roles_to_permissions VALUES ('Member', 'LeaveMeetingGroup');
INSERT INTO sss_users.roles_to_permissions VALUES ('Member', 'AddMeetingComment');
INSERT INTO sss_users.roles_to_permissions VALUES ('Member', 'EditMeetingComment');
INSERT INTO sss_users.roles_to_permissions VALUES ('Member', 'RemoveMeetingComment');
INSERT INTO sss_users.roles_to_permissions VALUES ('Member', 'AddMeetingCommentReply');
INSERT INTO sss_users.roles_to_permissions VALUES ('Member', 'LikeMeetingComment');
INSERT INTO sss_users.roles_to_permissions VALUES ('Member', 'UnlikeMeetingComment');
INSERT INTO sss_users.roles_to_permissions VALUES ('Member', 'GetAuthenticatedMemberMeetingGroups');
INSERT INTO sss_users.roles_to_permissions VALUES ('Member', 'GetMeetingGroupDetails');
INSERT INTO sss_users.roles_to_permissions VALUES ('Member', 'GetMeetingDetails');
INSERT INTO sss_users.roles_to_permissions VALUES ('Member', 'GetMeetingAttendees');
INSERT INTO sss_users.roles_to_permissions VALUES ('Member', 'MyMeetingsGroupsView');
INSERT INTO sss_users.roles_to_permissions VALUES ('Member', 'SubscriptionView');
INSERT INTO sss_users.roles_to_permissions VALUES ('Member', 'EmailsView');
INSERT INTO sss_users.roles_to_permissions VALUES ('Member', 'AllMeetingGroupsView');
INSERT INTO sss_users.roles_to_permissions VALUES ('Member', 'MyMeetingsView');
INSERT INTO sss_users.roles_to_permissions VALUES ('Member', 'GetAuthenticatedMemberMeetings');

-- Administration
INSERT INTO sss_users.roles_to_permissions VALUES ('Administrator', 'AcceptMeetingGroupProposal');
INSERT INTO sss_users.roles_to_permissions VALUES ('Administrator', 'AdministrationsView');

-- Payments
INSERT INTO sss_users.roles_to_permissions VALUES ('Member', 'RegisterPayment');
INSERT INTO sss_users.roles_to_permissions VALUES ('Member', 'BuySubscription');
INSERT INTO sss_users.roles_to_permissions VALUES ('Member', 'RenewSubscription');
INSERT INTO sss_users.roles_to_permissions VALUES ('Member', 'GetAuthenticatedPayerSubscription');
INSERT INTO sss_users.roles_to_permissions VALUES ('Member', 'GetPriceListItem');
INSERT INTO sss_users.roles_to_permissions VALUES ('Administrator', 'CreatePriceListItem');
INSERT INTO sss_users.roles_to_permissions VALUES ('Administrator', 'ActivatePriceListItem');
INSERT INTO sss_users.roles_to_permissions VALUES ('Administrator', 'DeactivatePriceListItem');
INSERT INTO sss_users.roles_to_permissions VALUES ('Administrator', 'ChangePriceListItemAttributes');
INSERT INTO sss_users.roles_to_permissions VALUES ('Administrator', 'GetPriceListItem');