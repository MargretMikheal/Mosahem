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
            public const string DeleteAdmin = $"{Prefix}/{SingleRoute}";
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
            public const string ChangePassword = $"{Prefix}/change-password";
            public const string SendEmailVerification = $"{Prefix}/send-email-verification";
            public const string VerifyEmail = $"{Prefix}/verify-email";

            public static class OrganizationRegistration
            {
                public const string Path = $"{Prefix}/organization";

                public const string ValidateBasicInfo = $"{Path}/validate-basic-info";
                public const string ValidateLocations = $"{Path}/validate-locations";
                public const string ValidateFields = $"{Path}/validate-fields";
                public const string RegisterOrganization = $"{Path}/register-organization";
            }
        }
        public static class OrganizationRouting
        {
            public const string Prefix = $"{Base}/organizations";
            public const string OrganizationData = "me";
            public const string AllOrganizations = "";
            public const string GetPendingOrganizations = "pending";
            public const string GetOrganizationLisence = $"{SingleRoute}/license";
            public const string EditOrganizationInfo = "me";
            public const string ApproveOrganization = "approve";
            public const string RejectOrganization = "reject";

            public const string Fields = $"{Prefix}/{SingleRoute}/fields";
            public const string AddOrganizationField = $"{Prefix}/{SingleRoute}/fields";
            public const string DeleteOrganizationField = $"{Prefix}/{{OrganizationId}}/fields/{SingleRoute}";

            public const string Locations = $"{Prefix}/{SingleRoute}/locations";
            public const string AddOrganizationAddress = $"{Prefix}/addresses";
            public const string DeleteOrganizationAddress = $"{Prefix}/addresses/{SingleRoute}";
            public const string EditOrganizationAddress = $"{Prefix}/addresses/{SingleRoute}";
        }
        public static class OpportunityRouting
        {
            public const string Prefix = $"{Base}/opportunities";
            public const string Create = "";
            public const string GetById = SingleRoute;
            public const string GetPending = "pending";
            public const string Approve = $"{SingleRoute}/approve";
            public const string Reject = $"{SingleRoute}/reject";
            public const string Stop = $"{SingleRoute}/stop";
        }
        public static class UserRouting
        {
            public const string Prefix = $"{Base}/users";
            public const string UserInfo = "me";
            public const string AllUsers = "";
            public const string SendChangeEmailOtp = "email/change/send-otp";
            public const string ChangeEmailOtpVerification = "email/change/verify-otp";
            public const string ChangeEmail = "email/change";
        }

        public static class FileRouting
        {
            public const string Prefix = $"{Base}/files";

            public const string Upload = $"{Prefix}/upload";
            public const string GetUrl = $"{Prefix}/url";
            public const string Delete = Prefix;
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
