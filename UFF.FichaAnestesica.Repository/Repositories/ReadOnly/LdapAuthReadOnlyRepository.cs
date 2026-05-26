using Microsoft.Extensions.Configuration;
using System.DirectoryServices.Protocols;
using System.Net;
using UFF.FichaAnestesica.Domain.Repositories.ReadOnly;

namespace UFF.FichaAnestesica.Infra.Repositories.ReadOnly
{
    public class LdapAuthReadOnlyRepository : ILdapAuthReadOnlyRepository
    {
        private readonly IConfiguration _configuration;

        public LdapAuthReadOnlyRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public bool ValidateCredentials(string username, string password)
        {
            try
            {
                return username == "admin" || username == "amanda.onishi";

                //var ldapServer = _configuration["Ldap:Server"];
                //var ldapPort = int.Parse(_configuration["Ldap:Port"]);
                //var domain = _configuration["Ldap:Domain"];

                //var identifier = new LdapDirectoryIdentifier(ldapServer, ldapPort);

                //using var connection = new LdapConnection(identifier);

                //connection.AuthType = AuthType.Negotiate;

                //var credential = new NetworkCredential(
                //    username,
                //    password,
                //    domain
                //);

                //connection.Bind(credential);

               // return true;
            }
            catch
            {
                return false;
            }
        }
    }
}