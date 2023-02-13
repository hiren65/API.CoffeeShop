using System.Data;
using CoffeeShop.API.Models;
using Microsoft.Data.SqlClient;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace CoffeeShop.API.Services
{
    public class SaveData
    {
        public IConfiguration _configuration;
        public string conStr;
        public SqlConnection con;
       
        public  CoffeeContext _context;
        public SaveData(IConfiguration configuration, CoffeeContext context)
        {
            _configuration = configuration;
            conStr =
                "Server=DESKTOP-1IC1TU6;Initial Catalog=CoffeeContext;Integrated Security=True;MultipleActiveResultSets=True;Encrypt=False;";
            _context = context;     
        }

        public string SaveMyData(CoffeeOrder co)
        {
            var ccList = _context.CustOrders.Select(m => m.Repeat).ToList();
            int last = 0;
            try
            {
                last = Convert.ToInt32( ccList.LastOrDefault());
            }
            catch (Exception e)
            {
                
            }
            int cc = 0;
            if (last >= 4)
            {
                co.Repeat = 0;
                cc = 0;
            }
            else
            {
                cc = last;
                cc = cc + 1;
            }
            string returnStr = "";
            string sql = "insert into CustOrders (TypeCoffee, message, Repeat, prepared)"; //values( {type}, {mesg}, {cc}, {prepared})";
            sql += " VALUES (@type, @mesg, @cc, @prepared)";
            using (con = new SqlConnection(conStr))
            {
                con.Open();
               
                    try
                    {
                        using (SqlCommand cmd = new SqlCommand(sql,con))
                        {
                           
                            cmd.Parameters.Add(new SqlParameter("@type", co.Type));
                            cmd.Parameters.Add(new SqlParameter("@mesg", co.message));
                            cmd.Parameters.Add(new SqlParameter("@cc", cc));
                            cmd.Parameters.Add(new SqlParameter("@prepared", co.prepared));

                            cmd.CommandType = CommandType.Text;


                            cmd.ExecuteNonQuery();
                        }

                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                        
                    }


            }


            return returnStr;
        }


    }                                                                                                                                 
}
