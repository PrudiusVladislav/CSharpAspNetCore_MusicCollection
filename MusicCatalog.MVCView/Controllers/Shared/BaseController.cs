using Microsoft.AspNetCore.Mvc;

namespace MusicCatalog.MVCView.Controllers.Shared;

public class BaseController : Controller
{
    protected static DateTime TimeOfLastDbUpdate { get; set; } = DateTime.Now;
}