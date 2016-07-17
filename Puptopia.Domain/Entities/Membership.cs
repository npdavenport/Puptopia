using Puptopia.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Puptopia.Domain.Entities
{
    public class Membership
    {
        public DateTime ExpirationDate { get; set; }
        public DateTime InitialDate { get; set; }
        public string MembershipId { get; set; }
        public MembershipType MembershipType { get; set; }
        public DateTime RenewalDate { get; set; }
        private List<Customer> MembershipCustomers { get; set; }

        /// <summary>
        /// Create a read-only list so the caller of the Membership class
        /// can have access to the Customers property values, yet be unable
        /// to add Customers directly to the list.  Instead, the caller must
        /// access the public AddCustomer method of the Membership class.
        /// </summary>
        public IReadOnlyList<Customer> Customers
        {
            get {
                return MembershipCustomers;
            }
        }

        public void AddCustomer(Guid customerId)
        {
            throw new NotImplementedException();
        }
    }
}
