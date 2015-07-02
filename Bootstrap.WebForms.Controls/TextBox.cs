using System;
using System.Web;
using System.Web.UI;
using System.ComponentModel;
using System.Security.Permissions;

namespace Bootstrap.WebForms.Controls
{
    [AspNetHostingPermission(SecurityAction.Demand, Level = AspNetHostingPermissionLevel.Minimal)]
    [ToolboxData(@"<{0}:TextBox runat=""server"" />")]
    public class TextBox : System.Web.UI.WebControls.TextBox
    {
        #region Properties

        [Category("Appearance")]
        [DefaultValue(Sizes.Default)]
        public Sizes Size { get; set; }

        [DefaultValue(false)]
        public bool AutoFocus { get; set; }

        [DefaultValue(false)]
        public bool Required { get; set; }

        [DefaultValue(false)]
        public bool Disabled { get; set; }

        [DefaultValue("")]
        public string PlaceHolder { get; set; }

        [DefaultValue("")]
        public string Pattern { get; set; }

        /// <summary>Gets or sets the type of the Glyphicon.</summary>
        /// <value>The type of the Glyphicon.</value>
        [Category("Appearance")]
        [DefaultValue(GlyphiconTypes.None)]
        public GlyphiconTypes GlyphiconType { get; set; }

        #endregion

        protected override void AddAttributesToRender(HtmlTextWriter writer)
        {
            writer.AddAttribute(HtmlTextWriterAttribute.Class, GetCssClass());

            if (this.Disabled)
                writer.AddAttribute(HtmlTextWriterAttribute.Disabled, string.Empty);

            if (this.AutoFocus)
                writer.AddAttribute("autofocus", null);

            if (this.Required)
                writer.AddAttribute("required", null);

            if (!string.IsNullOrEmpty(this.PlaceHolder))
                writer.AddAttribute("placeholder", this.PlaceHolder);

            if (!string.IsNullOrEmpty(this.Pattern))
                writer.AddAttribute("pattern", this.Pattern);

            // Call the base's AddAttributesToRender method 
            base.AddAttributesToRender(writer);
        }

        protected override void Render(HtmlTextWriter writer)
        {
            if (this.GlyphiconType != GlyphiconTypes.None)
            {
                //<div class="input-group">
                writer.AddAttribute(HtmlTextWriterAttribute.Class, "input-group");
                writer.RenderBeginTag(HtmlTextWriterTag.Div);

                //  <span class="input-group-addon glyphicon glyphicon-lock"></span>
                writer.AddAttribute(HtmlTextWriterAttribute.Class, "input-group-addon");
                writer.RenderBeginTag(HtmlTextWriterTag.Span);
                writer.AddAttribute(HtmlTextWriterAttribute.Class, string.Format("glyphicon {0}", Glyphicon.GetGlyphiconCss(this.GlyphiconType)));
                writer.RenderBeginTag(HtmlTextWriterTag.Span);
                //writer.RenderBeginTag(HtmlTextWriterTag.I);
                writer.RenderEndTag();
                writer.RenderEndTag();
            }

            base.Render(writer);

            if (this.GlyphiconType != GlyphiconTypes.None)
            {
                //</div>
                writer.RenderEndTag();
            }
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
            Default = 0,

            [Description("Large button")]
            Large = 1,

            [Description("Small button")]
            Small = -1,
        }
    }
}