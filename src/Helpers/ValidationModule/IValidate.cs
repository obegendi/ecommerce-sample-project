namespace ValidationModule
{
    public interface IValidate
    {
        ValidationResult DoValidation(params object[] parameters);
        bool HandleResults();
    }
}
