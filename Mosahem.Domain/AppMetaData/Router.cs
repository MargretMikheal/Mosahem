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
            public const string OrganizationData = "organization-data";
            public const string Fields = $"fields/{SingleRoute}";
            public const string Locations = $"locations/{SingleRoute}";
            public const string AllOrganizations = "all";
        }
        public static class UserRouting
        {
            public const string Prefix = $"{Base}/users";
            public const string UserInfo = "user-info";
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
        }
    }
}
