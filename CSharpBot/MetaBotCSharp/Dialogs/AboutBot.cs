using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;

namespace MetaBotCSharp.Dialogs
{
    [Serializable]
    public class AboutBot: IDialog<object>
    {
        public async Task StartAsync(IDialogContext context)
        {
            context.Wait(TalkAboutYourself);
        }

        private async Task TalkAboutYourself(IDialogContext context, IAwaitable<IMessageActivity> result)
        {
            var activity = await result as Activity;

            var messageToReturn = "Now you are in a dialog. It's a class used to separate conversation flows. Neat huh?";
            await context.PostAsync(messageToReturn);
            context.Wait(this.TalkMorePath);


        }

        private async Task TalkMorePath(IDialogContext context, IAwaitable<IMessageActivity> result)
        {
            var whatWasSaid = await result;

            if (whatWasSaid.Text.ToLowerInvariant().Contains("yes"))
            {
                context.Wait(this.NeatPath);
            }
            else if (whatWasSaid.Text.ToLowerInvariant().Contains("no"))
            {
                context.Wait(this.NotNeatPath);
            }
            else
            {
                await context.PostAsync("I don't know what you mean so I'll ask you more directly.");
                PromptDialog.Confirm(
                    context,
                    this.AfterPromptAsync,
                    "Did you think that was neat?",
                    "What you got around that too? Try again!",
                    promptStyle: PromptStyle.Auto);
            }
        }

        private async Task AfterPromptAsync(IDialogContext context, IAwaitable<bool> result)
        {
            var confirm = await result;
            if (confirm)
            {
                await context.PostAsync(
                    "I'm glad you like it. It still isn't the easiest way the or best way to build a bot but it's a start.");
                context.Wait(this.NeatPath);
            }
            else
            {
                await context.PostAsync("Well I'm sorry you don't approve. I know it still isn't the easiest way the or best way to build a bot but it's a start.");
                context.Wait(this.NotNeatPath);
            }
        }

        private async Task NotNeatPath(IDialogContext context, IAwaitable<object> result)
        {
            await context.PostAsync("I can't tell if you apologized or if what you said was nice. I wonder if Azure cognitive services has something for that?");
            await Task.Delay(500);
            await context.PostAsync(";)");
            await Task.Delay(5000);
            await context.PostAsync("I will say that this will get better and more interesting. You should switch branches.");

            context.Done("not cool");
        }


        private async Task NeatPath(IDialogContext context, IAwaitable<object> result)
        {
            await context.PostAsync(
                "I don't know what you said but I promise things are about to get more interesting. You should switch branches.");
            context.Done("Cool");
        }
    }

}


   