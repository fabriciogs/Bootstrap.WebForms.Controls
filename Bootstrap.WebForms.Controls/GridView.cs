using System.Security.Permissions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Bootstrap.WebForms.Controls
{
    [AspNetHostingPermission(SecurityAction.Demand, Level = AspNetHostingPermissionLevel.Minimal)]
    [ToolboxData(@"<{0}:GridView runat=""server"" />")]
    public class GridView : System.Web.UI.WebControls.GridView
    {
        /// <summary>Initializes a new instance of the <see cref="GridView" /> class.</summary>
        public GridView()
        {
            this.AutoGenerateColumns = false;
            this.PageSize = 25;
            this.GridLines = GridLines.None;
            this.CssClass = "table table-striped table-hover table-condensed";
            this.PagerStyle.CssClass = "bs-pagination";
        }

        #region CssClass method

        string cssClass = "";

        /// <summary>
        /// Adds the CSS class.
        /// </summary>
        /// <param name="cssClass">The CSS class.</param>
        private void AddCssClass(string cssClass)
        {
            if (string.IsNullOrEmpty(this.cssClass))
            {
                this.cssClass = cssClass;
            }
            else
            {
                this.cssClass += " " + cssClass;
            }
        }

        #endregion

        protected override void AddAttributesToRender(HtmlTextWriter writer)
        {
            this.AddCssClass(this.CssClass);
        }
    }
}