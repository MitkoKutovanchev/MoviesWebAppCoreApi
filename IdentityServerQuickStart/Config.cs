using Data.DB.Repositories;
using IdentityServer4.Models;
using IdentityServer4.Test;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace IdentityServerQuickStart
{
    public class Config
    {
        public static UserRepository userRepo = new UserRepository();

        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
                {
                    new ApiResource("Api1", "Api1")
                };
        }


        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>
            {
                new Client
                {
                    ClientId = "webappapi",
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,

                    ClientSecrets =
                    {
                        new Secret("webappapi".Sha256())
                    },
                    AllowedScopes = {"Api1"}
                }
            };
        }


        public static List<TestUser> GetUsers()
        {
            List<TestUser> testUsers = new List<TestUser>();
            foreach (var user in userRepo.GetAll())
            {
                testUsers.Add(new TestUser
                {
                    SubjectId = user.Id,
                    Username = user.Username,
                    Password = user.Password,
                    Claims = new[]
                        {
                            new Claim(user.IsAdmin?"admin":"user", user.IsAdmin?"admin":"user"),
                        }
                });
            }

            return testUsers;
               
        }

    }
}
