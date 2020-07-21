using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Collections;

namespace AdoSqlServerHelper
{
    public partial class Examples : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        //Getting all employees data
        protected void btnLoadData_Click(object sender, EventArgs e)
        {
            string sql = "SELECT * FROM Employees";
            GridView1.DataSource = SqlHelper.ExecuteQuery(sql, CommandType.Text, null);
            GridView1.DataBind();
        }

        //Selecting only the employee with id=1
        protected void btnSqlWhere_Click(object sender, EventArgs e)
        {
            string sql = "SELECT * FROM Employees WHERE EmpID=@ID";

            // SqlParameter uses for security to prevent SQL injection
            //@ID in the SQL above must match the parameter in SqlParameter
            SqlParameter[] parametersList = new SqlParameter[]{
                new SqlParameter ("@ID","1"), // you can read it from input or Textbox
           };

            GridView1.DataSource = SqlHelper.ExecuteQuery(sql, CommandType.Text, parametersList);
            GridView1.DataBind();
        }

        //Getting the maximum salary of all employees as only one value
        protected void btnExecuteScalar_Click(object sender, EventArgs e)
        {
            string sql = "SELECT MAX(Age) FROM Employees";
            lbMsg.Text = SqlHelper.ExecuteScalar(sql, CommandType.Text, null);
            
        }

        //Execute stored procedure
        protected void btnSP_Click(object sender, EventArgs e)
        {
            GridView1.DataSource = SqlHelper.ExecuteQuery("GetAllEmployees", CommandType.StoredProcedure, null);
            GridView1.DataBind();
        }

        protected void btnInsert_Click(object sender, EventArgs e)
        {
            string sql = "INSERT INTO Employees VALUES(@FName,@LName,@Age,@CountryID)";
            SqlParameter[] parametersList = new SqlParameter[]{
                new SqlParameter ("@FName",txtFName.Value),
                new SqlParameter ("@LName",txtLName.Value),
                new SqlParameter ("@Age",txtAge.Value),
                new SqlParameter ("@CountryID",ddlCouontries.Value),
           };

            if (SqlHelper.ExecuteNonQuery(sql, CommandType.Text, parametersList))
                lbMsg.Text = "Inserted successfully";
            else
                lbMsg.Text = "Error";
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            string sql = "UPDATE Employees SET FirstName=@FName,LastName=@LName,Age=@Age,CountryID=@CountryID WHERE EmpID =@ID";
            SqlParameter[] parametersList = new SqlParameter[]{
                new SqlParameter ("@ID",txtEmpID.Value),
                new SqlParameter ("@FName",txtFName.Value),
                new SqlParameter ("@LName",txtLName.Value),
                new SqlParameter ("@Age",txtAge.Value),
                new SqlParameter ("@CountryID",ddlCouontries.Value),
           };

            if (SqlHelper.ExecuteNonQuery(sql, CommandType.Text, parametersList))
                lbMsg.Text = "Updated successfully";
            else
                lbMsg.Text = "Error";
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            string sql = @"DELETE FROM Employees WHERE EmpID =@ID";

            SqlParameter[] parametersList = new SqlParameter[]{
                    new SqlParameter ("@ID",txtEmpID.Value),
                };

            if (SqlHelper.ExecuteNonQuery(sql, CommandType.Text, parametersList))
                lbMsg.Text = "Deleted successfully";
            else
                lbMsg.Text = "Error";
        }

        //'Execute two SQL statements Insert and update, which all SQL statements in a single transaction, rolling back if an error has occurred
        protected void btnExecuteTransaction_Click(object sender, EventArgs e)
        {
            ArrayList listOfSQLs = new ArrayList();
            List<SqlParameter[]> listOfParamerters = new List<SqlParameter[]>();

            string sql1 = "INSERT INTO Employees VALUES(@FName,@LName,@Age,@CountryID)";
            SqlParameter[] parameters1 = new SqlParameter[]{
                new SqlParameter ("@FName","Test F Name"),
                new SqlParameter ("@LName","Test L Name"),
                new SqlParameter ("@Age",25),
                new SqlParameter ("@CountryID",1),
           };

            listOfSQLs.Add(sql1);
            listOfParamerters.Add(parameters1);


            string sql2 = "UPDATE Employees SET FirstName=@FName,LastName=@LName,Age=@Age,CountryID=@CountryID WHERE EmpID =@ID";
            SqlParameter[] parameters2 = new SqlParameter[]{
                new SqlParameter ("@ID",4),
                new SqlParameter ("@FName","New F Name"),
                new SqlParameter ("@LName","New L Name"),
                new SqlParameter ("@Age",30),
                new SqlParameter ("@CountryID",2),
           };


            listOfSQLs.Add(sql2);
            listOfParamerters.Add(parameters2);

            if (SqlHelper.ExecuteTransaction(listOfSQLs, listOfParamerters))
                lbMsg.Text = "All SQL statements executed successfully";
            else
                lbMsg.Text = "Error";
           
        }

        //Execute two select queries, and returns employees and country tables
        protected void btnReturnDS_Click(object sender, EventArgs e)
        {
            // You can also use a stored procedure instead  of two queries in one string variable
            string sql = "SELECT * FROM Employees; SELECT * FROM Country";
            DataSet ds = SqlHelper.ExecuteQueryDS(sql, CommandType.Text, null);
            GridViewEmp.DataSource = ds.Tables[0];
            GridViewEmp.DataBind();

            GridViewCountry.DataSource = ds.Tables[1];
            GridViewCountry.DataBind();
        }
    }
}