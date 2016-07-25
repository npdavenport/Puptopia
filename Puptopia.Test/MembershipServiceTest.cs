using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FakeItEasy;
using Puptopia.Domain.Services;
using Puptopia.Domain.Interfaces;
using Puptopia.Domain.Entities;
using Puptopia.Domain.DTO;
using Puptopia.Domain.Exceptions;
using Puptopia.Test.Helpers;

namespace Puptopia.Test
{
    [TestClass]
    public class MembershipServiceTest
    {
        private IMembershipServiceRepository _membershipServiceRepository;
        private MembershipService _membershipService;

        private const string ValidMemberShipId = "VALIDMEMBERSHIPID";
        private const string InvalidMemberShipId = "INVALIDMEMBERSHIPID";
        private const string ExpiredMemberShipId = "EXPIREDMEMBERSHIPID";
        private readonly DateTime _initialDate = DateTime.Now.AddDays(-10);
        private readonly DateTime _expirationDate = DateTime.Now.AddYears(1);
        private readonly DateTime _earlyRenewalDate = DateTime.Now.AddMonths(10);

        private Customer _customer;
        private CustomerDto _customerDto;

        private Membership _membership;
        private MembershipDto _membershipDto;

        [TestInitialize]
        public void Initialize()
        {
            var customerInitializer = new CustomerInitializer();
            _customer = customerInitializer.CreateCustomer();
            _customerDto = customerInitializer.CreateCustomerDto();

            var membershipInitializer = new MembershipInitializer(new List<Customer>() { _customer}, 
                new List<CustomerDto>() { _customerDto });
            _membership = membershipInitializer.CreateMembership();
            _membershipDto = membershipInitializer.CreateMembershipDto();

            // FakeItEasy Initialization
            _membershipServiceRepository = A.Fake<IMembershipServiceRepository>();

            _membershipService = new MembershipService(_membershipServiceRepository);
        }

        [TestMethod]
        public void GetById_ValidMemberId_ReturnsMembership()
        {
            // Arrange
            _membershipService = new MembershipService(_membershipServiceRepository);
            _membership.MembershipId = ValidMemberShipId;
            _membershipDto.MembershipId = ValidMemberShipId;
            _membership.ExpirationDate = _expirationDate;
            _membership.InitialDate = _initialDate;
            _membership.EarlyRenewalDate = _earlyRenewalDate;
            A.CallTo(() => _membershipServiceRepository.GetById(ValidMemberShipId)).Returns(_membershipDto);

            // Act
            var membership = _membershipService.GetById(ValidMemberShipId);

            // Assert
            Assert.IsInstanceOfType(membership, typeof(Membership));
            Assert.AreEqual(_membershipDto.ExpirationDate, membership.ExpirationDate);
            Assert.AreEqual(_membershipDto.InitialDate, membership.InitialDate);
            Assert.AreEqual(_membershipDto.MembershipId, membership.MembershipId);
            Assert.AreEqual(_membershipDto.MembershipType, membership.MembershipType);
            Assert.AreEqual(_membershipDto.EarlyRenewalDate, membership.EarlyRenewalDate);
            Assert.AreEqual(_membershipDto.Customers[0].CustomerId, _membership.Customers[0].CustomerId);
        }

        [TestMethod, ExpectedException(typeof(ExpiredMembershipException))]
        public void GetById_ExpiredMemberId_ThrowsException()
        {
            // Arrange
            _membership.MembershipId = ExpiredMemberShipId;
            A.CallTo(() => _membershipServiceRepository.GetById(ExpiredMemberShipId)).Throws(new ExpiredMembershipException());

            // Act
            _membershipService.GetById(ExpiredMemberShipId);
        }

        [TestMethod]
        public void GetById_InvalidMemberId_ReturnsNull()
        {
            // Arrange
            A.CallTo(() => _membershipServiceRepository.GetById(InvalidMemberShipId)).Returns(null);

            // Act
            var membership = _membershipService.GetById(InvalidMemberShipId);

            // Asert
            Assert.IsNull(membership);
        }

        [TestMethod]
        public void Create_NewMembership_CreatesMembership()
        {
            // Arrange
            _membershipService = new MembershipService(_membershipServiceRepository);
            A.CallTo(() => _membershipServiceRepository.Save(A<MembershipDto>.Ignored))
                .Returns(_membershipDto);

            // Act
            var membership = _membershipService.Create(_customer);

            // Assert
            Assert.IsInstanceOfType(membership, typeof(Membership));
            Assert.IsNotNull(membership);
        }

        // TODO: RemoveCustomer, AddCustomer, UpdateMembership, DeactivateMembership
    }
}
