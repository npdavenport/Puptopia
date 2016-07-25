using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Puptopia.Domain.Interfaces;

namespace Puptopia.Domain.Utilities
{
    internal static class UtilityClass
    {
        /// <summary>
        /// Create a unique membership id that should be assigned to a membership.
        /// </summary>
        /// <param name="creationDate">The DateTime of the creation process.</param>
        /// <param name="customerId">An existing customer id</param>
        /// <returns>A unique membership id</returns>
        internal static string GenerateMembershipId(DateTime creationDate, Guid customerId)
        {
            return creationDate.ToString("yyyy-MM-dd") + "-" + customerId.ToString();
        }
    }
}
