using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetBy.Common.Request
{
    public class FilterQuery
    {
        public FilterQuery()
        {
            Id = "";
            Code = "";
            User = "";
            Event = "";
            DynamicQuery = "";
            QueryParams = "";

            SortProp = "";
            SortDirection = "";
        }

        public FilterQuery(string id, string code)
        {
            Id = id;
            Code = code;
        }

        public string Id { get; set; }
        public string Code { get; set; }
        public string Event { get; set; } = "";
        public string User { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool ExactValue { get; set; } = false;

        /// <summary>
        /// Dynamic linq query
        /// </summary>
        public string DynamicQuery { get; set; }
        /// <summary>
        /// Query params
        /// </summary>
        public string QueryParams { get; set; }
        /// <summary>
        /// Sorting properties
        /// </summary>
        public string SortProp { get; set; }
        /// <summary>
        /// Specifies ascending or descending sorting direction
        /// </summary>
        public string SortDirection { get; set; }
    }
}
