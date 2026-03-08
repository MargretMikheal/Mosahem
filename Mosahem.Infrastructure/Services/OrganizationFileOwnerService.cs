using mosahem.Application.Interfaces.Repositories;
using mosahem.Domain.Entities.Opportunities;
using mosahem.Domain.Entities.Profiles;
using mosahem.Domain.Enums;
using Mosahem.Application.Interfaces;

namespace Mosahem.Infrastructure.Services
{
    public class OrganizationFileOwnerService : IFileOwnerService<Organization>
    {
        private readonly IUnitOfWork _unitOfWork;

        public OrganizationFileOwnerService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public bool CanEditFileAsync(Organization owner, StorageFolder folder, Opportunity? opportunity)
        {
            switch (folder)
            {
                case StorageFolder.Licenses:
                    return true;

                case StorageFolder.OrganizationLogos:
                    return true;

                case StorageFolder.OpportunityPhotos:
                    return opportunity is not null;

                default:
                    return false;
            }

        }

        public string? GetOldFileKeyAsync(Organization owner, StorageFolder folder, Opportunity? opportunity)
        {
            switch (folder)
            {
                case StorageFolder.Licenses:
                    return owner.LicenseKey;

                case StorageFolder.OrganizationLogos:
                    return owner.LogoKey;

                case StorageFolder.OpportunityPhotos:
                    ArgumentNullException.ThrowIfNull(opportunity);
                    return opportunity.PhotoKey;
                default:
                    throw new ArgumentOutOfRangeException("Invalid Folder");
            }

        }

        public void SetNewFileKeyAsync(Organization owner, StorageFolder folder, string newKey, Opportunity? opportunity)
        {
            switch (folder)
            {
                case StorageFolder.Licenses:
                    owner.LicenseKey = newKey;
                    break;

                case StorageFolder.OrganizationLogos:
                    owner.LogoKey = newKey;
                    break;

                case StorageFolder.OpportunityPhotos:
                    ArgumentNullException.ThrowIfNull(opportunity);
                    opportunity.PhotoKey = newKey;
                    break;

                default:
                    throw new ArgumentOutOfRangeException("Invalid Folder");
            }
        }
    }
}
