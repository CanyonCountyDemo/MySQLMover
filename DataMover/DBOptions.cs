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
  public abstract class DBOptions
  {
    public DataGridView Grid;
    public DataTable Table;
    public abstract void Refresh();
    public abstract DataTable GetTable(string table);
    public abstract DataTable GetTableFromSQL(string sql);
    //public abstract int InsertSQL(string sql);
  }
}
