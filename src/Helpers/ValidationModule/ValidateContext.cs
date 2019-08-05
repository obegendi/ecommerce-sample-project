namespace ValidationModule
{
    public class ValidateContext
    {

        public ValidateContext(IValidate strategy)
        {
            Strategy = strategy;
        }
        public IValidate Strategy { get; set; }

        public ValidationResult DoValidation(params object[] parameters)
        {
            return Strategy.DoValidation(parameters);
        }

        public bool HandleResults()
        {
            return Strategy.HandleResults();
        }
    }
}
