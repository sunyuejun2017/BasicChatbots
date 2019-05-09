using System;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;

namespace HelloWorld.Dialogs
{
    [Serializable]
    public class RootDialog : IDialog<object>
    {
        string userName = string.Empty;

        int userAge = 0;

        // this is our entry point
        public async Task StartAsync(IDialogContext context)
        {
           

            context.Wait(MessageReceivedAsync);
        }

        public virtual async Task MessageReceivedAsync(IDialogContext context, IAwaitable<IMessageActivity> activity)
        {
            var response = await activity;
            PromptDialog.Text(
                context: context,
                resume: ResumeGetName,
                prompt: "How would you like to be called?"
            );
        }
        public virtual async Task ResumeGetName(IDialogContext context, IAwaitable<string> userResponse)
        {
            string response = await userResponse;
            userName = response;

            PromptDialog.Text(
               context: context,
               resume: ResumeGetAge,
               prompt: "How old are you?"
           );
            
        }

        private async Task ResumeGetAge(IDialogContext context, IAwaitable<string> result)
        {
            var response = await result;

            userAge = Int32.Parse(response);

            await context.PostAsync($"Welcome to this demo, {userName}:{userAge}!");
            context.Done(this);
        }
    }
}