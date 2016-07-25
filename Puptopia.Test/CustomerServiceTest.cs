using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Puptopia.Domain.Exceptions;
using Puptopia.Domain.Interfaces;
using Puptopia.Domain.Services;
using FakeItEasy;
using Puptopia.Domain.DTO;
using Puptopia.Domain.Entities;
using Puptopia.Test.Helpers;

namespace Puptopia.Test
{
    [TestClass]
    public class CustomerServiceTest
    {
        private ICustomerServiceRepository _customerServiceRepository;
        private CustomerService _customerService;
        private Customer _customer;
        private CustomerDto _customerDto;

        private readonly Guid _inactiveCustomerId = Guid.Parse("9D2B0228-4D0D-4C23-8B49-01A698857709");
        private readonly Guid _unknownCustomerId = Guid.Parse("9D2B0255-4D0D-4C23-8B49-01A698857709");
        private readonly Guid _validCustomerId = Guid.Parse("9D2B0255-4D0D-4C23-8B49-01A698857722");

        private const string InactiveFirstName = "INACTIVEFIRSTNAME";
        private const string ValidFirstName = "VALIDFIRSTNAME";

        [TestInitialize]
        public void Initialize()
        {
            var customerInitializer = new CustomerInitializer();
            _customer = customerInitializer.CreateCustomer();
            _customerDto = customerInitializer.CreateCustomerDto();

            // FakeItEasy
            _customerServiceRepository = A.Fake<ICustomerServiceRepository>();
            _customerService = new CustomerService(_customerServiceRepository);
        }

        [TestMethod]
        public void GetById_UnknownCustomer_ReturnsNull()
        {
            // Arrange
            _customer.CustomerId = _unknownCustomerId;
            A.CallTo(() => _customerServiceRepository.GetById(_unknownCustomerId)).Returns(null);

            // Act
            var customer = _customerService.GetById(_unknownCustomerId);

            // Assert
            Assert.IsNull(customer);
        }

        [TestMethod]
        public void GetById_ValidCustomer_ReturnsCustomer()
        {
            // Arrange
            _customer.CustomerId = _validCustomerId;
            _customerDto.CustomerId = _validCustomerId;
            A.CallTo(() => _customerServiceRepository.GetById(_validCustomerId)).Returns(_customerDto);

            // Act
            var customer = _customerService.GetById(_validCustomerId);

            // Assert
            Assert.IsInstanceOfType(customer, typeof(Customer));
            Assert.AreEqual(_customerDto.CustomerId, customer.CustomerId);
            Assert.AreEqual(_customerDto.Address2, customer.Address2);
            Assert.AreEqual(_customerDto.SecondaryPhone, customer.SecondaryPhone);
            Assert.AreEqual(_customerDto.IsActive, customer.IsActive);
            Assert.AreEqual(_customerDto.CustomerRequiredFields.FirstName, customer.CustomerRequiredFields.FirstName);
            Assert.AreEqual(_customerDto.CustomerRequiredFields.Address1, customer.CustomerRequiredFields.Address1);
            Assert.AreEqual(_customerDto.CustomerRequiredFields.City, customer.CustomerRequiredFields.City);
            Assert.AreEqual(_customerDto.CustomerRequiredFields.DateOfBirth, customer.CustomerRequiredFields.DateOfBirth);
            Assert.AreEqual(_customerDto.CustomerRequiredFields.EmailAddress, customer.CustomerRequiredFields.EmailAddress);
            Assert.AreEqual(_customerDto.CustomerRequiredFields.LastName, customer.CustomerRequiredFields.LastName);
            Assert.AreEqual(_customerDto.CustomerRequiredFields.PostalCode, customer.CustomerRequiredFields.PostalCode);
            Assert.AreEqual(_customerDto.CustomerRequiredFields.PrimaryPhone, customer.CustomerRequiredFields.PrimaryPhone);
            Assert.AreEqual(_customerDto.CustomerRequiredFields.State, customer.CustomerRequiredFields.State);
        }

        [TestMethod]
        public void GetById_InactiveCustomer_ReturnsCustomer()
        {
            // Arrange
            _customer.CustomerId = _inactiveCustomerId;
            _customerDto.CustomerId = _inactiveCustomerId;
            _customerDto.IsActive = false;
            A.CallTo(() => _customerServiceRepository.GetById(_inactiveCustomerId)).Returns(_customerDto);

            // Act
            var customer = _customerService.GetById(_inactiveCustomerId);

            // Assert
            Assert.IsInstanceOfType(customer, typeof(Customer));
            Assert.AreEqual(_customerDto.CustomerId, customer.CustomerId);
            Assert.AreEqual(_customerDto.Address2, customer.Address2);
            Assert.AreEqual(_customerDto.SecondaryPhone, customer.SecondaryPhone);
            Assert.AreEqual(false, customer.IsActive);
            Assert.AreEqual(_customerDto.CustomerRequiredFields.FirstName, customer.CustomerRequiredFields.FirstName);
            Assert.AreEqual(_customerDto.CustomerRequiredFields.Address1, customer.CustomerRequiredFields.Address1);
            Assert.AreEqual(_customerDto.CustomerRequiredFields.City, customer.CustomerRequiredFields.City);
            Assert.AreEqual(_customerDto.CustomerRequiredFields.DateOfBirth, customer.CustomerRequiredFields.DateOfBirth);
            Assert.AreEqual(_customerDto.CustomerRequiredFields.EmailAddress, customer.CustomerRequiredFields.EmailAddress);
            Assert.AreEqual(_customerDto.CustomerRequiredFields.LastName, customer.CustomerRequiredFields.LastName);
            Assert.AreEqual(_customerDto.CustomerRequiredFields.PostalCode, customer.CustomerRequiredFields.PostalCode);
            Assert.AreEqual(_customerDto.CustomerRequiredFields.PrimaryPhone, customer.CustomerRequiredFields.PrimaryPhone);
            Assert.AreEqual(_customerDto.CustomerRequiredFields.State, customer.CustomerRequiredFields.State);
        }

        [TestMethod]
        public void GetbyRequiredFields_UnknownCustomer_ReturnsNull()
        {
            // Arrange
            _customer.CustomerRequiredFields.FirstName = null;
            A.CallTo(
                () =>
                    _customerServiceRepository.GetByRequiredFields(
                        A<CustomerRequiredFieldsDto>.That.Matches(x => x.FirstName == null)))
                .Returns(null);

            // Act
            var customer = _customerService.GetByRequiredFields(_customer.CustomerRequiredFields);

            // Assert
            Assert.IsNull(customer);
        }

        [TestMethod]
        public void GetByRequiredFields_ValidCustomer_ReturnsCustomer()
        {
            // Arrange
            _customer.CustomerRequiredFields.FirstName = ValidFirstName;
            A.CallTo(
                () =>
                    _customerServiceRepository.GetByRequiredFields(
                        A<CustomerRequiredFieldsDto>.That.Matches(x => x.FirstName == ValidFirstName)))
                .Returns(_customerDto);

            // Act
            var customer = _customerService.GetByRequiredFields(_customer.CustomerRequiredFields);

            // Assert
            Assert.IsInstanceOfType(customer, typeof(Customer));
            Assert.IsInstanceOfType(customer, typeof(Customer));
            Assert.AreEqual(_customerDto.CustomerId, customer.CustomerId);
            Assert.AreEqual(_customerDto.Address2, customer.Address2);
            Assert.AreEqual(_customerDto.SecondaryPhone, customer.SecondaryPhone);
            Assert.AreEqual(_customerDto.IsActive, customer.IsActive);
            Assert.AreEqual(_customerDto.CustomerRequiredFields.FirstName, customer.CustomerRequiredFields.FirstName);
            Assert.AreEqual(_customerDto.CustomerRequiredFields.Address1, customer.CustomerRequiredFields.Address1);
            Assert.AreEqual(_customerDto.CustomerRequiredFields.City, customer.CustomerRequiredFields.City);
            Assert.AreEqual(_customerDto.CustomerRequiredFields.DateOfBirth, customer.CustomerRequiredFields.DateOfBirth);
            Assert.AreEqual(_customerDto.CustomerRequiredFields.EmailAddress, customer.CustomerRequiredFields.EmailAddress);
            Assert.AreEqual(_customerDto.CustomerRequiredFields.LastName, customer.CustomerRequiredFields.LastName);
            Assert.AreEqual(_customerDto.CustomerRequiredFields.PostalCode, customer.CustomerRequiredFields.PostalCode);
            Assert.AreEqual(_customerDto.CustomerRequiredFields.PrimaryPhone, customer.CustomerRequiredFields.PrimaryPhone);
            Assert.AreEqual(_customerDto.CustomerRequiredFields.State, customer.CustomerRequiredFields.State);

        }

        [TestMethod]
        public void GetByRequiredFields_InactiveCustomer_ReturnsCustomer()
        {
            // Arrange
            _customer.CustomerRequiredFields.FirstName = InactiveFirstName;
            _customerDto.IsActive = false;
            A.CallTo(
                () =>
                    _customerServiceRepository.GetByRequiredFields(
                        A<CustomerRequiredFieldsDto>.That.Matches(x => x.FirstName == InactiveFirstName)))
                .Returns(_customerDto);

            // Act
            var customer = _customerService.GetByRequiredFields(_customer.CustomerRequiredFields);

            // Assert
            Assert.IsInstanceOfType(customer, typeof(Customer));
            Assert.AreEqual(_customerDto.CustomerId, customer.CustomerId);
            Assert.AreEqual(_customerDto.Address2, customer.Address2);
            Assert.AreEqual(_customerDto.SecondaryPhone, customer.SecondaryPhone);
            Assert.AreEqual(false, customer.IsActive);
            Assert.AreEqual(_customerDto.CustomerRequiredFields.FirstName, customer.CustomerRequiredFields.FirstName);
            Assert.AreEqual(_customerDto.CustomerRequiredFields.Address1, customer.CustomerRequiredFields.Address1);
            Assert.AreEqual(_customerDto.CustomerRequiredFields.City, customer.CustomerRequiredFields.City);
            Assert.AreEqual(_customerDto.CustomerRequiredFields.DateOfBirth, customer.CustomerRequiredFields.DateOfBirth);
            Assert.AreEqual(_customerDto.CustomerRequiredFields.EmailAddress, customer.CustomerRequiredFields.EmailAddress);
            Assert.AreEqual(_customerDto.CustomerRequiredFields.LastName, customer.CustomerRequiredFields.LastName);
            Assert.AreEqual(_customerDto.CustomerRequiredFields.PostalCode, customer.CustomerRequiredFields.PostalCode);
            Assert.AreEqual(_customerDto.CustomerRequiredFields.PrimaryPhone, customer.CustomerRequiredFields.PrimaryPhone);
            Assert.AreEqual(_customerDto.CustomerRequiredFields.State, customer.CustomerRequiredFields.State);
        }

        [TestMethod, ExpectedException(typeof(RequiredFieldMissingException))]
        public void Create_NullFirstName_ThrowsError()
        {   
            // Arrange
            _customer.CustomerRequiredFields.FirstName = null;

            // Act
            _customerService.Create(_customer.CustomerRequiredFields, 
                _customer.Address2, _customer.SecondaryPhone);
        }

        [TestMethod, ExpectedException(typeof(RequiredFieldMissingException))]
        public void Create_EmptyFirstName_ThrowsError()
        {
            // Arrange
            _customer.CustomerRequiredFields.FirstName = string.Empty;

            // Act
            _customerService.Create(_customer.CustomerRequiredFields, 
                _customer.Address2, _customer.SecondaryPhone);
        }

        [TestMethod, ExpectedException(typeof(RequiredFieldMissingException))]
        public void Create_DateOfBirthMinValue_ThrowsError()
        {
            // Arrange
            _customer.CustomerRequiredFields.DateOfBirth = new DateTime();

            // Act
            _customerService.Create(_customer.CustomerRequiredFields, 
                _customer.Address2, _customer.SecondaryPhone);
        }

        [TestMethod, ExpectedException(typeof(CustomerUnderAgeException))]
        public void Create_UnderageCustomer_ThrowsException()
        {
            // Arrange
            _customer.CustomerRequiredFields.DateOfBirth = DateTime.Now.AddYears(-17);

            // Act
            _customerService.Create(_customer.CustomerRequiredFields, 
                _customer.Address2, _customer.SecondaryPhone);
        }

        [TestMethod]
        public void Create_ValidRequiredFields_CreatesCustomer()
        {
            // Arrange
            _customer.CustomerRequiredFields.DateOfBirth = DateTime.Now.AddYears(-18);
            _customer.CustomerRequiredFields.FirstName = ValidFirstName;
            A.CallTo(() => _customerServiceRepository.Save(A<CustomerDto>.That.Matches(
                x => x.CustomerRequiredFields.FirstName == ValidFirstName))).DoesNothing();

            // Act
            var customer = _customerService.Create(_customer.CustomerRequiredFields, 
                _customer.Address2, _customer.SecondaryPhone);

            // Assert
            Assert.IsInstanceOfType(customer, typeof(Customer));

        }

        [TestMethod]
        public void Deactivate_Customer_ThrowsNoException()
        {
            // Arrange
            _customer.IsActive = true;
            A.CallTo(() => _customerServiceRepository.Update(A<CustomerDto>.That.Matches(x => x.IsActive)))
                .DoesNothing();

            // Act
            _customerService.Deactivate(_customer);
        }

        [TestMethod]
        public void Update_Customer_ThrowsNoException()
        {
            // Arrange
            _customer.Address2 = "NEWADDRESS";
            A.CallTo(() => _customerServiceRepository.Update(A<CustomerDto>.That.Matches(x => x.Address2 == "NEWADDRESS")))
                .DoesNothing();

            // Act
            _customerService.Update(_customer);
        }
        
    }
}
