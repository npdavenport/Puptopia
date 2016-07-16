using Puptopia.Domain.Entities;
using Puptopia.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Puptopia.Domain.DTO
{
    /// <summary>
    /// This Data Transfer Object will carry membership information between the
    /// domain and persistence layers.
    /// </summary>
    public class MembershipDTO
    {
        public DateTime ExpirationDate { get; set; }
        public DateTime InitialDate { get; set; }
        public string MembershipId { get; set; }
        public MembershipType MembershipType { get; set; }
        public DateTime RenewalDate { get; set; }

    }
}
