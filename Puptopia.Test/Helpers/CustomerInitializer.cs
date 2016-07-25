using System;
using System.Collections.Generic;
using Puptopia.Domain.DTO;
using Puptopia.Domain.Entities;

namespace Puptopia.Test.Helpers
{
    /// <summary>
    /// This class is used to initiate Customer objects that can be used
    /// in multiple test classes for the test harness.
    /// </summary>
    internal class CustomerInitializer
    {
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public Guid CustomerId { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string EmailAddress { get; set; }
        public string FirstName { get; set; }
        public bool IsActive { get; set; }
        public string LastName { get; set; }
        public string PostalCode { get; set; }
        public Membership Membership { get; set; }
        public MembershipDto MembershipDto { get; set; }
        public string PrimaryPhone { get; set; }
        public string SecondaryPhone { get; set; }
        public string State { get; set; }

        public CustomerInitializer()
        {
            Address1 = "ADDRESS1";
            Address2 = "ADDRESS2";
            City = "CITY";
            CustomerId = Guid.NewGuid();
            DateOfBirth = DateTime.Now;
            EmailAddress = "EMAILADDRESS";
            FirstName = "FIRSTNAME";
            IsActive = true;
            LastName = "LASTNAME";
            PostalCode = "POSTALCODE";
            PrimaryPhone = "PRIMARYPHONE";
            SecondaryPhone = "SECONDARYPHONE";
            State = "STATE";
        }
        #region Customer

        internal Customer CreateCustomer()
        {
            return new Customer
            {
                Address2 = this.Address2,
                CustomerId = this.CustomerId,
                CustomerRequiredFields = CreateCustomerRequiredFields(),
                SecondaryPhone = this.SecondaryPhone
            };
        }

        internal CustomerRequiredFields CreateCustomerRequiredFields()
        {
            return new CustomerRequiredFields
            {
                Address1  = this.Address1,
                City = this.City,
                DateOfBirth = this.DateOfBirth,
                EmailAddress = this.EmailAddress,
                FirstName = this.FirstName,
                LastName = this.LastName,
                PostalCode = this.PostalCode,
                PrimaryPhone = this.PrimaryPhone,
                State = this.State
            };
        }

        #endregion

        #region CustomerDto

        internal CustomerDto CreateCustomerDto()
        {
            return new CustomerDto
            {
                Address2 = this.Address2,
                CustomerId = this.CustomerId,
                CustomerRequiredFields = CreateCustomerRequiredFieldsDto(),
                SecondaryPhone = this.SecondaryPhone
            };
        }

        internal CustomerRequiredFieldsDto CreateCustomerRequiredFieldsDto()
        {
            return new CustomerRequiredFieldsDto
            {
                Address1 = this.Address1,
                City = this.City,
                DateOfBirth = this.DateOfBirth,
                EmailAddress = this.EmailAddress,
                FirstName = this.FirstName,
                LastName = this.LastName,
                PostalCode = this.PostalCode,
                PrimaryPhone = this.PrimaryPhone,
                State = this.State
            };
        }

        #endregion

    }
}
