using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Security.Permissions;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Bootstrap.WebForms.Controls
{
    [AspNetHostingPermission(SecurityAction.Demand, Level = AspNetHostingPermissionLevel.Minimal)]
    [ToolboxData(@"<{0}:LoginForm runat=""server"" />")]
    [ParseChildren(true)]
    [PersistChildren(false)]
    public class LoginForm : Control, INamingContainer
    {
        #region Events

        private static readonly object SubmitEvent = new object();

        public event EventHandler Submit
        {
            add
            {
                Events.AddHandler(SubmitEvent, value);
            }
            remove
            {
                Events.RemoveHandler(SubmitEvent, value);
            }
        }

        protected virtual void OnSubmit(LoginFormSubmitEventArgs e)
        {
            var submitEventDelegate = (EventHandler)Events[SubmitEvent];
            if (submitEventDelegate != null)
                submitEventDelegate(this, e);
        }

        #endregion

        #region Properties

        public string Login { get; set; }
        public string Password { get; set; }

        [DefaultValue(false)]
        public bool LoginFailed { get; set; }

        [DefaultValue("Login")]
        [Localizable(true)]
        public string LoginPlaceHolderText { get; set; }

        [DefaultValue("Senha")]
        [Localizable(true)]
        public string PasswordPlaceHolderText { get; set; }

        [DefaultValue("Usuário ou senha inválidos")]
        [Localizable(true)]
        public string ErrorMessage { get; set; }

        [Bindable(true)]
        [Category("Appearance")]
        [DefaultValue("Log in")]
        [Localizable(true)]
        public string Title { get; set; }

        [Bindable(true)]
        [Category("Appearance")]
        [DefaultValue("Login")]
        [Localizable(true)]
        public string OkButtonText { get; set; }

        //[Bindable(true)]
        //[Category("Appearance")]
        //[DefaultValue("Cancelar")]
        //[Localizable(true)]
        //public string CancelButtonText { get; set; }

        #endregion

        private List<Control> _LoginFieldTemplate = new List<Control>();

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        private List<Control> LoginFieldTemplate
        {
            get { return _LoginFieldTemplate; }
        }

        protected override void OnInit(System.EventArgs e)
        {
            base.OnInit(e);
            this.CreateChildControls();
            this.ChildControlsCreated = true;
        }

        protected override void CreateChildControls()
        {
            this.LoginFieldTemplate.Add(new TextField { PlaceHolder = string.IsNullOrEmpty(this.LoginPlaceHolderText) ? "Login" : this.LoginPlaceHolderText });
            this.LoginFieldTemplate.Add(new PasswordField { PlaceHolder = string.IsNullOrEmpty(this.PasswordPlaceHolderText) ? "Senha" : this.PasswordPlaceHolderText });
            this.LoginFieldTemplate.Add(new Literal());
            foreach (var item in this.LoginFieldTemplate)
                this.Controls.Add(item);

            var loginButton = new LoginButton();
            loginButton.Text = string.IsNullOrEmpty(this.OkButtonText) ? "Login" : this.OkButtonText;
            loginButton.Click += loginButton_Click;
            this.Controls.Add(loginButton);
        }

        void loginButton_Click(object sender, EventArgs e)
        {
            foreach (var item in this.LoginFieldTemplate)
            {
                if (item is TextField)
                    this.Login = ((TextField)item).Text;
                else if (item is PasswordField)
                    this.Password = FormsAuthentication.HashPasswordForStoringInConfigFile(((PasswordField)item).Text, "MD5");
            }

            OnSubmit(new LoginFormSubmitEventArgs(this.Login, this.Password));
        }

        protected override void AddParsedSubObject(object obj)
        {
            if (obj is TextField)
            {
                LoginFieldTemplate.Add((TextField)obj);
                return;
            }
        }

        protected override void Render(HtmlTextWriter writer)
        {
            Page.VerifyRenderingInServerForm(this);

            foreach (var item in this.Controls)
            {
                if (item is Literal)
                {
                    var error = new StringBuilder();
                    error.AppendLine(@"<div class=""alert alert-danger"" role=""alert"">");
                    error.AppendLine(@"<span class=""glyphicon glyphicon-exclamation-sign"" aria-hidden=""true""></span>");
                    error.AppendFormat(@"<span class=""sr-only"">Erro:</span>{0}", string.IsNullOrEmpty(this.ErrorMessage) ? "Usuário ou senha inválidos" : this.ErrorMessage).AppendLine();
                    error.AppendLine("</div>");
                    var lit = (Literal)item;
                    lit.Text = error.ToString();
                    lit.Visible = this.LoginFailed;
                }
            }

            writer.AddAttribute(HtmlTextWriterAttribute.Class, "form-signin");
            writer.AddAttribute("role", "form");
            writer.RenderBeginTag(HtmlTextWriterTag.Div);

            writer.AddAttribute(HtmlTextWriterAttribute.Class, "form-signin-heading");
            writer.RenderBeginTag(HtmlTextWriterTag.H2);
            writer.Write(string.IsNullOrEmpty(this.Title) ? "Log in" : this.Title);
            writer.RenderEndTag();

            base.Render(writer);

            writer.RenderEndTag();
        }
    }

    public class LoginFormSubmitEventArgs : EventArgs
    {
        public string Login { get; set; }
        public string Password { get; set; }

        public LoginFormSubmitEventArgs(string _login, string _password)
        {
            this.Login = _login;
            this.Password = _password;
        }
    }

    [ToolboxItem(false)]
    public class TextField : TextBox
    {
        public TextField()
        {
            this.Required = true;
            this.AutoFocus = true;
            //this.GlyphiconType = GlyphiconTypes.User;
        }
    }

    [ToolboxItem(false)]
    public class PasswordField : TextBox
    {
        public PasswordField()
        {
            this.TextMode = TextBoxMode.Password;
            this.Required = true;
            //this.GlyphiconType = GlyphiconTypes.Lock;
        }
    }

    [ToolboxItem(false)]
    public class LoginButton : Button, IPostBackEventHandler
    {
        public event EventHandler Click;

        protected virtual void OnClick(EventArgs e)
        {
            if (Click != null) Click(this, e);
        }

        public void RaisePostBackEvent(string eventArgument)
        {
            OnClick(new EventArgs());
        }

        protected override void Render(HtmlTextWriter output)
        {
            output.Write(string.Format(@"<input type=""submit"" class=""btn btn-lg btn-primary btn-block"" name=""{0}"" value=""{1}"" />", this.UniqueID, this.Text));
        }
    }
}