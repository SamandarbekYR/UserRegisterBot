using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using UserRegisterBot.Entity;
using UserRegisterBot.Repositories;

namespace UserRegisterBot
{
    class Program
    {
        static Dictionary<long, List<string>> values = new Dictionary<long, List<string>>();
        static Users users = new Users();
        static UserRepository repository = new UserRepository();

        static void Main(string[] args)
        {
            string YOUR_ACCESS_TOKEN_HERE = "6598331215:AAG_69keyDD5aQf7qna1glDXzsY6W-CP4aY";
            var client = new TelegramBotClient(YOUR_ACCESS_TOKEN_HERE);
            client.StartReceiving(Update, Error);
            Console.WriteLine("running ... ");
            Console.ReadKey();
        }
        async static Task Update(ITelegramBotClient botClient, Update update, CancellationToken cancellation)
        {

            var message = update.Message;
            if (message!.Text == "/start")
            {
                values[message.Chat.Id] = new List<string>();
                await botClient.SendTextMessageAsync(message.Chat.Id, "Enter Username:\n" +
                                    "Example => Username: Samandarbek ");
            }

            else
            {
                if (!values.ContainsKey(message.Chat.Id))
                {
                    await botClient.SendTextMessageAsync(message.Chat.Id, "Please issue the /start command.");
                }
                else if (message!.Text!.StartsWith("Username: "))
                {

                    users.UserName = string.Join(" ", message.Text.Split(" ").Skip(1));

                    var result = await repository.GetByUserNameAsync(users.UserName);

                    if (result is not null)
                    {
                        await botClient.SendTextMessageAsync(message.Chat.Id,
                                                                $"Username: {result.UserName}\n" +
                                                                $"Firstname: {result.FirstName}\n" +
                                                                $"Lastname: {result.LastName}\n" +
                                                                $"Companyname: {result.CompanyName}");

                        message.Text = "";
                        await botClient.SendTextMessageAsync(message.Chat.Id, "This is already registered with UserName.\n" +
                                                                "Please issue the /start command.");
                        return;
                    }

                    values[message.Chat.Id].Add(message.Text);

                    await botClient.SendTextMessageAsync(message.Chat.Id, "Username saved!");
                    await botClient.SendTextMessageAsync(message.Chat.Id, "Enter Firstname \n" +
                                                            "Example => Firstname: Samandarbek");
                    users.ChatId = message.Chat.Id;
                }

                else if (message!.Text!.StartsWith("Firstname: "))
                {
                    users.FirstName = string.Join(" ", message.Text.Split(" ").Skip(1));
                    values[message.Chat.Id].Add(message.Text);

                    await botClient.SendTextMessageAsync(message.Chat.Id, "Firstname saved!");

                    await botClient.SendTextMessageAsync(message.Chat.Id, "Enter Lastname!\n" +
                                                        "Example => Lastname: Yigitaliyev");
                }

                else if (message!.Text!.StartsWith("Lastname: "))
                {
                    users.LastName = string.Join(" ", message.Text.Split(" ").Skip(1));
                    values[message.Chat.Id].Add(message.Text);

                    await botClient.SendTextMessageAsync(message.Chat.Id, "Lastname saved!");
                    await botClient.SendTextMessageAsync(message.Chat.Id, "Enter Companyname!\n" +
                                                           "Example => Companyname: Green Sale");
                }

                else if (message!.Text!.StartsWith("Companyname: "))
                {
                    users.CompanyName = string.Join(" ", message.Text.Split(" ").Skip(1));
                    values[message.Chat.Id].Add(message.Text);

                    UserRepository repository = new UserRepository();
                    var result = repository.CreateAsync(users);

                    if (result != null)
                    {
                        await botClient.SendTextMessageAsync(message.Chat.Id, "Your data has been added to the database!");

                        string joinedValues = string.Join("\n", values[message.Chat.Id]);
                        await botClient.SendTextMessageAsync(message.Chat.Id, joinedValues);
                    }


                    InlineKeyboardMarkup inlineKeyboard = new(new[]
                    {
                    InlineKeyboardButton.WithUrl(
                                text: "Link to the GitHub",
                                url: "https://github.com/SamandarbekYR/UserRegisterBot")
                    });

                    Message sentMessage = await botClient.SendTextMessageAsync(
                        chatId: message.Chat.Id,
                        text: "Click to view bot's code",
                        replyMarkup: inlineKeyboard
                        );
                }

                else
                {
                    await botClient.SendTextMessageAsync(message.Chat.Id, "You made an error while filling out the information.\n" +
                                                                                "Refill according to the sample, Or issue the /start command.");
                }
            }
        }

        private static Task Error(ITelegramBotClient arg1, Exception arg2, CancellationToken arg3)
        {
            throw new NotImplementedException();
        }
    }
}