using System;
using System.Collections.Generic;
using System.Text;

namespace EntidadesNurvey
{
    public class ElementSurvey
    {
        public string type { get; set; }
        public string isRequired { get; set; }
        public string name { get; set; }
        public string title { get; set; }
        public List<string> choices { get; set; }
        public List<string> columns { get; set; }
        public List<string> rows { get; set; }
        public string html { get; set; }
    }
}
