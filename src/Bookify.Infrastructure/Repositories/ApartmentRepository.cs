using Bookify.Domain.Appartments;

namespace Bookify.Infrastructure.Repositories;
internal sealed class ApartmentRepository : Repository<Apartment>, IApartmentRepository
{
    public ApartmentRepository(ApplicationDbContext context) : base(context)
    {
    }
}
