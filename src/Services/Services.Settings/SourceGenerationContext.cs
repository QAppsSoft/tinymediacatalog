using System.Text.Json.Serialization;
using Services.Abstractions.Settings;
using Services.Settings.Models;

namespace Services.Settings;

[JsonSourceGenerationOptions(WriteIndented = true)]
[JsonSerializable(typeof(State))]
[JsonSerializable(typeof(GeneralSettings))]
internal partial class SourceGenerationContext : JsonSerializerContext;