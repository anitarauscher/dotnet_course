using Epam.Automation.src.Api.Models;

namespace Epam.Automation.src.Api
{
    public class UserRequestBuilder
    {
        private readonly UserDto _user = new();

        public UserRequestBuilder WithName(string name) { _user.Name = name; return this; }
        public UserRequestBuilder WithUsername(string username) { _user.Username = username; return this; }
        public UserRequestBuilder WithEmail(string email) { _user.Email = email; return this; }
        public UserRequestBuilder WithPhone(string phone) { _user.Phone = phone; return this; }
        public UserRequestBuilder WithWebsite(string website) { _user.Website = website; return this; }

        public UserDto Build() => _user;
    }
}