using System.Data.SqlClient;
using System.Linq;
using Dapper;
using Microsoft.Extensions.Options;
using UserAuthentication.Models;
using UserAuthentication.Repository.Interface;

namespace UserAuthentication.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ConnectionString _connectionString;

        public UserRepository(IOptions<ConnectionString>connectionString)
        {
            _connectionString = connectionString.Value;
        }
        public int Authenticate(UserCred userCred)
        {
            const string query = @"
SELECT id
FROM users
WHERE username = @Username
	AND password = @Password";

            var connection = new SqlConnection(_connectionString.UserDB);
            var id = connection.Query<int>(query,new 
                                            { 
                                                userCred.Username,
                                                userCred.Password 
                                            });
            if(id.Count() == 0)
            {
                return 0;
            }
            return id.Single();
        }

        public int Register(User user)
        {
            const string sql = @"
INSERT INTO Users
VALUES (
	@Username
	,@Password
	,@Name
	)
SELECT SCOPE_IDENTITY()";
            using var connection = new SqlConnection(_connectionString.UserDB);
            var id = connection.Query<int>(sql, new { user.Username, user.Password, user.Name });
            return id.Single();
        }

        public User Get(int id)
        {
            const string query = @"
SELECT Name
	,Username
	,Id
FROM users
WHERE id = @id";
            using var connection = new SqlConnection(_connectionString.UserDB);
            var user = connection.QuerySingle<User>(query, new { id });
            return user;
        }

    }
}
