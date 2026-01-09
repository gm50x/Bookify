using Bogus;
using Bookify.Application.Abstractions.Data;
using Bookify.Domain.Appartments;
using Dapper;

namespace Bookify.WebApi.Extensions;

public static class SeedDataExtensions
{
    public static void SeedData(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();
        var sqlConnectionFactory = scope.ServiceProvider.GetRequiredService<ISqlConnectionFactory>();
        using var connection = sqlConnectionFactory.CreateConnection();

        var countSql = "SELECT COUNT(1) FROM public.apartments";
        var countResult = connection.Query(countSql);

        if (countResult.First().count > 0)
        {
            return;
        }

        var faker = new Faker();

        List<object> apartments = new();

        for (var i = 0; i < 100; i++)
        {
            apartments.Add(new
            {
                Id = Guid.NewGuid(),
                Name = faker.Company.CompanyName(),
                Description = faker.Lorem.Paragraph(),
                Country = faker.Address.Country(),
                State = faker.Address.State(),
                ZipCode = faker.Address.ZipCode(),
                City = faker.Address.City(),
                Street = faker.Address.StreetAddress(),
                AddressNumber = faker.Address.BuildingNumber(),
                PriceAmount = faker.Random.Decimal(50, 1000),
                PriceCurrency = "USD",
                CleaningFeeAmount = faker.Random.Decimal(25, 200),
                CleaningFeeCurrency = "USD",
                Amenities = new List<int> { (int)Amenity.Parking, (int)Amenity.MountainView },
                LastBookedOnUtc = DateTime.MinValue
            });
        }

        const string sql = """
                INSERT INTO public.apartments
                (id, "name", description, address_country, address_state, address_zip_code, address_city, address_street, address_number, price_amount, price_currency, cleaning_fee_amount, cleaning_fee_currency, amenities, last_booked_on_utc)
                VALUES(@Id, @Name, @Description, @Country, @State, @ZipCode, @City, @Street, @AddressNumber, @PriceAmount, @PriceCurrency, @CleaningFeeAmount, @CleaningFeeCurrency, @Amenities, @LastBookedOnUtc)
                """;

        connection.Execute(sql, apartments);
    }
}
