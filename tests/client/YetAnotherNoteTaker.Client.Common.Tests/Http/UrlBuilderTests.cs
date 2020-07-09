using FluentAssertions;
using Xunit;
using YetAnotherNoteTaker.Client.Common.Http;

namespace YetAnotherNoteTaker.Client.Common.UnitTests.Http
{
    public class UrlBuilderTests
    {
        [Fact]
        public void UserUrlsShouldBeBuilt()
        {
            var users = new UrlBuilder("base").Users;
            users.Post.Should().Be("base/v0/users");
            users.Auth.Should().Be("base/connect/token");
        }

        [Fact]
        public void NotebooksUrlsShouldBeBuilt()
        {
            var notebooks = new UrlBuilder("base").Notebooks;
            notebooks.GetAll("foo@bar").Should().Be("base/v0/users/foo@bar/notebooks");
            notebooks.Get("foo@bar", "key").Should().Be("base/v0/users/foo@bar/notebooks/key");
            notebooks.Post("foo@bar").Should().Be("base/v0/users/foo@bar/notebooks");
            notebooks.Put("foo@bar", "key").Should().Be("base/v0/users/foo@bar/notebooks/key");
            notebooks.Delete("foo@bar", "key").Should().Be("base/v0/users/foo@bar/notebooks/key");
        }

        [Fact]
        public void NotesUrlsShouldBeBuilt()
        {
            var notes = new UrlBuilder("base").Notes;
            notes.GetAll("foo@bar").Should().Be("base/v0/users/foo@bar/notebooks/notes");
            notes.GetByNotebookKey("foo@bar", "nkey").Should().Be("base/v0/users/foo@bar/notebooks/nkey/notes");
            notes.Get("foo@bar", "nkey", "key").Should().Be("base/v0/users/foo@bar/notebooks/nkey/notes/key");
            notes.Post("foo@bar", "nkey").Should().Be("base/v0/users/foo@bar/notebooks/nkey/notes");
            notes.Put("foo@bar", "nkey", "key").Should().Be("base/v0/users/foo@bar/notebooks/nkey/notes/key");
            notes.Delete("foo@bar", "nkey", "key").Should().Be("base/v0/users/foo@bar/notebooks/nkey/notes/key");
        }

        [Fact]
        public void SettingsUrlsShouldBeBuilt()
        {
            var settings = new UrlBuilder("base").Settings;
            settings.Get("foo@bar").Should().Be("base/v0/users/foo@bar/settings");
            settings.Put("foo@bar").Should().Be("base/v0/users/foo@bar/settings");
        }
    }
}
