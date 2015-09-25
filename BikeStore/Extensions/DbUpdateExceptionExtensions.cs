using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;

namespace BikeStore.Extensions
{
    public static class DbUpdateExceptionExtensions
    {
        public static bool IsUniqueConstraintViolation(this DbUpdateException ex)
        {
            return 
                ex.InnerException != null 
                && ex.InnerException.InnerException != null
                && ex.InnerException.InnerException.Message.ToLower().Contains("duplicate");
        }
    }
}