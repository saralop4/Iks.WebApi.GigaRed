using System.Text.Json;
using System.Text.Json.Serialization;

namespace Iks.WebApi.Dominio.DTOs;

public class IksDto
{
    public long? IdIks { get; set; }
    public long? IdCliente { get; set; }
    public string? CodigoDeLlave { get; set; }
    public bool? EstadoEliminado { get; set; }
    public string? UsuarioQueRegistra { get; set; }
    public string? UsuarioQueActualiza { get; set; }
    public string? IpDeRegistro { get; set; }

    [JsonIgnore]
    public DateTime? FechaDeActualizado { get; set; }

    [JsonIgnore]
    [JsonConverter(typeof(TimeSpanToStringConverter))]
    public TimeSpan? HoraDeActualizado { get; set; }

    public string? IpDeActualizado { get; set; }
}

public class TimeSpanToStringConverter : JsonConverter<TimeSpan?>
{
    public override TimeSpan? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType == JsonTokenType.String &&
            TimeSpan.TryParse(reader.GetString(), out TimeSpan parsedTimeSpan))
        {
            return parsedTimeSpan;
        }
        return null;
    }

    public override void Write(Utf8JsonWriter writer, TimeSpan? value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value?.ToString(@"hh\:mm\:ss"));
    }
}
