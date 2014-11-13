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
  public partial class frmElection : Form
  {
    private DataMover _mover;
    private int _count = 0;
    private Dictionary<string, int> _vendor;
    private Dictionary<string, int> _position;

    public frmElection()
    {
      InitializeComponent();
    }

    private void frmElection_Load(object sender, EventArgs e)
    {
      Cursor.Current = Cursors.WaitCursor;
      
      MySQLOptions my = new MySQLOptions(
        connectionString: Properties.Settings.Default.MySQL,
        commandText: "SELECT * FROM Elections.COMPENSATION", 
        grid:myGrid);

      SQLServerOptions sql = new SQLServerOptions(
        connectionString: Properties.Settings.Default.SQLServer.Replace("ccsql08dev", "ccsql12dev1"),
        commandText: "select * from Election.PollworkerElectionXref",
        grid: ssGrid);

      _mover = new DataMover(my, sql);
      _mover.Load();

      Cursor.Current = Cursors.Default;
    }

    private void btnImport_Click(object sender, EventArgs e)
    {
      progress.Maximum = myGrid.Rows.Count;
      _count = 0;
      progress.Visible = true;

      Cursor.Current = Cursors.WaitCursor;

      DataTable vendor = _mover.SQLServer.GetTableFromSQL("select pollworkerid, vendorno from Election.Pollworker where DefaultPollworkerTypeID is not null");
      _vendor = new Dictionary<string, int>();
      //_vendor.Add("", ""); // to handle "blank" precincts...
      foreach (DataRow row in vendor.Rows)
      {
        _vendor.Add(row["VendorNo"].ToString(), Int32.Parse(row["PollworkerID"].ToString()));
      }

      DataTable position = _mover.SQLServer.GetTable("Election.PollworkerType");
      _position = new Dictionary<string,int>();
      foreach(DataRow row in position.Rows)
      {
        _position.Add(row["PollworkerTypeCode"].ToString(), Int32.Parse(row["PollworkerTypeID"].ToString()));
      }

      _mover.Insert(InsertRow);
      _mover.SQLServer.Refresh();
      
      Cursor.Current = Cursors.Default;
      MessageBox.Show("Done");
      progress.Visible = false;
    }

    private int GetPollworkerID(string vendorNo)
    {
      return _vendor[vendorNo];
    }

    private int GetPositionID(string position)
    {
      return _position[position];
    }

    private void InsertRow(DataRow row, ref bool cancel)
    {
      // This is called once per row in the MySQL table
      _count++;
      progress.Value = _count;
      Application.DoEvents();

      PollworkerElectionXref pex = new PollworkerElectionXref();
      pex.PollworkerID = GetPollworkerID(row["VendorID"].ToString());
      pex.PollworkerTypeID = GetPositionID(row["PositionCode"].ToString());
      pex.ElectionID = 2;
      pex.ExtraPay = Decimal.Parse(row["ExtraPay"].ToString());

      cancel = !_mover.SQLServer.Insert(pex);
    }
  }
}
