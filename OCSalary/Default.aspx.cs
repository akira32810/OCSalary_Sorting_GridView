using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;


namespace OCSalary
{
    public partial class Default : System.Web.UI.Page
    {
        private string myConn = ConfigurationManager.ConnectionStrings["HLDBConnection"].ConnectionString;
        private int countRows = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                bindData();
            }

        }

        protected DataTable bindData()
        {
   
            DataTable result = new DataTable();
            try
            {
                using (SqlConnection conn = new SqlConnection(myConn))
                {
                    if (conn.State == ConnectionState.Closed)
                    {
                        conn.Open();
                    }
                    using (SqlCommand cmd = new SqlCommand("select [Last name], [First name], Department, Title,  CAST(ROUND([Annual salary], 2) AS NUMERIC(12,2)) as [Annual Salary ($)]  from dbo.OCSalary", conn))
                    {
                        cmd.CommandType = CommandType.Text;

                      
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(result);
                            da.Dispose();

                            foreach (DataRow row in result.Rows)
                            {
                                countRows++;
                            }
  
                        }


                    }
                    
                }
                lblcountrows.Text = "<div class='numofRows'>There are a total of <b>" + countRows + "</b> rows found.</div>";
                Session["TaskTable"] = result;
                GVOCSalary.DataSource = Session["TaskTable"];
                GVOCSalary.DataBind();
            }

            catch (Exception e)
            {
                Response.Write(e);
            }
            return result;
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            DataTable result = new DataTable();
            try
            {
                using (SqlConnection conn = new SqlConnection(myConn))
                {

                    if (conn.State == ConnectionState.Closed)
                    {
                        conn.Open();
                    }
                    using (SqlCommand cmd = new SqlCommand("select [Last name], [First name], Department, Title, CAST(ROUND([Annual salary], 2) AS NUMERIC(12,2)) as [Annual Salary ($)] from dbo.OCSalary where [last name] like '%'+@LastName+'%' and [first name] like '%'+@FirstName+'%' and Department  like '%'+@Department+'%' order by [Last Name]  ", conn))
                    {
                        cmd.CommandType = CommandType.Text;

                        cmd.Parameters.Add("@FirstName", SqlDbType.VarChar).Value = txtFName.Text.Trim();
                        cmd.Parameters.Add("@LastName", SqlDbType.VarChar).Value = txtLName.Text.Trim();
                        cmd.Parameters.Add("@Department", SqlDbType.VarChar).Value = txtDepartment.Text.Trim();


                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(result);
                            da.Dispose();

                           // GVOCSalary.DataSource = result;
                            //GVOCSalary.DataBind();
                            foreach (DataRow row in result.Rows)
                            {
                                countRows++;
                            }
                        }


                    
                    }
                }
                lblcountrows.Text = "<div class='numofRows'>There are a total of <b>" + countRows + "</b> rows found.</div>";
                Session["TaskTable"] = result;
                GVOCSalary.DataSource = Session["TaskTable"];
                GVOCSalary.DataBind();

            }

            catch
            {
                Response.Write("Can't connect to Server, try again later");
            }
        }

        protected void GVOCSalary_Sorting(object sender, GridViewSortEventArgs e)
        {
            DataTable dt = Session["TaskTable"] as DataTable;

            if (dt != null)
            {
                dt.DefaultView.Sort = e.SortExpression + " " + GetSortDirection(e.SortExpression);
                GVOCSalary.DataSource = Session["TaskTable"];
                GVOCSalary.DataBind();

            }

        }

        //sort direction
        private string GetSortDirection(string column)
        {

            // By default, set the sort direction to ascending.
            string sortDirection = "ASC";

            // Retrieve the last column that was sorted.
            string sortExpression = ViewState["SortExpression"] as string;

            if (sortExpression != null)
            {
                // Check if the same column is being sorted.
                // Otherwise, the default value can be returned.
                if (sortExpression == column)
                {
                    string lastDirection = ViewState["SortDirection"] as string;
                    if ((lastDirection != null) && (lastDirection == "ASC"))
                    {
                        sortDirection = "DESC";
                    }
                }
            }

            // Save new values in ViewState.
            ViewState["SortDirection"] = sortDirection;
            ViewState["SortExpression"] = column;

            return sortDirection;
        }

        protected void GVOCSalary_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            DataTable dt = Session["TaskTable"] as DataTable;

            if (dt != null) {
            GVOCSalary.PageIndex = e.NewPageIndex;
            GVOCSalary.DataSource = Session["TaskTable"];
            GVOCSalary.DataBind();
            }

        }

        

    }
}