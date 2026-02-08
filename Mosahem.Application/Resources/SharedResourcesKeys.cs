namespace mosahem.Application.Resources
{
    public static class SharedResourcesKeys
    {
        public static class General
        {
            public const string Success = "Success";
            public const string Created = "Created";
            public const string Updated = "Updated";
            public const string Deleted = "Deleted";
            public const string NoContent = "NoContent";

            public const string OperationFailed = "OperationFailed";
            public const string ActionCompleted = "ActionCompleted";
        }
        public static class Success
        {
            public const string OtpSent = "Success_OtpSent";
            public const string OtpValid = "Success_OtpValid";
            public const string PasswordReset = "Success_PasswordReset";
            public const string AdminAdded = "Success_AdminAdded";
            public const string PasswordChanged = "Success_PasswordChanged";

            public const string Added = "Added";
        }

        public static class Validation
        {
            public const string Required = "Required";
            public const string Invalid = "Invalid";
            public const string BadRequest = "BadRequest";
            public const string UnprocessableEntity = "UnprocessableEntity";
            public const string CannotDeleteSelf = "Validation_CannotDeleteSelf";

            public const string MinLength = "MinLength";
            public const string MaxLength = "MaxLength";
            public const string OutOfRange = "OutOfRange";
            public const string DuplicateEntry = "DuplicateEntry";
            public const string NotFound = "NotFound";
            public const string OtpUsed = "Validation_OtpUsed";
            public const string OtpExpired = "Validation_OtpExpired";
            public const string PasswordResetFailed = "Validation_PasswordResetFailed";
            public const string PasswordRequiresUpper = "Validation_PasswordRequiresUpper";
            public const string PasswordRequiresLower = "Validation_PasswordRequiresLower";
            public const string PasswordRequiresDigit = "Validation_PasswordRequiresDigit";
            public const string PasswordRequiresNonAlphanumeric = "Validation_PasswordRequiresNonAlphanumeric";
        }

        public static class Auth
        {
            public const string Unauthorized = "Unauthorized";
            public const string Forbidden = "Forbidden";

            public const string LoginSuccess = "LoginSuccess";
            public const string LogoutSuccess = "LogoutSuccess";

            public const string InvalidCredentials = "InvalidCredentials";
            public const string InvalidToken = "InvalidToken";
            public const string TokenExpired = "TokenExpired";

            public const string AccountLocked = "AccountLocked";
            public const string AccountDisabled = "AccountDisabled";
        }

        public static class User
        {
            public const string NotFound = "UserNotFound";
            public const string EmailAlreadyTaken = "EmailAlreadyTaken";
            public const string UserNameAlreadyTaken = "UserNameAlreadyTaken";
            public const string EmailNotConfirmed = "EmailNotConfirmed";
            public const string PasswordsDoNotMatch = "PasswordsDoNotMatch";
        }

        public static class System
        {
            public const string FailedToAdd = "FailedToAdd";
            public const string FailedToUpdate = "FailedToUpdate";
            public const string FailedToDelete = "FailedToDelete";

            public const string ConcurrencyConflict = "ConcurrencyConflict";
        }

        public static class Http
        {
            public const string InternalServerError = "InternalServerError";
            public const string ServiceUnavailable = "ServiceUnavailable";
            public const string TooManyRequests = "TooManyRequests";
        }

        public static class Authorization
        {
            public const string PermissionDenied = "PermissionDenied";
            public const string RoleNotAllowed = "RoleNotAllowed";
        }

        public static class State
        {
            public const string AlreadyExists = "AlreadyExists";
            public const string AlreadyProcessed = "AlreadyProcessed";
            public const string OperationNotAllowed = "OperationNotAllowed";
        }
    }
}
