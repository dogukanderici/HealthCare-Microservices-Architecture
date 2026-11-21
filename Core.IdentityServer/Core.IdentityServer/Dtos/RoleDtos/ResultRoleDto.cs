namespace Core.IdentityServer.Dtos.RoleDtos
{
    public class ResultRoleDto
    {
        public string Id { get; set; }
        public string Name { get; set; }

        public ResultRoleDto(string id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}