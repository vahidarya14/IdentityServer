using System;
using System.Globalization;

namespace IdentityServer
{
    public static class Exttension
    {
        public static string Show(this bool val,bool reverse=false)
            => !reverse  ?
            val ? "<span class='badge bg-success'>√</span>" : "<span class='badge bg-danger'>X</span>":
            val ? "<span class='badge bg-danger'>X</span>":"<span class='badge bg-success'>√</span>";

        public static string ToPersian(this DateTime? val, bool includeHour = false, string defaultString = "--")
            => (!val.HasValue) ? defaultString : val.Value.ToPersian(includeHour);

        public static string ToPersian(this DateTime val, bool includeHour = false)
        {
            var pc = new PersianCalendar();
            return
                includeHour ?
                $"{pc.GetYear(val)}/{pc.GetMonth(val)}/{pc.GetDayOfMonth(val)} {val.Hour}:{val.Minute}" :
                $"{pc.GetYear(val)}/{pc.GetMonth(val)}/{pc.GetDayOfMonth(val)}";
        }

    }

}
