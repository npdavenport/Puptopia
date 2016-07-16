using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FakeItEasy;

namespace Puptopia.Test
{
    [TestClass]
    public class MembershipServiceTest
    {
        private IMembershipServiceRepository _membershipServiceRepository;
        private MembershipService _membershipService;

        private const string ValidMemberId = "VALIDMEMBERID";
        private const string InvalidMemberId = "INVALIDMEMBERID";
        private const string ExpiredMemberId = "EXPIREDMEMBERID";

        [TestInitialize]
        public void Initialize()
        {
            _membershipServiceRepository = A.Fake<IMembershipServiceRepository>();
        }

        [TestMethod]
        public void GetById_ValidMemberId_ReturnsMembership()
        {
            // Arrange
            _membershipService = new MembershipService(_membershipServiceRepository);

            // Act
            var membership = _membershipService.GetById(ValidMemberId);

            // Assert
            Assert.IsInstanceOfType(membership, typeof(Membership));
            Assert.IsTrue(membership.ExpirationDate > DateTime.Now);
            Assert.AreEqual(ValidMemberId, membership.MemberId);
        }
    }
}
