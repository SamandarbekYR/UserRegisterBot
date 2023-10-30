using Telegram.Bot;
using Telegram.Bot.Types;

namespace UserRegisterBot
{
    class Program
    {
        static Dictionary<long, List<string>> values = new Dictionary<long, List<string>>();    
        static void Main(string[] args)
        {
            string YOUR_ACCESS_TOKEN_HERE = "6780239837:AAF2Dhaps0rUx9mEd81GD8PsslXSboeHTng";
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
                await botClient.SendTextMessageAsync(message.Chat.Id, "UserName kiriting: Misol uchun: UserName: Samandarbek ");
            }

            else
            {
                if (!values.ContainsKey(message.Chat.Id))
                {
                    await botClient.SendTextMessageAsync(message.Chat.Id, "Iltimos, /start buyrug'ini bering.");
                    return;
                }
                else if (message!.Text!.StartsWith("UserName:"))
                {
                    values[message.Chat.Id].Add(message.Text);
                    await botClient.SendTextMessageAsync(message.Chat.Id, "UserName saqlandi!");
                    await botClient.SendTextMessageAsync(message.Chat.Id, "FirstName kiriting! \n" +
                                                        "Misol uchun => FirstName: Yigitaliyev");
                }
                else if (message!.Text!.StartsWith("FirstName:"))
                {
                    values[message.Chat.Id].Add(message.Text);
                    await botClient.SendTextMessageAsync(message.Chat.Id, "FirstName saqlandi!");
                    await botClient.SendTextMessageAsync(message.Chat.Id, "LastName kiriting!\n" +
                                                        "Misol uchun => LastName: Yigitaliyev");
                }
                else if (message!.Text!.StartsWith("LastName:"))
                {
                    values[message.Chat.Id].Add(message.Text);
                    await botClient.SendTextMessageAsync(message.Chat.Id, "LastName saqlandi!");
                    await botClient.SendTextMessageAsync(message.Chat.Id, "CompanyName kiriting!\n" +
                                                        "Misol uchun => CompanyName: Green Sale");
                }
                else if (message!.Text!.StartsWith("CompanyName:"))
                {
                    values[message.Chat.Id].Add(message.Text);
                    await botClient.SendTextMessageAsync(message.Chat.Id, "Malumotlaringiz basaga qo'shildi!");

                    string joinedValues = string.Join("\n", values[message.Chat.Id]);
                    await botClient.SendTextMessageAsync(message.Chat.Id, joinedValues);
                }
                else
                {
                    await botClient.SendTextMessageAsync(message.Chat.Id, "Iltimos, /start buyrug'ini bering.");
                }
            }
        }

        private static Task Error(ITelegramBotClient arg1, Exception arg2, CancellationToken arg3)
        {
            throw new NotImplementedException();
        }
    }
}