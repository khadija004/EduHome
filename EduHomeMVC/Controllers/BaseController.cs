using Microsoft.AspNetCore.Mvc;
using static EduHome.Utilities.Enums.Enum;

namespace EduHome.Controllers
{
    public abstract class BaseController : Controller
    {
        public void Alert(string message, NotificationType notificationType)
        {
            var msg = "swal('" + notificationType.ToString().ToUpper() + "', '" + message + "','" + notificationType + "')" + "";
            TempData["notification"] = msg;
        }
    }
}
