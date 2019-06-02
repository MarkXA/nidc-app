using System;
using System.IO;
using System.Net.Http;
using System.Reactive;
using System.Reactive.Subjects;
using System.Reflection;
using Newtonsoft.Json;
using NidcApp.Models;
using Xamarin.Forms;

namespace NidcApp
{
    public static class AppState
    {
        public static readonly ReplaySubject<Conference> Conference = new ReplaySubject<Conference>(1);
        public static readonly ReplaySubject<Page> NavigatePage = new ReplaySubject<Page>(1);
        public static readonly Subject<Unit> Refresh = new Subject<Unit>();

        private static bool initialised;
        private static string localPath;

        public static void Init()
        {
            if (initialised)
                return;
            initialised = true;

            localPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "conference.json");

            Refresh.Subscribe(OnRefresh);

            Refresh.OnNext(Unit.Default);

            //if (File.Exists(localPath))
            //{
            //    try
            //    {
            //        //Conference.OnNext(JsonConvert.DeserializeObject<Conference>(File.ReadAllText(localPath)));
            //        return;
            //    }
            //    catch (Exception) { }
            //}

            var assembly = typeof(AppState).GetTypeInfo().Assembly;
            using (var stream = assembly.GetManifestResourceStream("NidcApp.Models.seed.json"))
            using (var reader = new StreamReader(stream))
            {
                Conference.OnNext(JsonConvert.DeserializeObject<Conference>(reader.ReadToEnd()));
            }
        }

        private static async void OnRefresh(Unit unit)
        {
            using (var httpClient = new HttpClient())
            {
                try
                {
                    //var json = await httpClient.GetStringAsync("https://www.nidevconf.com/app/data2.json");
                    //Conference.OnNext(JsonConvert.DeserializeObject<Conference>(json));
                    //File.WriteAllText(localPath, json);
                }
                catch (Exception) { }
            }
        }
    }
}