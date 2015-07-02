using System.Web;
using System.Web.UI;
using System.ComponentModel;
using System.Security.Permissions;

namespace Bootstrap.WebForms.Controls
{
    [AspNetHostingPermission(SecurityAction.Demand, Level = AspNetHostingPermissionLevel.Minimal)]
    [ToolboxData(@"<{0}:DropDownList runat=""server"" />")]
    public class DropDownList : System.Web.UI.WebControls.DropDownList
    {
        #region Properties

        [Category("Appearance")]
        [DefaultValue(Sizes.Default)]
        public Sizes Size { get; set; }

        [DefaultValue(false)]
        public bool Disabled { get; set; }

        [DefaultValue(false)]
        public bool AutoFocus { get; set; }

        #endregion

        protected override void AddAttributesToRender(HtmlTextWriter writer)
        {
            writer.AddAttribute(HtmlTextWriterAttribute.Class, GetCssClass());

            if (this.Disabled)
                writer.AddAttribute(HtmlTextWriterAttribute.Disabled, string.Empty);

            if (this.AutoFocus)
                writer.AddAttribute("autofocus", string.Empty);

            // Call the base's AddAttributesToRender method 
            base.AddAttributesToRender(writer);
        }

        private string GetCssClass()
        {
            var css = "form-control";

            switch (this.Size)
            {
                case Sizes.Large:
                    css += " input-lg";
                    break;
                case Sizes.Small:
                    css += " input-sm";
                    break;
                default:
                    break;
            }

            return css;
        }

        public enum Sizes
        {
            [Description("Default button")]
            Default,

            [Description("Large button")]
            Large,

            [Description("Small button")]
            Small,
        }
    }
}