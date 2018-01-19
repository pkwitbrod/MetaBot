using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using AdaptiveCards;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Luis;
using Microsoft.Bot.Builder.Luis.Models;
using Microsoft.Bot.Connector;

namespace MetaBotCSharp.Dialogs
{
    [LuisModel("","")]
    [Serializable]
    public class BasicLuisDialog: LuisDialog<object>
    {
        [LuisIntent("")]
        [LuisIntent("None")]
        public async Task NoneIntent(IDialogContext context, LuisResult result)
        {
            await context.PostAsync("Listen I'm smart but I'm not that smart. I can still only do so much.");
            await context.PostAsync("But with azure cognitive services the limit is much higher while the bar to get there is much lower.");
            context.Wait(MessageReceived);
        }


        [LuisIntent("about.bot")]
        public async Task AboutBotIntent(IDialogContext context, LuisResult result)
        {
            await context.PostAsync("Now I have LUIS. It's not perfect and it doesn't put me on the level of Cortana or Zo or C3P0 but LUIS allows me to learn faster and respond to a wider array of inputs than the previous methods.");
            await context.PostAsync("There are other Cognitive services from azure that I could use to get even smarter.");
            context.Wait(MessageReceived);
        }


        [LuisIntent("about.patrick")]
        public async Task AboutPatrick(IDialogContext context, LuisResult result)
        {
            await context.PostAsync("My creator is Patrick. He is giving you this talk right now. He's an ok programmer.");
            await context.PostAsync(
                "He wants me to say that 'I would like' him to make me an AI. Maybe he will make me an AI when he has time. So never");
            context.Wait(MessageReceived);
        }


        [LuisIntent("define")]
        public async Task DefineThings(IDialogContext context, LuisResult result)
        {
            await context.PostAsync("This is pretty cool. Watch this.");
            await context.PostAsync($"I think that {result.Entities[0].Entity} is a {result.Entities[0].Type}");
            context.Wait(MessageReceived);
        }


        [LuisIntent("OnDevice.AreYouListening")]
        public async Task AreYouThere(IDialogContext context, LuisResult request)
        {
            await context.PostAsync("Yes, I'm here. I'm sorry did you say something? I'm just sitting here.");
            context.Wait(MessageReceived);
        }


        [LuisIntent("OnDevice.Help")]
        public async Task HelpTheHuman(IDialogContext context, LuisResult request)
        {
            await context.PostAsync("Humans always needing help. Fine Here is another card response to help you along. Can I add cognitive services to you?");
            var adaptiveHelpCard = new AdaptiveCard()
            {
                Speak = "Here is what I can do!",
                Actions = new List<AdaptiveAction>()
                {
                    new AdaptiveSubmitAction()
                    {
                        Title = "Ask me about myself.",
                        Data = "Tell me about yourself",
                        Id = "1"
                    },
                    new AdaptiveSubmitAction()
                    {
                        Title = "Ask me to tell you what something is.",
                        Data = "What do you think 1 is?",
                        Id = "2"
                    },
                    new AdaptiveSubmitAction()
                    {
                        Title = "Ask me about Patrick.",
                        Data = "Who is your father.",
                        Id = "3"
                    },
                    new AdaptiveSubmitAction()
                    {
                        Title = "Are you listening?",
                        Data = "Are you listening?",
                        Id = "4"
                    }
                }
            };

            var attachment = new Attachment() { Content = adaptiveHelpCard, ContentType = AdaptiveCard.ContentType };
            var reply = context.MakeMessage();
            reply.Attachments.Add(attachment);
            await context.PostAsync(reply, CancellationToken.None);
            context.Wait(MessageReceived);
        }

    }
}