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
        #region Private Methods

        // Ensure the generated configuration is valid.
        private void AssertValidMapperConfiguration(MapperConfiguration config)
        {
            try
            {
                config.AssertConfigurationIsValid();
            }
            catch (AutoMapperConfigurationException e)
            {

                throw e;
            }
            catch (AutoMapperMappingException e)
            {
                throw e;
            }
        }

        #endregion

        #region Internal Methods

        /// <summary>
        /// AutoMapper configuration that is required for the MembershipService.
        /// </summary>
        /// <returns>An AutoMapper MapperConfiguration object needed by the MembershipService.</returns>
        internal MapperConfiguration GetMembershipServiceMapperConfiguration()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<MembershipDTO, Membership>();
                cfg.CreateMap<CustomerDTO, Customer>();
            });

            // Validate configuration
            AssertValidMapperConfiguration(config);

            return config;
        }

        #endregion
    }
}
