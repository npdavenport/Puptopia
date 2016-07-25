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
    public class MembershipDto
    {
        public DateTime ExpirationDate { get; set; }
        public DateTime InitialDate { get; set; }
        public bool IsActive { get; set; }
        public string MembershipId { get; set; }
        public MembershipType MembershipType { get; set; }
        public DateTime EarlyRenewalDate { get; set; }
        private List<CustomerDto> MembershipCustomers { get; set; }

        // Get only
        public IReadOnlyList<CustomerDto> Customers => MembershipCustomers;

        /// <summary>
        /// Use this constructor to populate data that can be read by
        /// the customers readonly list.
        /// </summary>
        /// <param name="customers">CustomerDto objects associated with the MembershipDto</param>
        public MembershipDto(List<CustomerDto> customers)
        {
            MembershipCustomers = customers;
        }

    }
}
