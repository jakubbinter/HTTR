using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HTTR
{
    public class HttrRequest
    {
        public List<HttrSelector> Selectors { get; set; }
        public bool RetrieveAttributes { get; set; }
        public string XPath
        {
            get
            {
                string result = "";
                for (int i = 0; i < Selectors.Count; i++)
                {
                    result+= Selectors[i].XPath+" | ";
                }
                result = result.Remove(result.Length - 3);
                return result;
            }
        }
        /// <summary>
        /// Basic constructor
        /// </summary>
        /// <param name="tagToRetrive">html tag you want to retrieve</param>
        /// <param name="retrieveAttributes">bool indicating if the outputed json will contain attributes</param>
        public HttrRequest(HttrSelector selector, bool retrieveAttributes = true)
        {
            Selectors = new List<HttrSelector>();
            Selectors.Add(selector);
            RetrieveAttributes = retrieveAttributes;
        }
        /// <summary>
        /// Basic constructor
        /// </summary>
        /// <param name="tagsToRetrive">list of html tags you want to retrieve</param>
        /// <param name="retrieveAttributes">bool indicating if the outputed json will contain attributes</param>
        public HttrRequest(List<HttrSelector> selectors, bool retrieveAttributes=true)
        {
            Selectors = new List<HttrSelector>();
            Selectors.AddRange(selectors);
            RetrieveAttributes= retrieveAttributes;
        }
    }
}
