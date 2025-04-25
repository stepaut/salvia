namespace salvia.Telegram;

public class BotResponse
{
    public string Message { get; init; } = string.Empty;
    public byte[]? Image { get; init; }

    public static readonly BotResponse Default = new() { Message = BotResources.R_DEFAULT };
}
