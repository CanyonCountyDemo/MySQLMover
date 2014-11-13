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
  public partial class SQLServerOptions: DBOptions
  {
    private DataTable _table;
    private SqlConnection _con;
    private SqlCommand _cmd;
    private SqlCommand ssCmd;
    private DataGridView _grid;
    private string _connectionString;
    private string _commandText;

    public SQLServerOptions(string connectionString, string commandText, DataGridView grid)
    {
      //_con = con;
      _connectionString = connectionString;
      _commandText = commandText;
      _grid = grid;
    }

    public void PerformCommand()
    {
      //Properties.Settings.Default.SQLServer
      //"select * from vwPrecinctInfo"
      _con = new SqlConnection(_connectionString);
      _cmd = new SqlCommand("", _con);
      _cmd.CommandText = _commandText;
      _cmd.CommandType = CommandType.Text;
      _con.Open();

      Refresh();
    }

    public override void Refresh()
    {
      SqlDataReader reader = _cmd.ExecuteReader();
      _table = new DataTable();
      _table.Load(reader);
      _grid.DataSource = _table;
    }

    public override DataTable GetTable(string table)
    {
      return GetTableFromSQL("select * from " + table);
    }

    public override DataTable GetTableFromSQL(string sql)
    {
      DataTable ret = new DataTable();
      SqlCommand cmd = new SqlCommand("", _con);
      cmd.CommandText = sql;
      cmd.CommandType = CommandType.Text;
      SqlDataReader reader = cmd.ExecuteReader();
      ret.Load(reader);
      return ret;
    }

    public new DataGridView Grid
    {
      get { return _grid; }
    }

    public new DataTable Table
    {
      get 
      { 
        if (_table == null) 
          _table = new DataTable();
        return _table;
      }
    }
  }
}
