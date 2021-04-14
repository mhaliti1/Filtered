using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Filtered.Models
{
    public class Project
    {
        public int Row { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Developer { get; set; }
        public string Status { get; set; }
        public string Business { get; set; }
        public DateTime Date { get; set; }

        public string ShortName
        {
            get
            {

                if (!string.IsNullOrEmpty(Name))
                {
                    Regex initials = new Regex(@"(\b[a-zA-Z])[a-zA-Z]* ?");
                    string init = initials.Replace(Name, "$1");
                    return init;
                }
                else
                {
                    return "";
                }

            }
        }
    }
}
