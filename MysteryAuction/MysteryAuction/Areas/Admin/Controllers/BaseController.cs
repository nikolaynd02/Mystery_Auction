using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MysteryAuction.Core.Constants;

namespace MysteryAuction.Areas.Admin.Controllers
{
    [Authorize(Roles = RoleConstants.Admin)]
    [Area("Admin")]
    public class BaseController : Controller
    {
       
    }
}
