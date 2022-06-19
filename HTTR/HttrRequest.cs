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
        public string XPath
        {
            get
            {
                string result = "";
                for (int i = 0; i < TagsToRetrive.Count; i++)
                {
                    if (TagsToRetrive[i].IsInnerTag)
                        continue;
                    result+=TagsToRetrive[i].XPath+" | ";
                }
                result = result.Remove(result.Length - 3);
                return result;
            }
        }
        /// <summary>
        /// Basic constructor
        /// </summary>
        /// <param name="TagToRetrive">html tag you want to retrieve, if not set than retrievs from al tags</param>
        /// <param name="AtributeToRetrieve">html atribute which you want to get, if not set it will retrieve value</param>
        public HttrRequest(HttrTag tagToRetrive)
        {
            TagsToRetrive = new List<HttrTag>();
            TagsToRetrive.Add(tagToRetrive);
        }
        public HttrRequest(List<HttrTag> tagsToRetrive)
        {
            TagsToRetrive = new List<HttrTag>();
            TagsToRetrive.AddRange(tagsToRetrive);
        }

    }
}
