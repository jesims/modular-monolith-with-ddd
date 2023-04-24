using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CompanyName.MyMeetings.BuildingBlocks.Application.Data;
using CompanyName.MyMeetings.Modules.Meetings.Application.Configuration.Queries;
using Dapper;

namespace CompanyName.MyMeetings.Modules.Meetings.Application.MeetingComments.GetMeetingCommentLikes
{
    internal class GetMeetingCommentLikersQueryHandler : IQueryHandler<GetMeetingCommentLikersQuery, List<MeetingCommentLikerDto>>
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        public GetMeetingCommentLikersQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
        }

        public async Task<List<MeetingCommentLikerDto>> Handle(GetMeetingCommentLikersQuery query, CancellationToken cancellationToken)
        {
            var connection = _sqlConnectionFactory.GetOpenConnection();

            var sql =
                "SELECT " +
                $"  liker.id AS {nameof(MeetingCommentLikerDto.Id)}," +
                $"  liker.name AS {nameof(MeetingCommentLikerDto.Name)} " +
                "FROM sss_meetings.members AS liker " +
                "   INNER JOIN sss_meetings.meeting_member_comment_likes AS comment_likes " +
                "       ON liker.id = comment_likes.member_id " +
                "WHERE comment_likes.meeting_comment_id = @MeetingCommentId";

            var meetingCommentLikers = await connection.QueryAsync<MeetingCommentLikerDto>(sql, new { query.MeetingCommentId });

            return meetingCommentLikers.AsList();
        }
    }
}