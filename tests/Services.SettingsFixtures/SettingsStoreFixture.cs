using System.Text.Json;
using Services.Abstractions.Settings;
using TestsCommons.Helpers;

namespace Services.SettingsFixtures;

public class SettingsStoreFixture
{
    [Test]
    public void Write_Custom_State_Value_Should_Succeed()
    {
        const string key = "WriteCustomStateTestFile";
        const string testJsonValue = "{\"TestValue\" : 1}";
        using var _ = MocksFixture.BuildFileSettingsStore(key, out var store);
        var state = new State(1, testJsonValue);

        store.Save(key, state);
        var restored = store.Load(key);

        restored.Should().BeEquivalentTo(state, o => o.ComparingByMembers<State>());
    }

    [Test]
    public void Write_Custom_Record_Struct_Should_Succeed()
    {
        const string key = "WriteCustomRecordStructTestFile";
        var value = new TestStruct("None", 10);
        using var _ = MocksFixture.BuildFileSettingsStore(key, out var store);
        
        var jsonValue = JsonSerializer.Serialize(value, new JsonSerializerOptions { WriteIndented = true });
        var state = new State(1, jsonValue);

        store.Save(key, state);
        var restored = store.Load(key);

        restored.Should().BeEquivalentTo(state, o => o.ComparingByMembers<State>());
    }
}

public readonly record struct TestStruct(string Name, int Age);
