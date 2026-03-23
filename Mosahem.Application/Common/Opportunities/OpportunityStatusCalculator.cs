using mosahem.Domain.Entities.Opportunities;
using mosahem.Domain.Enums;

namespace mosahem.Application.Common.Opportunities
{
    public static class OpportunityStatusCalculator
    {
        public static OpportunityStatus Calculate(
            Opportunity opportunity,
            int acceptedApplicantsCount,
            DateTime? utcNow = null)
        {
            ArgumentNullException.ThrowIfNull(opportunity);

            var now = utcNow ?? DateTime.UtcNow;
            var hasVacancy = acceptedApplicantsCount < opportunity.Vacancies;
            var resolvedStatus = opportunity.Status & OpportunityStatus.Stopped;

            if (now >= opportunity.EndDate)
            {
                resolvedStatus |= OpportunityStatus.Closed | OpportunityStatus.Ended;
                return resolvedStatus;
            }

            if (now >= opportunity.StartDate)
            {
                resolvedStatus |= OpportunityStatus.Active;
                resolvedStatus |= hasVacancy ? OpportunityStatus.Open : OpportunityStatus.Closed;
                return resolvedStatus;
            }

            resolvedStatus |= hasVacancy ? OpportunityStatus.Open : OpportunityStatus.Closed;
            return resolvedStatus;
        }

        public static bool TryApply(
            Opportunity opportunity,
            int acceptedApplicantsCount,
            DateTime? utcNow = null)
        {
            var resolvedStatus = Calculate(opportunity, acceptedApplicantsCount, utcNow);
            if (opportunity.Status == resolvedStatus)
            {
                return false;
            }

            opportunity.Status = resolvedStatus;
            return true;
        }

        public static List<string> ToNames(OpportunityStatus status)
        {
            return Enum.GetValues<OpportunityStatus>()
                .Where(value => status.HasFlag(value))
                .Select(value => value.ToString())
                .ToList();
        }
    }
}
