using System.Web;
using System.Web.UI;
using System.ComponentModel;
using System.Security.Permissions;

namespace Bootstrap.WebForms.Controls
{
    [AspNetHostingPermission(SecurityAction.Demand, Level = AspNetHostingPermissionLevel.Minimal)]
    public class HyperLink : System.Web.UI.WebControls.HyperLink
    {
        #region Properties

        [Category("Appearance")]
        [DefaultValue(Sizes.Default)]
        public Sizes Size { get; set; }

        [Category("Appearance")]
        [DefaultValue(Options.Default)]
        public Options Option { get; set; }

        [DefaultValue(false)]
        public bool Disabled { get; set; }

        [DefaultValue(false)]
        public bool BlockLevel { get; set; }

        [DefaultValue("")]
        public string AlertMessage { get; set; }

        #endregion

        protected override void AddAttributesToRender(HtmlTextWriter writer)
        {
            writer.AddAttribute(HtmlTextWriterAttribute.Class, GetCssClass());

            if (this.Disabled)
                writer.AddAttribute(HtmlTextWriterAttribute.Disabled, string.Empty);

            if (!string.IsNullOrEmpty(this.AlertMessage))
                writer.AddAttribute(HtmlTextWriterAttribute.Onclick, string.Format(@"return confirm('{0}')", this.AlertMessage));

            // Call the base's AddAttributesToRender method 
            base.AddAttributesToRender(writer);
        }

        private string GetCssClass()
        {
            var css = "btn";

            switch (this.Option)
            {
                case Options.Primary:
                    css += " btn-primary";
                    break;
                case Options.Success:
                    css += " btn-success";
                    break;
                case Options.Info:
                    css += " btn-info";
                    break;
                case Options.Warning:
                    css += " btn-warning";
                    break;
                case Options.Danger:
                    css += " btn-danger";
                    break;
                case Options.Link:
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

        [Description("Use any of the available button classes to quickly create a styled button.")]
        public enum Options
        {
            [Description("Standard button")]
            Default,

            [Description("Provides extra visual weight and identifies the primary action in a set of buttons")]
            Primary,

            [Description("Indicates a successful or positive action")]
            Success,

            [Description("Contextual button for informational alert messages")]
            Info,

            [Description("Indicates caution should be taken with this action")]
            Warning,

            [Description("Indicates a dangerous or potentially negative action")]
            Danger,

            [Description("Deemphasize a button by making it look like a link while maintaining button behavior")]
            Link,
        }

        #endregion
    }
}