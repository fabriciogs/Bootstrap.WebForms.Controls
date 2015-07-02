using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Web.UI;
using System.Linq;
using System.Web;
using System.Security.Permissions;

namespace Bootstrap.WebForms.Controls
{
    #region Collapse

    [AspNetHostingPermission(SecurityAction.Demand, Level = AspNetHostingPermissionLevel.Minimal)]
    [ToolboxData(@"<{0}:Collapse runat=""server""></{0}:Collapse>")]
    [ParseChildren(true)]
    [PersistChildren(false)]
    public class Collapse : Control, INamingContainer
    {
        #region CssClass method

        string cssClass = "";

        /// <summary>
        /// Adds the CSS class.
        /// </summary>
        /// <param name="cssClass">The CSS class.</param>
        private void AddCssClass(string cssClass)
        {
            if (String.IsNullOrEmpty(this.cssClass))
            {
                this.cssClass = cssClass;
            }
            else
            {
                this.cssClass += " " + cssClass;
            }
        }

        #endregion

        private List<CollapseItem> _CollapseTemplate = new List<CollapseItem>();

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        public List<CollapseItem> CollapseTemplate
        {
            get { return _CollapseTemplate; }
        }

        public Collapse()
        {

        }

        protected override void OnInit(System.EventArgs e)
        {
            base.OnInit(e);

            this.CreateChildControls();
            this.ChildControlsCreated = true;
        }

        protected override void Render(HtmlTextWriter writer)
        {
            writer.AddAttribute(HtmlTextWriterAttribute.Id, this.ClientID);
            writer.AddAttribute(HtmlTextWriterAttribute.Class, "panel-group");
            writer.RenderBeginTag(HtmlTextWriterTag.Div);

            base.Render(writer);

            writer.RenderEndTag();
        }

        protected override void CreateChildControls()
        {
            if (this.CollapseTemplate != null)
            {
                foreach (CollapseItem item in this.CollapseTemplate)
                {
                    this.Controls.Add(item);

                    CollapseItemBodyContainer body = (item.Controls.OfType<CollapseItemBodyContainer>().FirstOrDefault() as CollapseItemBodyContainer);
                    CollapseItemHeaderContainer header = (item.Controls.OfType<CollapseItemHeaderContainer>().FirstOrDefault() as CollapseItemHeaderContainer);

                    header.BodyId = body.ClientID;
                    header.ParentId = this.ClientID;
                }
            }
        }

        protected override void AddParsedSubObject(object obj)
        {
            if (obj is CollapseItem)
            {
                CollapseTemplate.Add((CollapseItem)obj);
                return;
            }
        }
    }

    #endregion

    #region Collapse Item

    [ParseChildren(true)]
    [PersistChildren(false)]
    [ToolboxItem(false)]
    public class CollapseItem : Control, INamingContainer
    {
        [Bindable(false)]
        [Localizable(true)]
        [Browsable(true)]
        public bool Opened { get; set; }

        [PersistenceMode(PersistenceMode.InnerProperty)]
        [TemplateContainer(typeof(CollapseItemHeaderContainer))]
        [TemplateInstance(TemplateInstance.Single)]
        public ITemplate HeaderTemplate { get; set; }

        [PersistenceMode(PersistenceMode.InnerProperty)]
        [TemplateContainer(typeof(CollapseItemBodyContainer))]
        [TemplateInstance(TemplateInstance.Single)]
        public ITemplate BodyTemplate { get; set; }

        #region

        public CollapseItem()
        {

        }

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

        protected override void Render(HtmlTextWriter writer)
        {
            writer.AddAttribute(HtmlTextWriterAttribute.Id, this.ClientID);
            writer.AddAttribute(HtmlTextWriterAttribute.Class, "panel panel-default");
            writer.RenderBeginTag(HtmlTextWriterTag.Div);

            base.Render(writer);

            writer.RenderEndTag();
        }

        protected override void CreateChildControls()
        {
            CollapseItemHeaderContainer headerTemplateContainer = new CollapseItemHeaderContainer();
            HeaderTemplate.InstantiateIn(headerTemplateContainer);

            CollapseItemBodyContainer bodyTemplateContainer = new CollapseItemBodyContainer();
            BodyTemplate.InstantiateIn(bodyTemplateContainer);

            Controls.Add(headerTemplateContainer);
            Controls.Add(bodyTemplateContainer);

            CollapseItemBodyContainer body = (Controls.OfType<CollapseItemBodyContainer>().FirstOrDefault() as CollapseItemBodyContainer);
            body.Opened = this.Opened;
        }

        #endregion
    }

    [ParseChildren(true)]
    [PersistChildren(false)]
    [ToolboxItem(false)]
    public class CollapseItemHeaderContainer : Control, INamingContainer
    {
        public string ParentId { get; set; }
        public string BodyId { get; set; }

        protected override void Render(HtmlTextWriter writer)
        {
            writer.AddAttribute(HtmlTextWriterAttribute.Class, "panel-heading");
            writer.RenderBeginTag(HtmlTextWriterTag.Div);

            writer.AddAttribute(HtmlTextWriterAttribute.Class, "panel-title");
            writer.RenderBeginTag(HtmlTextWriterTag.H4);

            writer.AddAttribute("data-toggle", "collapse");
            writer.AddAttribute("data-parent", string.Format("#{0}", this.ParentId));
            writer.AddAttribute(HtmlTextWriterAttribute.Href, string.Format("#{0}", this.BodyId));
            writer.RenderBeginTag(HtmlTextWriterTag.A);

            base.Render(writer);

            writer.RenderEndTag();

            writer.RenderEndTag();

            writer.RenderEndTag();
        }
    }

    [ParseChildren(true)]
    [PersistChildren(false)]
    [ToolboxItem(false)]
    public class CollapseItemBodyContainer : Control, INamingContainer
    {
        public bool Opened { get; set; }

        protected override void Render(HtmlTextWriter writer)
        {
            string cssClass = "panel-collapse collapse";
            if (this.Opened) cssClass = string.Format("{0} in", cssClass);

            writer.AddAttribute(HtmlTextWriterAttribute.Id, this.ClientID);
            writer.AddAttribute(HtmlTextWriterAttribute.Class, cssClass);
            writer.RenderBeginTag(HtmlTextWriterTag.Div);

            writer.AddAttribute(HtmlTextWriterAttribute.Class, "panel-body");
            writer.RenderBeginTag(HtmlTextWriterTag.Div);

            base.Render(writer);

            writer.RenderEndTag();

            writer.RenderEndTag();
        }
    }

    #endregion
}