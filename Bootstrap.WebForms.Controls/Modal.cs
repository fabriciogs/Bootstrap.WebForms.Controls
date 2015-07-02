using System;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Bootstrap.WebForms.Controls
{
    [ParseChildren(true)]
    [PersistChildren(false)]
    [ToolboxData(@"<{0}:Modal runat=""server""></{0}:Modal>")]
    public class Modal : WebControl, INamingContainer
    {
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

        public Modal() : base(HtmlTextWriterTag.Div) { }

        [Bindable(true)]
        [Category("Appearance")]
        [DefaultValue("")]
        [Localizable(true)]
        public string CancelButtonID { get; set; }

        [Bindable(true)]
        [Category("Appearance")]
        [DefaultValue("")]
        [Localizable(true)]
        public string ModalTitle { get; set; }

        [PersistenceMode(PersistenceMode.InnerProperty)]
        [TemplateContainer(typeof(HeaderTemplateContainer))]
        [TemplateInstance(TemplateInstance.Single)]
        public ITemplate Header { get; set; }

        [PersistenceMode(PersistenceMode.InnerProperty)]
        [TemplateContainer(typeof(BodyTemplateContainer))]
        [TemplateInstance(TemplateInstance.Single)]
        public ITemplate Body { get; set; }

        [PersistenceMode(PersistenceMode.InnerProperty)]
        [TemplateContainer(typeof(FooterTemplateContainer))]
        [TemplateInstance(TemplateInstance.Single)]
        public ITemplate Footer { get; set; }

        protected override void OnDataBinding(EventArgs e)
        {
            base.OnDataBinding(e);
            EnsureChildControls();
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            this.CreateChildControls();
            this.ChildControlsCreated = true;
        }

        public override void DataBind()
        {
            base.OnDataBinding(EventArgs.Empty);

            Controls.Clear();
            ClearChildViewState();
            this.CreateChildControls();
            this.ChildControlsCreated = true;
        }

        protected override void CreateChildControls()
        {
            HeaderTemplateContainer headerTemplateContainer = null;
            BodyTemplateContainer bodyTemplateContainer = null;
            FooterTemplateContainer footerTemplateContainer = null;

            if (Header != null)
            {
                headerTemplateContainer = new HeaderTemplateContainer();
                Header.InstantiateIn(headerTemplateContainer);
                Controls.Add(headerTemplateContainer);
            }

            if (Body != null)
            {
                bodyTemplateContainer = new BodyTemplateContainer();
                Body.InstantiateIn(bodyTemplateContainer);
                Controls.Add(bodyTemplateContainer);
            }

            if (Footer != null)
            {
                footerTemplateContainer = new FooterTemplateContainer();
                Footer.InstantiateIn(footerTemplateContainer);
                Controls.Add(footerTemplateContainer);
            }

            if (!string.IsNullOrEmpty(this.CancelButtonID))
            {
                if (bodyTemplateContainer != null || footerTemplateContainer != null)
                {
                    WebControl cancelButton = null;

                    if (bodyTemplateContainer.FindControl(this.CancelButtonID) != null)
                    {
                        cancelButton = (bodyTemplateContainer.FindControl(this.CancelButtonID) as WebControl);
                    }

                    if (footerTemplateContainer.FindControl(this.CancelButtonID) != null)
                    {
                        cancelButton = (footerTemplateContainer.FindControl(this.CancelButtonID) as WebControl);
                    }

                    if (cancelButton != null)
                    {
                        cancelButton.Attributes.Add("data-dismiss", "modal");
                    }
                    else
                    {
                        throw new Exception("The referenced object as CancelButtonID was not found.");
                    }
                }
            }
        }

        protected override void Render(HtmlTextWriter writer)
        {
            this.AddCssClass(this.CssClass);
            this.AddCssClass("modal fade");

            writer.AddAttribute(HtmlTextWriterAttribute.Id, this.ClientID);
            writer.AddAttribute(HtmlTextWriterAttribute.Class, this.cssClass);

            base.Render(writer);
        }

        protected override void RenderContents(HtmlTextWriter writer)
        {
            #region Modal

            writer.AddAttribute(HtmlTextWriterAttribute.Class, "modal-dialog");
            writer.RenderBeginTag(HtmlTextWriterTag.Div);

            #region Modal Content

            writer.AddAttribute(HtmlTextWriterAttribute.Class, "modal-content");
            writer.RenderBeginTag(HtmlTextWriterTag.Div);


            base.RenderContents(writer);

            writer.RenderEndTag();

            #endregion

            writer.RenderEndTag();

            #endregion
        }
    }

    [ToolboxItem(false)]
    public class HeaderTemplateContainer : Control, INamingContainer
    {
        protected override void Render(HtmlTextWriter writer)
        {
            #region Modal Header

            writer.AddAttribute(HtmlTextWriterAttribute.Class, "modal-header");
            writer.RenderBeginTag(HtmlTextWriterTag.Div);

            #region Modal Header Content

            #region Modal Close Button

            writer.AddAttribute(HtmlTextWriterAttribute.Class, "close");
            writer.AddAttribute("type", "button");
            writer.AddAttribute("data-dismiss", "modal");
            writer.AddAttribute("aria-hidden", "true");
            writer.RenderBeginTag(HtmlTextWriterTag.Button);
            writer.Write("&times;");
            writer.RenderEndTag();

            #endregion

            #region Modal Header Title

            writer.AddAttribute(HtmlTextWriterAttribute.Class, "modal-title");
            writer.RenderBeginTag(HtmlTextWriterTag.H4);

            base.Render(writer);

            writer.RenderEndTag();

            #endregion

            #endregion

            writer.RenderEndTag();

            #endregion
        }
    }

    [ToolboxItem(false)]
    public class BodyTemplateContainer : Control, INamingContainer
    {
        protected override void Render(HtmlTextWriter writer)
        {
            #region Modal Body

            writer.AddAttribute(HtmlTextWriterAttribute.Class, "modal-body");
            writer.RenderBeginTag(HtmlTextWriterTag.Div);

            #region Modal Body Content

            base.Render(writer);

            #endregion

            writer.RenderEndTag();

            #endregion
        }
    }

    [ToolboxItem(false)]
    public class FooterTemplateContainer : Control, INamingContainer
    {
        protected override void Render(HtmlTextWriter writer)
        {
            #region Modal Footer

            writer.AddAttribute(HtmlTextWriterAttribute.Class, "modal-footer");
            writer.RenderBeginTag(HtmlTextWriterTag.Div);

            #region Modal Footer Content

            base.Render(writer);

            #endregion

            writer.RenderEndTag();

            #endregion
        }
    }
}
