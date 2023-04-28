using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CompanyName.MyMeetings.BuildingBlocks.Application.Data;
using CompanyName.MyMeetings.Modules.Meetings.Application.Configuration.Queries;
using Dapper;

namespace CompanyName.MyMeetings.Modules.Meetings.Application.MeetingComments.GetMeetingComments
{
    internal class GetMeetingCommentsQueryHandler : IQueryHandler<GetMeetingCommentsQuery, List<MeetingCommentDto>>
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        public GetMeetingCommentsQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
        }

        public async Task<List<MeetingCommentDto>> Handle(GetMeetingCommentsQuery query, CancellationToken cancellationToken)
        {
            var connection = _sqlConnectionFactory.GetOpenConnection();

            string sql = "SELECT " +
                         $"comment.id AS {nameof(MeetingCommentDto.Id)}, " +
                         $"comment.in_reply_to_comment_id AS {nameof(MeetingCommentDto.InReplyToCommentId)}, " +
                         $"comment.author_id AS {nameof(MeetingCommentDto.AuthorId)}, " +
                         $"comment.comment AS {nameof(MeetingCommentDto.Comment)}, " +
                         $"comment.create_date AS {nameof(MeetingCommentDto.CreateDate)}, " +
                         $"comment.edit_date AS {nameof(MeetingCommentDto.EditDate)}, " +
                         $"comment.likes_count AS {nameof(MeetingCommentDto.LikesCount)} " +
                         "FROM sss_meetings.meeting_comments AS comment " +
                         "WHERE comment.meeting_id = @MeetingId " +
                         "AND comment.is_removed = false";
            var meetingComments = await connection.QueryAsync<MeetingCommentDto>(sql, new { query.MeetingId });

            return meetingComments.AsList();
        }
    }
}