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
            Button[] buttons = { new Button() { Label = "Discord Server", Url = "https://discord.gg/eonfn" }, new Button() { Label = "Download Launcher", Url = "https://api.lunafn.xyz/Launcherdownload" } };

            presence = new RichPresence()
            {
                Details = "Relive OG Fortnite Chapter 2 Season 2",
                State = "",
                Timestamps = rpctimestamp,
                Buttons = buttons,

                Assets = new Assets()
                {
                    LargeImageKey = "bigtestlogo",
                    LargeImageText = "Join Discord.gg/eonfn To Play",
                    SmallImageKey = "testlogo",
                    SmallImageText = "Project Eon"
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