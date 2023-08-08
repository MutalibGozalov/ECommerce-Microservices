using ECommerce.Shared.Dtos;
using Microsoft.AspNetCore.Mvc;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;


namespace ECommerce.Shared.ControllerBases
{
    public class CustomBaseController : ControllerBase
    {
        public IActionResult CreateActionResultInstance<T>(Response<T> response)// where T : class
        {
            return new ObjectResult(response)
            {
                StatusCode = response.StatusCode

            };
        }
    }
}