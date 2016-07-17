using Puptopia.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Puptopia.Domain
{
    public interface IMembershipServiceRepository
    {
        MembershipDTO GetById(string membershipId);
    }
}
