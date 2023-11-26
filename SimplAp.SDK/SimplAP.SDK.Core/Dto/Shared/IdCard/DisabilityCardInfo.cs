using System;
using ECoding.SimpleAPI.Dto.AI.TextParser.ParserStandards;

namespace ECoding.SimpleAPI.Dto.AI.TextParser;
public class DisabilityCardInfo : IParsedInfo, IFullName, IDateOfBirth, IIssuedDate, IIssuedBy, IIdNo, IAddress
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string IssuedBy { get; set; }
    public DateTime? IssuedDate { get; set; }
    public DateTime? DateOfBirth { get; set; }
    public string IdNumber { get; set; }
    public string Address { get; set; }
    public string StreetName { get; set; }
    public string City { get; set; }
    public string StreetNumber { get; set; }
    public string PostalCode { get; set; }
    public string CountryCode { get; set; }
}
