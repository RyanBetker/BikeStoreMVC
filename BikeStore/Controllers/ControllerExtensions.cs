using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BikeStore.Controllers
{
    public static class ControllerExtensions
    {
        public static string GetUserName(this Controller controller)
        {
            return String.IsNullOrWhiteSpace(controller.User.Identity.Name) ? "Admin" : controller.User.Identity.Name;
        }
    }
}