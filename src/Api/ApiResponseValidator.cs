using RestSharp;
using NUnit.Framework;
using System.Net;

namespace Epam.Automation.src.Api
{
    public static class ApiResponseValidator
    {
        public static void AssertStatusCode(RestResponse response, HttpStatusCode expected)
        {
            Assert.That(response.StatusCode, Is.EqualTo(expected),
                        $"Expected status code: {expected}, Actual: {response.StatusCode}");
        }

        public static void AssertHasHeader(RestResponse response, string headerName)
        {
            var hasHeader = (response.Headers != null && response.Headers.Any(h => h.Name?.ToLower() == headerName.ToLower())) ||
                           (response.ContentHeaders != null && response.ContentHeaders.Any(h => h.Name?.ToLower() == headerName.ToLower()));
            Assert.That(hasHeader, Is.True, $"Missing header: {headerName}");
        }

        public static void AssertHeaderValue(RestResponse response, string headerName, string expectedValue)
        {
            var header = response.Headers?.FirstOrDefault(h => h.Name?.Equals(headerName, StringComparison.OrdinalIgnoreCase) == true) ??
                        response.ContentHeaders?.FirstOrDefault(h => h.Name?.Equals(headerName, StringComparison.OrdinalIgnoreCase) == true);
            
            Assert.That(header, Is.Not.Null, $"Header '{headerName}' not found");
            var actualValue = header?.Value?.ToString() ?? string.Empty;
            Assert.That(actualValue, Does.Contain(expectedValue), 
                $"Expected header '{headerName}' to contain '{expectedValue}', but got '{actualValue}'");
        }

        public static void AssertContainsInBody(RestResponse response, string expectedContent)
        {
            Assert.That(response.Content, Does.Contain(expectedContent), $"Body should contain: {expectedContent}");
        }

        public static void AssertNoErrorMessages(RestResponse response)
        {
            Assert.That(response.ErrorMessage, Is.Null.Or.Empty, 
                $"Expected no error messages, but got: {response.ErrorMessage}");
            Assert.That(response.ErrorException, Is.Null, 
                $"Expected no error exception, but got: {response.ErrorException?.Message}");
        }

        public static void AssertBodyNotEmpty(RestResponse response)
        {
            Assert.That(response.Content, Is.Not.Null.And.Not.Empty, "Response body should not be empty");
        }
    }
}