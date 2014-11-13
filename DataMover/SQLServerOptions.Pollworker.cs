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
    public bool Insert(Pollworker pw)
    {
      bool ret = false;
      try
      {
        // Poll worker
        if (ssCmd == null) ssCmd = new SqlCommand("", _con);

        ssCmd.Parameters.Clear();
        ssCmd.CommandText = "[ElectionAuto].[spPollworkerIns]";
        ssCmd.CommandType = CommandType.StoredProcedure;
        SqlParameter pollworker = new SqlParameter("@PollworkerID", SqlDbType.Int);
        pollworker.Direction = ParameterDirection.Output;
        ssCmd.Parameters.Add(pollworker);
        ssCmd.Parameters.AddWithValue("@FirstName", pw.FirstName);
        ssCmd.Parameters.AddWithValue("@MiddleName", pw.MiddleName);
        ssCmd.Parameters.AddWithValue("@LastName", pw.LastName);

        if (!String.IsNullOrEmpty(pw.WorkPrecinct))
          ssCmd.Parameters.AddWithValue("@WorkingPrecinctID", pw.WorkPrecinct);
        ssCmd.Parameters.AddWithValue("@VendorNo", pw.VendorID);

        ssCmd.Parameters.AddWithValue("@ResidenceStreetNumber", pw.ResStreetNumber);
        ssCmd.Parameters.AddWithValue("@ResidenceStreetName", pw.ResStreetName);
        ssCmd.Parameters.AddWithValue("@ResidenceUnit", pw.ResUnit);
        ssCmd.Parameters.AddWithValue("@ResidenceCity", pw.ResCity);
        ssCmd.Parameters.AddWithValue("@ResidenceState", pw.ResState);
        ssCmd.Parameters.AddWithValue("@ResidenceZipCode", pw.ResZip);
        ssCmd.Parameters.AddWithValue("@ResidenceZip4Code", pw.ResZip4);

        ssCmd.Parameters.AddWithValue("@MailingStreetNumber", pw.MailStreetNumber);
        ssCmd.Parameters.AddWithValue("@MailingStreetName", pw.MailStreetName);
        ssCmd.Parameters.AddWithValue("@MailingUnit", pw.MailUnit);
        ssCmd.Parameters.AddWithValue("@MailingCity", pw.MailCity);
        ssCmd.Parameters.AddWithValue("@MailingState", pw.MailState);
        ssCmd.Parameters.AddWithValue("@MailingZipCode", pw.MailZip);
        ssCmd.Parameters.AddWithValue("@MailingZip4Code", pw.MailZip4);

        ssCmd.Parameters.AddWithValue("@DefaultPollworkerTypeID", pw.PositionCode);

        ssCmd.Parameters.AddWithValue("@InsertDateTime", DateTime.Now);
        ssCmd.Parameters.AddWithValue("@InsertBy", "Import");
        ssCmd.Parameters.AddWithValue("@ModifiedDateTime", DateTime.Now);
        ssCmd.Parameters.AddWithValue("@ModifiedBy", "Import");

        ssCmd.Parameters.AddWithValue("@Notes", pw.Notes);
        /*
         * 	@InsertDateTime             datetime,
	@InsertByUsername           varchar(50),
	@ModifiedDateTime           datetime,
	@ModifiedByUsername         varchar(50),
         */
        ssCmd.Parameters.AddWithValue("@InsertDateTime", DateTime.Now.ToShortDateString());
        ssCmd.Parameters.AddWithValue("", "Import");

        ssCmd.ExecuteNonQuery();
        int pwId = (int)ssCmd.Parameters["@PollworkerID"].Value;

        // Poll worker phone
        ssCmd.Parameters.Clear();
        ssCmd.CommandText = "[ElectionAuto].[spPollworkerPhoneIns]";
        ssCmd.CommandType = CommandType.StoredProcedure;
        SqlParameter pollingPlace = new SqlParameter("@PollworkerPhoneID", SqlDbType.Int);
        pollingPlace.Direction = ParameterDirection.Output;
        ssCmd.Parameters.Add(pollingPlace);
        ssCmd.Parameters.AddWithValue("@PollworkerPhoneNo", pw.Phone);
        ssCmd.Parameters.AddWithValue("@PollworkerPhoneTypeID", pw.PhoneType);
        ssCmd.Parameters.AddWithValue("@PollworkerID", pwId);
        ssCmd.ExecuteNonQuery();
        int phoneId = (int)ssCmd.Parameters["@PollworkerPhoneID"].Value;
        if (phoneId < 0)
        {
          MessageBox.Show("Oops!");
          ret = false;
        }
        else
          ret = true;
      }
      catch (Exception e)
      {
        MessageBox.Show(e.Message + "\n\n" + pw.ToString());
        ret = false;
      }

      return ret;
    }

  }
}
