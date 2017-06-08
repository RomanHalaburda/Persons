﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using Persons.Models;
using System.Text;

namespace Persons
{
    public partial class Grades : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (TextBox1.Text == "")
            {
                PlaceHolder1.Controls.Add(new Literal { Text = "Введите минимальное значение отметки." });
            }
            else
            {
                string minGrade = TextBox1.Text;
                DataTable dt = this.GetData(minGrade);
                StringBuilder html = new StringBuilder();
                html.Append("<table border = '1'>");

                html.Append("<tr>");
                foreach (DataColumn column in dt.Columns)
                {
                    html.Append("<th>");
                    html.Append(column.ColumnName);
                    html.Append("</th>");
                }
                html.Append("</tr>");

                foreach (DataRow row in dt.Rows)
                {
                    html.Append("<tr>");
                    foreach (DataColumn column in dt.Columns)
                    {
                        html.Append("<td>");
                        html.Append(row[column.ColumnName]);
                        html.Append("</td>");
                    }
                    html.Append("</tr>");
                }

                html.Append("</table>");

                PlaceHolder1.Controls.Add(new Literal { Text = html.ToString() });
            }
        }

        private DataTable GetData(string _mg)
        {
            string constr = ConfigurationManager.ConnectionStrings["PassportConnection"].ConnectionString;
            string query = "select students.first_name, students.last_name, students.email, student_grade.test, student_grade.grade from students join student_grade on students.id = student_grade.student_id where grade > " + _mg + ";";
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        cmd.Connection = con;
                        sda.SelectCommand = cmd;
                        using (DataTable dt = new DataTable())
                        {
                            sda.Fill(dt);
                            return dt;
                        }
                    }
                }
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {

        }
    }
}