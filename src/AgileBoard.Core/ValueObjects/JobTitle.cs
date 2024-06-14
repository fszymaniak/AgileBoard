using System.Reflection.Metadata;

namespace AgileBoard.Core.ValueObjects;

public sealed record JobTitle
{
    public string Value { get;  }

    public const string DevelopmentTeamMember = nameof(DevelopmentTeamMember);
    public const string ScrumMaster = nameof(ScrumMaster);
    public const string ProductOwner = nameof(ProductOwner);

    private JobTitle(string value)
        => Value = value;

    public static implicit operator string(JobTitle jobTitle)
        => jobTitle.Value;

    public static implicit operator JobTitle(string value)
        => new(value);
}