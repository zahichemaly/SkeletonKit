namespace CME.Configuration
{
    /// <summary>
    /// Annotate a propery of a class of type <seealso cref="IConfig"/> and specify the environment variable name
    /// which value should be mapped to that property during runtime.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class EnvironmentVariableAttribute : Attribute
    {
        public string VariableName { get; }

        public EnvironmentVariableAttribute(string variableName)
        {
            VariableName = variableName;
        }
    }
}
