using System.Text.Json.Serialization;
using Services.Abstractions.Settings;

namespace Services.Settings;

[JsonSourceGenerationOptions(WriteIndented = true)]
[JsonSerializable(typeof(State))]
internal partial class SourceGenerationContext : JsonSerializerContext;