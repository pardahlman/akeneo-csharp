using System;
using System.Collections.Generic;

namespace Akeneo.Model
{
	public class Product : ModelBase
	{
		/// <summary>
		///  Product identifier, i.e. the value of the only `pim_catalog_identifier` attribute 
		/// </summary>
		public string Identifier { get; set; }

		/// <summary>
		///  Family code from which the product inherits its attributes and attributes requirements 
		/// </summary>
		public string Family { get; set; }

        /// <summary>
        ///  Code of the parent product model when the product is a variant (only available since the 2.0). This parent can be modified since the 2.3. 
        /// </summary>
        public string Parent { get; set; }
        
		/// <summary>
		/// Codes of the groups to which the product belong 
		/// </summary>
		public List<string> Groups { get; set; }

	    /// <summary>
	    ///  Codes of the categories in which the product is classified 
	    /// </summary>
	    public List<string> Categories { get; set; }

        /// <summary>
        /// Whether the product is enable
        /// </summary>
        public bool Enabled { get; set; }

		/// <summary>
		///  Product attributes values
		/// </summary>
		public Dictionary<string, List<ProductValue>> Values { get; set; }

		/// <summary>
		///  Date of creation
		/// </summary>
		public DateTime? Created { get; set; }

		/// <summary>
		/// Date of the last update
		/// </summary>
		public DateTime? Updated { get; set; }

		/// <summary>
		///  Several associations related to groups and/or other products, grouped by association types 
		/// </summary>
		public Dictionary<string,ProductAssociation> Associations { get; set; }

        /// <summary>
        ///  Status of the product regarding the user permissions (only available since the v2.0 in the Enterprise Edition)  
        /// </summary>
        public Dictionary<string,string> Metadata { get; set; }

        public Product()
		{
			Groups = new List<string>();
			Categories = new List<string>();
			Associations = new Dictionary<string, ProductAssociation>();
			Values = new Dictionary<string, List<ProductValue>>();
            Metadata = new Dictionary<string, string>();
		}
	}
}
