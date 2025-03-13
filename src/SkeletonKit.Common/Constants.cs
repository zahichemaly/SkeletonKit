namespace SkeletonKit.Common
{
    public static class Constants
    {
        public const string System = "System";

        public static class ErrorCode
        {
            public const string InvalidRequest = "invalid_request";
            public const string GenericError = "generic_error";
            public const string ValidationError = "validation_error";

            public const string EmailNotFound = "email_not_found";
            public const string EmailNotConfirmed = "email_not_confirmed";
            public const string PhoneNumberNotConfirmed = "phonenumber_not_confirmed";
            public const string AccountLocked = "account_locked";
            public const string AttemptsExceeded = "attempts_exceeded";
            public const string WrongOTP = "wrong_otp";
            public const string NoOTP = "no_otp";
            public const string InvalidPassword = "invalid_password";
            public const string AccountAlreadyActivated = "account_already_activated";
        }

        public static class ThresholdType
        {
            public const string Email = "EMAIL";
            public const string OTP = "OTP";
            public const string OTPActivation = "OTPActivation";
        }

        public static class Headers
        {
            public const string TenantId = "X-TenantId";
            public const string TenantConfig = "X-TenantConfig";
            public const string SubSystem = "X-SubSystem";
            public const string TimeZone = "TimeZone";
            public const string OsName = "X-Os-Name";
            public const string AppVersion = "X-App-Version";
            public const string AppName = "app-version";
            public const string AcceptLanguage = "X-Accept-Language";
            public const string CorrelationId = "CorrelationId";
            public const string UserId = "UserName";
            public const string Bearer = "Bearer ";
            public const string WSRefresh = "ws-refresh";
            public const string ConversationId = "c-id";
        }

        public static class QueryParams
        {
            public const string Tenant = "tenant";
        }

        public static class Claims
        {
            public const string SubjectId = "sub";
            public const string UserName = "user_name";
            public const string Tenant = "tenant";
            public const string Name = "name";
            public const string GivenName = "given_name";
            public const string FamilyName = "family_name";
            public const string Email = "email";
            public const string Gender = "gender";
            public const string DateOfBirth = "date_of_birth";
        }

        public static class SendGridTemplate
        {
            public const string EmailConfirmation = "d-45336654198a4d45a5643d9ca7961650";
            public const string ForgotPassword = "d-f322c0dda9994fb49467410f7612a1e6";
            public const string EmailAlreadyRegistered = "d-7e80e475c0e741458c144d77f066c1f9";
        }

        public static class SendGridUnsubscribeGroup
        {
            public const string Profile = "42253";
        }

        public static class FilePath
        {
            public const string ProfilePicture = "ProfilePicture";
        }

        public static class LookupKeys
        {
            public const string Countries = "countries";
        }

        public static class HealthCheck
        {
            public const string Liveness = "Liveness";
            public const string Readiness = "Readiness";
        }

        public static class EnvironmentVariables
        {

            public const string STORAGE_ACCOUNT_URL = "STORAGE_ACCOUNT_URL";
            public const string STORAGE_ACCOUNT_NAME = "STORAGE_ACCOUNT_NAME";
            public const string STORAGE_ACCOUNT_KEY = "STORAGE_ACCOUNT_KEY";
            public const string STORAGE_CONTAINER_NAME = "STORAGE_CONTAINER_NAME";
            public const string STORAGE_TYPE = "STORAGE_TYPE";
            public const string STORAGE_FILE_PATH = "STORAGE_FILE_PATH";
            public const string STORAGE_FTP_HOST = "STORAGE_FTP_HOST";
            public const string STORAGE_FTP_USERNAME = "STORAGE_FTP_USERNAME";
            public const string STORAGE_FTP_PASSWORD = "STORAGE_FTP_PASSWORD";
        }
    }
}
