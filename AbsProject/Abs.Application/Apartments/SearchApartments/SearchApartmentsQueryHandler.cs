using Abs.Application.Abstractions.Data;
using Abs.Application.Abstractions.Messaging;
using Abs.Domain.Abstractions;
using Abs.Domain.Bookings;
using Dapper;

namespace Abs.Application.Apartments.SearchApartments
{
    internal sealed class SearchApartmentsQueryHandler : IQueryHandler<SearchApartmnetsQuery, IReadOnlyList<ApartmentResponse>>
    {
        private static readonly int[] ActiveBookingstatuses =
        {
            (int)BookingStatus.Reserved,
            (int)BookingStatus.Confirmed,
            (int)BookingStatus.Completed
        };

        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        public SearchApartmentsQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
        }

        public async Task<Result<IReadOnlyList<ApartmentResponse>>> Handle(SearchApartmnetsQuery request, CancellationToken cancellationToken)
        {
            using var connection = _sqlConnectionFactory.CreateConnection();

            if (request.StartDate > request.EndDate) 
            {
                return new List<ApartmentResponse>();
            }

            const string sql = """
                SELECT 
                    a.id AS Id,
                    a.name AS Name,
                    a.description AS Description,
                    a.price_amount AS Price,
                    a.price_currency AS Currency,
                    a.address_country AS Country,
                    a.address_state AS State,
                    a.address_zip_code AS ZipCode,
                    a.address_city AS City,
                    a.address_street AS Street
                FROM apartments AS a
                WHERE NOT EXITS (
                    SELECT 1
                    FROM bookings AS b
                    WHERE
                        b.apartment_id = a.id AND
                        b.duration_start <= @EndTime AND
                        b.duration_end >= @StartTime AND
                        b.status = ANY(@ActiveBookingStatuses)
                )
                """;

            var apartments = await connection
                .QueryAsync<ApartmentResponse, AddressResponse, ApartmentResponse>(
                    sql,
                    (apartment, address) =>
                    {
                        apartment.Address = address;

                        return apartment;
                    },
                    new
                    {
                        request.StartDate,
                        request.EndDate,
                        ActiveBookingstatuses
                    },
                    splitOn: "Country"
                );

            return apartments.ToList();
        }
    }
}
