using NUnit.Framework;
using RestSharp;
using System.Net;
using Epam.Automation.src.Api;
using Epam.Automation.src.Api.Models;
using Epam.Automation.src.Core;
using Newtonsoft.Json;

namespace Epam.Automation.src.Tests
{
    [TestFixture]
    [Category("API")]
    [Parallelizable(ParallelScope.All)]
    public class ApiTests
    {
        private ApiClient _client;
        private string _baseUrl;

        [SetUp]
        public void SetUp()
        {
            _baseUrl = ConfigManager.ApiUrl;
            _client = new ApiClient(_baseUrl);
            Logger.Info($"Starting test: {TestContext.CurrentContext.Test.Name}");
        }

        [TearDown]
        public void TearDown()
        {
            Logger.Info($"Finished test: {TestContext.CurrentContext.Test.Name}");
        }

        #region Task #1: Validate that the list of users can be received successfully

        [Test]
        [Description("Task #1: Validate that the list of users can be received successfully")]
        public void Task1_GetUsers_ShouldReturnListOfUsersWithAllFields()
        {
            // Arrange & Act
            var response = _client.Get("/users");

            // Assert - Validate 200 OK status code
            ApiResponseValidator.AssertStatusCode(response, HttpStatusCode.OK);
            ApiResponseValidator.AssertNoErrorMessages(response);

            // Validate response is not empty
            ApiResponseValidator.AssertBodyNotEmpty(response);

            // Deserialize and validate user structure
            var users = JsonConvert.DeserializeObject<List<UserDto>>(response.Content!);
            Assert.That(users, Is.Not.Null, "Users list should not be null");
            Assert.That(users!.Count, Is.GreaterThan(0), "Users list should contain at least one user");

            // Validate each user has required fields: id, name, username, email, address, phone, website, company
            foreach (var user in users)
            {
                Assert.That(user.Id, Is.Not.Null, "User Id should not be null");
                Assert.That(user.Name, Is.Not.Null.And.Not.Empty, "User Name should not be null or empty");
                Assert.That(user.Username, Is.Not.Null.And.Not.Empty, "User Username should not be null or empty");
                Assert.That(user.Email, Is.Not.Null.And.Not.Empty, "User Email should not be null or empty");
                Assert.That(user.Address, Is.Not.Null, "User Address should not be null");
                Assert.That(user.Phone, Is.Not.Null.And.Not.Empty, "User Phone should not be null or empty");
                Assert.That(user.Website, Is.Not.Null.And.Not.Empty, "User Website should not be null or empty");
                Assert.That(user.Company, Is.Not.Null, "User Company should not be null");
            }

            Logger.Info($"Successfully validated {users.Count} users with all required fields");
        }

        #endregion

        #region Task #2: Validate response header for a list of users

        [Test]
        [Description("Task #2: Validate response header for a list of users")]
        public void Task2_GetUsers_ShouldHaveCorrectContentTypeHeader()
        {
            // Arrange & Act
            var response = _client.Get("/users");

            // Assert - Validate content-type header exists
            ApiResponseValidator.AssertHasHeader(response, "Content-Type");

            // Validate the value of the content-type header is application/json; charset=utf-8
            ApiResponseValidator.AssertHeaderValue(response, "Content-Type", "application/json");
            ApiResponseValidator.AssertHeaderValue(response, "Content-Type", "charset=utf-8");

            // Validate 200 OK status code
            ApiResponseValidator.AssertStatusCode(response, HttpStatusCode.OK);
            ApiResponseValidator.AssertNoErrorMessages(response);

            Logger.Info("Successfully validated Content-Type header");
        }

        #endregion

        #region Task #3: Validate response header for a list of users (body content validation)

        [Test]
        [Description("Task #3: Validate response body for a list of users")]
        public void Task3_GetUsers_ShouldReturnArrayOf10UsersWithValidData()
        {
            // Arrange & Act
            var response = _client.Get("/users");

            // Assert - Validate 200 OK status code
            ApiResponseValidator.AssertStatusCode(response, HttpStatusCode.OK);
            ApiResponseValidator.AssertNoErrorMessages(response);

            // Validate that the content of the response body is the array of 10 users
            var users = JsonConvert.DeserializeObject<List<UserDto>>(response.Content!);
            Assert.That(users, Is.Not.Null, "Users list should not be null");
            Assert.That(users!.Count, Is.EqualTo(10), "Users list should contain exactly 10 users");

            // Validate that each user should be with different ID
            var userIds = users.Select(u => u.Id).ToList();
            var distinctIds = userIds.Distinct().ToList();
            Assert.That(distinctIds.Count, Is.EqualTo(userIds.Count), "All user IDs should be unique");

            // Validate that each user should be with non-empty Name and Username
            foreach (var user in users)
            {
                Assert.That(user.Name, Is.Not.Null.And.Not.Empty, 
                    $"User with ID {user.Id} should have non-empty Name");
                Assert.That(user.Username, Is.Not.Null.And.Not.Empty, 
                    $"User with ID {user.Id} should have non-empty Username");
            }

            // Validate that each user contains the Company with non-empty Name
            foreach (var user in users)
            {
                Assert.That(user.Company, Is.Not.Null, 
                    $"User with ID {user.Id} should have Company");
                Assert.That(user.Company!.Name, Is.Not.Null.And.Not.Empty, 
                    $"User with ID {user.Id} should have Company with non-empty Name");
            }

            Logger.Info("Successfully validated 10 users with unique IDs, non-empty names/usernames, and company names");
        }

        #endregion

        #region Task #4: Validate that user can be created

        [Test]
        [Description("Task #4: Validate that user can be created")]
        public void Task4_CreateUser_ShouldReturnCreatedUserWithId()
        {
            // Arrange - Create user with Name and Username fields using Builder pattern
            var newUser = new UserRequestBuilder()
                .WithName("John Doe")
                .WithUsername("johndoe")
                .Build();

            // Act
            var response = _client.Post("/users", newUser);

            // Assert - Validate that response is not empty and contains the ID value
            ApiResponseValidator.AssertBodyNotEmpty(response);
            
            var createdUser = JsonConvert.DeserializeObject<UserDto>(response.Content!);
            Assert.That(createdUser, Is.Not.Null, "Created user should not be null");
            Assert.That(createdUser!.Id, Is.Not.Null, "Created user should have an ID");
            Assert.That(createdUser.Id, Is.GreaterThan(0), "Created user ID should be greater than 0");

            // Validate that user receives 201 Created response code
            ApiResponseValidator.AssertStatusCode(response, HttpStatusCode.Created);
            ApiResponseValidator.AssertNoErrorMessages(response);

            Logger.Info($"Successfully created user with ID: {createdUser.Id}");
        }

        #endregion

        #region Task #5: Validate that user is notified if resource doesn't exist

        [Test]
        [Description("Task #5: Validate that user is notified if resource doesn't exist")]
        public void Task5_GetInvalidEndpoint_ShouldReturn404NotFound()
        {
            // Arrange & Act - Create and send a request to invalid endpoint
            var response = _client.Get("/invalidendpoint");

            // Assert - Validate that user receives 404 Not Found response code
            ApiResponseValidator.AssertStatusCode(response, HttpStatusCode.NotFound);
            // Note: For 404, error exception is expected behavior, so we don't assert no error messages

            Logger.Info("Successfully validated 404 Not Found for invalid endpoint");
        }

        #endregion

        #region Additional Validation Tests (existing tests kept for reference)

        [Test]
        public void GetUser_ShouldReturnCorrectUser()
        {
            var response = _client.Get("/users/1");
            ApiResponseValidator.AssertStatusCode(response, HttpStatusCode.OK);
            ApiResponseValidator.AssertContainsInBody(response, "\"id\": 1");
        }

        [Test]
        public void GetUser_ResponseShouldContainHeadersAndBody()
        {
            var response = _client.Get("/users/1");
            ApiResponseValidator.AssertStatusCode(response, HttpStatusCode.OK);
            ApiResponseValidator.AssertHasHeader(response, "Content-Type");
            ApiResponseValidator.AssertContainsInBody(response, "Leanne Graham");
        }

        [Test]
        public void CreateUser_ShouldReturnCreatedUser()
        {
            var newUser = new UserRequestBuilder()
                .WithName("Test User")
                .WithUsername("testuser")
                .WithEmail("testuser@example.com")
                .WithPhone("123-456-7890")
                .WithWebsite("testsite.com")
                .Build();

            var response = _client.Post("/users", newUser);
            Assert.That(
                response.StatusCode == HttpStatusCode.Created || response.StatusCode == HttpStatusCode.OK,
                $"Expected status code 201 or 200, Actual: {response.StatusCode}"
            );
            Assert.That(response.Content, Does.Contain("Test User"));
        }

        [Test]
        public void GetNonExistentEndpoint_ShouldReturnNotFound()
        {
            var response = _client.Get("/nonexistent");
            ApiResponseValidator.AssertStatusCode(response, HttpStatusCode.NotFound);
        }

        [Test]
        public void CreateUser_WithInvalidBody_ShouldReturnBadRequest()
        {
            var response = _client.Post("/users", new { });
            Assert.That(
                response.StatusCode == HttpStatusCode.Created || response.StatusCode == HttpStatusCode.BadRequest || response.StatusCode == HttpStatusCode.OK,
                $"Expected status 201/400/200, Actual: {response.StatusCode}"
            );
        }

        #endregion
    }
}