using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MySQLMover
{
  public class FieldMapping
  {
    private string _fieldTo;
    private string _fieldFrom;

    public FieldMapping(string to, string from)
    {
      _fieldTo = to;
      _fieldFrom = from;
    }

    public string To
    {
      get { return _fieldTo; }
    }

    public string From
    {
      get { return _fieldFrom; }
    }
  }
}
