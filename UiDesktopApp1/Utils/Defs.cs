/*using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using DnsClient;

namespace Astro.Utils
{
    internal static class Defs
    {
        public static string GamePath851 = "NONE";
        public static string GamePath1000 = "NONE";
        public static string GamePath1241 = "NONE";
        public static string GamePath1440 = "NONE";
        public static string EAC = "AstroEAC.exe";
        public static string BE = "FortniteGame\\Binaries\\Win64\\FortniteClient-Win64-Shipping_BE.exe";
        public static string Launcher = "FortniteGame\\Binaries\\Win64\\FortniteLauncher.exe";
        public static string CalderaBE = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJhY2NvdW50X2lkIjoiMTM5ZDAzOGFmOTM2NDcyODgxMTdlYWU3MWYxZGQ5ZTQiLCJnZW5lcmF0ZWQiOjE3MDQ0MTE3MDIsImNhbGRlcmFHdWlkIjoiYTU1ZmVkMWMtOTg0NC00Y2FiLTlmYjAtOTc3MjczNDYwM2JkIiwiYWNQcm92aWRlciI6IkJhdHRsRXllIiwibm90ZXMiOiIiLCJmYWxsYmFjayI6ZmFsc2V9.UC5vNufRcMbKrsoGJpyRGrSRp7EWcAaTXXoXLAHgMU0";
        public static string CalderaEAC = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJhY2NvdW50X2lkIjoiMTM5ZDAzOGFmOTM2NDcyODgxMTdlYWU3MWYxZGQ5ZTQiLCJnZW5lcmF0ZWQiOjE3MDQ0MTE5MDQsImNhbGRlcmFHdWlkIjoiODhjZmQ5NzYtM2U2OS00MWYzLWI2ODEtYzQyOTcxM2ZkMWFlIiwiYWNQcm92aWRlciI6IkVhc3lBbnRpQ2hlYXQiLCJub3RlcyI6IiIsImZhbGxiYWNrIjpmYWxzZX0.Q8hdxvrW2sH-3on6JEBLANB0rkPAGUwbZYPrCOMTtvA";
        public static string Params = "-astro -epicapp=Fortnite -epicenv=Prod -epiclocale=en-us -epicportal -nobe -fromfl=eac -fltoken=h1cdhchd10150221h130eB56 -skippatchcheck";
        public static bool UseEAC = true;
        public static string Backend = null;
        public static string Email = "NONE";
        public static string Password = "NONE";

        public static void SetBackendURL()
        {
            if (Backend == null)
            {
                var lc = new LookupClient();
                var res = lc.Query("meowscles.ploosh.dev", QueryType.TXT);
                var rec = res.Answers.TxtRecords().FirstOrDefault();
                Backend = rec.Text.FirstOrDefault();
            }
        }
    }
}
*/