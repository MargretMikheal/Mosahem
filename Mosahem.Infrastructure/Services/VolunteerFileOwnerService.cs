using mosahem.Domain.Entities.Opportunities;
using mosahem.Domain.Entities.Profiles;
using mosahem.Domain.Enums;
using Mosahem.Application.Interfaces;

namespace Mosahem.Infrastructure.Services
{
    public class VolunteerFileOwnerService : IFileOwnerService<Volunteer>
    {
        public bool CanEditFileAsync(Volunteer owner, StorageFolder folder, Opportunity? opportunity)
        {
            return folder switch
            {
                StorageFolder.VolunteerProfileImages => true,
                StorageFolder.VolunteerCoverImages => true,
                StorageFolder.VolunteerCVs => true,
                _ => false
            };
        }

        public string? GetOldFileKeyAsync(Volunteer owner, StorageFolder folder, Opportunity? opportunity)
        {
            return folder switch
            {
                StorageFolder.VolunteerProfileImages => owner.ProfileImgKey,
                StorageFolder.VolunteerCoverImages => owner.CoverImgKey,
                StorageFolder.VolunteerCVs => owner.CVKey,
                _ => throw new ArgumentOutOfRangeException("Invalid Folder")
            };
        }

        public void SetNewFileKeyAsync(Volunteer owner, StorageFolder folder, string newKey, Opportunity? opportunity)
        {
            switch (folder)
            {
                case StorageFolder.VolunteerProfileImages:
                    owner.ProfileImgKey = newKey;
                    break;

                case StorageFolder.VolunteerCoverImages:
                    owner.CoverImgKey = newKey;
                    break;

                case StorageFolder.VolunteerCVs:
                    owner.CVKey = newKey;
                    break;

                default:
                    throw new ArgumentOutOfRangeException("Invalid Folder");
            }
        }
    }
}
