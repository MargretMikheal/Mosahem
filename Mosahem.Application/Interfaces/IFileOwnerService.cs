using mosahem.Domain.Entities.Opportunities;
using mosahem.Domain.Enums;

namespace Mosahem.Application.Interfaces
{
    public interface IFileOwnerService<T>
        where T : class
    {
        bool CanEditFileAsync(
            T owner,
            StorageFolder folder,
            Opportunity? opportunity);

        string? GetOldFileKeyAsync(
             T owner,
             StorageFolder folder,
             Opportunity? opportunity);

        void SetNewFileKeyAsync(
            T owner,
            StorageFolder folder,
            string newKey,
            Opportunity? opportunity);
    }
}
