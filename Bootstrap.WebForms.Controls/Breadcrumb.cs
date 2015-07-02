using System.Web;
using System.Web.UI;
using System.ComponentModel;
using System.Security.Permissions;

namespace Bootstrap.WebForms.Controls
{
    [AspNetHostingPermission(SecurityAction.Demand, Level = AspNetHostingPermissionLevel.Minimal)]
    [ToolboxData(@"<{0}:Breadcrumb runat=""server""></{0}:Breadcrumb>")]
    [DefaultProperty("Text")]
    public class Breadcrumb : System.Web.UI.WebControls.BulletedList
    {
        #region CssClass method

        string sCssClass = "";

        /// <summary>
        /// Adds the CSS class.
        /// </summary>
        /// <param name="cssClass">The CSS class.</param>
        private void AddCssClass(string cssClass)
        {
            if (string.IsNullOrEmpty(this.sCssClass))
            {
                this.sCssClass = cssClass;
            }
            else
            {
                this.sCssClass += " " + cssClass;
            }
        }

        #endregion

        protected override void Render(HtmlTextWriter writer)
        {
            this.AddCssClass(this.CssClass);
            this.AddCssClass("breadcrumb");

            this.CssClass = this.sCssClass;

            base.Render(writer);
        }

        protected override void RenderContents(HtmlTextWriter output)
        {
            output.Write(Text);
            base.RenderContents(output);
        }
    }
}