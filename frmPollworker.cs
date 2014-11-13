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
  public partial class frmPollworker : Form
  {
    private DataMover _mover;
    private DataTable _types;
    private Dictionary<string, string> _precincts;

    private int? _phoneType;
    private int _count = 0;

    public frmPollworker()
    {
      InitializeComponent();
    }

    private void frmPollworker_Load(object sender, EventArgs e)
    {
      Cursor.Current = Cursors.WaitCursor;
      
      MySQLOptions my = new MySQLOptions(
        connectionString: Properties.Settings.Default.MySQL,
        commandText: "SELECT VendorID,WorkPrecinct,PositionCode,WorkerName,Address,CityState,Phone,WorkerInactive,Notes,Address2 FROM Elections.WORKERS order by vendorid -- where address like '%#%' and positioncode <> ''",//where vendorid <= 64202335 
        grid:myGrid);

      SQLServerOptions sql = new SQLServerOptions(
        connectionString: Properties.Settings.Default.SQLServer.Replace("ccsql08dev", "ccsql12prod1"),
        commandText: "select VendorID,WorkPrecinct,PositionCode,WorkerName,Address,CityState,Phone,WorkerInactive,Notes,Address2 from ElectionExt.Workers order by vendorid",//where vendorid <= 64202335
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

      // Get Lookup Values
      _types = _mover.SQLServer.GetTable("Election.PollworkerType");
      DataTable phone = _mover.SQLServer.GetTable("Election.PollworkerPhoneType");
      foreach (DataRow row in phone.Rows)
      {
        if (row["PollworkerPhoneTypeName"].ToString() == "_Imported")
        {
          _phoneType = Int32.Parse(row["PollworkerPhoneTypeID"].ToString());
          break;
        }
      }

      DataTable precincts = _mover.SQLServer.GetTable("Election.Precinct");
      _precincts = new Dictionary<string, string>();
      _precincts.Add("", ""); // to handle "blank" precincts...
      foreach (DataRow row in precincts.Rows)
      {
        // Crazyness - The old data has a two digit precinct (without legislative district
        // We need to add the precinct from the new system (with leg dist)
        // To the dictionary, with the id, for quick fast lookup
        _precincts.Add(row["PrecinctNo"].ToString().Split('-')[0], row["PrecinctID"].ToString());
      }

      _mover.Insert(InsertRow);
      _mover.SQLServer.Refresh();
      
      Cursor.Current = Cursors.Default;
      MessageBox.Show("Done");
      progress.Visible = false;
    }

    private int? GetTypeIDForCode(string code)
    {
      int? ret = null;
      if (code != String.Empty)
      {
        foreach (DataRow row in _types.Rows)
        {
          if (row["PollworkerTypeCode"].ToString() == code)
          {
            ret = Int32.Parse(row["PollworkerTypeID"].ToString());
            break;
          }
        }
      }
      return ret;
    }

    private string GetPrecinctId(string precinct)
    {
      //string ret = String.Empty;
      //if (_precincts.ContainsKey(precinct))
      //  ret = _precincts[precinct];
      //return ret;

      // By adding the blank precinct, we no longer need to do the check
      return _precincts[precinct];
    }

    private void InsertRow(DataRow row, ref bool cancel)
    {
      // This is called once per row in the MySQL table
      _count++;
      progress.Value = _count;
      Application.DoEvents();

      int? id = GetTypeIDForCode(row["POSITIONCODE"].ToString());

      Pollworker pw = new Pollworker();
      pw.VendorID = row["VENDORID"].ToString();
      pw.WorkPrecinct = GetPrecinctId(row["WorkPrecinct"].ToString());
      pw.PositionCode = id; //??

      pw.WorkerName = row["WorkerName"].ToString();
      pw.ResAddress = row["Address"].ToString();
      pw.ResCityState = row["CityState"].ToString();
      pw.Phone = row["Phone"].ToString();
      pw.PhoneType = _phoneType;
      pw.Notes = row["Notes"].ToString();
      pw.MailAddress = row["Address2"].ToString();

      cancel = !_mover.SQLServer.Insert(pw);
    }

    private DataRow FindWorker(string vendorID)
    {
      DataRow ret = null;
      int count = 0;
      foreach (DataRow row in _mover.SQLServer.Table.Rows)
      {
        count++;
        if (row["VendorID"].ToString().Equals(vendorID))
        {
          ret = row;
          break;
        }
      }

      if (ret == null) throw new Exception("'" + vendorID + "',");
      return ret;
    }

    private string DataRowToString(DataRow row, int count)
    {
      string ret = String.Empty;
      for (int i = 0; i < count; i++)
      {
        ret += row[i].ToString().Trim();
      }
      return ret;
    }

    private void GetDifference(DataRow first, DataRow second, string keyCol, string displayCol, List<string>colNames)
    {
      string diff = String.Empty;
      string diff2 = String.Empty;

      //for (int i = 0; i < count; i++)
      //{
      //  string fs = first[i].ToString();
      //  string ss = second[i].ToString();
      //  if (!fs.Equals(ss))
      //  {
      //    diff += String.Format("{0}", second[i].ToString());
      //  }
      //}
      bool b = false;

      foreach (string col in colNames)
      {
        string f = first[col].ToString();
        string s = second[col].ToString();
        if (!f.Equals(s))
        {
          _count++;
          if (!b) diff += "update ElectionExt.Workers set ";
          diff += col + "='" + f.Replace("'", "''") + "',";
          diff2 += "\t" + col + " Old: '" + f + "' New:'" + s + "'" + Environment.NewLine;
          b = true;
        }
      }

      if (diff != String.Empty)
      {
        diff = diff.Substring(0, diff.Length - 1);
        textBox1.Text += diff + " where " + keyCol + " = '" + first[keyCol] + "'" + Environment.NewLine;
        //MessageBox.Show(diff + " where " + keyCol + " = '" + first[keyCol] + "'" );
        textBox2.Text += first[displayCol] + " - " + first[keyCol] + Environment.NewLine + diff2 + Environment.NewLine + Environment.NewLine;
      }
    }

    private void btnUpdate_Click(object sender, EventArgs e)
    {
      _count = 0;
      foreach (DataRow myRow in _mover.MySQL.Table.Rows)
      {
        try
        {
          DataRow ssRow = FindWorker(myRow["vendorid"].ToString());
          List<string> cols = new List<string>();
          foreach (DataColumn col in _mover.MySQL.Table.Columns)
            cols.Add(col.ColumnName);
          // Now compare the values
          string my = DataRowToString(myRow, _mover.MySQL.Table.Columns.Count);
          string ss = DataRowToString(ssRow, _mover.SQLServer.Table.Columns.Count);
          if (!my.Equals(ss))
          {
            GetDifference(myRow, ssRow, "VendorID", "WorkerName", cols);
          }
        }
        catch (Exception ex)
        {
          MessageBox.Show(ex.Message);
        }
      }
      MessageBox.Show("Done - " + _count.ToString());
    }
  }
}
