using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HTTR
{
    public class HttrRequest
    {
        public List<HttrTag> TagsToRetrive { get; set; }
        public bool RetrieveAttributes { get; set; }
        public string XPath
        {
            get
            {
                string result = "";
                for (int i = 0; i < TagsToRetrive.Count; i++)
                {
                    result+=TagsToRetrive[i].XPath+" | ";
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
        public HttrRequest(HttrTag tagToRetrive, bool retrieveAttributes = true)
        {
            TagsToRetrive = new List<HttrTag>();
            TagsToRetrive.Add(tagToRetrive);
            RetrieveAttributes = retrieveAttributes;
        }
        /// <summary>
        /// Basic constructor
        /// </summary>
        /// <param name="tagsToRetrive">list of html tags you want to retrieve</param>
        /// <param name="retrieveAttributes">bool indicating if the outputed json will contain attributes</param>
        public HttrRequest(List<HttrTag> tagsToRetrive, bool retrieveAttributes=true)
        {
            TagsToRetrive = new List<HttrTag>();
            TagsToRetrive.AddRange(tagsToRetrive);
            RetrieveAttributes= retrieveAttributes;
        }
    }
}
