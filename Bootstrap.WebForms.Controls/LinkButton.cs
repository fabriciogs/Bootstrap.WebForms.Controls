using System.Web;
using System.Web.UI;
using System.ComponentModel;
using System.Security.Permissions;

namespace Bootstrap.WebForms.Controls
{
    [AspNetHostingPermission(SecurityAction.Demand, Level = AspNetHostingPermissionLevel.Minimal)]
    [ToolboxData(@"<{0}:LinkButton runat=""server"" />")]
    [DefaultProperty("Text")]
    public class LinkButton : System.Web.UI.WebControls.LinkButton
    {
        #region Properties

        [Category("Appearance")]
        [DefaultValue(Sizes.Default)]
        public Sizes Size { get; set; }

        [Category("Appearance")]
        [DefaultValue(ButtonTypes.Default)]
        public ButtonTypes ButtonType { get; set; }

        [DefaultValue(false)]
        public bool Disabled { get; set; }

        [DefaultValue(false)]
        public bool BlockLevel { get; set; }

        [DefaultValue("")]
        public string AlertMessage { get; set; }

        [Category("Appearance")]
        [DefaultValue("")]
        public string LoadingText { get; set; }

        [Category("Appearance")]
        [DefaultValue("")]
        public string TargetModalID
        {
            get { return (string)ViewState["TargetModalID"]; }
            set { ViewState["TargetModalID"] = value; }
        }

        #endregion

        #region CssClass method

        string cssClass = "";

        /// <summary>Adds the CSS class.</summary>
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
            //writer.AddAttribute(HtmlTextWriterAttribute.Class, GetCssClass());
            this.AddCssClass(this.CssClass);
            this.AddCssClass(GetCssClass());

            writer.AddAttribute("role", "button");

            if (this.Disabled)
                writer.AddAttribute(HtmlTextWriterAttribute.Disabled, string.Empty);

            if (!string.IsNullOrEmpty(this.AlertMessage))
                writer.AddAttribute(HtmlTextWriterAttribute.Onclick, string.Format(@"return confirm('{0}')", this.AlertMessage));

            if (!string.IsNullOrEmpty(this.TargetModalID))
            {
                writer.AddAttribute("data-toggle", "modal");
                writer.AddAttribute("data-target", string.Format("#{0}", this.TargetModalID));
            }

            if (!string.IsNullOrEmpty(this.LoadingText))
                writer.AddAttribute("data-loading-text", this.LoadingText);

            writer.AddAttribute(HtmlTextWriterAttribute.Name, this.UniqueID);
            if (!string.IsNullOrEmpty(this.cssClass))
                writer.AddAttribute(HtmlTextWriterAttribute.Class, this.cssClass);

            // Call the base's AddAttributesToRender method 
            base.AddAttributesToRender(writer);
        }

        private string GetCssClass()
        {
            var css = "btn";

            switch (this.ButtonType)
            {
                case ButtonTypes.Primary:
                    css += " btn-primary";
                    break;
                case ButtonTypes.Success:
                    css += " btn-success";
                    break;
                case ButtonTypes.Info:
                    css += " btn-info";
                    break;
                case ButtonTypes.Warning:
                    css += " btn-warning";
                    break;
                case ButtonTypes.Danger:
                    css += " btn-danger";
                    break;
                case ButtonTypes.Link:
                    css += " btn-link";
                    break;
                default:
                    css += " btn-default";
                    break;
            }

            switch (this.Size)
            {
                case Sizes.Large:
                    css += " btn-lg";
                    break;
                case Sizes.Small:
                    css += " btn-sm";
                    break;
                case Sizes.ExtraSmall:
                    css += " btn-xs";
                    break;
                default:
                    break;
            }

            if (this.BlockLevel)
                css += " btn-block";

            return css;
        }

        #region Enums

        public enum Sizes
        {
            [Description("Default button")]
            Default,

            [Description("Large button")]
            Large,

            [Description("Small button")]
            Small,

            [Description("Extra small button")]
            ExtraSmall,
        }

        #endregion
    }
}