using mosahem.Domain.Entities.Profiles;

namespace mosahem.Application.Interfaces.Repositories
{
    public interface IVolunteerRepository : IGenericRepository<Volunteer>
    {
        Task<Volunteer?> GetVolunteerWithDetailsAsync(Guid volunteerId, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<Volunteer>> GetVolunteersWithProfilesAsync(CancellationToken cancellationToken);
    }
}