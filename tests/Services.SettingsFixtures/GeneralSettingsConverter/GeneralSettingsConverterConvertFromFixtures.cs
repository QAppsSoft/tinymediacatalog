using Services.Settings.Models;

namespace Services.SettingsFixtures.GeneralSettingsConverter;

[TestFixture]
public class GeneralSettingsConverterConvertFromFixtures : GeneralSettingsConverterConvertBase
{
    [Test]
    public void Passing_a_setting_should_return_correct_state()
    {
        var settings = GeneralSettings.Default;
        
        var state = GeneralSettingsConverter.Convert(settings);

        state.Version.Should().BeGreaterThan(0);
        state.Value.Should().ContainAll("{", "}");
    }
}