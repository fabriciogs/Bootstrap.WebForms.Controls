using System;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Bootstrap.WebForms.Controls
{
    /// <summary>http://jeremyknight.wordpress.com/2014/02/25/asp-net-forms-bootstrap-menu-control/</summary>
    [ControlValueProperty("SelectedValue")]
    [DefaultEvent("MenuItemClick")]
    [SupportsEventValidation]
    [ToolboxData(@"<{0}:Menu runat=""server"" />")]
    public sealed class Menu : System.Web.UI.WebControls.Menu
    {
        private const string hightlightActiveKey = "HighlightActive";

        #region Overrides

        public Menu() { }

        public override void RenderBeginTag(HtmlTextWriter writer)
        {
            // don't call base.RenderBeginTag()
        }

        public override void RenderEndTag(HtmlTextWriter writer)
        {
            // don't call base.RenderEndTag()
        }

        protected override void OnPreRender(EventArgs e)
        {
            // don't call base.OnPreRender(e);
            this.EnsureDataBound();
        }

        protected override void Render(HtmlTextWriter writer)
        {
            this.BuildItems(writer, this.Items, true);
        }

        protected override void EnsureDataBound()
        {
            base.EnsureDataBound();
        }

        #endregion

        /// <summary>Gets or sets the header text over the left list box.</summary>
        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [DefaultValue(false)]
        [DisplayName("HighlightActive")]
        public bool HighlightActive
        {
            get { return this.ViewState[hightlightActiveKey] != null && Convert.ToBoolean(this.ViewState[hightlightActiveKey]); }
            set { this.ViewState[hightlightActiveKey] = value; }
        }

        private bool HasChildren(MenuItem item)
        {
            if (item == null)
                throw new ArgumentNullException("item");

            return item.ChildItems.Count > 0;
        }

        private bool IsLink(MenuItem item)
        {
            if (item == null)
                throw new ArgumentNullException("item");

            return item.Enabled && !string.IsNullOrEmpty(item.NavigateUrl);
        }

        private bool IsDivider(MenuItem item)
        {
            if (item == null)
                throw new ArgumentNullException("item");

            return item.ToolTip.Equals("*IsDivider*");
        }

        private bool IsNavHeader(MenuItem item)
        {
            if (item == null)
                throw new ArgumentNullException("item");

            return item.ToolTip.Equals("*IsNavHeader*");
        }

        private void BuildItems(HtmlTextWriter writer, MenuItemCollection items, bool isRoot = false)
        {
            if (items.Count <= 0)
                return;

            var cssClass = "dropdown-menu";

            if (isRoot)
            {
                cssClass = "nav navbar-nav";
                if (!string.IsNullOrEmpty(this.CssClass))
                    cssClass += " " + this.CssClass;
            }

            writer.AddAttribute(HtmlTextWriterAttribute.Class, cssClass);
            writer.RenderBeginTag(HtmlTextWriterTag.Ul);

            foreach (MenuItem item in items)
                this.BuildItem(writer, item);

            writer.RenderEndTag(); // </ul>
        }

        private void BuildItem(HtmlTextWriter writer, MenuItem item)
        {
            if (item == null)
                throw new ArgumentNullException("item");

            if (writer == null)
                throw new ArgumentNullException("writer");

            if (IsDivider(item))
            {
                writer.AddAttribute(HtmlTextWriterAttribute.Class, "divider");
                writer.RenderBeginTag(HtmlTextWriterTag.Li);
                writer.RenderEndTag(); // </li>
                return;
            }

            if (IsNavHeader(item))
            {
                writer.AddAttribute(HtmlTextWriterAttribute.Class, "dropdown-header");
                writer.RenderBeginTag(HtmlTextWriterTag.Li);
                writer.Write(item.Text);
                writer.RenderEndTag(); // </li>
                return;
            }

            if (item.Selected)
                writer.AddAttribute(HtmlTextWriterAttribute.Class, "active");

            writer.RenderBeginTag(HtmlTextWriterTag.Li); // <li>

            if (HasChildren(item))
            {
                writer.AddAttribute(HtmlTextWriterAttribute.Class, "dropdown");
                RenderDropDown(writer, item);
            }
            else if (IsLink(item))
            {
                if (this.HighlightActive && this.Page.ResolveUrl(item.NavigateUrl) == this.Page.Request.Url.AbsolutePath)
                    writer.AddAttribute(HtmlTextWriterAttribute.Class, "active");
                RenderLink(writer, item);
            }
            else
            {
                writer.RenderBeginTag(HtmlTextWriterTag.A);
                writer.Write(item.Text);
                writer.RenderEndTag();
            }

            writer.RenderEndTag(); // </li>
        }

        private void RenderLink(HtmlTextWriter writer, MenuItem item)
        {
            var href = !string.IsNullOrEmpty(item.NavigateUrl)
                    ? this.Page.Server.HtmlEncode(this.ResolveClientUrl(item.NavigateUrl))
                    : this.Page.ClientScript.GetPostBackClientHyperlink(this, "b" + item.ValuePath.Replace(this.PathSeparator.ToString(), "\\"), true);
            writer.AddAttribute(HtmlTextWriterAttribute.Href, href);

            var toolTip = !string.IsNullOrEmpty(item.ToolTip) && !IsDivider(item) && !IsNavHeader(item) ? item.ToolTip : item.Text;
            writer.AddAttribute(HtmlTextWriterAttribute.Title, toolTip);

            writer.RenderBeginTag(HtmlTextWriterTag.A);
            writer.Write(item.Text);
            writer.RenderEndTag(); // </a>
        }

        private void RenderDropDown(HtmlTextWriter writer, MenuItem item)
        {
            writer.AddAttribute(HtmlTextWriterAttribute.Href, "#");
            writer.AddAttribute(HtmlTextWriterAttribute.Class, "dropdown-toggle");
            writer.AddAttribute("data-toggle", "dropdown");
            writer.RenderBeginTag(HtmlTextWriterTag.A);

            var anchorValue = item.Text + "&nbsp;";
            writer.Write(anchorValue);

            writer.AddAttribute(HtmlTextWriterAttribute.Class, "caret");
            writer.RenderBeginTag(HtmlTextWriterTag.B);
            writer.RenderEndTag(); // </b>          

            writer.RenderEndTag(); // </a>

            BuildItems(writer, item.ChildItems);
        }
    }
}