using DataLayer.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Codingtron_prp_formdata.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public UserController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        [HttpPost]
        public JsonResult Register(Codingtron_User user)
        {
            DataTable tbl = new DataTable();
            string SqlDataSource = _configuration.GetConnectionString("infinitloop_db");
            using(SqlConnection mycon= new SqlConnection(SqlDataSource))
            {
                mycon.Open();
                using (SqlCommand mycmd = new SqlCommand("Codingtron_User_Insert", mycon))
                {
                    mycmd.CommandType = CommandType.StoredProcedure;
                    mycmd.Parameters.Add("@pFirstName", SqlDbType.NVarChar).Value = user.FirstName;
                    mycmd.Parameters.Add("@pLastname", SqlDbType.NVarChar).Value = user.LastName;
                    mycmd.Parameters.Add("@pEmail", SqlDbType.NVarChar).Value = user.Email;
                    mycmd.Parameters.Add("@pPassword", SqlDbType.NVarChar).Value = user.Password;
                    mycmd.ExecuteNonQuery();

                }

            }
            return new JsonResult("success");
        }
    }
   
}
