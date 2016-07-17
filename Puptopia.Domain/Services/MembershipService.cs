using AutoMapper;
using Puptopia.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Puptopia.Domain.Services
{
    /// <summary>
    /// This is the service class responsible for managaging membership functionality.
    /// </summary>
    public class MembershipService
    {
        private readonly IMembershipServiceRepository _membershipServiceRepository;
        private readonly IMapper _mapper;

        /// <summary>
        /// This constructor accepts an object that implements the IMembershipServiceRepository
        /// interface.  By supplying the interface, we are implementing dependency inversion as
        /// the depdencency to the persistence layer is done through an interface, thus helping 
        /// to de-couple the layers.  In other words, the type of persistence layer technology
        /// is not important as long as it implements the IMembershipServiceRepository.
        /// </summary>
        /// <param name="membershipServiceRepository">A repository object allowing access to the memberships.</param>
        public MembershipService(IMembershipServiceRepository membershipServiceRepository)
        {
            // Initialize repository
            _membershipServiceRepository = membershipServiceRepository;

            // Initialize AutoMapper
            var config = new Bootstrapper().GetMembershipServiceMapperConfiguration();
            _mapper = new Mapper(config);
        }

        /// <summary>
        /// Return a membership using the supplied id.
        /// </summary>
        /// <param name="membershipId">The membership id to lookup.</param>
        /// <returns></returns>
        public Membership GetById(string membershipId)
        {
            var membershipDto = _membershipServiceRepository.GetById(membershipId);
            return _mapper.Map<Membership>(membershipDto);
        }
    }
}
