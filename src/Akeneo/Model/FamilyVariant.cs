using System;
using System.Collections.Generic;
using System.Text;

namespace Akeneo.Model
{
    public class FamilyVariant : ModelBase
    {
        public string Code { get; set; }

        public Dictionary<string, string> Labels { get; set; }

        public List<VariantAttributeSet> VariantAttributeSets { get; set; }
    }
}