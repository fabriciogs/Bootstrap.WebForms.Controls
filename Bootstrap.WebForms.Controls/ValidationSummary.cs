using System;
using System.ComponentModel;
using System.Security.Permissions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Bootstrap.WebForms.Controls
{
    [AspNetHostingPermission(SecurityAction.Demand, Level = AspNetHostingPermissionLevel.Minimal)]
    [ToolboxData(@"<{0}:ValidationSummary runat=""server"" />")]
    public class ValidationSummary : System.Web.UI.WebControls.ValidationSummary
    {
        /// <summary>Initializes a new instance of the <see cref="ValidationSummary" /> class.</summary>
        public ValidationSummary()
        {
            this.CssClass = "alert alert-danger";
            this.DisplayMode = ValidationSummaryDisplayMode.BulletList;
            this.ShowMessageBox = false;
            this.ShowSummary = true;
            this.HeaderText = "O(s) seguinte(s) erro(s) foram encontrado(s):";
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

        protected override void Render(HtmlTextWriter writer)
        {
            this.AddCssClass(this.CssClass);
            this.CssClass = this.cssClass;
            base.Render(writer);
        }
    }
}