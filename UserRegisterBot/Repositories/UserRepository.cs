using Dapper;
using Telegram.Bot.Types;
using UserRegisterBot.Entity;

namespace UserRegisterBot.Repositories
{
    public class UserRepository : BaseRepository
    {
        public UserRepository()
        {

        }
        public async Task<long> CountAsync()
        {
            try
            {
                await _connection.OpenAsync();
                string query = "Select Count(*) from public.users ;";
                var result = await _connection.QuerySingleAsync<long>(query);

                return result;
            }
            catch
            {
                return 0;
            }
            finally
            {
                await _connection.CloseAsync();
            }
        }

        public async Task<int> CreateAsync(Users entity)
        {
            try
            {
                await _connection.OpenAsync();

                string query = "INSERT INTO public.users(chat_id, user_name, first_name, last_name, company_name ) " +
                                " VALUES (@ChatId, @UserName, @FirstName, @LastName, @CompanyName );"; 
                              
                var result = await _connection.ExecuteScalarAsync<int>(query, entity);

                return result;
            }

            catch
            {
                return 0;
            }

            finally
            {
                await _connection.CloseAsync();
            }
        }

        public async Task<Users> GetByIdAsync(long ChatId)
        {
            try
            {
                await _connection.OpenAsync();

                string query = "SELECT id, user_name, first_name, last_name, company_name" +
                                    $"WHERE chat_id= {ChatId};";

                var result = await _connection.QuerySingleOrDefaultAsync<Users>(query, new { chat_id = ChatId });

                return result!;
            }
            catch
            {
                return new Users();
            }
            finally
            {
                await _connection.CloseAsync();
            }
        }

        public async Task<int> UpdateAsync(long ChatId, Users entity)
        {
            try
            {
                await _connection.OpenAsync();
                string query = "UPDATE public.users SET user_name=@UserName, first_name=@FirstName, " +
                                    "last_name= @Lastname, company_name = @CompanyName" +
                                        $"WHERE chat_id={ChatId} RETURNING id ";

                var result = await _connection.ExecuteScalarAsync<int>(query, entity);

                return result;
            }
            catch
            {
                return 0;
            }
            finally
            {
                await _connection.CloseAsync();
            }
        }
    }
}
