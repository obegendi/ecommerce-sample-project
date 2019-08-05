using System;

namespace ValidationModule
{
    [Serializable]
    public class ValidationResultItem
    {

        public ValidationResultItem(string message, bool isRequired)
        {
            Message = message;
            IsRequired = isRequired;
        }
        public string Message { get; set; }
        public bool IsRequired { get; set; }
    }
}
