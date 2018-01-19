using System;
using System.Threading;
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
            string returnMessage;
            switch (activity.Text.ToLowerInvariant())
            {
                case "hello":
                    returnMessage = "Hello there!";
                    await this.StandardMessageReturn(returnMessage, context);
                    break;
                case "tell me about yourself":
                    var aboutBot = new AboutBot();
                    await context.Forward(aboutBot, this.ResumeAfterTalkingAboutYourSelf, activity, new CancellationToken());
                    break;
                case "what did we do":
                    returnMessage =
                        "You added an about bot dialog. Ask me about myself and I'll explain.";
                    await this.StandardMessageReturn(returnMessage, context);
                    break;
                default:
                    returnMessage = "I don't know what you mean. I just use a switch statement.";
                    await this.StandardMessageReturn(returnMessage, context);
                    break;
            }

   
        }

        private async Task StandardMessageReturn(string returnMessage, IDialogContext context)
        {
            await context.PostAsync(returnMessage);

            context.Wait(MessageReceivedAsync);
        }


        private async Task ResumeAfterTalkingAboutYourSelf(IDialogContext context, IAwaitable<object> result)
        {
            var resultFromTalkingAboutMyself = await result;
            await Task.Delay(3000);
            await context.PostAsync($"I'm done talking about myself and am back in the root dialog you thought dialogs were '{resultFromTalkingAboutMyself}'");
            context.Wait(this.MessageReceivedAsync);
        }
    }
}