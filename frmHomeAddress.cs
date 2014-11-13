using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Data.SqlClient;

namespace MySQLMover
{
  public partial class frmHomeAddress : Form
  {
    private DataTable myTable;
    private DataTable ssTable;
    private Dictionary<string, int> HomeNames;

    private MySqlConnection myCon;
    private MySqlCommand myCmd;
    private SqlConnection ssCon;
    private SqlCommand ssCmd;

    public frmHomeAddress()
    {
      InitializeComponent();
    }

    private string GetCity(string cityCode)
    {
      string ret = String.Empty;
      
      if (cityCode == "CAL") ret = "CALDWELL";
      if (cityCode == "GRE") ret = "GREANLEAF";
      if (cityCode == "PAR") ret = "PARMA";
      if (cityCode == "NAM") ret = "NAMPA";

      return ret;
    }

    private int HomeID(string HomeName)
    {
      return HomeNames[HomeName];
    }

    private void frmHomeAddress_Load(object sender, EventArgs e)
    {
      // Get Table Names
      myCon = new MySqlConnection(Properties.Settings.Default.MySQL);
      myCmd = new MySqlCommand("", myCon);
      myCmd.CommandText = @"SELECT HOMEID, STREETNUMBER, STREETNAME, CITYCODE FROM Elections.NURSINGHOMEADDRESS";
      myCmd.CommandType = System.Data.CommandType.Text;
      
      myCon.Open();
      MySqlDataReader myReader = myCmd.ExecuteReader();
      myTable = new DataTable();
      myTable.Load(myReader);
      myCon.Close();
      myGrid.DataSource = myTable;

      ssCon = new SqlConnection(Properties.Settings.Default.SQLServer);
      ssCmd = new SqlCommand("", ssCon);
      ssCmd.CommandText = "select * from ElectionExt.NursingHomeAddress";
      ssCmd.CommandType = CommandType.Text;
      ssCon.Open();
      SqlDataReader ssReader = ssCmd.ExecuteReader();
      ssTable = new DataTable();
      ssTable.Load(ssReader);
      //ssCon.Close();
      ssGrid.DataSource = ssTable;

      SqlCommand cmd = new SqlCommand("", ssCon);
      cmd.CommandText = "select * from ElectionExt.NursingHomeAddress";
      cmd.CommandType = CommandType.Text;
      SqlDataReader reader = cmd.ExecuteReader();
      DataTable table = new DataTable();
      table.Load(reader);

      HomeNames = new Dictionary<string, int>();
      foreach(DataRow row in table.Rows)
      {
        int id = Int32.Parse(row["NursingHomeId"].ToString());
        HomeNames.Add(row["NursingHomeKey"].ToString(), id);
      }
    }

    private void btnImport_Click(object sender, EventArgs e)
    {
      // Import Data - show it...
      const string basesql = "insert into ElectionExt.NursingHomeAddress (NursingHomeStreetNumber, NursingHomeStreetName, NursingHomeCity, NursingHomeID) values ('{0}', '{1}', '{2}', {3})";
      foreach (DataRow row in myTable.Rows)
      {
        string sql = String.Format(basesql, 
          row["STREETNUMBER"].ToString(),
          row["STREETNAME"].ToString(),
          GetCity(row["CITYCODE"].ToString()),
          HomeID(row["HOMEID"].ToString()).ToString()
          );

        ssCmd.CommandText = sql;
        try
        {
          int count = ssCmd.ExecuteNonQuery();
          if (count != 1)
          {
            MessageBox.Show("Error Inserting record: " + sql);
            break;
          }
        }
        catch { }
      }

      ssCmd.CommandText = "select NursingHomeName from ElectionExt.NursingHomeAddress";
      ssCmd.CommandType = CommandType.Text;
      //ssCon.Open();
      SqlDataReader ssReader = ssCmd.ExecuteReader();
      ssTable = new DataTable();
      ssTable.Load(ssReader);
      ssCon.Close();
      ssGrid.DataSource = ssTable;

    }
  }
}
