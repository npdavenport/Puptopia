using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FakeItEasy;
using Puptopia.Domain.Services;
using Puptopia.Domain;
using Puptopia.Domain.Entities;
using Puptopia.Domain.DTO;

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
        private MembershipDTO _validMembershipDto;

        [TestInitialize]
        public void Initialize()
        {
            var bootstrapper = new Bootstrapper();

            _validMembershipDto = bootstrapper.ValidMembershipDto;
            _membershipServiceRepository = A.Fake<IMembershipServiceRepository>();

            // Setup FakeItEasy expectations
            A.CallTo(() => _membershipServiceRepository.GetById(ValidMemberShipId))
                .Returns(_validMembershipDto);
        }

        [TestMethod]
        public void GetById_ValidMemberId_ReturnsMembership()
        {
            // Arrange
            _membershipService = new MembershipService(_membershipServiceRepository);

            // Act
            var membership = _membershipService.GetById(ValidMemberShipId);

            // Assert
            Assert.IsInstanceOfType(membership, typeof(Membership));
            Assert.IsTrue(membership.ExpirationDate > DateTime.Now);
            Assert.AreEqual(ValidMemberShipId, membership.MembershipId);
            Assert.AreEqual(_validMembershipDto.ExpirationDate, membership.ExpirationDate);
            Assert.AreEqual(_validMembershipDto.InitialDate, membership.InitialDate);
            Assert.AreEqual(_validMembershipDto.MembershipId, ValidMemberShipId);
            Assert.AreEqual(_validMembershipDto.MembershipType, membership.MembershipType);
            Assert.AreEqual(_validMembershipDto.RenewalDate, membership.RenewalDate);
        }
    }
}
