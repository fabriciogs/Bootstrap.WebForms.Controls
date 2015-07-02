using System;
using System.ComponentModel;
using System.Security.Permissions;
using System.Web;
using System.Web.UI;

namespace Bootstrap.WebForms.Controls
{
    [AspNetHostingPermission(SecurityAction.Demand, Level = AspNetHostingPermissionLevel.Minimal)]
    [ToolboxData(@"<{0}:Panel runat=""server""></{0}:Panel>")]
    [ParseChildren(true)]
    [PersistChildren(false)]
    public class Panel : System.Web.UI.WebControls.Panel
    {
        [Category("Appearance")]
        [DefaultValue(true)]
        public bool ShowPanelTitle
        {
            get { return (bool)ViewState["ShowPanelTitle"]; }
            set { ViewState["ShowPanelTitle"] = value; }
        }

        [Category("Appearance")]
        [DefaultValue(false)]
        public bool ShowPanelBody
        {
            get { return (bool)ViewState["ShowPanelBody"]; }
            set { ViewState["ShowPanelBody"] = value; }
        }

        [Category("Appearance")]
        [DefaultValue(false)]
        public bool ShowPanelFooter
        {
            get { return (bool)ViewState["ShowPanelFooter"]; }
            set { ViewState["ShowPanelFooter"] = value; }
        }

        [Category("Appearance")]
        [DefaultValue(PanelTypes.Primary)]
        public PanelTypes PanelType
        {
            get { return (PanelTypes)ViewState["PanelTypes"]; }
            set { ViewState["PanelTypes"] = value; }
        }

        [PersistenceMode(PersistenceMode.InnerProperty)]
        [TemplateContainer(typeof(PanelTitleTemplateContainer))]
        [TemplateInstance(TemplateInstance.Single)]
        public ITemplate TitleTemplate { get; set; }

        [PersistenceMode(PersistenceMode.InnerProperty)]
        [TemplateContainer(typeof(PanelTableTemplateContainer))]
        [TemplateInstance(TemplateInstance.Single)]
        public ITemplate TableTemplate { get; set; }

        [PersistenceMode(PersistenceMode.InnerProperty)]
        [TemplateContainer(typeof(PanelBodyTemplateContainer))]
        [TemplateInstance(TemplateInstance.Single)]
        public ITemplate BodyTemplate { get; set; }


        [PersistenceMode(PersistenceMode.InnerProperty)]
        [TemplateContainer(typeof(PanelFooterTemplateContainer))]
        [TemplateInstance(TemplateInstance.Single)]
        public ITemplate FooterTemplate { get; set; }

        public Panel()
        {
            this.ShowPanelTitle = true;
            this.ShowPanelBody = true;
            this.ShowPanelFooter = false;
            this.PanelType = PanelTypes.Primary;
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

        protected override void CreateChildControls()
        {
            PanelTitleTemplateContainer panelTitleTemplateContainer = null;
            PanelBodyTemplateContainer panelBodyTemplateContainer = null;
            PanelTableTemplateContainer panelTableTemplateContainer = null;
            PanelFooterTemplateContainer panelFooterTemplateContainer = null;

            if (this.TitleTemplate != null && this.ShowPanelTitle)
            {
                panelTitleTemplateContainer = new PanelTitleTemplateContainer();
                this.TitleTemplate.InstantiateIn(panelTitleTemplateContainer);
                Controls.Add(panelTitleTemplateContainer);
            }

            if (this.BodyTemplate != null && this.ShowPanelBody)
            {
                panelBodyTemplateContainer = new PanelBodyTemplateContainer();
                this.BodyTemplate.InstantiateIn(panelBodyTemplateContainer);
                Controls.Add(panelBodyTemplateContainer);
            }

            if (this.TableTemplate != null)
            {
                panelTableTemplateContainer = new PanelTableTemplateContainer();
                this.TableTemplate.InstantiateIn(panelTableTemplateContainer);
                Controls.Add(panelTableTemplateContainer);
            }

            if (this.FooterTemplate != null && this.ShowPanelFooter)
            {
                panelFooterTemplateContainer = new PanelFooterTemplateContainer();
                this.FooterTemplate.InstantiateIn(panelFooterTemplateContainer);
                Controls.Add(panelFooterTemplateContainer);
            }
        }

        protected override void Render(HtmlTextWriter writer)
        {
            string cssClass = "panel";

            switch (this.PanelType)
            {
                case PanelTypes.Success:
                    cssClass = string.Format("{0} panel-{1}", cssClass, "success");
                    break;
                case PanelTypes.Info:
                    cssClass = string.Format("{0} panel-{1}", cssClass, "info");
                    break;
                case PanelTypes.Warning:
                    cssClass = string.Format("{0} panel-{1}", cssClass, "warning");
                    break;
                case PanelTypes.Danger:
                    cssClass = string.Format("{0} panel-{1}", cssClass, "danger");
                    break;
                case PanelTypes.Primary:
                default:
                    cssClass = string.Format("{0} panel-{1}", cssClass, "primary");
                    break;
            }

            this.Attributes.Add("class", cssClass);

            base.Render(writer);
        }
    }

    [ToolboxItem(false)]
    public class PanelTitleTemplateContainer : Control, INamingContainer
    {
        protected override void Render(HtmlTextWriter writer)
        {
            writer.AddAttribute(HtmlTextWriterAttribute.Class, "panel-heading");
            writer.RenderBeginTag(HtmlTextWriterTag.Div);

            writer.AddAttribute(HtmlTextWriterAttribute.Class, "panel-title");
            writer.RenderBeginTag(HtmlTextWriterTag.H3);
            base.Render(writer);
            writer.RenderEndTag();

            writer.RenderEndTag();
        }
    }

    [ToolboxItem(false)]
    public class PanelBodyTemplateContainer : Control, INamingContainer
    {
        protected override void Render(HtmlTextWriter writer)
        {
            writer.AddAttribute(HtmlTextWriterAttribute.Class, "panel-body");
            writer.RenderBeginTag(HtmlTextWriterTag.Div);
            base.Render(writer);
            writer.RenderEndTag();
        }
    }

    [ToolboxItem(false)]
    public class PanelTableTemplateContainer : Control, INamingContainer
    {
        protected override void Render(HtmlTextWriter writer)
        {
            base.Render(writer);
        }
    }

    [ToolboxItem(false)]
    public class PanelFooterTemplateContainer : Control, INamingContainer
    {
        protected override void Render(HtmlTextWriter writer)
        {
            writer.AddAttribute(HtmlTextWriterAttribute.Class, "panel-footer");
            writer.RenderBeginTag(HtmlTextWriterTag.Div);
            base.Render(writer);
            writer.RenderEndTag();
        }
    }
}