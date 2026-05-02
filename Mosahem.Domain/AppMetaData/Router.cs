namespace Mosahem.Domain.AppMetaData
{
    public static class Router
    {
        public const string Root = "api";
        public const string Version = "v1";
        public const string Base = $"{Root}/{Version}";

        public const string SingleRoute = "{id}";
        public static class AdminRouting
        {
            public const string Prefix = $"{Base}/admins";

            public const string AddAdmin = Prefix;
            public const string GetAdminById = $"{Prefix}/{SingleRoute}";
            public const string GetAllAdmins = Prefix;
            public const string EditAdminInfo = $"{Prefix}/me";

        }

        public static class AuthRouting
        {
            public const string Prefix = $"{Base}/auth";

            public const string Login = $"{Prefix}/login";
            public const string RefreshToken = $"{Prefix}/refresh-token";
            public const string RevokeToken = $"{Prefix}/revoke-token";
            public const string ForgetPassword = $"{Prefix}/forgot-password";
            public const string VerifyOtp = $"{Prefix}/verify-otp";
            public const string ResetPassword = $"{Prefix}/reset-password";

            public const string SendEmailVerification = $"{Prefix}/send-email-verification";
            public const string VerifyEmail = $"{Prefix}/verify-email";
            public const string ValidateLocations = $"{Prefix}/validate-locations";

            public static class OrganizationRegistration
            {
                public const string Path = $"{Prefix}/organization";

                public const string ValidateBasicInfo = $"{Path}/validate-basic-info";
                public const string RegisterOrganization = $"{Path}/register-organization";
            }
            public static class VolunteerRegistration
            {
                public const string Path = $"{Prefix}/volunteer";

                public const string ValidateBasicInfo = $"{Path}/validate-basic-info";
                public const string RegisterVolunteer = $"{Path}";
            }
        }
        public static class OrganizationRouting
        {
            public const string Prefix = $"{Base}/organizations";
            public const string OrganizationData = "me";
            public const string AllOrganizations = "";
            public const string OrganizationFollowers = $"{SingleRoute}/followers";

            public const string GetPendingOrganizations = "pending";
            public const string GetOrganizationLisence = $"{SingleRoute}/license";
            public const string EditOrganizationInfo = "me";
            public const string ApproveOrganization = "approve";
            public const string RejectOrganization = "reject";

            public const string GetVerificationComment = $"{SingleRoute}/verification-comment";
            public const string EditVerificationComment = "verification-comment";

            public const string Fields = $"{Prefix}/{SingleRoute}/fields";

            public const string Locations = $"{Prefix}/{SingleRoute}/locations";
            public const string AddOrganizationAddress = $"{Prefix}/addresses";
            public const string DeleteOrganizationAddress = $"{Prefix}/addresses/{SingleRoute}";
            public const string EditOrganizationAddress = $"{Prefix}/addresses/{SingleRoute}";
            public const string EditFields = $"{SingleRoute}/fields";
            public const string EditAboutUs = $"{SingleRoute}/about-us";
            public const string GetVolunteersByVerificationStatus = "me/volunteers/by-verification-status";
            public const string GetOpportunitiesByVerificationStatus = $"{Prefix}/{{organizationId}}/opportunities/by-verification-status";
            public const string GetOpportunitiesByStatus = $"{Prefix}/{{organizationId}}/opportunities/by-status";

            public const string GetUnratedVolunteers = $"volunteers/unrated";
        }
        public static class VolunteerRouting
        {
            public const string Prefix = $"{Base}/volunteers";
            public const string EditAddress = $"{Prefix}/address/me";
            public const string DeleteAddress = $"{Prefix}/address/me";
            public const string EditFields = $"{Prefix}/fields/me";
            public const string EditSkills = $"{Prefix}/skills/me";
            public const string EditBasicInfo = $"{Prefix}/basic-info/me";
            public const string FollowOrganization = "follow/{organizationId}";
            public const string UnfollowOrganization = "unfollow/{organizationId}";
            public const string GetAllVolunteers = $"";
            public const string VolunteerFollowedOrganizations = "me/followed-organizations";
            public const string VolunteerProfileById = "{id}/profile";
        }
        public static class OpportunityRouting
        {
            public const string Prefix = $"{Base}/opportunities";
            public const string Create = "";
            public const string GetAll = "all";
            public const string GetById = SingleRoute;
            public const string GetByVerificationStatus = "by-verification-status";
            public const string Approve = $"{SingleRoute}/approve";
            public const string Reject = $"{SingleRoute}/reject";
            public const string Stop = $"{SingleRoute}/stop";
            public const string Resume = $"{SingleRoute}/resume";
            public const string EditFields = $"{SingleRoute}/fields";
            public const string EditSkills = $"{SingleRoute}/skills";
            public const string EditQuestions = $"{SingleRoute}/questions";
            public const string EditInfo = $"{SingleRoute}/info";

            public const string Apply = $"{SingleRoute}/apply";

            public const string Like = $"{SingleRoute}/like";
            public const string Save = $"{SingleRoute}/save";
            public const string Comment = $"{SingleRoute}/comment";
            public const string GetLikes = $"{SingleRoute}/likes";
            public const string GetSaves = $"{SingleRoute}/saves";
            public const string GetComments = $"{SingleRoute}/comments";
            public const string GetApplicantsByStatus = $"{SingleRoute}/applicants";
            public const string AcceptApplicant = $"{SingleRoute}/applicants/{{applicantId}}/accept";
            public const string RejectApplicant = $"{SingleRoute}/applicants/{{applicantId}}/reject";
            public const string RateApplicant = $"{SingleRoute}/applicants/{{applicantId}}/rating";
            public const string GetApplication = $"{SingleRoute}/volunteers/{{volunteerId}}/application";
        }
        public static class UserRouting
        {
            public const string Prefix = $"{Base}/users";
            public const string UserInfo = "me";
            public const string AllUsers = "";
            public const string SendChangeEmailOtp = "email/change/send-otp";
            public const string ChangeEmailOtpVerification = "email/change/verify-otp";
            public const string ChangeEmail = "email/change";
            public const string ChangePassword = $"password";
            public const string DeleteUser = SingleRoute;
        }

        public static class FileRouting
        {
            public const string Prefix = $"{Base}/files";

            public const string Upload = $"{Prefix}/upload";
            public const string GetUrl = $"{Prefix}/url";
            public const string Delete = Prefix;
            public const string Edit = Prefix;
        }

        public static class FieldRouting
        {
            public const string Prefix = $"{Base}/fields";

            public const string AddField = Prefix;
            public const string DeleteField = $"{Prefix}/{SingleRoute}";
            public const string EditField = $"{Prefix}/{SingleRoute}";
            public const string GetAllFields = $"{Prefix}/get-all-fields";
        }

        public static class SkillRouting
        {
            public const string Prefix = $"{Base}/skills";
            public const string AddSkill = Prefix;
            public const string DeleteSkill = $"{Prefix}/{SingleRoute}";
            public const string EditSkill = $"{Prefix}/{SingleRoute}";
            public const string GetAllSkillsGrouped = $"{Prefix}/grouped";
            public const string GetAllSkills = $"{Prefix}";
        }

        public static class CityRouting
        {
            public const string Prefix = $"{Base}/cities";
            public const string AddCity = Prefix;
            public const string GetCitiesByGovernate = $"{Prefix}/get-cities-by-governate/{SingleRoute}";
        }

        public static class GovernateRouting
        {
            public const string Prefix = $"{Base}/governates";
            public const string GetAllGovernates = $"{Prefix}/get-all-governates";
        }


    }
}
