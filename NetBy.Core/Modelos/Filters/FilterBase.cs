using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetBy.Core.Modelos.Filters
{
    public class FilterBase
    {
        public FilterBase()
        {
            Id = "";
            Code = "";
            Event = "";
            DynamicQuery = "";
            QueryParams = "";
            SortProp = "";
            SortDirection = "";
        }

        public FilterBase(string id, string code, string @event, string user)
        {
            Id = id;
            Code = code;
            Event = @event;
            User = user;

            if (Id == null)
                Id = "";

            if (Code == null)
                Code = "";

            if (Event == null)
                Event = "";

            if (User == null)
                User = "";
        }



        public string Id { get; set; }
        public string Code { get; set; }
        public string Event { get; set; }
        public string User { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string DynamicQuery { get; set; }
        public string QueryParams { get; set; }

        public string SortProp { get; set; }
        public string SortDirection { get; set; }

        public bool ExactValue { get; set; } = false;
    }
}
