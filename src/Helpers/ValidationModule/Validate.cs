using System;

namespace ValidationModule
{

    public abstract class Validate : IValidate
    {

        protected Validate(ValidationResult previosResult)
        {
            Result = previosResult;

            if (Result == null)
                Result = new ValidationResult();
        }
        public ValidationResult Result { get; }
        /// <inheritdoc />
        public abstract ValidationResult DoValidation(params object[] parameters);

        /// <inheritdoc />
        public bool HandleResults()
        {
            if (Result.Items.Count > 0)
            {
                foreach (var item in Result.Items)
                    Console.WriteLine(item.Message);
                return false;
            }
            return true;

        }

        public void Add(bool isValid, ValidationResultItem validationResultItem)
        {
            if (!isValid)
                Result.Items.Add(validationResultItem);
        }
    }

}
