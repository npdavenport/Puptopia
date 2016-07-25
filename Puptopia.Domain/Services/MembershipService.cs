using System;
using System.Collections.Generic;
using AutoMapper;
using Puptopia.Domain.DTO;
using Puptopia.Domain.Entities;
using Puptopia.Domain.Interfaces;
using Puptopia.Domain.Utilities;

namespace Puptopia.Domain.Services
{
    /// <summary>
    /// This is the service class responsible for managaging membership functionality.
    /// </summary>
    internal class MembershipService
    {
        private readonly IMembershipServiceRepository _membershipServiceRepository;
        private readonly IMapper _mapper;

        #region Constructors

        /// <summary>
        /// This constructor accepts an object that implements the IMembershipServiceRepository
        /// interface.  By supplying the interface, we are implementing dependency inversion as
        /// the depdencency to the persistence layer is done through an interface, thus helping 
        /// to de-couple the layers.  In other words, the type of persistence layer technology
        /// is not important as long as it implements the IMembershipServiceRepository.
        /// </summary>
        /// <param name="membershipServiceRepository">IMembershipServiceRepository</param>
        internal MembershipService(IMembershipServiceRepository membershipServiceRepository)
        {
            // Initialize repository
            _membershipServiceRepository = membershipServiceRepository;

            // Initialize AutoMapper
            var config = new Bootstrapper().GetCustomerMembershipServiceMapperConfiguration();
            _mapper = new Mapper(config);
        }

        #endregion

        #region Private Method

        //
        // Summary
        //      Create a new Membership object.
        //
        // param customers: The list of Customer objects to associate with the membership.
        private Membership CreateNewMembership(Customer customer)
        {
            var now = DateTime.Now;
            var membership = new Membership(new List<Customer>() { customer });
            var membershipId = UtilityClass.GenerateMembershipId(DateTime.Now, Guid.NewGuid());

            membership.InitialDate = now;
            membership.ExpirationDate = now.AddYears(1);
            membership.MembershipId = membershipId;

            return membership;
        }

        #endregion

        #region Internal Methods

        /// <summary>
        /// Return a membership using the supplied id.
        /// </summary>
        /// <param name="membershipId">The membership id to lookup.</param>
        /// <returns>A valid membership.</returns>
        internal Membership GetById(string membershipId)
        {
            var membershipDto = _membershipServiceRepository.GetById(membershipId);
            return _mapper.Map<Membership>(membershipDto);
        }

        /// <summary>
        /// Creates a new Membership object.
        /// </summary>
        /// <param name="customer">The Customer to add to the membership upon creation.</param>
        /// <returns>The newly created Membership object.</returns>
        internal Membership Create(Customer customer)
        {
            // Call the private method to create the new membership.
            var membership = CreateNewMembership(customer);

            // Map to DTO
            var membershipDto = _mapper.Map<MembershipDto>(membership);

            // Save new membership
            _membershipServiceRepository.Save(membershipDto);

            return membership;
        }

        #endregion
    }
}
