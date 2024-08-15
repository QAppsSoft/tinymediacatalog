using System.Globalization;

namespace Services.Settings;

public class SettingsException : Exception
{
    public SettingsException()
    {
    }

    public SettingsException(string message) : base(message)
    {
    }

    public SettingsException(string message, Exception innerException) : base(message, innerException)
    {
    }
    
    public static class Messages
    {
        public static string EmptyStateValue => "State value can't be an empty string in {0}";
        
        public static string WithParameter(string message, params object[] parameter)
        {
            return string.Format(CultureInfo.InvariantCulture, message, parameter);
        }
    }
}