using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace OfficeOpenXml.Sparkline
{
    /// <summary>
    /// Represents a single sparkline within the sparkline group
    /// </summary>
    public class ExcelSparkline : XmlHelper
    {
        internal ExcelSparkline(XmlNamespaceManager nsm, XmlNode topNode) : base(nsm, topNode)
        {
            SchemaNodeOrder = new string[] { "f", "sqref" };
        }   
        const string _fPath = "xm:f";
        /// <summary>
        /// The datarange
        /// </summary>
        internal ExcelAddressBase RangeAddress
        {
            /*get
            {
                return new ExcelAddressBase(GetXmlNodeString(_fPath));
            }
            internal*/
            set
            {
                //SetXmlNodeString(_fPath, value.FullAddress);

                if (value is ExcelNamedRange)
                    SetXmlNodeString(_fPath, (value as ExcelNamedRange).Name);
                else
                    SetXmlNodeString(_fPath, value.FullAddress);
            }
        }

        /// <summary>
        /// Get the data range address.
        /// </summary>
        /// <param name="namedRangeCol">workbook or worksheet Names</param>
        /// <returns></returns>
        internal ExcelAddressBase GetRangeAddress(ExcelNamedRangeCollection namedRangeCol)
        {
            string addrOrName = GetXmlNodeString(_fPath);
            if (namedRangeCol.ContainsKey(addrOrName)) //ExcelNamedRange
                return namedRangeCol[addrOrName];
            else
                return new ExcelAddressBase(addrOrName);
        }

        const string _sqrefPath = "xm:sqref";
        /// <summary>
        /// Location of the sparkline
        /// </summary>
        public ExcelCellAddress Cell
        {
            get
            {
                return new ExcelCellAddress(GetXmlNodeString(_sqrefPath));
            }
            internal set
            {
                SetXmlNodeString("xm:sqref", value.Address);
            }
        }
        public override string ToString()
        {
            //return Cell.Address + ", " +RangeAddress.Address;
            return Cell.Address + ", " + GetXmlNodeString(_fPath);
        }
    }
}
