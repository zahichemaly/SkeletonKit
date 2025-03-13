namespace SkeletonKit.VersionManager.Exceptions
{
    /// <summary>
    /// This is the base exception class for InvalidOsNameException and VersionFormatExcetpion
    /// </summary>
    internal class VersionManagerBaseException : Exception
    {
        public string Code { get; private set; }
        public VersionManagerBaseException(string code, string message) : base(message)
        {
            this.Code = code;
        }
    }
}
