using Newtonsoft.Json;
using PlayFab;
using PlayFab.ClientModels;
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection.Emit;
using System.Runtime.Remoting.Services;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace playfabshit
{
    internal class Program
    {
        static void Main(string[] args)
        {
            RunAsync().GetAwaiter().GetResult();
        }
        static async Task RunAsync()
        {
            try
            {
                Console.SetWindowSize(100, 30);   // width: 100 chars, height: 30 lines
                Console.BufferWidth = 100;        // match buffer width
                Console.BufferHeight = 300;       // allow scrolling
            }
            catch (ArgumentOutOfRangeException)
            {
                MessageBox.Show("your screen isnt big enough for this tool, get a higher resolution monitor", "get a bigger monitor", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            Console.WriteLine(" ________  ________  ___  ___  ________  _______      \r\n|\\   __  \\|\\_____  \\|\\  \\|\\  \\|\\   __  \\|\\  ___ \\     \r\n\\ \\  \\|\\  \\\\|___/  /\\ \\  \\\\\\  \\ \\  \\|\\  \\ \\   __/|    \r\n \\ \\   __  \\   /  / /\\ \\  \\\\\\  \\ \\   _  _\\ \\  \\_|/__  \r\n  \\ \\  \\ \\  \\ /  /_/__\\ \\  \\\\\\  \\ \\  \\\\  \\\\ \\  \\_|\\ \\ \r\n   \\ \\__\\ \\__\\\\________\\ \\_______\\ \\__\\\\ _\\\\ \\_______\\\r\n    \\|__|\\|__|\\|_______|\\|_______|\\|__|\\|__|\\|_______|\r\n                                                      \r\n                                                      \r\n                                                      ");
            Console.WriteLine("--------------------------------------------------------");
            Console.WriteLine("uhh multitool coded by tk9");
            Console.WriteLine("not a rat (if you dont believe me js look at the src)");
            Console.WriteLine("--------------------------------------------------------");
            Console.WriteLine("Select wtv you fucking want idfk");
            Console.WriteLine("Playfab Spammer : 1 ");
            Console.WriteLine("Get Playfab Session Ticket : 2");
            Console.WriteLine("Call Playfab Revision : 3");
            Console.WriteLine("Webhook Spammer : 4");
            Console.WriteLine("Send webhook message : 5");
            Console.Write("input the number : ");
            string numberinput = Console.ReadLine();
            int numinputint;
            if (int.TryParse(numberinput, out numinputint))
            {
                if (numinputint == 1)
                {
                    Console.Clear();
                    Console.Write("Enter the Playfab Title Id : ");
                    string titleid = Console.ReadLine();
                    Console.Write("Enter the amount of accounts to create : ");
                    string AmountOfAccounts = Console.ReadLine();

                    spamthread(titleid, AmountOfAccounts);

                }
                else if (numinputint == 2)
                {
                    Console.Clear();
                    Console.Write("Enter the Playfab Title Id : ");
                    string titleid = Console.ReadLine();
                    EnterInfoSessTick(titleid);

                }
                else if (numinputint == 3)
                {
                    callcloudscript().GetAwaiter().GetResult();
                }
                else if (numinputint == 4)
                {
                    spamwebhook();
                }
                else if (numinputint == 5)
                {
                    sendwebhookmessage();
                }
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Invalid number");
                Console.WriteLine("Press any key to exit");
                Console.ReadKey();
            }
        }
        static string GetRandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            Random rnd = new Random();
            string result = "";

            for (int i = 0; i < length; i++)
            {
                result += chars[rnd.Next(chars.Length)];
            }

            return result;
        }
        static async void spamthread(string titleid, string AmountOfAccountsa)
        {
            int AnmountOfAccountsint;
            if (int.TryParse(AmountOfAccountsa, out AnmountOfAccountsint))
            {
                for (int i = 0; i < AnmountOfAccountsint; i++)
                {
                    Thread.Sleep(2000);
                    string randomid = GetRandomString(14);

                    PlayFabSettings.staticSettings.TitleId = titleid;

                    var request = new LoginWithCustomIDRequest
                    {
                        CustomId = randomid,
                        CreateAccount = true
                    };

                    // Async Login
                    PlayFabResult<LoginResult> loginResultWrapper = await PlayFabClientAPI.LoginWithCustomIDAsync(request);
                    if (loginResultWrapper.Error != null)
                    {
                        // Fehler behandeln
                        Console.WriteLine($"Login failed: {loginResultWrapper.Error.ErrorMessage}");
                        if (loginResultWrapper.Error.ErrorDetails != null)
                        {
                            foreach (var detail in loginResultWrapper.Error.ErrorDetails)
                                Console.WriteLine($"{detail.Key}: {string.Join(", ", detail.Value)}");
                        }
                    }
                    else
                    {
                        // Erfolg
                        var loginResult = loginResultWrapper.Result;
                        Console.WriteLine($"Account Created PlayFabId: {loginResult.PlayFabId}");
                    }
                }
                
            }
            else
            {
                Console.Clear();
                Console.WriteLine("invalid amount of accounts");
                Console.WriteLine("Press any key to exit");
                Console.ReadKey();
            }
        }

        static void EnterInfoSessTick(string titleid)
        {
            Console.WriteLine("enter player id (put 0 for a random id)");
            string sessionselector = Console.ReadLine();
            if (sessionselector == "0")
            {
                GetSessionTicket(titleid).GetAwaiter().GetResult();
            }
            else
            {
                GetManualSessionTicket(titleid, sessionselector).GetAwaiter().GetResult();
            }

        }
        static async Task GetSessionTicket(string titleid)
        {
            string randomida = GetRandomString(14);

            PlayFabSettings.staticSettings.TitleId = titleid;

            var request = new LoginWithCustomIDRequest
            {
                CustomId = randomida,
                CreateAccount = true
            };

            // Async Login
            PlayFabResult<LoginResult> loginResultWrapper = await PlayFabClientAPI.LoginWithCustomIDAsync(request);
            if (loginResultWrapper.Error != null)
            {
                // Fehler behandeln
                Console.WriteLine($"Login failed: {loginResultWrapper.Error.ErrorMessage}");
                if (loginResultWrapper.Error.ErrorDetails != null)
                {
                    foreach (var detail in loginResultWrapper.Error.ErrorDetails)
                        Console.WriteLine($"{detail.Key}: {string.Join(", ", detail.Value)}");
                }
            }
            else
            {
                // Erfolg
                var loginResult = loginResultWrapper.Result;
                Console.WriteLine($"Session Ticket : {loginResult.SessionTicket}");
                Console.WriteLine("Press any key to exit");
                Console.ReadKey();
            }
        }
        static async Task GetManualSessionTicket(string titleid, string playerid)
        {

            PlayFabSettings.staticSettings.TitleId = titleid;

            var request = new LoginWithCustomIDRequest
            {
                CustomId = playerid,
                CreateAccount = true
            };

            // Async Login
            PlayFabResult<LoginResult> loginResultWrapper = await PlayFabClientAPI.LoginWithCustomIDAsync(request);
            if (loginResultWrapper.Error != null)
            {
                // Fehler behandeln
                Console.WriteLine($"Login failed: {loginResultWrapper.Error.ErrorMessage}");
                if (loginResultWrapper.Error.ErrorDetails != null)
                {
                    foreach (var detail in loginResultWrapper.Error.ErrorDetails)
                        Console.WriteLine($"{detail.Key}: {string.Join(", ", detail.Value)}");
                }
            }
            else
            {
                // Erfolg
                var loginResult = loginResultWrapper.Result;
                Console.WriteLine($"Session Ticket : {loginResult.SessionTicket}");
                Console.WriteLine("press any key to exit");
                Console.ReadKey();

            }
        }

        static async Task callcloudscript()
        {
            Console.Clear();
            Console.Write("Enter the title id : ");
            string titleid = Console.ReadLine();
            EnterInfoSessTick(titleid);
            ExecuteCloudScriptRequest executeCloudScriptRequest = new ExecuteCloudScriptRequest();
            Console.Write("Enter the function name : ");
            executeCloudScriptRequest.FunctionName = Console.ReadLine();
            Console.Write("you have to edit source code to change arguments");
            executeCloudScriptRequest.FunctionParameter = new { };

            await PlayFabClientAPI.ExecuteCloudScriptAsync(executeCloudScriptRequest);
            Console.WriteLine("executed cloud script");
            Console.WriteLine("Press any key to exit");
            Console.ReadKey();

        }

        static void spamwebhook()
        {
            Console.Clear();
            Console.Write("enter the webhook : ");
            string webhookUrl = Console.ReadLine();
            Console.Write("Enter the message you want to send : ");
            string message = Console.ReadLine();
            Console.Write("Enter the Amount of messages : ");
            string amountofmessages = Console.ReadLine();
            int amountofmessagesint = int.Parse(amountofmessages);
            for (int i = 0; i < amountofmessagesint; i++)
            {
                using (WebClient client = new WebClient())
                {
                    client.Headers.Add("Content-Type", "application/json");

                    string json = "{\"content\":\"" + message + "\"}";

                    try
                    {
                        string response = client.UploadString(webhookUrl, "POST", json);
                        Console.WriteLine("Message sent successfully!");
                    }
                    catch (WebException ex)
                    {
                        Console.WriteLine("Error sending message: " + ex.Message);
                    }
                }
            }
        }
        static void sendwebhookmessage()
        {
            Console.Clear();
            Console.Write("enter the webhook : ");
            string webhookUrl = Console.ReadLine();
            Console.Write("Enter the message you want to send : ");
            string message = Console.ReadLine();
            for (int i = 0; i < 1; i++)
            {
                using (WebClient client = new WebClient())
                {
                    client.Headers.Add("Content-Type", "application/json");

                    string json = "{\"content\":\"" + message + "\"}";

                    try
                    {
                        string response = client.UploadString(webhookUrl, "POST", json);
                        Console.WriteLine("Message sent successfully!");
                    }
                    catch (WebException ex)
                    {
                        Console.WriteLine("Error sending message: " + ex.Message);
                    }
                }
            }
        }
    }
}

