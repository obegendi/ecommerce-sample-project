using System;
using System.Linq;
using System.Text;

namespace ValidationModule
{
    public class ValidationException : Exception
    {
        private readonly ValidationResult Result;

        public ValidationException(ValidationResult result)
        {
            Result = result;
        }

        public bool IsRequired
        {
            get { return Result.Items.Where(x => x.IsRequired).Count() > 0; }
        }

        public override string Message
        {
            get
            {
                var message = new StringBuilder();
                Result.Items.ForEach(r => message.Append(r.Message));
                return message.ToString().TrimEnd(Environment.NewLine.ToCharArray());
            }
        }
    }
}
