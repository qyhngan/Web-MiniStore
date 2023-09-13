using Microsoft.AspNetCore.Mvc.Rendering;
using System.Globalization;

namespace MiniStoreRazorPage.Pages.ManageWorkShiftEmployee
{
    public static class ManageNavPages
    {
        public static string Index => "Index";
        public static string Assign => "Assign";
        public static string HistoryShift => "HistoryShift";
        public static string FutureShift => "FutureShift";
        public static string RequestList => "RequestList";

        public static string IndexNavClass(ViewContext viewContext) => PageNavClass(viewContext, Index);
        public static string AssignNavClass(ViewContext viewContext) => PageNavClass(viewContext, Assign);
        public static string HistoryShiftNavClass(ViewContext viewContext) => PageNavClass(viewContext, HistoryShift);
        public static string FutureShiftNavClass(ViewContext viewContext) => PageNavClass(viewContext, FutureShift);
        public static string RequestListNavClass(ViewContext viewContext) => PageNavClass(viewContext, RequestList);

        private static string PageNavClass(ViewContext viewContext, string page)
        {
            var activePage = viewContext.ViewData["ActivePage"] as string
                ?? System.IO.Path.GetFileNameWithoutExtension(viewContext.ActionDescriptor.DisplayName);
            return string.Equals(activePage, page, System.StringComparison.OrdinalIgnoreCase) ? "active" : null;
        }
    }
}
