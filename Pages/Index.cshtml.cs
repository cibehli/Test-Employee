using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Project_Employees.Clases;
using System.Data.SqlClient;
using Microsoft.Data.SqlClient;


namespace Project_Employees.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        [BindProperty]
        public Employee emp { get; set; }
        public List<Employee> listemployees = new List<Employee>();

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }
 
        public IActionResult OnGetGetCurrentTime( string name)
        {
            try
            {
                string Connectionstring = "Data Source=localhost;Initial Catalog=Employees;Integrated Security=True; Persist Security Info=False;TrustServerCertificate=True";
                using (SqlConnection connection = new SqlConnection(Connectionstring))
                {
                    connection.Open();
                    string sql = "select Id, Name, LastName, RFC, BornDate, Status  from Employee order by BornDate;";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                emp = new Employee();
                                emp.ID = reader.GetInt32(0);
                                emp.Name = reader.GetString(1);
                                emp.LastName = reader.GetString(2);
                                emp.RFC = reader.GetString(3);
                                emp.BornDate = reader.GetDateTime(4);
                                emp.Status = (EmployeeStatus)reader.GetInt32(5);

                               // listemployees.Add(emp);
                            }
                        }
                    }
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }


            return new JsonResult(emp);
        }
        public void OnGet()
        {
            try
            {
                string Connectionstring = "Data Source=localhost;Initial Catalog=Employees;Integrated Security=True; Persist Security Info=False;TrustServerCertificate=True";
                using(SqlConnection connection = new SqlConnection(Connectionstring))
                {
                    connection.Open();
                    string sql = "select Id, Name, LastName, RFC, BornDate, Status  from Employee order by BornDate;";
                    using(SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using(SqlDataReader reader = command.ExecuteReader())
                        {
                            while(reader.Read())
                            {
                                emp = new Employee();
                                emp.ID = reader.GetInt32(0);
                                emp.Name = reader.GetString(1);
                                emp.LastName = reader.GetString(2);
                                emp.RFC = reader.GetString(3);
                                emp.BornDate = reader.GetDateTime(4);
                                emp.Status = (EmployeeStatus)reader.GetInt32(5);
                                
                                listemployees.Add(emp);
                            }
                        }
                    }
                    connection.Close();
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
    }
}
