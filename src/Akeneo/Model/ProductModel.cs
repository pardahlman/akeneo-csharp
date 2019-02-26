using System;
using System.Collections.Generic;
using System.Text;

namespace Akeneo.Model
{
    public class ProductModel : ModelBase
    {
        public string Code { get; set; }

        public string FamilyVariant { get; set; }

        public string Parent { get; set; }

        public List<string> Categories { get; set; }

        public Dictionary<string, List<ProductValue>> Values { get; set; }

        public DateTime? Created { get; set; }

        /// <summary>
        /// Date of the last update
        /// </summary>
        public DateTime? Updated { get; set; }

        /// <summary>
        ///  Several associations related to groups and/or other products, grouped by association types 
        /// </summary>
        public Dictionary<string, ProductAssociation> Associations { get; set; }
    }
}