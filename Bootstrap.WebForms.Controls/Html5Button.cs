using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;
using System.Security.Permissions;

namespace Bootstrap.WebForms.Controls
{
    [AspNetHostingPermission(SecurityAction.Demand, Level = AspNetHostingPermissionLevel.Minimal)]
    [ToolboxData(@"<{0}:Html5Button runat=""server"" />")]
    [DefaultProperty("Text")]
    public class Html5Button : WebControl, INamingContainer //, IButtonControl, IPostBackEventHandler
    {
        #region Properties

        //public event System.EventHandler Click
        //{
        //    add { throw new System.NotImplementedException(); }
        //    remove { throw new System.NotImplementedException(); }
        //}

        //public event CommandEventHandler Command
        //{
        //    add { throw new System.NotImplementedException(); }
        //    remove { throw new System.NotImplementedException(); }
        //}

        [DefaultValue("")]
        public string OnClientClick { get; set; }

        public string PostBackUrl { get; set; }
        public string ValidationGroup { get; set; }
        public bool CausesValidation { get; set; }

        [Bindable(true)]
        [DefaultValue("")]
        [Themeable(false)]
        public string CommandArgument { get; set; }

        [DefaultValue("")]
        [Themeable(false)]
        public string CommandName { get; set; }

        [Bindable(true)]
        [DefaultValue("")]
        [Localizable(true)]
        public string Text { get; set; }

        [Category("Appearance")]
        [DefaultValue(Sizes.Default)]
        public Sizes Size { get; set; }

        [Category("Appearance")]
        [DefaultValue(ButtonTypes.Default)]
        public ButtonTypes ButtonType
        {
            get { return (ButtonTypes)ViewState["ButtonType"]; }
            set { ViewState["ButtonType"] = value; }
        }

        [Category("Appearance")]
        [DefaultValue("")]
        public string TargetModalID
        {
            get { return (string)ViewState["TargetModalID"]; }
            set { ViewState["TargetModalID"] = value; }
        }

        [DefaultValue(false)]
        public bool Disabled { get; set; }

        [DefaultValue(false)]
        public bool BlockLevel { get; set; }

        [DefaultValue("")]
        public string AlertMessage { get; set; }

        [Category("Appearance")]
        [DefaultValue("")]
        public string LoadingText { get; set; }

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

        /// <summary>Renders the HTML opening tag of the control to the specified writer. This method is used primarily by control developers.</summary>
        /// <param name="writer">A <see cref="T:System.Web.UI.HtmlTextWriter" /> that represents the output stream to render HTML content on the client.</param>
        public override void RenderBeginTag(HtmlTextWriter writer)
        {
            writer.RenderBeginTag("button");
            writer.AddAttribute(HtmlTextWriterAttribute.Type, "button");
        }

        /// <summary>Renders the HTML closing tag of the control into the specified writer. This method is used primarily by control developers.</summary>
        /// <param name="writer">A <see cref="T:System.Web.UI.HtmlTextWriter" /> that represents the output stream to render HTML content on the client.</param>
        public override void RenderEndTag(HtmlTextWriter writer)
        {
            if (!string.IsNullOrEmpty(this.Text))
                writer.Write(this.Text);

            writer.RenderEndTag();
        }

        protected override void Render(HtmlTextWriter writer)
        {
            this.AddCssClass(this.CssClass);
            this.AddCssClass(GetCssClass());

            if (this.Disabled)
                writer.AddAttribute(HtmlTextWriterAttribute.Disabled, string.Empty);

            if (!string.IsNullOrEmpty(this.OnClientClick))
                writer.AddAttribute("onclick", this.OnClientClick);

            if (!string.IsNullOrEmpty(this.AlertMessage))
                writer.AddAttribute(HtmlTextWriterAttribute.Onclick, string.Format(@"return confirm('{0}')", this.AlertMessage));

            if (!string.IsNullOrEmpty(this.TargetModalID))
            {
                writer.AddAttribute("data-toggle", "modal");
                writer.AddAttribute("data-target", string.Format("#{0}", this.TargetModalID));
            }

            if (!string.IsNullOrEmpty(this.LoadingText))
                writer.AddAttribute("data-loading-text", this.LoadingText);

            writer.AddAttribute(HtmlTextWriterAttribute.Id, this.ClientID);
            writer.AddAttribute(HtmlTextWriterAttribute.Name, this.UniqueID);
            if (!string.IsNullOrEmpty(this.cssClass))
                writer.AddAttribute(HtmlTextWriterAttribute.Class, this.cssClass);

            // Call the base's AddAttributesToRender method 
            base.Render(writer);
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

        //void IPostBackEventHandler.RaisePostBackEvent(string eventArgument)
        //{
        //    throw new System.NotImplementedException();
        //}
    }
}