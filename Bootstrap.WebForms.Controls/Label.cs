using System.ComponentModel;
using System.Security.Permissions;
using System.Web;
using System.Web.UI;

namespace Bootstrap.WebForms.Controls
{
    [AspNetHostingPermission(SecurityAction.Demand, Level = AspNetHostingPermissionLevel.Minimal)]
    [ToolboxData(@"<{0}:Label runat=""server"" />")]
    [DefaultProperty("Text")]
    public class Label : System.Web.UI.WebControls.Label
    {
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

        public Label()
        {
            this.LabelType = LabelTypes.Default;
        }

        [Category("Appearance")]
        [DefaultValue(LabelTypes.Default)]
        public LabelTypes LabelType
        {
            get { return (LabelTypes)ViewState["LabelType"]; }
            set { ViewState["LabelType"] = value; }
        }

        //[Bindable(true)]
        //[Category("Appearance")]
        //[DefaultValue("")]
        //[Localizable(true)]
        //public string Text
        //{
        //    get
        //    {
        //        var s = (string)ViewState["Text"];
        //        return (s == null) ? string.Empty : s;
        //    }
        //    set
        //    {
        //        ViewState["Text"] = value;
        //    }
        //}

        protected override void Render(HtmlTextWriter writer)
        {
            this.AddCssClass(this.CssClass);
            this.AddCssClass("label");

            switch (this.LabelType)
            {
                case LabelTypes.Primary:
                    this.AddCssClass("label-primary");
                    break;
                case LabelTypes.Success:
                    this.AddCssClass("label-success");
                    break;
                case LabelTypes.Info:
                    this.AddCssClass("label-info");
                    break;
                case LabelTypes.Warning:
                    this.AddCssClass("label-warning");
                    break;
                case LabelTypes.Danger:
                    this.AddCssClass("label-danger");
                    break;
                case LabelTypes.Default:
                default:
                    this.AddCssClass("label-default");
                    break;
            }

            this.CssClass = this.cssClass;

            base.Render(writer);
        }

        protected override void RenderContents(HtmlTextWriter output)
        {
            output.Write(this.Text);
            this.RenderChildren(output);
        }
    }
}