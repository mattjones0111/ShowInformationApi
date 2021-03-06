﻿using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Api.IntegrationTests.Data;
using Api.IntegrationTests.Helpers;
using Api.Models;
using NUnit.Framework;

namespace Api.IntegrationTests
{
    public class ShowInformationTests
    {
        private ApiWebApplicationFactory _factory;
        private HttpClient _client;

        [OneTimeSetUp]
        public void Setup()
        {
            _factory = new ApiWebApplicationFactory();
            _client = _factory.CreateClient();
        }

        [Test]
        public async Task CanPutAndGetShowInformation()
        {
            // arrange
            Show expected = Build.Show();

            // act
            await PutAsync(expected, $"shows/{expected.Id}");

            IEnumerable<Show> getResponse =
                await GetAsync<IEnumerable<Show>>("shows");

            // assert
            Show actual = getResponse.Single(x => x.Id == 1);

            Assert.AreEqual(expected.Id, actual.Id);
            Assert.AreEqual(expected.Name, actual.Name);

            CastMember castMember = actual.Cast.Single();

            Assert.AreEqual(castMember.Id, actual.Cast[0].Id);
            Assert.AreEqual(castMember.Name, actual.Cast[0].Name);
            Assert.AreEqual(castMember.Birthday, actual.Cast[0].Birthday);
        }

        [Test]
        public async Task CanRetrievePagedShowInformation()
        {
            // arrange
            IEnumerable<Show> shows = Build.RandomShows(100);

            foreach(Show s in shows)
            {
                await PutAsync(s, $"shows/{s.Id}");
            }

            // act
            IEnumerable<Show> actual = await GetAsync<IEnumerable<Show>>(
                "shows?pageNumber=1&pageSize=10");

            // assert
            Assert.AreEqual(10, actual.Count());
        }

        [Test]
        public async Task CanGetShowInformationWithSortedCast()
        {
            // arrange
            Show input = Build.UnsortedCast();
            input.Id = 1;

            await PutAsync(input, "shows/1");

            // act
            IEnumerable<Show> fromApi = await GetAsync<IEnumerable<Show>>("shows");

            // assert
            Show actual = fromApi.Single(x => x.Id == 1);

            CastMemberBirthdayComparer comparer = new CastMemberBirthdayComparer();

            Assert.IsTrue(actual.Cast.AreInDescendingOrder(comparer));
        }

        [Test]
        public async Task CannotPutShowWithInvalidName()
        {
            Show expected = Build.InvalidShow_NoName();

            await PutAsync(expected, $"shows/{expected.Id}", HttpStatusCode.BadRequest);
        }

        [Test]
        public async Task CannotPutShowWithInvalidCastMemberName()
        {
            Show expected = Build.InvalidShow_NoCastMemberName();

            await PutAsync(expected, $"shows/{expected.Id}", HttpStatusCode.BadRequest);
        }

        private async Task<T> GetAsync<T>(string url)
        {
            HttpResponseMessage response = await _client.GetAsync(url);

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

            string json = await response.Content.ReadAsStringAsync();

            T result = JsonSerializer.Deserialize<T>(
                json,
                SerializationSettings.Default);

            return result;
        }

        private async Task PutAsync<T>(
            T @object,
            string url,
            HttpStatusCode expectedStatus = HttpStatusCode.NoContent)
        {
            string requestJson = JsonSerializer.Serialize(
                @object,
                SerializationSettings.Default);

            HttpResponseMessage message = await _client.PutAsync(
                url,
                new StringContent(requestJson, Encoding.UTF8, "application/json"));

            Assert.AreEqual(expectedStatus, message.StatusCode);
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            _client.Dispose();
            _factory.Dispose();
        }
    }
}
