using System;

namespace Puptopia.Domain.DTO
{
    public  class CustomerDto
    {
        public string Address2 { get; set; }
        public Guid CustomerId { get; set; }
        public CustomerRequiredFieldsDto CustomerRequiredFields { get; set; }
        public bool IsActive { get; set; }
        public MembershipDto Membership;
        public string SecondaryPhone { get; set; }
    }
}
