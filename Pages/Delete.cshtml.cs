using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Project_Employees.Clases;
using Microsoft.Data.SqlClient;

namespace Project_Employees.Pages
{
    public class DeleteModel : PageModel
    {
        public String errorMessage = "";
        public String successMessage = "";
        public void OnGet()
        {
            int id = Convert.ToInt32(Request.Query["id"]);

            try
            {
                string Connectionstring = "Data Source=localhost;Initial Catalog=Employees;Integrated Security=True; Persist Security Info=False;TrustServerCertificate=True";
                using (SqlConnection connection = new SqlConnection(Connectionstring))
                {
                    connection.Open();
                    string sql = "delete from employee where id=@id;";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@id", id);

                        command.ExecuteNonQuery();
                    }
                    connection.Close();
                }
            }
            catch (Exception ex)
            {

                errorMessage = ex.Message;

            }
            Response.Redirect("/Index");
        }
            
    }
}
