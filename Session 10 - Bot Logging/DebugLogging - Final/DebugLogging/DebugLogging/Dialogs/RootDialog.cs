using System;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;

using DebugLogging;

namespace DebugLogging.Dialogs
{
    [Serializable]
    public class RootDialog : IDialog<object>
    {
        // this is our entry point
        public async Task StartAsync(IDialogContext context)
        {
            context.Wait(MessageReceivedAsync);
        }

        public virtual async Task MessageReceivedAsync(IDialogContext context, IAwaitable<IMessageActivity> activity)
        {
            var response = await activity;
            //DebugActivityLogger logger = new DebugActivityLogger();
            //await logger.LogAsync(response);
            // just post a welcome message and do nothing
            await context.PostAsync("Welcome to the bot framework demo!");
        }
    }
}