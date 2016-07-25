using System;

namespace Puptopia.Domain.DTO
{
    public class CustomerRequiredFieldsDto
    {
        public string Address1 { get; set; }
        public string City { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string EmailAddress { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PostalCode { get; set; }
        public string PrimaryPhone { get; set; }
        public string State { get; set; }
    }
}
