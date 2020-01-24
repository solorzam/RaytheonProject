using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
//using RaytheonProject;

namespace RaytheonProject.Views
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //txtName.Text = "";
            SqlConnection con = null;

            try
            {
                string CS = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
                using (con = new SqlConnection(CS))
                //using (SqlConnection con = new SqlConnection("data source=DELL-TABLET\\MYPROJECTS; database=Raytheon; integrated security=SSPI"))
                {
                    SqlCommand cmd;

                    if (!IsPostBack)
                    {
                        cmd = new SqlCommand("select distinct Region from [salaries-by-region] order by Region", con);
                        con.Open();

                        DropDownList1.DataSource = cmd.ExecuteReader();
                        DropDownList1.DataTextField = "Region";
                        DropDownList1.DataValueField = "Region";
                        DropDownList1.DataBind();
                    }
                    else
                    {
                        string salaries = @"select r.School_Name,  [Region], [School_Type] , 
                                t.[Mid_Career_Median_Salary],
                                t.[Mid_Career_10th_Percentile_Salary],
                                t.[Mid_Career_25th_Percentile_Salary],
                                t.[Mid_Career_75th_Percentile_Salary],
                                t.[Mid_Career_90th_Percentile_Salary]
                                from    [dbo].[salaries-by-region] r, 
                                        [dbo].[salaries-by-college-type] t
                                where r.School_Name = t.School_Name and Region ='";

                        salaries += DropDownList1.SelectedValue;
                        salaries += "' order by r.School_Name";

                        cmd = new SqlCommand(salaries, con);
                        con.Open();
                        SqlDataReader drSalary = cmd.ExecuteReader();
                        GridView1.DataSource = drSalary;
                        GridView1.DataBind();
                    }

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Database Connection/Execution problem");
            }
            finally
            {
                con.Close();
            }
        }

        protected void TextBox1_TextChanged(object sender, EventArgs e)
        {

        }


        protected void btnSayHello_Click1(object sender, EventArgs e)
        {
            ltrMessage.Text = DropDownList1.SelectedValue;

        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}