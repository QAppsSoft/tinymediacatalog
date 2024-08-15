using Services.Settings.Models;

namespace Services.SettingsFixtures.GeneralSettingsConverter;

[TestFixture]
public class GeneralSettingsConverterConvertDefaultFixtures : GeneralSettingsConverterConvertBase
{
    [Test]
    public void Should_return_correct_default_value()
    {
        var setting = GeneralSettingsConverter.GetDefaultValue();

        setting.Should().Be(GeneralSettings.Default);
    }
}