using System.Text.Json.Serialization;

namespace UFF.FichaAnestesica.Domain.Dto
{

    public class UserListDto
    {
        [JsonPropertyName("profissionais")]
        public List<UserDto> Professionals { get; set; }
    }


    public class UserDto
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("nome")]
        public string Name { get; set; }

        [JsonPropertyName("email")]
        public string Email { get; set; }

        [JsonPropertyName("login")]
        public string Login { get; set; }

        [JsonPropertyName("matricula")]
        public string Registration { get; set; }
    }
}
