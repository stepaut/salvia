using salvia.Core;
using System;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using static salvia.Telegram.BotResources;

namespace salvia.Telegram.Handlers;

internal class TelegramUpdateHandler : ITelegramUpdateHandler
{
    private readonly IDiseaseService _diseaseService;
    private readonly ITemperatureService _temperatureService;


    public TelegramUpdateHandler(IDiseaseService diseaseService, ITemperatureService temperatureService)
    {
        _diseaseService = diseaseService;
        _temperatureService = temperatureService;
    }


    public async Task<BotResponse> HandleUpdate(Update update)
    {
        try
        {
            switch (update.Type)
            {
                case UpdateType.Message:
                    return await ResolveMessage(update);
                case UpdateType.CallbackQuery:
                    return await ResolveCallbackQuery(update);
                default:
                    throw new NotImplementedException();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }

        return new BotResponse();
    }

    private async Task<BotResponse> ResolveTextMessage(User user, string? message)
    {
        var elements = message?.Split(' ');
        if (elements is null || elements.Length == 0)
        {
            return new BotResponse() { Message = R_DEFAULT };
        }

        var command = elements[0];
        var parameter = elements.Length >= 2 ? elements[1] : null;

        var parameters = new BotCommandParameters()
        {
            Command = command,
            Parameter = parameter,
            UserId = user.Id,
        };

        var enviroment = new BotCommandEnviroment(parameters, _diseaseService, _temperatureService);

        var handler = BotCommandHanblerFactory.Create(enviroment);
        var response = await handler.HandleCommand();

        return response;
    }

    private async Task<BotResponse> ResolveMessage(Update update)
    {
        var message = update.Message;
        if (message is null)
        {
            throw new Exception("message is null.");
        }

        var user = message.From;
        if (user is null)
        {
            throw new Exception("user is null.");
        }

        switch (message.Type)
        {
            case MessageType.Text:
                return await ResolveTextMessage(user, message.Text);
            /*
            if (message.Text == "/inline")
            {
                // Тут создаем нашу клавиатуру
                var inlineKeyboard = new InlineKeyboardMarkup(
                    new List<InlineKeyboardButton[]>() // здесь создаем лист (массив), который содрежит в себе массив из класса кнопок
                    {
                                // Каждый новый массив - это дополнительные строки,
                                // а каждая дополнительная кнопка в массиве - это добавление ряда

                                new InlineKeyboardButton[] // тут создаем массив кнопок
                                {
                                    InlineKeyboardButton.WithUrl("Это кнопка с сайтом", "https://habr.com/"),
                                    InlineKeyboardButton.WithCallbackData("А это просто кнопка", "button1"),
                                },
                                new InlineKeyboardButton[]
                                {
                                    InlineKeyboardButton.WithCallbackData("Тут еще одна", "button2"),
                                    InlineKeyboardButton.WithCallbackData("И здесь", "button3"),
                                },
                    });

                await _botClient.SendMessage(
                    chat.Id,
                    "Это inline клавиатура!",
                    replyMarkup: inlineKeyboard); // Все клавиатуры передаются в параметр replyMarkup

                return;
            }

            if (message.Text == "/reply")
            {
                // Тут все аналогично Inline клавиатуре, только меняются классы
                // НО! Тут потребуется дополнительно указать один параметр, чтобы
                // клавиатура выглядела нормально, а не как абы что

                var replyKeyboard = new ReplyKeyboardMarkup(
                    new List<KeyboardButton[]>()
                    {
                                new KeyboardButton[]
                                {
                                    new KeyboardButton("Привет!"),
                                    new KeyboardButton("Пока!"),
                                },
                                new KeyboardButton[]
                                {
                                    new KeyboardButton("Позвони мне!")
                                },
                                new KeyboardButton[]
                                {
                                    new KeyboardButton("Напиши моему соседу!")
                                }
                    })
                {
                    // автоматическое изменение размера клавиатуры, если не стоит true,
                    // тогда клавиатура растягивается чуть ли не до луны,
                    // проверить можете сами
                    ResizeKeyboard = true,
                };

                await _botClient.SendMessage(
                    chat.Id,
                    "Это reply клавиатура!",
                    replyMarkup: replyKeyboard); // опять передаем клавиатуру в параметр replyMarkup

                return;
            }

            if (message.Text == "Позвони мне!")
            {
                await _botClient.SendMessage(
                    chat.Id,
                    "Хорошо, присылай номер!");

                return;
            }
            */

            default:
                return new BotResponse() { Message = R_DEFAULT };
        }
    }

    private async Task<BotResponse> ResolveCallbackQuery(Update update)
    {
        return new BotResponse() { Message = R_DEFAULT };
        /*
        // Переменная, которая будет содержать в себе всю информацию о кнопке, которую нажали
        var callbackQuery = update.CallbackQuery;

        // Аналогично и с Message мы можем получить информацию о чате, о пользователе и т.д.
        var user = callbackQuery.From;

        // Выводим на экран нажатие кнопки
        Console.WriteLine($"{user.FirstName} ({user.Id}) нажал на кнопку: {callbackQuery.Data}");

        // Вот тут нужно уже быть немножко внимательным и не путаться!
        // Мы пишем не callbackQuery.Chat , а callbackQuery.Message.Chat , так как
        // кнопка привязана к сообщению, то мы берем информацию от сообщения.
        var chat = callbackQuery.Message.Chat;

        // Добавляем блок switch для проверки кнопок
        switch (callbackQuery.Data)
        {
            // Data - это придуманный нами id кнопки, мы его указывали в параметре
            // callbackData при создании кнопок. У меня это button1, button2 и button3

            case "button1":
                {
                    // В этом типе клавиатуры обязательно нужно использовать следующий метод
                    await _botClient.AnswerCallbackQuery(callbackQuery.Id);
                    // Для того, чтобы отправить телеграмму запрос, что мы нажали на кнопку

                    await _botClient.SendMessage(
                        chat.Id,
                        $"Вы нажали на {callbackQuery.Data}");
                    return;
                }

            case "button2":
                {
                    // А здесь мы добавляем наш сообственный текст, который заменит слово "загрузка", когда мы нажмем на кнопку
                    await _botClient.AnswerCallbackQuery(callbackQuery.Id, "Тут может быть ваш текст!");

                    await _botClient.SendMessage(
                        chat.Id,
                        $"Вы нажали на {callbackQuery.Data}");
                    return;
                }

            case "button3":
                {
                    // А тут мы добавили еще showAlert, чтобы отобразить пользователю полноценное окно
                    await _botClient.AnswerCallbackQuery(callbackQuery.Id, "А это полноэкранный текст!", showAlert: true);

                    await _botClient.SendMessage(
                        chat.Id,
                        $"Вы нажали на {callbackQuery.Data}");
                    return;
                }
        }
        */
    }
}
