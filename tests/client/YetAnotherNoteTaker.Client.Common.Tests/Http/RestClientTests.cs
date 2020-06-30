using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Newtonsoft.Json;
using Xunit;
using YetAnotherNoteTaker.Client.Common.Http;
using YetAnotherNoteTaker.Common.Dtos;

namespace YetAnotherNoteTaker.Client.Common.UnitTests.Http
{
    public class RestClientTests
    {
        [Fact]
        public async Task GetShouldReturnADto()
        {
            var expected = new NoteDto
            {
                Body = "Your message goes here",
                Key = "dto"
            };

            var method = string.Empty;

            var restClient = new MockRestClient((HttpRequestMessage message, CancellationToken cancel) =>
            {
                method = message.Method.Method;
                return Task.FromResult(new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = CreateJson(expected)
                });
            });

            var actual = await restClient.Get<NoteDto>("http://some.valid.url");
            actual.Should().BeEquivalentTo(expected);

            method.ToUpper().Should().Be("GET");
        }

        [Fact]
        public async Task PostShouldReturnDto()
        {
            var expected = new NoteDto
            {
                Body = "Your message goes here",
                Key = "dto"
            };

            var method = string.Empty;

            var restClient = new MockRestClient((HttpRequestMessage message, CancellationToken cancel) =>
            {
                method = message.Method.Method;
                return Task.FromResult(new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = CreateJson(expected)
                });
            });

            var actual = await restClient.Post<NoteDto>(
                "http://some.valid.url", new NoteDto());
            actual.Should().BeEquivalentTo(expected);

            method.ToUpper().Should().Be("POST");
        }

        [Fact]
        public async Task PutShouldReturnDto()
        {
            var expected = new NoteDto
            {
                Body = "Your message goes here",
                Key = "dto"
            };

            var method = string.Empty;

            var restClient = new MockRestClient((HttpRequestMessage message, CancellationToken cancel) =>
            {
                method = message.Method.Method;
                return Task.FromResult(new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = CreateJson(expected)
                });
            });

            var actual = await restClient.Put<NoteDto>(
                "http://some.valid.url", new NoteDto());
            actual.Should().BeEquivalentTo(expected);

            method.ToUpper().Should().Be("PUT");
        }

        [Fact]
        public async Task DeleteShouldSendDeleteMessage()
        {
            var method = string.Empty;

            var restClient = new MockRestClient((HttpRequestMessage message, CancellationToken cancel) =>
            {
                method = message.Method.Method;
                return Task.FromResult(new HttpResponseMessage(HttpStatusCode.OK));
            });

            await restClient.Delete("http://some.valid.url");
            method.ToUpper().Should().Be("DELETE");
        }

        private StringContent CreateJson<T>(T body)
        {
            var hateoas = new HateoasDto<T>(
                body,
                new Dictionary<string, string>
                {
                    { "key", "value" }
                });

            return new StringContent(
                JsonConvert.SerializeObject(hateoas),
                Encoding.UTF8,
                "application/json");
        }

        private class MockRestClient : RestClient
        {
            private readonly Func<HttpRequestMessage, CancellationToken, Task<HttpResponseMessage>> _sendAsyncFunc;

            public MockRestClient(
                Func<HttpRequestMessage, CancellationToken, Task<HttpResponseMessage>> sendAsyncFunc)
            {
                _sendAsyncFunc = sendAsyncFunc;
            }

            protected override HttpClient GetClient(string authToken)
            {
                return new HttpClient(new MockHttpHandler(_sendAsyncFunc));
            }
        }

        private class MockHttpHandler : HttpClientHandler
        {
            private readonly Func<HttpRequestMessage, CancellationToken, Task<HttpResponseMessage>> _sendAsyncFunc;

            public MockHttpHandler(
                Func<HttpRequestMessage, CancellationToken, Task<HttpResponseMessage>> sendAsyncFunc)
            {
                _sendAsyncFunc = sendAsyncFunc;
            }

            protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
            {
                return _sendAsyncFunc(request, cancellationToken);
            }
        }
    }
}
