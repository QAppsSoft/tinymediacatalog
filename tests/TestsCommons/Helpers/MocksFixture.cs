using Common.Vars;
using Moq.AutoMock;
using Services.Settings;

namespace TestsCommons.Helpers;

public static class MocksFixture
{
    public static StorageFixture BuildFileSettingsStore(string key, out FileSettingsStore settingStore)
    {
        StorageFixture storageFixture = new();
        var mocker = new AutoMocker();
        var appInfo = mocker.GetMock<IAppInfo>();
        appInfo.Setup(x => x.SettingsPath).Returns(storageFixture.GetTemporalFileName(".setting", key));
        settingStore = mocker.CreateInstance<FileSettingsStore>();

        return storageFixture;
    }
    
    public static StorageFixture BuildAppInfo(out IAppInfo appInfo)
    {
        StorageFixture storageFixture = new();
        var mocker = new AutoMocker();
        var mock = mocker.GetMock<IAppInfo>();
        mock.Setup(x => x.SettingsPath).Returns(storageFixture.GetTemporalFileName());
        mock.Setup(x => x.DatabasePath).Returns(storageFixture.GetTemporalDirectory());
        mock.Setup(x => x.DataBaseFileName).Returns("database.db");
        appInfo = mocker.CreateInstance<AppInfo>();

        return storageFixture;
    }
}