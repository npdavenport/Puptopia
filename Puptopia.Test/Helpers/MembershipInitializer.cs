using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Puptopia.Domain.DTO;
using Puptopia.Domain.Entities;
using Puptopia.Domain.Enums;

namespace Puptopia.Test.Helpers
{
    /// <summary>
    /// This class is used to initiate Membership objects that can be used
    /// in multiple test classes for the test harness.
    /// </summary>
    internal class MembershipInitializer
    {
        public DateTime ExpirationDate { get; set; }
        public DateTime InitialDate { get; set; }
        public bool IsActive { get; set; }
        public string MembershipId { get; set; }
        public MembershipType MembershipType { get; set; }
        public DateTime EarlyRenewalDate { get; set; }
        public List<Customer> Customers { get; set; }
        public List<CustomerDto> CustomerDtos { get; set; }

        internal MembershipInitializer()
        {
            ExpirationDate = DateTime.Now.AddYears(1);
            InitialDate = DateTime.Now;
            MembershipId = "MEMBERSHIPID";
            MembershipType = MembershipType.INDIVIDUAL;
            EarlyRenewalDate = DateTime.Now.AddMonths(10);
            Customers = new List<Customer>() { new CustomerInitializer().CreateCustomer() };
            CustomerDtos = new List<CustomerDto>() { new CustomerInitializer().CreateCustomerDto() };
        }

        internal MembershipInitializer(List<Customer> customers)
        {
            ExpirationDate = DateTime.Now.AddYears(1);
            InitialDate = DateTime.Now;
            MembershipId = "MEMBERSHIPID";
            MembershipType = MembershipType.INDIVIDUAL;
            EarlyRenewalDate = DateTime.Now.AddMonths(10);
            Customers = customers;
            CustomerDtos = new List<CustomerDto>() { new CustomerInitializer().CreateCustomerDto() };
        }

        internal MembershipInitializer(List<CustomerDto> customerDtos)
        {
            ExpirationDate = DateTime.Now.AddYears(1);
            InitialDate = DateTime.Now;
            MembershipId = "MEMBERSHIPID";
            MembershipType = MembershipType.INDIVIDUAL;
            EarlyRenewalDate = DateTime.Now.AddMonths(10);
            Customers = new List<Customer>() { new CustomerInitializer().CreateCustomer() };
            CustomerDtos = customerDtos;
        }

        internal MembershipInitializer(List<Customer> customers, List<CustomerDto> customerDtos)
        {
            ExpirationDate = DateTime.Now.AddYears(1);
            InitialDate = DateTime.Now;
            MembershipId = "MEMBERSHIPID";
            MembershipType = MembershipType.INDIVIDUAL;
            EarlyRenewalDate = DateTime.Now.AddMonths(10);
            Customers = customers;
            CustomerDtos = customerDtos;
        }

        #region Membership

        internal Membership CreateMembership()
        {
            return new Membership(Customers)
            {
                ExpirationDate = this.ExpirationDate,
                InitialDate = this.InitialDate,
                MembershipId = this.MembershipId,
                MembershipType = this.MembershipType,
                EarlyRenewalDate = this.EarlyRenewalDate
            };
        }

        #endregion

        #region MembershipDto

        internal MembershipDto CreateMembershipDto()
        {
            return new MembershipDto(CustomerDtos)
            {
                ExpirationDate = this.ExpirationDate,
                InitialDate = this.InitialDate,
                MembershipId = this.MembershipId,
                MembershipType = this.MembershipType,
                EarlyRenewalDate = this.EarlyRenewalDate
            };
        }

        #endregion
    }
}
