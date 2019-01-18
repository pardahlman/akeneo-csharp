using System.Collections.Generic;

namespace Akeneo.Model
{
    public partial class VariantAttributeSet
    {
        public long Level { get; set; }
        public List<string> Axes { get; set; }
        public List<string> Attributes { get; set; }
    }
}