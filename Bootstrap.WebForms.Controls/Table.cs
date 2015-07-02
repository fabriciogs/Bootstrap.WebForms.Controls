using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Linq;
using System.Web;
using System.Security.Permissions;

namespace Bootstrap.WebForms.Controls
{
    [AspNetHostingPermission(SecurityAction.Demand, Level = AspNetHostingPermissionLevel.Minimal)]
    [ToolboxData(@"<{0}:Table runat=""server"" />")]
    [ParseChildren(true, "Footer")]
    [PersistChildren(false)]
    public class Table : DataBoundControl, INamingContainer
    {
        private System.Web.UI.WebControls.Table table = new System.Web.UI.WebControls.Table();
        private List<BoundColumn> _Columns = new List<BoundColumn>();

        /// <summary>Initializes a new instance of the <see cref="Table" /> class.</summary>
        public Table()
        {
            this.Striped = false;
            this.Bordered = false;
            this.Hover = false;
            this.Condensed = false;
        }

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

        /// <summary>Gets the tab pages.</summary>
        /// <value>The tab pages.</value>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        public List<BoundColumn> Columns
        {
            get { return _Columns; }
        }

        /// <summary>Gets or sets the content.</summary>
        /// <value>The content.</value>
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [TemplateContainer(typeof(Table))]
        [TemplateInstance(TemplateInstance.Single)]
        public virtual ITemplate Footer { get; set; }

        [Category("Appearance")]
        [DefaultValue(false)]
        public bool Striped
        {
            get { return (bool)ViewState["Striped"]; }
            set { ViewState["Striped"] = value; }
        }

        [Category("Appearance")]
        [DefaultValue(false)]
        public bool Bordered
        {
            get { return (bool)ViewState["Bordered"]; }
            set { ViewState["Bordered"] = value; }
        }

        [Category("Appearance")]
        [DefaultValue(false)]
        public bool Hover
        {
            get { return (bool)ViewState["Hover"]; }
            set { ViewState["Hover"] = value; }
        }

        [Category("Appearance")]
        [DefaultValue(false)]
        public bool Condensed
        {
            get { return (bool)ViewState["Condensed"]; }
            set { ViewState["Condensed"] = value; }
        }

        [Category("Appearance")]
        [DefaultValue(null)]
        public Paginator Pagination { get; set; }

        /// <summary>Sets the paginator.</summary>
        /// <param name="retrievedData">The retrieved data.</param>
        private void SetPaginator(ref System.Collections.IEnumerable retrievedData)
        {
            if (this.Pagination == null)
                return;

            var intItemCount = retrievedData.Cast<object>().Count();
            if (intItemCount == 0)
                throw new NullReferenceException("DataSource is empty.");
            else
                this.Pagination.ItemCount = intItemCount;

            retrievedData = retrievedData.Cast<object>().Skip(this.Pagination.PageSize * this.Pagination.CurrentPageIndex).Take(this.Pagination.PageSize);
        }

        /// <summary>Performs the data binding.</summary>
        /// <param name="retrievedData">The retrieved data.</param>
        protected override void PerformDataBinding(System.Collections.IEnumerable retrievedData)
        {
            table.Rows.Clear();

            base.PerformDataBinding(retrievedData);

            if (this.Columns.Count == 0)
                throw new Exception("List of columns is null or empty.");

            if (retrievedData == null)
                return;

            this.SetPaginator(ref retrievedData);

            TableRow row;
            var rowHeader = new TableRow();
            rowHeader.TableSection = TableRowSection.TableHeader;

            foreach (var boundColumn in this.Columns)
            {
                if (!DesignMode && TypeDescriptor.GetProperties(retrievedData.Cast<object>().First()).Find(boundColumn.FieldName, false) == null)
                    throw new NullReferenceException(String.Format("Column with name '{0}' not founded in datasource.", boundColumn.FieldName));

                var cellHeader = new TableHeaderCell() { Text = boundColumn.Header };
                rowHeader.Cells.Add(cellHeader);
            }

            table.Rows.Add(rowHeader);

            foreach (object dataItem in retrievedData)
            {
                row = new TableRow();
                row.TableSection = TableRowSection.TableBody;

                foreach (BoundColumn boundColumn in this.Columns)
                {
                    var prop = TypeDescriptor.GetProperties(dataItem).Find(boundColumn.FieldName, false);
                    if (prop == null)
                    {
                        continue;
                    }

                    if (prop.GetValue(dataItem) == null)
                    {
                        row.Cells.Add(new TableCell());
                        continue;
                    }

                    switch (boundColumn.GetType().FullName)
                    {

                        case "Luzes.Core.UI.Web.Controls.Bootstrap.HyperlinkColumn":
                            row.Cells.Add(CreateHyperlinkColumn(prop, dataItem, (HyperlinkColumn)boundColumn));
                            break;

                        case "Luzes.Core.UI.Web.Controls.Bootstrap.DateColumn":
                            row.Cells.Add(CreateDateColumn(prop, dataItem, (DateColumn)boundColumn));
                            break;

                        case "Luzes.Core.UI.Web.Controls.Bootstrap.CheckBoxColumn":
                            row.Cells.Add(CreateCheckBoxColumn(prop, dataItem, (CheckBoxColumn)boundColumn));
                            break;

                        default:
                            row.Cells.Add(CreateColumn(prop, dataItem, boundColumn));
                            break;
                    }
                }

                table.Rows.Add(row);
            }
        }

        /// <summary>Raises the <see cref="E:System.Web.UI.Control.Init" /> event.</summary>
        /// <param name="e">An <see cref="T:System.EventArgs" /> object that contains the event data.</param>
        protected override void OnInit(System.EventArgs e)
        {
            base.OnInit(e);

            // Initialize all child controls.
            this.CreateChildControls();
            this.ChildControlsCreated = true;
        }

        /// <summary>Called by the ASP.NET page framework to notify server controls that use composition-based implementation to create any child controls they contain in preparation for posting back or rendering.</summary>
        protected override void CreateChildControls()
        {
            this.AddCssClass(this.CssClass);
            this.AddCssClass("table");

            if (this.Striped) this.AddCssClass("table-striped");
            if (this.Bordered) this.AddCssClass("table-bordered");
            if (this.Hover) this.AddCssClass("table-hover");
            if (this.Condensed) this.AddCssClass("table-condensed");

            table.ID = this.ID;
            table.CssClass = this.sCssClass;

            var footer = new Control();
            if (this.Footer != null) this.Footer.InstantiateIn(footer);

            this.Controls.Clear();
            this.Controls.Add(table);
            if (this.Footer != null) this.Controls.Add(footer);
        }

        /// <summary>Notifies the server control that an element, either XML or HTML, was parsed, and adds the element to the server control's <see cref="T:System.Web.UI.ControlCollection" /> object.</summary>
        /// <param name="obj">An <see cref="T:System.Object" /> that represents the parsed element.</param>
        protected override void AddParsedSubObject(object obj)
        {
            if (obj is BoundColumn)
            {
                Columns.Add((BoundColumn)obj);
                return;
            }
        }

        #region Create Columns methods

        /// <summary>Creates the column.</summary>
        /// <param name="prop">The prop.</param>
        /// <param name="dataItem">The data item.</param>
        /// <param name="column">The column.</param>
        /// <returns></returns>
        private TableCell CreateColumn(PropertyDescriptor prop, object dataItem, BoundColumn column)
        {
            var cell = new TableCell() { Text = prop.GetValue(dataItem).ToString() };

            if (column.Width.HasValue)
            {
                cell.Width = column.Width.Value;
            }

            return cell;
        }

        /// <summary>
        /// Creates the check box column.
        /// </summary>
        /// <param name="prop">The prop.</param>
        /// <param name="dataItem">The data item.</param>
        /// <param name="column">The column.</param>
        /// <returns></returns>
        private TableCell CreateCheckBoxColumn(PropertyDescriptor prop, object dataItem, CheckBoxColumn column)
        {
            var cell = new TableCell();

            var checkBox = new CheckBox() { Text = "" };
            checkBox.Checked = (bool)prop.GetValue(dataItem);
            cell.Controls.Add(checkBox);

            if (column.Width.HasValue)
            {
                cell.Width = column.Width.Value;
            }

            return cell;
        }

        /// <summary>
        /// Creates the date column.
        /// </summary>
        /// <param name="prop">The prop.</param>
        /// <param name="dataItem">The data item.</param>
        /// <param name="column">The column.</param>
        /// <returns></returns>
        private TableCell CreateDateColumn(PropertyDescriptor prop, object dataItem, DateColumn column)
        {
            var cell = new TableCell() { Text = String.Format(column.DateFormatString, prop.GetValue(dataItem).ToString()) };

            if (column.Width.HasValue)
            {
                cell.Width = column.Width.Value;
            }

            return cell;
        }

        /// <summary>
        /// Creates the hyperlink column.
        /// </summary>
        /// <param name="prop">The prop.</param>
        /// <param name="dataItem">The data item.</param>
        /// <param name="column">The column.</param>
        /// <returns></returns>
        private TableCell CreateHyperlinkColumn(PropertyDescriptor prop, object dataItem, HyperlinkColumn column)
        {
            var strNavigationUrl = prop.GetValue(dataItem).ToString();
            strNavigationUrl = string.Format(column.NavigationUrlFormatString, strNavigationUrl);
            strNavigationUrl = this.ResolveClientUrl(strNavigationUrl);

            var a = new HyperLink() { Text = prop.GetValue(dataItem).ToString() };
            a.NavigateUrl = strNavigationUrl;
            a.Target = column.Target;

            var cell = new TableCell();
            cell.Controls.Add(a);

            if (column.Width.HasValue)
            {
                cell.Width = column.Width.Value;
            }

            return cell;
        }

        #endregion
    }

    [ToolboxData(@"<{0}:BoundColumn runat=""server""></{0}:BoundColumn>")]
    [ToolboxItem(false)]
    public class BoundColumn : Control, INamingContainer
    {
        /// <summary>Gets or sets a value indicating whether this <see cref="TabPage" /> is enabled.</summary>
        /// <value><c>true</c> if enabled; otherwise, <c>false</c>.</value>
        [NotifyParentProperty(true)]
        [Browsable(true)]
        [DefaultValue("")]
        public string Header { get; set; }

        /// <summary>Gets or sets the title.</summary>
        /// <value>The title.</value>
        [NotifyParentProperty(true)]
        [Browsable(true)]
        [Localizable(true)]
        [DefaultValue("")]
        public string FieldName { get; set; }

        /// <summary>Gets or sets the width.</summary>
        /// <value>The width.</value>
        [NotifyParentProperty(true)]
        [Browsable(true)]
        [Localizable(true)]
        public Unit? Width { get; set; }
    }

    [ToolboxData(@"<{0}:HyperlinkColumn runat=""server""></{0}:HyperlinkColumn>")]
    [ToolboxItem(false)]
    public class CheckBoxColumn : BoundColumn { }

    [ToolboxData(@"<{0}:DateColumn runat=""server""></{0}:DateColumn>")]
    [ToolboxItem(false)]
    public class DateColumn : BoundColumn
    {
        /// <summary>Initializes a new instance of the <see cref="DateColumn" /> class.</summary>
        public DateColumn()
        {
            this.DateFormatString = "{0:dd/MM/yyyy hh:mm}";
        }

        /// <summary>Gets or sets the date format string.</summary>
        /// <value>The date format string.</value>
        [NotifyParentProperty(true)]
        [Browsable(true)]
        [DefaultValue("{0:dd/MM/yyyy hh:mm}")]
        [UrlProperty]
        public string DateFormatString { get; set; }
    }

    [ToolboxData(@"<{0}:HyperlinkColumn runat=""server""></{0}:HyperlinkColumn>")]
    [ToolboxItem(false)]
    public class HyperlinkColumn : BoundColumn
    {

        /// <summary>Initializes a new instance of the <see cref="HyperlinkColumn" /> class.</summary>
        public HyperlinkColumn()
        {
            this.NavigationUrlFormatString = "{0}";
            this.Target = "_top";
        }

        /// <summary>Gets or sets the navigation URL format string.</summary>
        /// <value>The navigation URL format string.</value>
        [NotifyParentProperty(true)]
        [Browsable(true)]
        [DefaultValue("{0}")]
        [UrlProperty]
        public string NavigationUrlFormatString { get; set; }

        /// <summary>Gets or sets the navigation URL field.</summary>
        /// <value>The navigation URL field.</value>
        [NotifyParentProperty(true)]
        [Browsable(true)]
        [DefaultValue("")]
        [UrlProperty]
        public string NavigationUrlField { get; set; }

        /// <summary>Gets or sets the target.</summary>
        /// <value>The target.</value>
        [NotifyParentProperty(true)]
        [Browsable(true)]
        [DefaultValue("_top")]
        [UrlProperty]
        public string Target { get; set; }
    }
}