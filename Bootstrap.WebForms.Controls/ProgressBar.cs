using System;
using System.ComponentModel;
using System.Security.Permissions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Bootstrap.WebForms.Controls
{
    [AspNetHostingPermission(SecurityAction.Demand, Level = AspNetHostingPermissionLevel.Minimal)]
    [ToolboxData(@"<{0}:ProgressBar runat=""server"" />")]
    public class ProgressBar : WebControl, INamingContainer
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

        /// <summary>
        /// Initializes a new instance of the <see cref="ProgressBar" /> class.
        /// </summary>
        public ProgressBar()
        {
            this.Animated = false;
            this.Striped = false;
            this.ShowLabel = false;
            this.LabelFormat = "{0}% Completed";
            this.MinimumValue = 0;
            this.MaximumValue = 100;
            this.CurrentValue = 50;
            this.ProgressBarStyle = ProgressBarStyles.Info;
        }

        [DefaultValue(100)]
        [Category("Behavior")]
        public decimal MaximumValue
        {
            get { return (decimal)ViewState["MaximumValue"]; }
            set { ViewState["MaximumValue"] = value; }
        }

        [DefaultValue(0)]
        [Category("Behavior")]
        public decimal MinimumValue
        {
            get { return (decimal)ViewState["MinimumValue"]; }
            set { ViewState["MinimumValue"] = value; }
        }

        [DefaultValue(0)]
        [Category("Behavior")]
        public decimal CurrentValue
        {
            get { return (decimal)ViewState["CurrentValue"]; }
            set { ViewState["CurrentValue"] = value; }
        }

        [Category("Appearance")]
        [DefaultValue(ProgressBarStyles.Info)]
        public ProgressBarStyles ProgressBarStyle
        {
            get { return (ProgressBarStyles)ViewState["ProgressBarStyle"]; }
            set { ViewState["ProgressBarStyle"] = value; }
        }

        [Category("Appearance")]
        [DefaultValue(false)]
        public bool Animated
        {
            get { return (bool)ViewState["Animated"]; }
            set { ViewState["Animated"] = value; }
        }

        [Category("Appearance")]
        [DefaultValue(false)]
        public bool Striped
        {
            get { return (bool)ViewState["Striped"]; }
            set { ViewState["Striped"] = value; }
        }

        [Category("Appearance")]
        [DefaultValue(false)]
        public bool ShowLabel
        {
            get { return (bool)ViewState["ShowLabel"]; }
            set { ViewState["ShowLabel"] = value; }
        }

        [Category("Appearance")]
        [DefaultValue("{0}% Completed")]
        public string LabelFormat
        {
            get { return (string)ViewState["LabelFormat"]; }
            set { ViewState["LabelFormat"] = value; }
        }


        /// <summary>
        /// Renders the HTML opening tag of the control to the specified writer. This method is used primarily by control developers.
        /// </summary>
        /// <param name="writer">A <see cref="T:System.Web.UI.HtmlTextWriter" /> that represents the output stream to render HTML content on the client.</param>
        public override void RenderBeginTag(HtmlTextWriter writer)
        {
            writer.RenderBeginTag(HtmlTextWriterTag.Div);
        }

        /// <summary>
        /// Renders the control to the specified HTML writer.
        /// </summary>
        /// <param name="writer">The <see cref="T:System.Web.UI.HtmlTextWriter" /> object that receives the control content.</param>
        protected override void Render(HtmlTextWriter writer)
        {
            this.AddCssClass(this.CssClass);
            this.AddCssClass("progress");

            if (this.Striped) this.AddCssClass("progress-striped");
            if (this.Animated) this.AddCssClass("active");

            writer.AddAttribute(HtmlTextWriterAttribute.Id, this.ClientID);
            writer.AddAttribute(HtmlTextWriterAttribute.Class, this.sCssClass);

            base.Render(writer);
        }

        protected override void RenderContents(HtmlTextWriter output)
        {
            output.AddAttribute("role", "progressbar");

            switch (this.ProgressBarStyle)
            {
                case ProgressBarStyles.Info:
                    output.AddAttribute(HtmlTextWriterAttribute.Class, "progress-bar progress-bar-info");
                    break;
                case ProgressBarStyles.Success:
                    output.AddAttribute(HtmlTextWriterAttribute.Class, "progress-bar progress-bar-success");
                    break;
                case ProgressBarStyles.Warning:
                    output.AddAttribute(HtmlTextWriterAttribute.Class, "progress-bar progress-bar-warning");
                    break;
                case ProgressBarStyles.Danger:
                    output.AddAttribute(HtmlTextWriterAttribute.Class, "progress-bar progress-bar-danger");
                    break;
                default:
                    break;
            }

            output.AddStyleAttribute(HtmlTextWriterStyle.Width, string.Format("{0}%", this.CurrentValue));
            output.AddAttribute("aria-valuenow", this.CurrentValue.ToString());
            output.AddAttribute("aria-valuemin", this.MinimumValue.ToString());
            output.AddAttribute("aria-valuemax", this.MaximumValue.ToString());

            output.RenderBeginTag(HtmlTextWriterTag.Div);

            if (!this.ShowLabel)
            {
                output.AddAttribute(HtmlTextWriterAttribute.Class, "sr-only");
            }

            output.RenderBeginTag(HtmlTextWriterTag.Span);
            output.Write(string.Format(this.LabelFormat, this.CurrentValue.ToString()));
            output.RenderEndTag();
            output.RenderEndTag();
        }

        /// <summary>
        /// Renders the HTML closing tag of the control into the specified writer. This method is used primarily by control developers.
        /// </summary>
        /// <param name="writer">A <see cref="T:System.Web.UI.HtmlTextWriter" /> that represents the output stream to render HTML content on the client.</param>
        public override void RenderEndTag(HtmlTextWriter writer)
        {
            writer.RenderEndTag();
        }
    }
}