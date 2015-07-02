using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Web.UI;
using System.Web;
using System.Security.Permissions;

namespace Bootstrap.WebForms.Controls
{
    [AspNetHostingPermission(SecurityAction.Demand, Level = AspNetHostingPermissionLevel.Minimal)]
    [ToolboxData("<{0}:Carousel runat=server></{0}:Carousel>")]
    [ParseChildren(true)]
    [PersistChildren(false)]
    public class Carousel : Control, INamingContainer
    {
        public Carousel()
        {
            this.Interval = 3000;
        }

        [DefaultValue("3000")]
        [Bindable(false)]
        [Localizable(true)]
        [Browsable(true)]
        public int Interval { get; set; }

        [DefaultValue(GlyphiconTypes.ChevronRight)]
        [Bindable(false)]
        [Localizable(true)]
        [Browsable(true)]
        public GlyphiconTypes GlyphiconNext { get; set; }

        [DefaultValue(GlyphiconTypes.ChevronLeft)]
        [Bindable(false)]
        [Localizable(true)]
        [Browsable(true)]
        public GlyphiconTypes GlyphiconPrev { get; set; }

        private List<CarouselItem> _CarouselTemplate = new List<CarouselItem>();

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        public List<CarouselItem> CarouselTemplate
        {
            get { return _CarouselTemplate; }
            set { _CarouselTemplate = value; }
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            this.CreateChildControls();
            this.ChildControlsCreated = true;
        }

        protected override void CreateChildControls()
        {
            if (this.CarouselTemplate != null)
            {
                foreach (CarouselItem item in this.CarouselTemplate)
                {
                    this.Controls.Add(item);
                    item.Active = (this.CarouselTemplate.IndexOf(item) == 0);
                }
            }
        }

        protected override void AddParsedSubObject(object obj)
        {
            if (obj is CarouselItem)
            {
                base.AddParsedSubObject(obj);
                return;
            }
        }

        protected override void Render(HtmlTextWriter writer)
        {
            writer.AddAttribute(HtmlTextWriterAttribute.Id, this.ClientID);
            writer.AddAttribute(HtmlTextWriterAttribute.Class, "carousel slide");
            writer.AddAttribute("data-ride", "carousel");
            writer.AddAttribute("data-interval", this.Interval.ToString());
            writer.RenderBeginTag(HtmlTextWriterTag.Div);

            writer.AddAttribute(HtmlTextWriterAttribute.Class, "carousel-indicators");
            writer.RenderBeginTag(HtmlTextWriterTag.Ol);

            for (int i = 0; i < this.CarouselTemplate.Count; i++)
            {
                if (i == 0) writer.AddAttribute("class", "active");
                writer.AddAttribute("data-slide-to", i.ToString());
                writer.AddAttribute("data-target", string.Format("#{0}", this.ClientID));
                writer.RenderBeginTag(HtmlTextWriterTag.Li);
                writer.RenderEndTag();
            }

            writer.RenderEndTag();

            writer.AddAttribute(HtmlTextWriterAttribute.Class, "carousel-inner");
            writer.RenderBeginTag(HtmlTextWriterTag.Div);

            base.Render(writer);

            writer.RenderEndTag();

            #region Carousel Control

            #region Carousel Control Prev

            writer.AddAttribute(HtmlTextWriterAttribute.Class, "left carousel-control");
            writer.AddAttribute(HtmlTextWriterAttribute.Href, string.Format("#{0}", this.ClientID));
            writer.AddAttribute("data-slide", "prev");
            writer.RenderBeginTag(HtmlTextWriterTag.A);

            #region Glyphicon Prev

            Glyphicon glyphiconPrev = new Glyphicon();
            glyphiconPrev.GlyphiconType = this.GlyphiconPrev;
            glyphiconPrev.RenderControl(writer);

            #endregion

            writer.RenderEndTag();

            #endregion

            #region Carousel Control Prev

            writer.AddAttribute(HtmlTextWriterAttribute.Class, "right carousel-control");
            writer.AddAttribute(HtmlTextWriterAttribute.Href, string.Format("#{0}", this.ClientID));
            writer.AddAttribute("data-slide", "next");
            writer.RenderBeginTag(HtmlTextWriterTag.A);

            #region Glyphicon Next

            Glyphicon glyphiconNext = new Glyphicon();
            glyphiconNext.GlyphiconType = this.GlyphiconNext;
            glyphiconNext.RenderControl(writer);

            #endregion

            writer.RenderEndTag();

            #endregion

            #endregion

            writer.RenderEndTag();
        }
    }

    [ParseChildren(true)]
    [PersistChildren(false)]
    [ToolboxItem(false)]
    public class CarouselItem : Control, INamingContainer
    {
        [DefaultValue("")]
        [Bindable(true)]
        [Localizable(true)]
        [Browsable(true)]
        public string ImageUrl { get; set; }

        [DefaultValue("")]
        [Bindable(true)]
        [Localizable(true)]
        [Browsable(true)]
        public string AlternativeText { get; set; }

        [PersistenceMode(PersistenceMode.InnerProperty)]
        [TemplateContainer(typeof(CarouselCaptionTemplate))]
        [TemplateInstance(TemplateInstance.Single)]
        public ITemplate CarouselCaptionTemplate { get; set; }

        public bool Active { get; set; }

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
            if (this.CarouselCaptionTemplate != null)
            {
                CarouselCaptionTemplate carouselCaptionTemplate = new CarouselCaptionTemplate();
                this.CarouselCaptionTemplate.InstantiateIn(carouselCaptionTemplate);
                Controls.Add(carouselCaptionTemplate);
            }
        }

        protected override void Render(HtmlTextWriter writer)
        {
            writer.AddAttribute(HtmlTextWriterAttribute.Class, "item" + (this.Active ? " active" : string.Empty));
            writer.RenderBeginTag(HtmlTextWriterTag.Div);

            if (!string.IsNullOrEmpty(this.AlternativeText)) writer.AddAttribute(HtmlTextWriterAttribute.Alt, this.AlternativeText);
            writer.AddAttribute(HtmlTextWriterAttribute.Src, this.ImageUrl);
            writer.RenderBeginTag(HtmlTextWriterTag.Img);
            writer.RenderEndTag();

            base.Render(writer);

            writer.RenderEndTag();
        }
    }

    [ParseChildren(true)]
    [PersistChildren(false)]
    [ToolboxItem(false)]
    public class CarouselCaptionTemplate : Control, INamingContainer
    {
        protected override void Render(HtmlTextWriter writer)
        {
            writer.AddAttribute(HtmlTextWriterAttribute.Class, "carousel-caption");
            writer.RenderBeginTag(HtmlTextWriterTag.Div);
            base.Render(writer);
            writer.RenderEndTag();
        }
    }
}