using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MySQLMover
{
  public class PollworkerElectionXref
  {
    public int PollworkerElectionXrefID { get; set; }
    public int PollworkerID { get; set; }
    public int ElectionID { get; set; }
    public int PollworkerTypeID { get; set; }
    public int PollworkerTaskID { get; set; }
    public decimal ExtraPay { get; set; }

    public override string ToString()
    {
      return PollworkerID + " " + ElectionID + " " + PollworkerTypeID;
    }
  }
}
