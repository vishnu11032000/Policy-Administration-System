using System;
using System.Collections.Generic;

namespace ConsumerApi.Models;

public partial class ConsumerPolicy
{
    public string? PolicyId { get; set; }
    public long BusinessId { get; set; }

    public long ConsumerId { get; set; }

    public string? AcceptanceStatus { get; set; }

    public string? AcceptedQuote { get; set; }

    public string? CoveredSum { get; set; }

    public string? Duration { get; set; }

    public string? EffectiveDate { get; set; }

    public string? PaymentDetails { get; set; }

    public string? PolicyStatus { get; set; }

    public string? PropertyType { get; set; }

    public string? ConsumerType { get; set; }

    public string? AssuredSum { get; set; }

    public string? Tenure { get; set; }

    public long? BusinessValue { get; set; }

    public long? PropertyValue { get; set; }

    public string? BaseLocation { get; set; }

    public string? Type { get; set; }
}
