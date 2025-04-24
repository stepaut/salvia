namespace salvia.Telegram.Handlers;

internal class BotCommandParameters
{
    public required long UserId { get; init; }
    public required string Command { get; init; }
    public string? Parameter { get; init; }
}
