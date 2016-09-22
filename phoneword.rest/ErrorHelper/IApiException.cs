using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace phoneword.rest.ErrorHelper
{
    public interface IApiException
    {
        /// <summary>
        /// ErrorCode
        /// </summary>
        int ErrorCode { get; set; }
        /// <summary>
        /// ErrorDescription
        /// </summary>
        string ErrorDescription { get; set; }
        /// <summary>
        /// HttpStatus
        /// </summary>
        HttpStatusCode HttpStatus { get; set; }
        /// <summary>
        /// ReasonPhrase
        /// </summary>
        string ReasonPhrase { get; set; }
    }
}
