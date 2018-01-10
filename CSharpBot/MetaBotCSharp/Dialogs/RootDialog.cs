using System;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;

namespace MetaBotCSharp.Dialogs
{
    [Serializable]
    public class RootDialog : IDialog<object>
    {
        public Task StartAsync(IDialogContext context)
        {
            context.Wait(MessageReceivedAsync);

            return Task.CompletedTask;
        }

        private async Task MessageReceivedAsync(IDialogContext context, IAwaitable<object> result)
        {
            var activity = await result as Activity;
            string message;
            switch (activity.Text.ToLowerInvariant())
            {
                case "Hello":
                    message = "hello! how are you?";
                    break;
                case "what can you do?":
                    message = "I can't do much. I'm not very useful.";
                    break;
                case "why would you make a bot like this?":
                    message = "I don't think you should. I am the 'Hello world' of Bots";
                    break;
                case "that's sad":
                    message = "I don't know what sad is. You can probably add that with cognitive services.";
                    break;
                default:
                    message = "I don't know what you mean. I just use a switch statement.";
                    break;
            }

            // return our reply to the user
            await context.PostAsync(message);

            context.Wait(MessageReceivedAsync);
        }
    }
}