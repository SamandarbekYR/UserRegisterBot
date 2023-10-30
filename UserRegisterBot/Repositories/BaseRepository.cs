using Npgsql;

namespace UserRegisterBot.Repositories
{
    public class BaseRepository
    {
        protected NpgsqlConnection _connection;

        public BaseRepository()
        {
            Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;

            string connection = "Host=localhost; Port=5432; User Id=postgres; Password=1234; Database=register-bot-db;";
            this._connection = new NpgsqlConnection(connection);
        }
    }
}
