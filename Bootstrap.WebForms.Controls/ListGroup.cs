using System.Collections.Generic;
using System.ComponentModel;
using System.Security.Permissions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Bootstrap.WebForms.Controls
{
    [AspNetHostingPermission(SecurityAction.Demand, Level = AspNetHostingPermissionLevel.Minimal)]
    [ToolboxData(@"<{0}:ListGroup runat=""server""></{0}:ListGroup>")]
    [ParseChildren(true)]
    [PersistChildren(false)]
    public class ListGroup : Control, INamingContainer
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

        private List<ListGroupItem> _ListGroupItens = new List<ListGroupItem>();

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        public List<ListGroupItem> ListGroupItens
        {
            get { return _ListGroupItens; }
        }

        [Bindable(true)]
        [Localizable(true)]
        [Browsable(true)]
        public bool Linked { get; set; }

        public ListGroup()
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
            writer.AddAttribute(HtmlTextWriterAttribute.Class, "list-group");
            writer.RenderBeginTag(this.Linked ? HtmlTextWriterTag.Div : HtmlTextWriterTag.Ul);

            base.Render(writer);

            writer.RenderEndTag();
        }

        protected override void CreateChildControls()
        {
            if (this.ListGroupItens != null)
            {
                foreach (var item in this.ListGroupItens)
                {
                    this.Controls.Add(item);
                    item.Linked = this.Linked;
                }
            }
        }

        protected override void AddParsedSubObject(object obj)
        {
            if (obj is ListGroupItem)
            {
                ListGroupItens.Add((ListGroupItem)obj);
                return;
            }
        }
    }

    [ParseChildren(true)]
    [PersistChildren(false)]
    [ToolboxItem(false)]
    public class ListGroupItemContentContainer : Control, INamingContainer
    {
        protected override void Render(HtmlTextWriter writer)
        {
            base.Render(writer);
        }
    }

    [ParseChildren(true)]
    [PersistChildren(false)]
    [ToolboxItem(false)]
    public class ListGroupItem : Control, INamingContainer
    {
        public bool Linked { get; set; }

        [PersistenceMode(PersistenceMode.InnerProperty)]
        [TemplateInstance(TemplateInstance.Single)]
        [TemplateContainer(typeof(ListGroupItemContentContainer))]
        public ITemplate Content { get; set; }

        [Bindable(true)]
        [Localizable(true)]
        [Browsable(true)]
        public bool ActiveItem { get; set; }

        [Bindable(true)]
        [Localizable(true)]
        [Browsable(true)]
        public string ItemUrl { get; set; }


        protected override void Render(HtmlTextWriter writer)
        {
            string cssClass = "list-group-item";

            if (this.ActiveItem)
            {
                cssClass = string.Format("{0} active", cssClass);
            }

            writer.AddAttribute(HtmlTextWriterAttribute.Id, this.ClientID);
            writer.AddAttribute(HtmlTextWriterAttribute.Class, cssClass);

            if (this.Linked)
            {
                string _url = string.IsNullOrEmpty(this.ItemUrl) ? "#" : this.ItemUrl;
                writer.AddAttribute(HtmlTextWriterAttribute.Href, _url);
                writer.RenderBeginTag(HtmlTextWriterTag.A);
            }
            else
            {
                writer.RenderBeginTag(HtmlTextWriterTag.Li);
            }

            base.Render(writer);

            writer.RenderEndTag();
        }

        protected override void CreateChildControls()
        {
            var listGroupItemContentContainer = new ListGroupItemContentContainer();
            Content.InstantiateIn(listGroupItemContentContainer);
            Controls.Add(listGroupItemContentContainer);
        }
    }
}