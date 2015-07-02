using System;
using System.ComponentModel;
using System.Security.Permissions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Bootstrap.WebForms.Controls
{
    [AspNetHostingPermission(SecurityAction.Demand, Level = AspNetHostingPermissionLevel.Minimal)]
    [ToolboxData(@"<{0}:Paginator runat=""server""></{0}:Paginator>")]
    public class Paginator : WebControl, INamingContainer, IPostBackDataHandler
    {
        public event EventHandler<PageIndexChangedEventArgs> PageIndexChanged;

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

        /// <summary>Initializes a new instance of the <see cref="Paginator" /> class.</summary>
        public Paginator()
        {
            this.PageSize = 25;
            this.CurrentPageIndex = 0;
        }

        [Category("Appearance")]
        [DefaultValue(0)]
        public int CurrentPageIndex
        {
            get { return (int)ViewState["CurrentPageIndex"]; }
            private set { ViewState["CurrentPageIndex"] = value; }
        }

        /// <summary>Gets or sets the size of the page.</summary>
        /// <value>The size of the page.</value>
        [Category("Appearance")]
        [DefaultValue(25)]
        public int PageSize
        {
            get { return (int)ViewState["PageSize"]; }
            set { ViewState["PageSize"] = value; }
        }

        [Category("Appearance")]
        [DefaultValue(0)]
        public int ItemCount
        {
            get { return (int)ViewState["ItemCount"]; }
            set { ViewState["ItemCount"] = value; }
        }

        /// <summary>Renders the HTML opening tag of the control to the specified writer. This method is used primarily by control developers.</summary>
        /// <param name="writer">A <see cref="T:System.Web.UI.HtmlTextWriter" /> that represents the output stream to render HTML content on the client.</param>
        public override void RenderBeginTag(HtmlTextWriter writer)
        {
            //writer.RenderBeginTag(HtmlTextWriterTag.Div);
            writer.RenderBeginTag("nav");
        }

        /// <summary>Renders the HTML closing tag of the control into the specified writer. This method is used primarily by control developers.</summary>
        /// <param name="writer">A <see cref="T:System.Web.UI.HtmlTextWriter" /> that represents the output stream to render HTML content on the client.</param>
        public override void RenderEndTag(HtmlTextWriter writer)
        {
            writer.RenderEndTag();
        }

        /// <summary>Renders the control to the specified HTML writer.</summary>
        /// <param name="writer">The <see cref="T:System.Web.UI.HtmlTextWriter" /> object that receives the control content.</param>
        protected override void Render(HtmlTextWriter writer)
        {
            this.AddCssClass(this.CssClass);
            //this.AddCssClass("pagination");

            writer.AddAttribute(HtmlTextWriterAttribute.Id, this.ClientID);
            writer.AddAttribute(HtmlTextWriterAttribute.Name, this.UniqueID);
            if (!string.IsNullOrEmpty(this.sCssClass))
                writer.AddAttribute(HtmlTextWriterAttribute.Class, this.sCssClass);

            base.Render(writer);
        }

        /// <summary>Renders the contents.</summary>
        /// <param name="output">The output.</param>
        protected override void RenderContents(HtmlTextWriter output)
        {
            var totalPages = GetTotalPages();

            output.AddAttribute(HtmlTextWriterAttribute.Class, "pagination");
            output.RenderBeginTag(HtmlTextWriterTag.Ul);

            //Mostra o botão anterior "<<" apenas se não for a primeira página
            if (this.CurrentPageIndex > 0)
            {
                output.RenderBeginTag(HtmlTextWriterTag.Li);
                output.AddAttribute(HtmlTextWriterAttribute.Href, Page.ClientScript.GetPostBackClientHyperlink(this, this.CurrentPageIndex.ToString(), false));
                output.AddAttribute("aria-label", "página anterior");
                output.RenderBeginTag(HtmlTextWriterTag.A);
                output.Write(@"<span aria-hidden=""true"">&laquo;</span>");
                output.RenderEndTag(); //A
                output.RenderEndTag(); //Li
            }

            //Loop pelo número de páginas
            for (int i = 0; i < totalPages; i++)
            {
                if (this.CurrentPageIndex == i)
                {
                    output.AddAttribute(HtmlTextWriterAttribute.Class, "active");
                }

                output.RenderBeginTag(HtmlTextWriterTag.Li);

                output.AddAttribute(HtmlTextWriterAttribute.Href, Page.ClientScript.GetPostBackClientHyperlink(this, (i + 1).ToString(), false));
                output.RenderBeginTag(HtmlTextWriterTag.A);
                output.Write((i + 1).ToString());
                output.RenderEndTag();

                output.RenderEndTag();
            }

            //Mostra o botão posterior ">>" apenas se não for a última página
            if (this.CurrentPageIndex < (totalPages - 1))
            {
                output.RenderBeginTag(HtmlTextWriterTag.Li);
                output.AddAttribute(HtmlTextWriterAttribute.Href, Page.ClientScript.GetPostBackClientHyperlink(this, (this.CurrentPageIndex + 2).ToString(), false));
                output.AddAttribute("aria-label", "próxima página");
                output.RenderBeginTag(HtmlTextWriterTag.A);
                output.Write(@"<span aria-hidden=""true"">&raquo;</span>");
                output.RenderEndTag(); //A
                output.RenderEndTag(); //Li
            }

            output.RenderEndTag();

            this.RenderChildren(output);
        }

        /// <summary>Gets the total pages.</summary>
        /// <returns></returns>
        private int GetTotalPages()
        {
            return Convert.ToInt32(Math.Ceiling(Decimal.Divide((decimal)ItemCount, (decimal)PageSize)));
        }

        /// <summary>Raises the <see cref="E:System.Web.UI.Control.Init" /> event.</summary>
        /// <param name="e">An <see cref="T:System.EventArgs" /> object that contains the event data.</param>
        protected override void OnInit(System.EventArgs e)
        {
            this.Page.RegisterRequiresPostBack(this);
            base.OnInit(e);
        }

        /// <summary>When implemented by a class, processes postback data for an ASP.NET server control.</summary>
        /// <param name="postDataKey">The key identifier for the control.</param>
        /// <param name="postCollection">The collection of all incoming name values.</param>
        /// <returns>True if the server control's state changes as a result of the postback; otherwise, false.</returns>
        public bool LoadPostData(string postDataKey, System.Collections.Specialized.NameValueCollection postCollection)
        {
            if (!(postCollection["__EVENTTARGET"] == this.UniqueID))
            {
                return false;
            }

            int pageIndex = Convert.ToInt32(postCollection["__EVENTARGUMENT"]) - 1;

            if (!(this.CurrentPageIndex == pageIndex))
            {
                this.CurrentPageIndex = pageIndex;
                return true;
            }

            return false;
        }

        /// <summary>When implemented by a class, signals the server control to notify the ASP.NET application that the state of the control has changed.</summary>
        public void RaisePostDataChangedEvent()
        {
            OnPageIndexChanged();
        }

        /// <summary>Called when [page index changed].</summary>
        private void OnPageIndexChanged()
        {
            if (PageIndexChanged != null)
            {
                PageIndexChanged(this, new PageIndexChangedEventArgs() { PageIndex = this.CurrentPageIndex });
            }
        }
    }

    public class PageIndexChangedEventArgs : EventArgs
    {
        public int PageIndex { get; set; }
    }
}