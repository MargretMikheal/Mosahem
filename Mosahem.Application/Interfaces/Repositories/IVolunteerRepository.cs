using mosahem.Domain.Entities.Profiles;

namespace mosahem.Application.Interfaces.Repositories
{
    public interface IVolunteerRepository : IGenericRepository<Volunteer>
    {
        Task<IReadOnlyList<Volunteer>> GetVolunteersWithProfilesAsync(CancellationToken cancellationToken);
    }
}