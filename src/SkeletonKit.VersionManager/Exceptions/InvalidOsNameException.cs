namespace CME.VersionManager.Exceptions
{
    /// <summary>
    /// This exception is to be thrown when the os name sent in the header is invalid
    /// </summary>
    internal class InvalidOsNameException : VersionManagerBaseException
    {
        private static string InvalidOsNameExceptionCode = "invalid_os_name";
        private static string InvalidOsNameExceptionMessage = "Check the os name that you are sending!";

        public InvalidOsNameException() : base(InvalidOsNameExceptionCode, InvalidOsNameExceptionMessage) { }
    }
}
