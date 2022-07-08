using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HTTR
{
    public class HttrXPathSelector:HttrSelector
    {
        public override string XPath { get { return xpath; } }
        private string xpath { get; set; }
        public HttrXPathSelector(string Xpath)
        {
            xpath = Xpath;
        }
    }
}
