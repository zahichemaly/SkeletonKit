namespace CME.Common.Entities
{
    public sealed class UserProfile : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Nickname { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string CountryCode { get; set; }
        public string ImageUrl { get; set; }

        public string GetFullName() => $"{FirstName} {LastName}";
    }
}
