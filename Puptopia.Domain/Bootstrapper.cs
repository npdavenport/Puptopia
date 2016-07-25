using AutoMapper;
using Puptopia.Domain.DTO;
using Puptopia.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Puptopia.Domain
{
    /// <summary>
    /// Use this class to initialize any global properties 
    /// or values needed by the domain.
    /// </summary>
    internal class Bootstrapper
    {
        #region Internal Methods

        /// <summary>
        /// AutoMapper configuration that is required for both the 
        /// MembershipService and CustomerService classes.
        /// </summary>
        /// <returns>An AutoMapper MapperConfiguration.</returns>
        internal MapperConfiguration GetCustomerMembershipServiceMapperConfiguration()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<MembershipDto, Membership>().ReverseMap();
                cfg.CreateMap<CustomerDto, Customer>().ReverseMap();
                cfg.CreateMap<CustomerRequiredFieldsDto, CustomerRequiredFields>().ReverseMap();
            });

            // Validate configuration
            config.AssertConfigurationIsValid();

            return config;
        }

        #endregion
    }
}
