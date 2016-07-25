using System;
using AutoMapper;
using Puptopia.Domain.DTO;
using Puptopia.Domain.Interfaces;
using Puptopia.Domain.Entities;
using Puptopia.Domain.Exceptions;

namespace Puptopia.Domain.Services
{
    internal class CustomerService
    {
        #region Member Variables

        private readonly ICustomerServiceRepository _customerServiceRepository;
        private readonly IMapper _mapper;

        #endregion

        #region Constructors

        /// <summary>
        /// This constructor accepts an object that implements the ICustomerServiceRepository
        /// interface.  By supplying the interface, we are implementing dependency inversion as
        /// the depdencency to the persistence layer is done through an interface, thus helping 
        /// to de-couple the layers.  In other words, the type of persistence layer technology
        /// is not important as long as it implements the ICustomerServiceRepository.
        /// <param name="customerServiceRepository">ICustomerServiceRepository</param>
        /// </summary>
        internal CustomerService(ICustomerServiceRepository customerServiceRepository)
        {
            _customerServiceRepository = customerServiceRepository;
            var config = new Bootstrapper().GetCustomerMembershipServiceMapperConfiguration();
            _mapper = new Mapper(config);
        }

        #endregion

        #region Private Methods

        //
        // Summary
        //      Ensure that all required fields have been populated.
        private void ValidateRequiredFiels(CustomerRequiredFields requiredFields)
        {
            // Use reflection to iterate over the required fields.
            var type = requiredFields.GetType();
            var properties = type.GetProperties();
            foreach (var property in properties)
            {
                // Check for a NULL value
                if (property.GetValue(requiredFields, null) == null)
                {
                    throw new RequiredFieldMissingException(property.Name + " is null.");
                }
                // For String types, check for an empty field.
                if (property.PropertyType.Name == "String")
                {
                    if ((string)property.GetValue(requiredFields, null) == string.Empty)
                    {
                        throw new RequiredFieldMissingException(property.Name + " is not supplied or invalid.");
                    }
                }
                // For DateTime types, check for a min value.
                if (property.PropertyType.Name == "DateTime")
                {
                    if ((DateTime)property.GetValue(requiredFields, null) == DateTime.MinValue)
                    {
                        throw new RequiredFieldMissingException(property.Name + " is not supplied or invalid.");
                    }
                }
            }
        }

        //
        // Summary
        //      Create a Customer object after all fields have been validated
        private Customer CreateCustomer(CustomerRequiredFields requiredFields, string address2, string secondaryPhone)
        {
            return new Customer
            {
                Address2 = address2,
                CustomerId = Guid.NewGuid(),
                CustomerRequiredFields = requiredFields,
                IsActive = true,
                Membership = null,
                SecondaryPhone = secondaryPhone
            };
        }

        #endregion

        #region Internal Methods

        /// <summary>
        /// Retrieve a customer by using the supplied customer id.
        /// </summary>
        /// <param name="customerId">The unique id for the customer.</param>
        /// <returns>The requested Customer object.</returns>
        internal Customer GetById(Guid customerId)
        {
            var customerDto = _customerServiceRepository.GetById(customerId);
            return _mapper.Map<Customer>(customerDto);
        }

        /// <summary>
        /// Retrieve a customer by using the supplied required fields.
        /// </summary>
        /// <param name="requiredFields">KeyValuePair of required fields.</param>
        /// <returns>The requested Customer object.</returns>
        internal Customer GetByRequiredFields(CustomerRequiredFields requiredFields)
        {
            var requiredFieldsDto = _mapper.Map<CustomerRequiredFieldsDto>(requiredFields);
            var customerDto = _customerServiceRepository.GetByRequiredFields(requiredFieldsDto);
            return _mapper.Map<Customer>(customerDto);
        }

        /// <summary>
        /// Create a new customer by using the supplied Customer fields.
        /// </summary>
        /// <param name="requiredFields">The CustomerRequiredFields object containing data
        /// necessary for the creation of a customer.</param>
        /// <param name="address2">The optional addres line 2 address.</param>
        /// <param name="secondaryPhone">The optional secondary phone number.</param>
        /// <returns>The newly created Customer object.</returns>
        internal Customer Create(CustomerRequiredFields requiredFields, string address2, string secondaryPhone)
        {
            ValidateRequiredFiels(requiredFields);

            // Check if customer is underage
            if (requiredFields.DateOfBirth >= DateTime.Now.AddYears(-18))
            {
                throw new CustomerUnderAgeException();
            }

            // With validation complete, create a new customer.
            var customer = CreateCustomer(requiredFields, address2, secondaryPhone);

            // Save to persistence layer
            var customerDto = _mapper.Map<CustomerDto>(customer);
            _customerServiceRepository.Save(customerDto);

            return customer;
        }

        /// <summary>
        /// Deactivate a Customer in the system so they can no longer be part
        /// of the Puptopia club membership.
        /// </summary>
        /// <param name="customer">The Customer object to deactivate.</param>
        internal void Deactivate(Customer customer)
        {
            // Deactivate customer
            customer.IsActive = false;

            // Map
            var customerDto = _mapper.Map<CustomerDto>(customer);

            // Update changes
            _customerServiceRepository.Update(customerDto);
        }

        /// <summary>
        /// Update a Customer by saving changes to the persistence layer.
        /// </summary>
        /// <param name="customer">The Customer object to update.</param>
        internal void Update(Customer customer)
        {
            _customerServiceRepository.Update(_mapper.Map<CustomerDto>(customer));
        }

        #endregion
    }
}
