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
  public partial class SQLServerOptions : DBOptions
  {
    public bool Insert(PollworkerElectionXref pex)
    {
      bool ret = false;
      if (ssCmd == null) ssCmd = new SqlCommand("", _con);

      try
      {
        ssCmd.Parameters.Clear();
        ssCmd.CommandText = "[ElectionAuto].[spPollworkerElectionXrefIns]";
        ssCmd.CommandType = CommandType.StoredProcedure;
        SqlParameter xref = new SqlParameter("@PollworkerElectionXrefID", SqlDbType.Int);
        xref.Direction = ParameterDirection.Output;
        ssCmd.Parameters.Add(xref);

        ssCmd.Parameters.AddWithValue("@PollworkerID", pex.PollworkerID);
        ssCmd.Parameters.AddWithValue("@ElectionID", pex.ElectionID);
        ssCmd.Parameters.AddWithValue("@PollworkerTypeID", pex.PollworkerTypeID);
        // Task is always null, we didn't have those prior to the new version
        ssCmd.Parameters.AddWithValue("@ExtraPay", pex.ExtraPay);
        
        ssCmd.ExecuteNonQuery();
        int xrefId = (int)ssCmd.Parameters["@PollworkerElectionXrefID"].Value;
        if (xrefId < 0)
        {
          MessageBox.Show("Oops!");
          ret = false;
        }
        else
          ret = true;
      }
      catch (Exception e)
      {
        MessageBox.Show(e.Message + "\n\n" + pex.ToString());
        ret = false;
      }

      return ret;
    }
  }
}
