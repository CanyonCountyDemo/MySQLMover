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
  public class DataMover
  {
    private MySQLOptions _my;
    private SQLServerOptions _sql;

    public DataMover(MySQLOptions mySql, SQLServerOptions sqlServer)
    {
      _my = mySql;
      _sql = sqlServer;
    }

    public MySQLOptions MySQL
    {
      get { return _my; }
    }

    public SQLServerOptions SQLServer
    {
      get { return _sql; }
    }

    public void Load()
    {
      _my.PerformCommand();
      _sql.PerformCommand();
    }

    public void Insert(InsertRow insert)
    {
      if (insert != null)
      {
        bool cancel = false;
        foreach (DataRow row in _my.Table.Rows)
        {
          insert(row, ref cancel);
          if (cancel) break;
        }
      }
      else
      {
        throw new Exception("No delegate method to call");
      }
    }

    //public void Refresh(RefreshOptions which = RefreshOptions.Both)
    //{
    //  switch(which)
    //  {
    //    case RefreshOptions.MySQL: _my.Refresh(); break;
    //    case RefreshOptions.SQLServer: _sql.Refresh(); break;
    //    case RefreshOptions.Both: _my.Refresh(); _sql.Refresh(); break;
    //  }
    //}
  }
}
