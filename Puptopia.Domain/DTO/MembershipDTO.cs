using Puptopia.Domain.Entities;
using Puptopia.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Puptopia.Domain.DTO
{
    /// <summary>
    /// This Data Transfer Object will carry membership information 
    /// between the domain and persistence layers.
    /// </summary>
    public class MembershipDTO
    {
        public DateTime ExpirationDate { get; set; }
        public DateTime InitialDate { get; set; }
        public string MembershipId { get; set; }
        public MembershipType MembershipType { get; set; }
        public DateTime RenewalDate { get; set; }
        private List<CustomerDTO> MembershipCustomers { get; set; }

        public IReadOnlyList<CustomerDTO> Customers
        {
            get
            {
                return MembershipCustomers;
            }
        }

        /// <summary>
        /// Use this constructor to populate data that can be read by
        /// the Customers readonly list.
        /// </summary>
        /// <param name="Customers">CustomerDTO objects associated with the MembershipDTO</param>
        public MembershipDTO(List<CustomerDTO> Customers)
        {
            MembershipCustomers = Customers;
        }

    }
}
