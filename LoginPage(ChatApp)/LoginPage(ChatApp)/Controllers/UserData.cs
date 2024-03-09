using LoginPage_ChatApp_.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data;
using System.Data.SqlClient;

namespace LoginPage_ChatApp_.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserData : ControllerBase
    {
        public readonly string ConnectionStrings;
        public UserData(IConfiguration configuration)
        {

            ConnectionStrings = configuration["ConnectionStrings:UserData"] ?? "";

        }
        [HttpPost("Post")]
        public IActionResult CreateUser([FromQuery]UserDto userDto)
        {
            try
            {
                using(var connection  = new SqlConnection(ConnectionStrings))
                {
                    connection.Open();

                    string sql = "INSERT INTO Users" +
                        " (id, name, email, password) VALUES" +
                        " (@id, @name, @email, @password);";

                    using (var command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@id", Guid.NewGuid());
                        command.Parameters.AddWithValue("@name", userDto.Name);
                        command.Parameters.AddWithValue("@email", userDto.Email);
                        command.Parameters.AddWithValue("@password", userDto.Password);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                ModelState.AddModelError("User", "Sorry! We have an exception");
                return BadRequest(ModelState);
            }
            
            return Ok();
        }

        [HttpGet("Get")]
        public IActionResult GetUser()
        {
            List<User> users = new List<User>();
            try
            {
                using(var connection = new SqlConnection(ConnectionStrings))
                {
                    connection.Open();

                   string sql = "SELECT * FROM Users;";
                    using(var command = new SqlCommand(sql, connection))
                    {
                        using(var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                User user = new User();
                                user.Id = reader.GetGuid(0);
                                user.Name = reader.GetString(1);
                                user.Email = reader.GetString(2);
                                user.Password = reader.GetString(3);

                                users.Add(user);
                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                ModelState.AddModelError("User", "Sorry! We have an exception");
                return BadRequest(ModelState);
            }

            return Ok(users);
        }

        [HttpGet("Get/{id}")]
        public IActionResult GetUser(Guid id)
        {
            User user = new User();
            try
            {
                using (var connection = new SqlConnection(ConnectionStrings))
                {
                    connection.Open();

                    string sql = "SELECT * FROM Users WHERE id = @id;";
                    using (var command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@id", id);
                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                user.Id = reader.GetGuid(0);
                                user.Name = reader.GetString(1);
                                user.Email = reader.GetString(2);
                                user.Password = reader.GetString(3);
                            }
                            else
                            {
                                return NotFound();
                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                ModelState.AddModelError("User", "Sorry! We have an exception");
                return BadRequest(ModelState);
            }

            return Ok(user);
        }

        [HttpPut("Put/{id}")]
        public IActionResult UpdateUser(Guid id, UserDto userDto)
        {
            try
            {
                using (var connection = new SqlConnection(ConnectionStrings))
                {
                    connection.Open();
                    string sql = "SELECT * FROM Users SET name = @name, email = @email, password = @password WHERE id = @id";
                    using (var command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@name", userDto.Name);
                        command.Parameters.AddWithValue("@email", userDto.Email);
                        command.Parameters.AddWithValue("@password", userDto.Password);
                        command.Parameters.AddWithValue("@id", id);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("User", "Sorry! We have an exception");
                return BadRequest(ModelState);
            }

            return Ok();
        }

        [HttpDelete("Delete")]
        public IActionResult DeleteUser(Guid id)
        {
            try
            {
                using (var connection = new SqlConnection(ConnectionStrings))
                {
                    connection.Open();

                    string sql = "DELETE * FROM Users WHERE id = @id";
                    using (var command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@id", id);

                        command.ExecuteNonQuery();
                    }

                    return Ok();
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("User", "Sorry! We have an exception");
                return BadRequest(ModelState);
            }
        }
    }
}
