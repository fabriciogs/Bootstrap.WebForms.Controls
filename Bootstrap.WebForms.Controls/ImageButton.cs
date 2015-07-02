using System.Web;
using System.Web.UI;
using System.ComponentModel;
using System.Security.Permissions;

namespace Bootstrap.WebForms.Controls
{
    [AspNetHostingPermission(SecurityAction.Demand, Level = AspNetHostingPermissionLevel.Minimal)]
    [ToolboxData(@"<{0}:ImageButton runat=""server"" />")]
    public class ImageButton : System.Web.UI.WebControls.ImageButton
    {
        #region Properties

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

            if (this.BlockLevel)
                css += " btn-block";

            return css;
        }
    }
}