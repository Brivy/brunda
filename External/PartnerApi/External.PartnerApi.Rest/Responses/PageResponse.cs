﻿using System.Text.Json.Serialization;

namespace Brunda.External.PartnerApi.Rest.Responses;

internal record PageResponse
{
    [JsonPropertyName("HuidigePagina")]
    public required int CurrentPage { get; init; }
    [JsonPropertyName("AantalPaginas")]
    public required int TotalPages { get; init; }
}
