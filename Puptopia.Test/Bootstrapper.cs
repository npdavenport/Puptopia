using Puptopia.Domain.DTO;
using Puptopia.Domain.Entities;
using System;
using System.Collections.Generic;

namespace Puptopia.Test
{
    /// <summary>
    /// Use this class to initialize objetcs to their valid state
    /// so they can be used in testing.  This class helps to reduce
    /// the amount of redundant initialization for tests.
    /// </summary>
    internal class Bootstrapper
    {
        public MembershipDTO ValidMembershipDto { get; set; }
        public CustomerDTO ValidCustomerDto1 { get; set; }
        public CustomerDTO ValidCustomerDto2 { get; set; }

        public Bootstrapper()
        {
            InitializeValidCustomerDto1();
            InitializeValidCustomerDto2();
            InitializeValidMembershipDto();
        }

        private void InitializeValidMembershipDto()
        {
            var customerDtoList = new List<CustomerDTO>() { ValidCustomerDto1, ValidCustomerDto2 };
            ValidMembershipDto = new MembershipDTO(customerDtoList);
            ValidMembershipDto.RenewalDate = DateTime.Now;
            ValidMembershipDto.ExpirationDate = DateTime.Now.AddDays(300);
            ValidMembershipDto.InitialDate = DateTime.Now.AddYears(-2);
            ValidMembershipDto.MembershipId = "VALIDMEMBERSHIPID";
            ValidMembershipDto.MembershipType = Domain.Enums.MembershipType.INDIVIDUAL;
        }

        private void InitializeValidCustomerDto1()
        {
            ValidCustomerDto1 = new CustomerDTO();
            ValidCustomerDto1.CustomerId = Guid.NewGuid();
        }

        private void InitializeValidCustomerDto2()
        {
            ValidCustomerDto2 = new CustomerDTO();
            ValidCustomerDto2.CustomerId = Guid.NewGuid();
        }
    }
}
