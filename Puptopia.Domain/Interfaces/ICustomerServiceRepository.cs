using Puptopia.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Puptopia.Domain.Entities;
using Puptopia.Domain.Enums;

namespace Puptopia.Domain.Interfaces
{
    /// <summary>
    /// The Interface required by the domain that must be implemented by the Perstence Layer.
    /// Defines methods required for working with customer(DTO) objects.
    /// </summary>
    public interface ICustomerServiceRepository
    {
        CustomerDto GetById(Guid customerId);
        CustomerDto GetByRequiredFields(CustomerRequiredFieldsDto requiredFieltDto);
        void Save(CustomerDto customerDto);
        void Update(CustomerDto customerDto);
    }
}
