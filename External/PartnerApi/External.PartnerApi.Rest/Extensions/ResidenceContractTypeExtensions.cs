using Brunda.External.PartnerApi.Contracts.Enums;
using System.ComponentModel;

namespace Brunda.External.PartnerApi.Rest.Extensions;

internal static class ResidenceContractTypeExtensions
{
    public static string ToQueryParameterName(this ResidenceContractType residenceType) => residenceType switch
    {
        ResidenceContractType.Buy => "koop",
        ResidenceContractType.Rent => "huur",
        _ => throw new InvalidEnumArgumentException(nameof(residenceType), (int)residenceType, typeof(ResidenceContractType))
    };
}
