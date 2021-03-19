using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Afisha.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Afisha
{
    [Authorize]
    public class BaseController : Controller
    {
        protected readonly ILogger<HomeController> _logger;
        protected readonly AfishaContext db;
        protected int UserId
        {
            get
            {
                var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
                return userId;
            }
        }
        public BaseController(ILogger<HomeController> logger, AfishaContext db)
        {
            _logger = logger;
            this.db = db;
        }
    }
}
