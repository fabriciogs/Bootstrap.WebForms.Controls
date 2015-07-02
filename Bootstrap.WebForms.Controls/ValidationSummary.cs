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
            this.CssClass = this.sCssClass;
            base.Render(writer);
        }
    }
}