using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Luis;
using Microsoft.Bot.Builder.Luis.Models;

namespace MetaBotCSharp.Dialogs
{
    [Serializable]
    public class BasicLuisDialog: LuisDialog<object>
    {
        private static string KeyString = "00a5b2eadfdd410380b707b15a76970d	";

        private static string endpoint ="https://westus.api.cognitive.microsoft.com/luis/v2.0/apps/5a2e5c1e-e320-496c-9fc6-0a90ef78450b?subscription-key=00a5b2eadfdd410380b707b15a76970d&verbose=true&timezoneOffset=0&q="
            ;

        private static string appID = "5a2e5c1e-e320-496c-9fc6-0a90ef78450b";

        private static string keys = "4fe18983dce64ee59197f7b4462f30d8";

        public BasicLuisDialog() : base(new LuisService(new LuisModelAttribute(
            ConfigurationManager.AppSettings["LuisAppId"],
            ConfigurationManager.AppSettings["LuisAPIKey"],
            domain: ConfigurationManager.AppSettings["LuisAPIHostName"])))
        {
        }


        [LuisIntent("None")]
        public async Task NoneIntent(IDialogContext context, LuisResult result)
        {
            await context.PostAsync("Listen I'm smart but I'm not that smart. I can still only do so much.");
            await context.PostAsync("But with azure cognitive services the limit is much higher.");
        }


        [LuisIntent("about.bot")]
        public async Task AboutBotIntent(IDialogContext context, LuisRequest result)
        {
            
        }


        [LuisIntent("about.patrick")]
        public async Task AboutPatrick(IDialogContext context, LuisRequest result)
        {
            
        }


        [LuisIntent("define")]
        public async Task DefineThings(IDialogContext context, LuisRequest result)
        {
            
        }


        [LuisIntent("OnDevice.AreYouListening")]
        public async Task AreYouThere(IDialogContext context, LuisRequest request)
        {
            
        }


        [LuisIntent("OnDevice.Help")]
        public async Task HelpTheHuman(IDialogContext context, LuisRequest request)
        {
            
        }

    }
}