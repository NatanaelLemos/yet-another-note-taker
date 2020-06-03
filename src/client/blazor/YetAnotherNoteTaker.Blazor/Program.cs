using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using YetAnotherNoteTaker.Client.Common.Http;
using YetAnotherNoteTaker.Client.Common.Data;
using YetAnotherNoteTaker.Blazor.Data;
using YetAnotherNoteTaker.Client.Common.Services;
using YetAnotherNoteTaker.Blazor.State;
using Blazored.LocalStorage;
using YetAnotherNoteTaker.Client.Common.State;
using YetAnotherNoteTaker.Client.Common.Events;
using YetAnotherNoteTaker.Client.Common.Events.AuthEvents;
using YetAnotherNoteTaker.Client.Common.Events.NotebookEvents;
using YetAnotherNoteTaker.Client.Common.Events.NoteEvents;
using Blazored.LocalStorage.StorageOptions;
using Blazored.LocalStorage.JsonConverters;

namespace YetAnotherNoteTaker.Blazor
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("app");

            var services = builder.Services;

            services
                .AddSingleton<ILocalStorageService, LocalStorageService>()
                .AddSingleton<ISyncLocalStorageService, LocalStorageService>()
                .Configure<LocalStorageOptions>(configureOptions =>
                {
                    configureOptions.JsonSerializerOptions.Converters.Add(new TimespanJsonConverter());
                });

            services
                .AddSingleton<IUserState, UserState>()
                .AddSingleton<IEventBroker>(new EventBroker(
                    t => Task.FromResult(true)));

            services
                .AddSingleton<IRestClient, RestClient>()
                .AddSingleton<IUrlBuilder>(_ => new UrlBuilder("http://localhost:5000"));

            services
                .AddSingleton<IAuthRepository, BlazorAuthRepository>()
                .AddSingleton<IAuthService, AuthService>()
                .AddSingleton<AuthEventsListener>();

            services
                .AddSingleton<INotebooksRepository, BlazorNotebooksRepository>()
                .AddSingleton<INotebooksService, NotebooksService>()
                .AddSingleton<NotebookEventsListener>();

            services
                .AddSingleton<INotesRepository, BlazorNotesRepository>()
                .AddSingleton<INotesService, NotesService>()
                .AddSingleton<NoteEventsListener>();

            await builder.Build().RunAsync();
        }
    }
}
