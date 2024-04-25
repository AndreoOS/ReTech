using System.Text.Json.Serialization;
using Ardalis.SmartEnum;
using Ardalis.SmartEnum.SystemTextJson;

namespace MobyLabWebProgramming.Core.Enums;

[JsonConverter(typeof(SmartEnumNameConverter<UserRoleEnum, string>))]
public sealed class FeedbackRatingEnum : SmartEnum<FeedbackRatingEnum, string>
{
    public static readonly FeedbackRatingEnum Unacceptable = new(nameof(Unacceptable), "Unacceptable");
    public static readonly FeedbackRatingEnum BelowPar = new(nameof(BelowPar), "Below Par");
    public static readonly FeedbackRatingEnum Good = new(nameof(Good), "Good");
    public static readonly FeedbackRatingEnum Great = new(nameof(Great), "Great");
    public static readonly FeedbackRatingEnum Best = new(nameof(Best), "Best");
    
    private FeedbackRatingEnum(string name, string value) : base(name, value)
    {
    }
}