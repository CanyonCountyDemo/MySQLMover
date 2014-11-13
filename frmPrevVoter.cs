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
  public partial class frmPrevVoter : Form
  {
    private DataTable myTable;
    private DataTable ssTable;
    private MySqlConnection myCon;
    private MySqlCommand myCmd;
    private SqlConnection ssCon;
    private SqlCommand ssCmd;

    public frmPrevVoter()
    {
      InitializeComponent();
    }

    private void frmPrecinct_Load(object sender, EventArgs e)
    {
      // Get Table Names
      myCon = new MySqlConnection(Properties.Settings.Default.MySQL);
      myCmd = new MySqlCommand("", myCon);
      //myCmd.CommandText = "SELECT COUNTYPRECINCT,ADDRESS1,ADDRESS2,ADDRESS3,CITYSTATE,CITY,LEGDIST FROM Elections.PRECINCT";
      myCmd.CommandText = @"
SELECT * FROM Elections.PREVSOSVOTER
";
      myCmd.CommandType = System.Data.CommandType.Text;
      
      myCon.Open();
      MySqlDataReader myReader = myCmd.ExecuteReader();
      myTable = new DataTable();
      myTable.Load(myReader);
      myCon.Close();
      myGrid.DataSource = myTable;

      ssCon = new SqlConnection(Properties.Settings.Default.SQLServer);
      ssCmd = new SqlCommand("", ssCon);
      ssCmd.CommandText = "select VoterID, PrecinctNo, DistrictNo from ElectionExt.PrevVoterInfo";
      ssCmd.CommandType = CommandType.Text;
      ssCon.Open();
      SqlDataReader ssReader = ssCmd.ExecuteReader();
      ssTable = new DataTable();
      ssTable.Load(ssReader);
      //ssCon.Close();
      ssGrid.DataSource = ssTable;
    }

    private void btnImport_Click(object sender, EventArgs e)
    {
      // Import Data - show it...
      //insert into ElectionExt.PrevVoterInfo (VoterID, PrecinctNo, DistrictNo) values ('', '', '')
      const string basesql = "insert into ElectionExt.PrevVoterInfo (VoterID, PrecinctNo, DistrictNo) values ('{0}', '{1}', '{2}')";
      foreach (DataRow row in myTable.Rows)
      {
        string sql = String.Format(basesql, row["VOTERID"].ToString(),
          row["COUNTYPRECINCT"].ToString(), row["LEGISLATIVEDISTRICT"].ToString()
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

      ssCmd.CommandText = "select VoterID, PrecinctNo, DistrictNo from ElectionExt.PrevVoterInfo";
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
