using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace MySQLMover
{
  public delegate void InsertRow(DataRow row, ref bool cancel);
}
