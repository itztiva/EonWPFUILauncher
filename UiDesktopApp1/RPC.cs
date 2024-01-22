using System;
using DiscordRPC;
using Button = DiscordRPC.Button;

namespace Luxury
{
    public class RPC
    {
        public static DiscordRpcClient client;
        public static Timestamps rpctimestamp { get; set; }
        private static RichPresence presence;
        public static void InitializeRPC()
        {
            client = new DiscordRpcClient("1196967148090839151");
            client.Initialize();
            Button[] buttons = { new Button() { Label = "Discord Server", Url = "https://discord.gg/eonfn" }, new Button() { Label = "Download Launcher", Url = "https://www.google.com/search?q=not+available+at+the+moment" } };

            presence = new RichPresence()
            {
                Details = "Relive OG Fortnite Chapter 2 Season 2",
                State = "",
                Timestamps = rpctimestamp,
                Buttons = buttons,

                Assets = new Assets()
                {
                    LargeImageKey = "testlogo",
                    LargeImageText = "Project Eon",
                    SmallImageKey = "",
                    SmallImageText = "Relive OG Fortnite"
                }
            };
            SetState("");
        }
        public static void SetState(string state, bool watching = false)
        {
            if (watching)
                state = "" + state;

            presence.State = state;
            client.SetPresence(presence);
        }
    }
}