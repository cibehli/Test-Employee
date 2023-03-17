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
    public class EmployeeFormModel : PageModel
    {
      
      
        public Employee employee = new Employee();
        public String errorMessage = "";
        public String successMessage = "";

        public void OnGet()
        {
            
            employee.ID = Convert.ToInt32(Request.Query["id"]);
            if(employee.ID>0)
            {
                try
                {
                    string Connectionstring = "Data Source=localhost;Initial Catalog=Employees;Integrated Security=True; Persist Security Info=False;TrustServerCertificate=True";
                    using (SqlConnection connection = new SqlConnection(Connectionstring))
                    {
                        connection.Open();
                        string sql = "select * from employee where id=	@id;";
                        using (SqlCommand command = new SqlCommand(sql, connection))
                        {
                            command.Parameters.AddWithValue("@id", employee.ID);

                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                while (reader.Read())
                                {

                                    employee.ID = reader.GetInt32(0);
                                    employee.Name = reader.GetString(1);
                                    employee.LastName = reader.GetString(2);
                                    employee.RFC = reader.GetString(3);
                                    employee.BornDate = reader.GetDateTime(4);
                                    employee.Status = (EmployeeStatus)reader.GetInt32(5);

                                }
                            }
                        }
                        connection.Close();
                    }
                }
                catch (Exception ex)
                {

                    errorMessage = ex.Message;

                }
            }
            else
                employee.BornDate = DateTime.Now;


        }
        public void OnPost()
        {
            employee.ID = Convert.ToInt32(Request.Form["id"]);
            employee.Name = Request.Form["txt_name"];
            employee.LastName = Request.Form["txt_lastname"];
            employee.RFC = Request.Form["txt_rfc"];
            employee.BornDate = Convert.ToDateTime( Request.Form["txt_birthday"]);
            employee.Status = (EmployeeStatus)Convert.ToInt64(Request.Form["cbo_status"]);

            try
            {
                string Connectionstring = "Data Source=localhost;Initial Catalog=Employees;Integrated Security=True; Persist Security Info=False;TrustServerCertificate=True";
                using (SqlConnection connection = new SqlConnection(Connectionstring))
                {
                    connection.Open();
                    string sql = "sp_InsertModEmployee @Id, @Name,	@LastName,	@RFC, @BornDate, @Status ";
                  
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@id", employee.ID);
                        command.Parameters.AddWithValue("@Name", employee.Name);
                        command.Parameters.AddWithValue("@LastName", employee.LastName);
                        command.Parameters.AddWithValue("@RFC", employee.RFC);
                        command.Parameters.AddWithValue("@BornDate", employee.BornDate);
                        command.Parameters.AddWithValue("@Status", (int)employee.Status);

                        
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int codigo = reader.GetInt32(0);
                                if(codigo==0)
                                {
                                    successMessage = reader.GetString(1);
                                    Response.Redirect("/Index");
                                }
                                else
                                    errorMessage = reader.GetString(1);
                            }
                        }

                    }
                    connection.Close();

                }
            }
            catch (Exception ex)
            {
         
                errorMessage = ex.Message;
                
            }
          


        }
    }
}
