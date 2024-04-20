using System.Text.Json;
using Microsoft.Extensions.Logging.Abstractions;
using Services.Abstractions.Settings;
using Services.Settings;
using TestsCommons;

namespace Services.SettingsFixtures;

public class SettingsStoreFixture
{
    [Test]
    public void Write_Custom_State_Value_Should_Succeed()
    {
        const string key = "WriteCustomStateTestFile";
        const string testJsonValue = "{\"TestValue\" : 1}";
        using StorageFixture storageFixture = new();
        
        var settingFilePath = storageFixture.GetTemporalFileName(".setting", key);
        var state = new State(1, testJsonValue);
        var store = new FileSettingsStore(NullLogger<FileSettingsStore>.Instance, settingFilePath);

        store.Save(key, state);
        var restored = store.Load(key);

        restored.Should().BeEquivalentTo(state, o => o.ComparingByMembers<State>());
    }

    [Test]
    public void Write_Custom_Record_Struct_Should_Succeed()
    {
        const string key = "WriteCustomRecordStructTestFile";
        var value = new TestStruct("None", 10);
        using StorageFixture storageFixture = new();
        
        var jsonValue = JsonSerializer.Serialize(value, new JsonSerializerOptions { WriteIndented = true });
        var state = new State(1, jsonValue);

        var settingFilePath = storageFixture.GetTemporalFileName(".setting", key);
        var store = new FileSettingsStore(NullLogger<FileSettingsStore>.Instance, settingFilePath);

        store.Save(key, state);
        var restored = store.Load(key);

        restored.Should().BeEquivalentTo(state, o => o.ComparingByMembers<State>());
    }
}

public readonly record struct TestStruct(string Name, int Age);
