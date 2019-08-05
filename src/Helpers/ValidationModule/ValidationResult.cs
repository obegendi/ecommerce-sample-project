using System.Collections.Generic;

namespace ValidationModule
{
    public class ValidationResult
    {
        public ValidationResult()
        {
            Items = new List<ValidationResultItem>();
        }

        public List<ValidationResultItem> Items { get; set; }

        public bool HasItem => Items != null && Items.Count > 0;
    }
}
