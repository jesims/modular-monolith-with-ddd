using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CompanyName.MyMeetings.BuildingBlocks.Application.Data;
using CompanyName.MyMeetings.Modules.Meetings.Application.Configuration.Queries;
using Dapper;

namespace CompanyName.MyMeetings.Modules.Meetings.Application.Countries
{
    internal class GetAllCountriesQueryHandler : IQueryHandler<GetAllCountriesQuery, List<CountryDto>>
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        public GetAllCountriesQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
        }

        public async Task<List<CountryDto>> Handle(GetAllCountriesQuery query, CancellationToken cancellationToken)
        {
            var connection = _sqlConnectionFactory.GetOpenConnection();

            return (await connection.QueryAsync<CountryDto>(
                "SELECT " +
                $"country.code AS {nameof(CountryDto.Code)}, " +
                $"country.name AS {nameof(CountryDto.Name)} " +
                "FROM sss_meetings.countries AS country")).AsList();
        }
    }
}