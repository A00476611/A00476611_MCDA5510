using CsvHelper.Configuration.Attributes;

namespace DotNetAssignment
{
    [NewLine("\n")]
    internal class Customer
    {
        [Name("First Name")]
        public string? firstName { get; set; }

        [Name("Last Name")]
        public string? lastName { get; set; }

        [Name("Street Number")]
        public string? streetNum { get; set; }

        [Name("Street")]
        public string? street { get; set; }

        [Name("City")]
        public string? city { get; set; }

        [Name("Province")]
        public string? province { get; set; }

        [Name("Postal Code")]
        public string? postalCode { get; set; }

        [Name("Country")]
        public string? country { get; set; }

        [Name("Phone Number")]
        public string? phoneNumber { get; set; }

        [Name("email Address")]
        public string? email { get; set; }

        [Name("Date"), Optional]
        public string? date { get; set; }
    }
}
