using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration.Configuration;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace GMed.Models
{
    public class DataTypePropertyAttributeConvention
    : PrimitivePropertyAttributeConfigurationConvention<DataTypeAttribute>
    {
        public override void Apply(ConventionPrimitivePropertyConfiguration configuration,
            DataTypeAttribute attribute)
        {
            if (attribute.DataType == DataType.Date)
            {
                configuration.HasColumnType("Date");
            }
        }
    }
}