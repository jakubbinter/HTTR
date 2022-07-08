using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HTTR
{
    public class HttrTag:HttrSelector
    {
        public override string XPath 
        {
            get
            {
                string result = "//" + TagToRetrive;
                if (conditions == null || conditions.Count == 0)
                    return result;
                result += "[";
                for (int i = 0; i < conditions.Count; i++)
                {
                    result += $"@{conditions[i]} and ";
                }
                result = result.Remove(result.Length - 5);
                result += "]";
                return result;
            }
        }
        List<string> conditions = new List<string>();
        public string TagToRetrive { get; private set; }

        /// <summary>
        /// Method that adds condition 
        /// </summary>
        /// <param name="atribute">name of html atribute you want to set contition for</param>
        /// <param name="eaquals">value you want the atribute to have</param>
        public void AddCondition(string atribute, string eaquals)
        {
            string condition = $"{atribute}='{eaquals}'";
            conditions.Add(condition);
        }
        /// <summary>
        /// Basic Constructor
        /// </summary>
        /// <param name="tagToRetrive">tag this object will retrieve</param>
        public HttrTag(string tagToRetrive)
        {
            TagToRetrive = tagToRetrive;
        }
    }
}
