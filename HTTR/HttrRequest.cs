using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HTTR
{
    public class HttrRequest
    {
        public string TagToRetrive { get; set; }
        //public string AtributeToRetrive { get; set; }
        public string Conditions
        {
            get
            {
                if(conditions==null || conditions.Count == 0)
                    return string.Empty;
                string result = "[";
                for (int i = 0; i < conditions.Count; i++)
                {
                    result += $"@{conditions[i]} and ";
                }
                result=result.Remove(result.Length - 5);
                result += "]";
                return result;
            }
        }
        List<string> conditions = new List<string>();
        /// <summary>
        /// Basic constructor
        /// </summary>
        /// <param name="TagToRetrive">html tag you want to retrieve, if not set than retrievs from al tags</param>
        /// <param name="AtributeToRetrieve">html atribute which you want to get, if not set it will retrieve value</param>
        public HttrRequest(string tagToRetrive="*"/*,string atributeToRetrieve="value"*/)
        {
            TagToRetrive = "//"+tagToRetrive;
            //AtributeToRetrive = atributeToRetrieve;
        }
        /// <summary>
        /// <summary>
        /// Method that adds condition 
        /// </summary>
        /// <param name="atribute">name of html atribute you want to set contition for</param>
        /// <param name="eaquals">value you want the atribute to have</param>
        public void AddCondition(string atribute,string eaquals)
        {
            string condition = $"{atribute}='{eaquals}'";
            conditions.Add(condition);
        }
    }
}
