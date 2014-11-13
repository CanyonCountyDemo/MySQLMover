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
  public partial class frmPrecinct : Form
  {
    private DataTable myTable;
    private DataTable ssTable;
    private MySqlConnection myCon;
    private MySqlCommand myCmd;
    private SqlConnection ssCon;
    private SqlCommand ssCmd;

    public frmPrecinct()
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
SELECT 
  concat(countyprecinct, '-', legdist) as Precinct,
  Address1 as Name,
  Address2 as Location,
  Address3 as Description,
  CityState
FROM Elections.PRECINCT
order by precinct
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
      ssCmd.CommandText = "select * from vwPrecinctInfo";
      ssCmd.CommandType = CommandType.Text;
      ssCon.Open();
      SqlDataReader ssReader = ssCmd.ExecuteReader();
      ssTable = new DataTable();
      ssTable.Load(ssReader);
      //ssCon.Close();
      ssGrid.DataSource = ssTable;
    }

    private string Clean(string str)
    {
      return str.Replace("'", "");
    }

    private void btnImport_Click(object sender, EventArgs e)
    {
      // Import Data - show it...
      //insert into Election.Precinct(CountyPrecinct, Address1, Address2, Address3, CityState, City, LegDist) values ('1', 'KIRKPATRICK CHURCH',	'305 1/2 BATES AVE',	'ANDERSON HALL',	'PARMA, ID 83660','',	'9')
      //const string basesql = "insert into Election.Precinct(CountyPrecinct,Address1,Address2,Address3,CityState,City,LegDist) values ('{0}','{1}','{2}','{3}','{4}','{5}','{6}')";
      //foreach (DataRow row in myTable.Rows)
      //{
      //  string sql = String.Format(basesql, Clean(row[0].ToString()), Clean(row[1].ToString()), Clean(row[2].ToString()),
      //    Clean(row[3].ToString()), Clean(row[4].ToString()), Clean(row[5].ToString()), Clean(row[6].ToString())
      //    );
        
        
      //  ssCmd.CommandText = sql;
      //  try
      //  {
      //    int count = ssCmd.ExecuteNonQuery();
      //    if (count != 1)
      //    {
      //      MessageBox.Show("Error Inserting record: " + sql);
      //      break;
      //    }
      //  }
      //  catch { }
      //}

      foreach (DataRow row in myTable.Rows)
      {
        //_da.Command.SetInsert("[ElectionAuto].[spPrecinctIns]");
        //_da.Command.Parameters.Clear();
        //_da.Command.Parameters.AddOutput("@PrecinctID", SqlDbType.Int);
        //_da.Command.Parameters.AddInput("@PrecinctNo", CCData.ToNullable(precinct.PrecinctNo));
        //precinct.PrecinctID = (int)_da.Command.Result.Parameters["@PrecinctID"].Value;

        ssCmd.Parameters.Clear();
        ssCmd.CommandText = "[ElectionAuto].[spPrecinctIns]";
        ssCmd.CommandType = CommandType.StoredProcedure;
        SqlParameter precinct = new SqlParameter("@PrecinctID", SqlDbType.Int);
        precinct.Direction = ParameterDirection.Output;
        ssCmd.Parameters.Add(precinct);
        ssCmd.Parameters.AddWithValue("@PrecinctNo", row["Precinct"].ToString());
        ssCmd.ExecuteNonQuery();
        int precinctId = (int)ssCmd.Parameters["@PrecinctID"].Value;

        //_da.Command.SetInsert("[ElectionAuto].[spPollingPlaceIns]");
        ////_da.Command.Parameters.Clear();
        //_da.Command.Parameters.AddOutput("@PollingPlaceID", SqlDbType.Int);
        //_da.Command.Parameters.AddInput("@PollingPlaceName", CCData.ToNullable(pollingPlace.PollingPlaceName));
        //_da.Command.Parameters.AddInput("@LocationName", CCData.ToNullable(pollingPlace.LocationName));
        //_da.Command.Parameters.AddInput("@LocationDesc", CCData.ToNullable(pollingPlace.LocationDesc));
        //_da.Command.Parameters.AddInput("@CityState", CCData.ToNullable(pollingPlace.CityState));
        //_da.Command.Parameters.AddInput("@PrecinctID", CCData.ToNullable(pollingPlace.Precinct.PrecinctID));

        ssCmd.Parameters.Clear();
        ssCmd.CommandText = "[ElectionAuto].[spPollingPlaceIns]";
        ssCmd.CommandType = CommandType.StoredProcedure;
        SqlParameter pollingPlace = new SqlParameter("@PollingPlaceID", SqlDbType.Int);
        pollingPlace.Direction = ParameterDirection.Output;
        ssCmd.Parameters.Add(pollingPlace);
        ssCmd.Parameters.AddWithValue("@PollingPlaceName", row["Name"].ToString());
        ssCmd.Parameters.AddWithValue("@LocationName", row["Location"].ToString());
        ssCmd.Parameters.AddWithValue("@LocationDesc", row["Description"].ToString());
        ssCmd.Parameters.AddWithValue("@CityState", row["CityState"].ToString());
        ssCmd.Parameters.AddWithValue("@PrecinctID", precinctId);
        ssCmd.ExecuteNonQuery();
        int pollingPlaceId = (int)ssCmd.Parameters["@PollingPlaceID"].Value;
        if (pollingPlaceId < 0)
          MessageBox.Show("Oops!");
      }


      ssCmd.CommandText = "select * from vwPrecinctInfo";
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
