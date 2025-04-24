namespace salvia.Telegram;

internal static class BotResources
{
    #region Commands
    public const string C_START = "/start";
    public const string C_START_DISEASE = "/sd";
    public const string C_FINISH_DISEASE = "/fd";
    public const string C_ADD_TEMP = "/at";
    public const string C_GET_TEMPS = "/gt";
    public const string C_GET_ALL_DISEASES = "/gd";
    #endregion

    #region Replies
    public const string R_HELLO = $"Hello, possible commands:\n" +
        $"{C_START_DISEASE} - Start new disease\n" +
        $"{C_FINISH_DISEASE} - Finish current disease\n" +
        $"{C_GET_ALL_DISEASES} - Get all diseases\n" +
        $"{C_ADD_TEMP} <temp> - Add temperature in current disease\n" +
        $"{C_GET_TEMPS} - Get temperatures in current disease\n" +
        $"{C_GET_TEMPS} <diseaseId> - Get temperatures in disease\n" +
        $"";
    
    public const string R_DEFAULT = "Sorry, I don't understand you";
    public const string R_DISEASE_STARTED = "New disase started";
    public const string R_DISEASE_FINISHD = "Current disase finished";
    public const string R_DISEASE_ALREADY_FINISHD = "Disase already finished";
    public const string R_TEMP_ADDED = "Temperature added";
    public const string R_DISEASE_NOT_STARTED = $"Disease not started. Start new disease with {C_START_DISEASE}";
    public const string R_DISEASE_NOT_FOUND = "Disease not found";
    public const string R_NO_TEMPS = "No temperatures in disease";
    public const string R_NOT_ALLOWED = "You are not allowed to using this bot";
    #endregion
}
