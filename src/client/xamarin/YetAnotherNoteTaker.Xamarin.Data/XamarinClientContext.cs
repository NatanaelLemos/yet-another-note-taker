using System;
using Microsoft.EntityFrameworkCore;

namespace YetAnotherNoteTaker.Xamarin.Data
{
    public class XamarinClientContext : DbContext
    {
        public static string DatabasePath = "notetaker.db";

        public XamarinClientContext()
        {
            Database.Migrate();
        }
    }
}
