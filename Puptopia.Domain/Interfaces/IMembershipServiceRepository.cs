using Puptopia.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Puptopia.Domain.Interfaces
{
    /// <summary>
    /// The Interface required by the domain that must be implemented by the Perstence Layer.
    /// Defines methods required for working with the membership(DTO) objects.
    /// </summary>
    public interface IMembershipServiceRepository
    {
        MembershipDto GetById(string membershipId);
        MembershipDto Save(MembershipDto membershipDto);
        MembershipDto Update(MembershipDto membershipDto);
    }
}
