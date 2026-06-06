using System;
using System.Text.Json.Serialization;

namespace GestionTurnos.Application.Response
{
    public class DolarResponseDto
    {
        [JsonPropertyName("compra")]
        public decimal Compra { get; set; }

        [JsonPropertyName("venta")]
        public decimal Venta { get; set; }

        [JsonPropertyName("casa")]
        public string Casa { get; set; } = string.Empty;

        [JsonPropertyName("nombre")]
        public string Nombre { get; set; } = string.Empty;

        [JsonPropertyName("moneda")]
        public string Moneda { get; set; } = string.Empty;

        [JsonPropertyName("fechaActualizacion")]
        public DateTime FechaActualizacion { get; set; }
    }
}