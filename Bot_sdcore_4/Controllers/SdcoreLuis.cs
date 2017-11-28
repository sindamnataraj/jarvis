using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using Microsoft.Bot.Builder.Luis;
using Microsoft.Bot.Builder.Luis.Models;
using System.Threading.Tasks;
using System.Text;

namespace Bot_sdcore_4.Controllers
{
    [Serializable]
    [LuisModel("cf18496d-1868-471d-a40b-8475854ffb1b", "81b8f7110d1345eba2c077b69bfc7c19")]
    public class SdcoreLuis : LuisDialog<object>
    {
    
        [LuisIntent("None")]
        public async Task None(IDialogContext context, LuisResult result)
        {
            await context.PostAsync("I'm sorry. I didn't understand you.");
            context.Wait(MessageReceived);
        }

        [LuisIntent("AboutMe")]
        public async Task AboutMe(IDialogContext context, LuisResult result)
        {
            await context.PostAsync("This a bot for sharing service desk core knowledge and tips");
            context.Wait(MessageReceived);
        }

        [LuisIntent("Acronyms")]
        public async Task Acronyms(IDialogContext context, LuisResult result)
        {
            List<EntityRecommendation> entityList = (List<EntityRecommendation>)result.Entities;

            //<entity type,entity>
            //<Acronym Symbols,lob>
            StringBuilder sb = new StringBuilder();

            foreach (EntityRecommendation entityRecommendation in entityList)
            {
                //auxResult.Add(entityRecommendation.Type, entityRecommendation.Entity);
                string type = entityRecommendation.Type;
                string entity = entityRecommendation.Entity;

                if (type == "Acronym Symbols")
                {
                    sb = sb.Append(GetFullForm(entity));
                }

            }

            await context.PostAsync(sb.ToString());
            context.Wait(MessageReceived);
        }


        private static string GetFullForm(string acronym)
        {
            string uCase = acronym.ToUpper();
            switch (uCase)
            {
                case "LOB":
                    return "Line Of Business";

                case "MaK":
                case "MAK":
                    return "Management And Knowledge Team";

                case "APAM":
                    return "Agent Profile Account Management, domain data for pylon comes from APAM and CAP(Consumer Access Portal)";

                case "CAP":
                    return "Consumre Access Portal,domain data for pylon comes from CAP and APAM (Agent Profile Account Management)";

                case "ASD":
                    return "Agent Service Desktop";

                case "BVT":
                    return "Build Verification Test";

                case "MTTF":
                    return "Mean Time To Failure";
                case "DRI":
                    return "Direct Response Individual a.k.a On Call";
                default:
                    return "Acronyms not found.";
            }
        }
    }

    
}