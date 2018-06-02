using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingKids.Models.Responses
{
    public class ErrorResponse : BaseResponse
    {
        public List<string> Errors { get; set; }

        public ErrorResponse(string errMsg)
        {
            Errors = new List<string>();

            Errors.Add(errMsg);
            this.IsSuccessful = false;
        }

        public ErrorResponse(IEnumerable<string> errMsg)
        {
            Errors = new List<string>();

            Errors.AddRange(errMsg);
            this.IsSuccessful = false;
        }
    }
}
