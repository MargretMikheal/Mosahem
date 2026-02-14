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
            public const string Prefix = $"{Base}/admin";

            public const string AddAdmin = $"{Prefix}/add-admin";
            public const string DeleteAdmin = $"{Prefix}/delete-admin";
            public const string GetAdminById = $"{Prefix}/get-admin-by-id/{SingleRoute}";
            public const string GetAllAdmins = $"{Prefix}/get-all-admins";
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

        public static class FileRouting
        {
            public const string Prefix = $"{Base}/files";

            public const string Upload = $"{Prefix}/upload";
            public const string GetUrl = $"{Prefix}/get-url";
            public const string Delete = $"{Prefix}/delete";
        }

        public static class FieldRouting
        {
            public const string Prefix = $"{Base}/fields";

            public const string AddField = $"{Prefix}/add-field";
            public const string DeleteField = $"{Prefix}/delete-field/{SingleRoute}";
            public const string EditField = $"{Prefix}/edit-field";
            public const string GetAllFields = $"{Prefix}/get-all-fields";
        }

        public static class SkillRouting
        {
            public const string Prefix = $"{Base}/skills";
            public const string AddSkill = $"{Prefix}/add-skill";
            public const string DeleteSkill = $"{Prefix}/delete-skill/{SingleRoute}";
            public const string EditSkill = $"{Prefix}/edit-skill";
        }

        public static class CityRouting
        {
            public const string Prefix = $"{Base}/cities";
            public const string AddCity = $"{Prefix}/add-city";
            public const string GetCitiesByGovernate = $"{Prefix}/get-cities-by-governate/{SingleRoute}";
        }

        public static class GovernateRouting
        {
            public const string Prefix = $"{Base}/governates";
            public const string GetAllGovernates = $"{Prefix}/get-all-governates";
        }
    }
}
