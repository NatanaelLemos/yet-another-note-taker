using System.Threading.Tasks;
using Blazored.LocalStorage;
using Blazored.LocalStorage.JsonConverters;
using Blazored.LocalStorage.StorageOptions;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Observatron;
using YetAnotherNoteTaker.Blazor.Data;
using YetAnotherNoteTaker.Blazor.State;
using YetAnotherNoteTaker.Client.Common.Data;
using YetAnotherNoteTaker.Client.Common.Events.AuthEvents;
using YetAnotherNoteTaker.Client.Common.Events.NotebookEvents;
using YetAnotherNoteTaker.Client.Common.Events.NoteEvents;
using YetAnotherNoteTaker.Client.Common.Http;
using YetAnotherNoteTaker.Client.Common.Services;
using YetAnotherNoteTaker.Client.Common.State;

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
                .AddObservatron();

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
