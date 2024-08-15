namespace Services.SettingsFixtures.GeneralSettingsConverter;

public class GeneralSettingsConverterConvertBase
{
    protected Settings.Converters.GeneralSettingsConverter GeneralSettingsConverter;
    
    [SetUp]
    public void SetUp()
    {
        GeneralSettingsConverter = new Settings.Converters.GeneralSettingsConverter();
    }
}