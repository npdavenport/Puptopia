using Puptopia.Domain.Enums;
using Puptopia.Domain.Exceptions;
using System;
using System.Collections.Generic;

namespace Puptopia.Domain.Entities
{
    internal class Membership
    {
        public DateTime ExpirationDate { get; set; }
        public DateTime InitialDate { get; set; }
        public bool IsActive { get; set; }
        public string MembershipId { get; set; }
        public MembershipType MembershipType { get; set; }
        public DateTime EarlyRenewalDate { get; set; }
        private List<Customer> MembershipCustomers { get; set; }

        /// <summary>
        /// Constructor needed in order to add the list
        /// of associated customers with the Membership.
        /// </summary>
        /// <param name="customers">The list of Customers 
        /// associated with the Membership.</param>
        public Membership(List<Customer> customers)
        {
            MembershipCustomers = customers;
        }

        /// <summary>
        /// Create a read-only list so the caller of the Membership class
        /// can have access to the Customers property values, yet be unable
        /// to add Customers directly to the list.  Instead, the caller must
        /// access the public AddCustomer method of the Membership class.
        /// </summary>
        public IReadOnlyList<Customer> Customers => MembershipCustomers;

        /// <summary>
        /// Adds a Customer to an existing membership.
        /// </summary>
        /// <param name="customer">The Customer object to add to the membership.</param>
        public void AddCustomer(Customer customer)
        {
            if (!customer.IsActive)
            {
                throw new UnableToAddCustomerException("Unable to add customer to Membership.  The customer is currently inactive.");
            }

            MembershipCustomers.Add(customer);
        }

        /// <summary>
        /// Removes a Customer from an existing membership.
        /// </summary>
        /// <param name="customer">The Customer Object to remove from the Membership.</param>
        public void RemoveCustomer(Customer customer)
        {
            MembershipCustomers.Remove(customer);
        }
    }
}
