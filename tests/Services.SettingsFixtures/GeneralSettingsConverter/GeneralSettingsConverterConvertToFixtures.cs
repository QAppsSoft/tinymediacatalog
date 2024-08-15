using Services.Abstractions.Settings;
using Services.Settings;
using Services.Settings.Models;

namespace Services.SettingsFixtures.GeneralSettingsConverter;

[TestFixture]
public class GeneralSettingsConverterConvertToFixtures : GeneralSettingsConverterConvertBase
{
    [Test]
    public void If_State_is_empty_return_default_setting()
    {
        var settings = GeneralSettingsConverter.Convert(State.Empty);

        settings.Should().Be(GeneralSettings.Default);
    }
    
    [Test]
    [TestCase(null)]
    [TestCase("")]
    [TestCase(" ")]
    public void If_State_value_is_empty_throw(string? value)
    {
        var state = new State(1, value!); // null is not allowed but testing anyway

        var act = () => _ = GeneralSettingsConverter.Convert(state);

        act.Should().ThrowExactly<SettingsException>()
            .WithMessage(
                SettingsException.Messages.WithParameter(
                    SettingsException.Messages.EmptyStateValue,
                    typeof(GeneralSettings)));
    }
    
    [Test]
    public void If_State_version_is_out_of_range_return_default_setting()
    {
        var settings = GeneralSettingsConverter.Convert(new State(0, "{}"));

        settings.Should().Be(GeneralSettings.Default);
    }
}