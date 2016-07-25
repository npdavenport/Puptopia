using System;

namespace Puptopia.Domain.Entities
{
    internal class Customer
    {
        public string Address2 { get; set; }
        public Guid CustomerId { get; set; }
        public CustomerRequiredFields CustomerRequiredFields { get; set; }
        public bool IsActive { get; set; }
        public Membership Membership { get; set; }
        public string SecondaryPhone { get; set; }
    }
}
