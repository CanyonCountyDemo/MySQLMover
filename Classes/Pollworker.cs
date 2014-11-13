using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MySQLMover
{
  public class Pollworker
  {
    private string _resStreetNumber;
    private string _resStreetName;
    private string _resUnit;
    private string _mailStreetNumber;
    private string _mailStreetName;
    private string _mailUnit;


    public string VendorID { get; set; }
    public string WorkPrecinct { get; set; }
    public int? PositionCode { get; set; }

    public string WorkerName { set { ParseName(value); } }

    public string ResAddress { set { ParseResAddress(value); } }
    public string ResCityState { set { ParseCityState(value); } }
    public string Phone { get; set; }
    public int? PhoneType { get; set; }
    public string MailAddress { set { ParseMailAddress(value); } }
    public string Notes { get; set; }
    
    // Generated properties
    public string LastName { get; private set; }
    public string FirstName { get; private set; }
    public string MiddleName { get; private set; }

    public string ResStreetNumber { get { return _resStreetNumber; } }
    public string ResStreetName { get { return _resStreetName; } }
    public string ResUnit { get { return _resUnit; } }
    public string ResCity { get; private set; }
    public string ResState { get; private set; }
    public string ResZip { get; private set; }
    public string ResZip4 { get; private set; }

    public string MailStreetNumber { get { return _mailStreetNumber; } }
    public string MailStreetName { get { return _mailStreetName; } }
    public string MailUnit { get { return _mailUnit; } }
    public string MailCity { get; private set; }
    public string MailState { get; private set; }
    public string MailZip { get; private set; }
    public string MailZip4 { get; private set; }

    private void ParseName(string name)
    {
      // Name is last, first [<space> middle]
      string[] parts = name.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
      switch (parts.Count())
      {
        case 1: LastName = parts[0]; FirstName = ""; MiddleName = ""; break;
        case 2: LastName = parts[0]; FirstName = parts[1]; MiddleName = ""; break;
        case 3: LastName = parts[0]; FirstName = parts[1]; MiddleName = parts[2]; break;
        default: LastName = ""; FirstName = ""; MiddleName = ""; break;
      }
      LastName = LastName.Replace(",", "");
    }

    private void ParseCityState(string cityState)
    {
      // Set mail to the same, since we don't have it... lame
      // City state is city, state zip
      string[] parts = cityState.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
      switch (parts.Count())
      {
        case 1: ResCity = parts[0]; ResState = ""; ResZip = ""; break;
        case 2: ResCity = parts[0]; ResState = parts[1]; ResZip = ""; break;
        case 3: ResCity = parts[0]; ResState = parts[1]; ResZip = parts[2]; break;
        default: ResCity = ""; ResState = ""; ResZip = ""; break;
      }
      ResCity = ResCity.Replace(",", "");
      
      // Don't forget to check the Zips!
      if (ResZip.Length > 5)
      {
        string[] zip = ResZip.Split(new char[] { '-' }, StringSplitOptions.RemoveEmptyEntries);
        ResZip = zip[0];
        ResZip4 = zip[1];
      }

      MailCity = ResCity;
      MailState = ResState;
      MailZip = ResZip;
      MailZip4 = ResZip4;
    }

    private void ParseAddress(string address, out string StreetNumber, out string StreetName, out string Unit)
    {
      // address is number, street name [#unit]
      StreetNumber = String.Empty;
      StreetName = String.Empty;
      Unit = String.Empty;

      string[] parts = address.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
      if (parts.Length > 1)
      {
        StreetNumber = parts[0];
        
        StreetName = address.Replace(StreetNumber + " ", "");

        int idx = StreetName.IndexOf('#');
        if (idx > 0)
        {
          Unit = StreetName.Substring(idx);
          StreetName = StreetName.Replace(Unit, "").Trim();
          Unit = Unit.Replace("#", "").Trim();
        }
      }
    }

    private void ParseResAddress(string address)
    {
      // Dumb as hell...
      // You can't use an accessor as an out parameter? why? it's a string type
      ParseAddress(address, out _resStreetNumber, out _resStreetName, out _resUnit);
    }

    private void ParseMailAddress(string address)
    {
      ParseAddress(address, out _mailStreetNumber, out _mailStreetName, out _mailUnit);
    }

    public override string ToString()
    {
      return VendorID + " " + FirstName + " " + LastName;
    }
  }
}
