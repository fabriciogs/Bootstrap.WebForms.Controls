using System.ComponentModel;
using System.Security.Permissions;
using System.Web;
using System.Web.UI;


namespace Bootstrap.WebForms.Controls
{
    [AspNetHostingPermission(SecurityAction.Demand, Level = AspNetHostingPermissionLevel.Minimal)]
    [ToolboxData(@"<{0}:Glyphicon runat=""server"" />")]
    public class Glyphicon : System.Web.UI.WebControls.Label
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

        public Glyphicon()
        {
            this.GlyphiconType = GlyphiconTypes.Ok;
        }

        /// <summary>Gets or sets the type of the Glyphicon.</summary>
        /// <value>The type of the Glyphicon.</value>
        [Category("Appearance")]
        [DefaultValue(GlyphiconTypes.Ok)]
        public GlyphiconTypes GlyphiconType
        {
            get { return (GlyphiconTypes)ViewState["GlyphiconTypes"]; }
            set { ViewState["GlyphiconTypes"] = value; }
        }

        /// <summary>Renders the control to the specified HTML writer.</summary>
        /// <param name="writer">The <see cref="T:System.Web.UI.HtmlTextWriter" /> object that receives the control content.</param>
        protected override void Render(HtmlTextWriter writer)
        {
            this.AddCssClass(this.CssClass);

            this.AddCssClass(string.Format("glyphicon {0}", GetGlyphiconCss(this.GlyphiconType)));

            if (!string.IsNullOrEmpty(this.sCssClass))
                writer.AddAttribute(HtmlTextWriterAttribute.Class, this.sCssClass);

            base.Render(writer);
        }

        public static string GetGlyphiconCss(GlyphiconTypes glyphiconType)
        {
            var glyphicon = string.Empty;

            switch (glyphiconType)
            {
                case GlyphiconTypes.Asterisk:
                    glyphicon = "glyphicon-asterisk";
                    break;
                case GlyphiconTypes.Plus:
                    glyphicon = "glyphicon-plus";
                    break;
                case GlyphiconTypes.Euro:
                    glyphicon = "glyphicon-euro";
                    break;
                case GlyphiconTypes.Minus:
                    glyphicon = "glyphicon-minus";
                    break;
                case GlyphiconTypes.Cloud:
                    glyphicon = "glyphicon-cloud";
                    break;
                case GlyphiconTypes.Envelope:
                    glyphicon = "glyphicon-envelope";
                    break;
                case GlyphiconTypes.Pencil:
                    glyphicon = "glyphicon-pencil";
                    break;
                case GlyphiconTypes.Glass:
                    glyphicon = "glyphicon-glass";
                    break;
                case GlyphiconTypes.Music:
                    glyphicon = "glyphicon-music";
                    break;
                case GlyphiconTypes.Search:
                    glyphicon = "glyphicon-search";
                    break;
                case GlyphiconTypes.Heart:
                    glyphicon = "glyphicon-heart";
                    break;
                case GlyphiconTypes.Star:
                    glyphicon = "glyphicon-star";
                    break;
                case GlyphiconTypes.StarEmpty:
                    glyphicon = "glyphicon-star-empty";
                    break;
                case GlyphiconTypes.User:
                    glyphicon = "glyphicon-user";
                    break;
                case GlyphiconTypes.Film:
                    glyphicon = "glyphicon-film";
                    break;
                case GlyphiconTypes.ThLarge:
                    glyphicon = "glyphicon-th-large";
                    break;
                case GlyphiconTypes.Th:
                    glyphicon = "glyphicon-th";
                    break;
                case GlyphiconTypes.ThList:
                    glyphicon = "glyphicon-th-list";
                    break;
                case GlyphiconTypes.Ok:
                    glyphicon = "glyphicon-ok";
                    break;
                case GlyphiconTypes.Remove:
                    glyphicon = "glyphicon-remove";
                    break;
                case GlyphiconTypes.ZoomIn:
                    glyphicon = "glyphicon-zoom-in";
                    break;
                case GlyphiconTypes.ZoomOut:
                    glyphicon = "glyphicon-zoom-out";
                    break;
                case GlyphiconTypes.Off:
                    glyphicon = "glyphicon-off";
                    break;
                case GlyphiconTypes.Signal:
                    glyphicon = "glyphicon-signal";
                    break;
                case GlyphiconTypes.Cog:
                    glyphicon = "glyphicon-cog";
                    break;
                case GlyphiconTypes.Trash:
                    glyphicon = "glyphicon-trash";
                    break;
                case GlyphiconTypes.Home:
                    glyphicon = "glyphicon-home";
                    break;
                case GlyphiconTypes.File:
                    glyphicon = "glyphicon-file";
                    break;
                case GlyphiconTypes.Time:
                    glyphicon = "glyphicon-time";
                    break;
                case GlyphiconTypes.Road:
                    glyphicon = "glyphicon-road";
                    break;
                case GlyphiconTypes.DownloadAlt:
                    glyphicon = "glyphicon-download-alt";
                    break;
                case GlyphiconTypes.Download:
                    glyphicon = "glyphicon-download";
                    break;
                case GlyphiconTypes.Upload:
                    glyphicon = "glyphicon-upload";
                    break;
                case GlyphiconTypes.Inbox:
                    glyphicon = "glyphicon-inbox";
                    break;
                case GlyphiconTypes.PlayCircle:
                    glyphicon = "glyphicon-play-circle";
                    break;
                case GlyphiconTypes.Repeat:
                    glyphicon = "glyphicon-repeat";
                    break;
                case GlyphiconTypes.Refresh:
                    glyphicon = "glyphicon-refresh";
                    break;
                case GlyphiconTypes.ListAlt:
                    glyphicon = "glyphicon-list-alt";
                    break;
                case GlyphiconTypes.Lock:
                    glyphicon = "glyphicon-lock";
                    break;
                case GlyphiconTypes.Flag:
                    glyphicon = "glyphicon-flag";
                    break;
                case GlyphiconTypes.Headphones:
                    glyphicon = "glyphicon-headphones";
                    break;
                case GlyphiconTypes.VolumeOff:
                    glyphicon = "glyphicon-volume-off";
                    break;
                case GlyphiconTypes.VolumeDown:
                    glyphicon = "glyphicon-volume-down";
                    break;
                case GlyphiconTypes.VolumeUp:
                    glyphicon = "glyphicon-volume-up";
                    break;
                case GlyphiconTypes.Qrcode:
                    glyphicon = "glyphicon-qrcode";
                    break;
                case GlyphiconTypes.Barcode:
                    glyphicon = "glyphicon-barcode";
                    break;
                case GlyphiconTypes.Tag:
                    glyphicon = "glyphicon-tag";
                    break;
                case GlyphiconTypes.Tags:
                    glyphicon = "glyphicon-tags";
                    break;
                case GlyphiconTypes.Book:
                    glyphicon = "glyphicon-book";
                    break;
                case GlyphiconTypes.Bookmark:
                    glyphicon = "glyphicon-bookmark";
                    break;
                case GlyphiconTypes.Print:
                    glyphicon = "glyphicon-print";
                    break;
                case GlyphiconTypes.Camera:
                    glyphicon = "glyphicon-camera";
                    break;
                case GlyphiconTypes.Font:
                    glyphicon = "glyphicon-font";
                    break;
                case GlyphiconTypes.Bold:
                    glyphicon = "glyphicon-bold";
                    break;
                case GlyphiconTypes.Italic:
                    glyphicon = "glyphicon-italic";
                    break;
                case GlyphiconTypes.TextHeight:
                    glyphicon = "glyphicon-text-height";
                    break;
                case GlyphiconTypes.TextWidth:
                    glyphicon = "glyphicon-text-width";
                    break;
                case GlyphiconTypes.AlignLeft:
                    glyphicon = "glyphicon-align-left";
                    break;
                case GlyphiconTypes.AlignCenter:
                    glyphicon = "glyphicon-align-center";
                    break;
                case GlyphiconTypes.AlignRight:
                    glyphicon = "glyphicon-align-right";
                    break;
                case GlyphiconTypes.AlignJustify:
                    glyphicon = "glyphicon-align-justify";
                    break;
                case GlyphiconTypes.List:
                    glyphicon = "glyphicon-list";
                    break;
                case GlyphiconTypes.IndentLeft:
                    glyphicon = "glyphicon-indent-left";
                    break;
                case GlyphiconTypes.IndentRight:
                    glyphicon = "glyphicon-indent-right";
                    break;
                case GlyphiconTypes.FacetimeVideo:
                    glyphicon = "glyphicon-facetime-video";
                    break;
                case GlyphiconTypes.Picture:
                    glyphicon = "glyphicon-picture";
                    break;
                case GlyphiconTypes.MapMarker:
                    glyphicon = "glyphicon-map-marker";
                    break;
                case GlyphiconTypes.Adjust:
                    glyphicon = "glyphicon-adjust";
                    break;
                case GlyphiconTypes.Tint:
                    glyphicon = "glyphicon-tint";
                    break;
                case GlyphiconTypes.Edit:
                    glyphicon = "glyphicon-edit";
                    break;
                case GlyphiconTypes.Share:
                    glyphicon = "glyphicon-share";
                    break;
                case GlyphiconTypes.Check:
                    glyphicon = "glyphicon-check";
                    break;
                case GlyphiconTypes.Move:
                    glyphicon = "glyphicon-move";
                    break;
                case GlyphiconTypes.StepBackward:
                    glyphicon = "glyphicon-step-backward";
                    break;
                case GlyphiconTypes.FastBackward:
                    glyphicon = "glyphicon-fast-backward";
                    break;
                case GlyphiconTypes.Backward:
                    glyphicon = "glyphicon-backward";
                    break;
                case GlyphiconTypes.Play:
                    glyphicon = "glyphicon-play";
                    break;
                case GlyphiconTypes.Pause:
                    glyphicon = "glyphicon-pause";
                    break;
                case GlyphiconTypes.Stop:
                    glyphicon = "glyphicon-stop";
                    break;
                case GlyphiconTypes.Forward:
                    glyphicon = "glyphicon-forward";
                    break;
                case GlyphiconTypes.FastForward:
                    glyphicon = "glyphicon-fast-forward";
                    break;
                case GlyphiconTypes.StepForward:
                    glyphicon = "glyphicon-step-forward";
                    break;
                case GlyphiconTypes.Eject:
                    glyphicon = "glyphicon-eject";
                    break;
                case GlyphiconTypes.ChevronLeft:
                    glyphicon = "glyphicon-chevron-left";
                    break;
                case GlyphiconTypes.ChevronRight:
                    glyphicon = "glyphicon-chevron-right";
                    break;
                case GlyphiconTypes.PlusSign:
                    glyphicon = "glyphicon-plus-sign";
                    break;
                case GlyphiconTypes.MinusSign:
                    glyphicon = "glyphicon-minus-sign";
                    break;
                case GlyphiconTypes.RemoveSign:
                    glyphicon = "glyphicon-remove-sign";
                    break;
                case GlyphiconTypes.OkSign:
                    glyphicon = "glyphicon-ok-sign";
                    break;
                case GlyphiconTypes.QuestionSign:
                    glyphicon = "glyphicon-question-sign";
                    break;
                case GlyphiconTypes.InfoSign:
                    glyphicon = "glyphicon-info-sign";
                    break;
                case GlyphiconTypes.Screenshot:
                    glyphicon = "glyphicon-screenshot";
                    break;
                case GlyphiconTypes.RemoveCircle:
                    glyphicon = "glyphicon-remove-circle";
                    break;
                case GlyphiconTypes.OkCircle:
                    glyphicon = "glyphicon-ok-circle";
                    break;
                case GlyphiconTypes.BanCircle:
                    glyphicon = "glyphicon-ban-circle";
                    break;
                case GlyphiconTypes.ArrowLeft:
                    glyphicon = "glyphicon-arrow-left";
                    break;
                case GlyphiconTypes.ArrowRight:
                    glyphicon = "glyphicon-arrow-right";
                    break;
                case GlyphiconTypes.ArrowUp:
                    glyphicon = "glyphicon-arrow-up";
                    break;
                case GlyphiconTypes.ArrowDown:
                    glyphicon = "glyphicon-arrow-down";
                    break;
                case GlyphiconTypes.ShareAlt:
                    glyphicon = "glyphicon-share-alt";
                    break;
                case GlyphiconTypes.ResizeFull:
                    glyphicon = "glyphicon-resize-full";
                    break;
                case GlyphiconTypes.ResizeSmall:
                    glyphicon = "glyphicon-resize-small";
                    break;
                case GlyphiconTypes.ExclamationSign:
                    glyphicon = "glyphicon-exclamation-sign";
                    break;
                case GlyphiconTypes.Gift:
                    glyphicon = "glyphicon-gift";
                    break;
                case GlyphiconTypes.Leaf:
                    glyphicon = "glyphicon-leaf";
                    break;
                case GlyphiconTypes.Fire:
                    glyphicon = "glyphicon-fire";
                    break;
                case GlyphiconTypes.EyeOpen:
                    glyphicon = "glyphicon-eye-open";
                    break;
                case GlyphiconTypes.EyeClose:
                    glyphicon = "glyphicon-eye-close";
                    break;
                case GlyphiconTypes.WarningSign:
                    glyphicon = "glyphicon-warning-sign";
                    break;
                case GlyphiconTypes.Plane:
                    glyphicon = "glyphicon-plane";
                    break;
                case GlyphiconTypes.Calendar:
                    glyphicon = "glyphicon-calendar";
                    break;
                case GlyphiconTypes.Random:
                    glyphicon = "glyphicon-random";
                    break;
                case GlyphiconTypes.Comment:
                    glyphicon = "glyphicon-comment";
                    break;
                case GlyphiconTypes.Magnet:
                    glyphicon = "glyphicon-magnet";
                    break;
                case GlyphiconTypes.ChevronUp:
                    glyphicon = "glyphicon-chevron-up";
                    break;
                case GlyphiconTypes.ChevronDown:
                    glyphicon = "glyphicon-chevron-down";
                    break;
                case GlyphiconTypes.Retweet:
                    glyphicon = "glyphicon-retweet";
                    break;
                case GlyphiconTypes.ShoppingCart:
                    glyphicon = "glyphicon-shopping-cart";
                    break;
                case GlyphiconTypes.FolderClose:
                    glyphicon = "glyphicon-folder-close";
                    break;
                case GlyphiconTypes.FolderOpen:
                    glyphicon = "glyphicon-folder-open";
                    break;
                case GlyphiconTypes.ResizeVertical:
                    glyphicon = "glyphicon-resize-vertical";
                    break;
                case GlyphiconTypes.ResizeHorizontal:
                    glyphicon = "glyphicon-resize-horizontal";
                    break;
                case GlyphiconTypes.Hdd:
                    glyphicon = "glyphicon-hdd";
                    break;
                case GlyphiconTypes.Bullhorn:
                    glyphicon = "glyphicon-bullhorn";
                    break;
                case GlyphiconTypes.Bell:
                    glyphicon = "glyphicon-bell";
                    break;
                case GlyphiconTypes.Certificate:
                    glyphicon = "glyphicon-certificate";
                    break;
                case GlyphiconTypes.ThumbsUp:
                    glyphicon = "glyphicon-thumbs-up";
                    break;
                case GlyphiconTypes.ThumbsDown:
                    glyphicon = "glyphicon-thumbs-down";
                    break;
                case GlyphiconTypes.HandRight:
                    glyphicon = "glyphicon-hand-right";
                    break;
                case GlyphiconTypes.HandLeft:
                    glyphicon = "glyphicon-hand-left";
                    break;
                case GlyphiconTypes.HandUp:
                    glyphicon = "glyphicon-hand-up";
                    break;
                case GlyphiconTypes.HandDown:
                    glyphicon = "glyphicon-hand-down";
                    break;
                case GlyphiconTypes.CircleArrowRight:
                    glyphicon = "glyphicon-circle-arrow-right";
                    break;
                case GlyphiconTypes.CircleArrowLeft:
                    glyphicon = "glyphicon-circle-arrow-left";
                    break;
                case GlyphiconTypes.CircleArrowUp:
                    glyphicon = "glyphicon-circle-arrow-up";
                    break;
                case GlyphiconTypes.CircleArrowDown:
                    glyphicon = "glyphicon-circle-arrow-down";
                    break;
                case GlyphiconTypes.Globe:
                    glyphicon = "glyphicon-globe";
                    break;
                case GlyphiconTypes.Wrench:
                    glyphicon = "glyphicon-wrench";
                    break;
                case GlyphiconTypes.Tasks:
                    glyphicon = "glyphicon-tasks";
                    break;
                case GlyphiconTypes.Filter:
                    glyphicon = "glyphicon-filter";
                    break;
                case GlyphiconTypes.Briefcase:
                    glyphicon = "glyphicon-briefcase";
                    break;
                case GlyphiconTypes.Fullscreen:
                    glyphicon = "glyphicon-fullscreen";
                    break;
                case GlyphiconTypes.Dashboard:
                    glyphicon = "glyphicon-dashboard";
                    break;
                case GlyphiconTypes.Paperclip:
                    glyphicon = "glyphicon-paperclip";
                    break;
                case GlyphiconTypes.HeartEmpty:
                    glyphicon = "glyphicon-heart-empty";
                    break;
                case GlyphiconTypes.Link:
                    glyphicon = "glyphicon-link";
                    break;
                case GlyphiconTypes.Phone:
                    glyphicon = "glyphicon-phone";
                    break;
                case GlyphiconTypes.Pushpin:
                    glyphicon = "glyphicon-pushpin";
                    break;
                case GlyphiconTypes.Usd:
                    glyphicon = "glyphicon-usd";
                    break;
                case GlyphiconTypes.Gbp:
                    glyphicon = "glyphicon-gbp";
                    break;
                case GlyphiconTypes.Sort:
                    glyphicon = "glyphicon-sort";
                    break;
                case GlyphiconTypes.SortByAlphabet:
                    glyphicon = "glyphicon-sort-by-alphabet";
                    break;
                case GlyphiconTypes.SortByAlphabetAlt:
                    glyphicon = "glyphicon-sort-by-alphabet-alt";
                    break;
                case GlyphiconTypes.SortByOrder:
                    glyphicon = "glyphicon-sort-by-order";
                    break;
                case GlyphiconTypes.SortByOrderAlt:
                    glyphicon = "glyphicon-sort-by-order-alt";
                    break;
                case GlyphiconTypes.SortByAttributes:
                    glyphicon = "glyphicon-sort-by-attributes";
                    break;
                case GlyphiconTypes.SortByAttributesAlt:
                    glyphicon = "glyphicon-sort-by-attributes-alt";
                    break;
                case GlyphiconTypes.Unchecked:
                    glyphicon = "glyphicon-unchecked";
                    break;
                case GlyphiconTypes.Expand:
                    glyphicon = "glyphicon-expand";
                    break;
                case GlyphiconTypes.CollapseDown:
                    glyphicon = "glyphicon-collapse-down";
                    break;
                case GlyphiconTypes.CollapseUp:
                    glyphicon = "glyphicon-collapse-up";
                    break;
                case GlyphiconTypes.LogIn:
                    glyphicon = "glyphicon-log-in";
                    break;
                case GlyphiconTypes.Flash:
                    glyphicon = "glyphicon-flash";
                    break;
                case GlyphiconTypes.LogOut:
                    glyphicon = "glyphicon-log-out";
                    break;
                case GlyphiconTypes.NewWindow:
                    glyphicon = "glyphicon-new-window";
                    break;
                case GlyphiconTypes.Record:
                    glyphicon = "glyphicon-record";
                    break;
                case GlyphiconTypes.Save:
                    glyphicon = "glyphicon-save";
                    break;
                case GlyphiconTypes.Open:
                    glyphicon = "glyphicon-open";
                    break;
                case GlyphiconTypes.Saved:
                    glyphicon = "glyphicon-saved";
                    break;
                case GlyphiconTypes.Import:
                    glyphicon = "glyphicon-import";
                    break;
                case GlyphiconTypes.Export:
                    glyphicon = "glyphicon-export";
                    break;
                case GlyphiconTypes.Send:
                    glyphicon = "glyphicon-send";
                    break;
                case GlyphiconTypes.FloppyDisk:
                    glyphicon = "glyphicon-floppy-disk";
                    break;
                case GlyphiconTypes.FloppySaved:
                    glyphicon = "glyphicon-floppy-saved";
                    break;
                case GlyphiconTypes.FloppyRemove:
                    glyphicon = "glyphicon-floppy-remove";
                    break;
                case GlyphiconTypes.FloppySave:
                    glyphicon = "glyphicon-floppy-save";
                    break;
                case GlyphiconTypes.FloppyOpen:
                    glyphicon = "glyphicon-floppy-open";
                    break;
                case GlyphiconTypes.CreditCard:
                    glyphicon = "glyphicon-credit-card";
                    break;
                case GlyphiconTypes.Transfer:
                    glyphicon = "glyphicon-transfer";
                    break;
                case GlyphiconTypes.Cutlery:
                    glyphicon = "glyphicon-cutlery";
                    break;
                case GlyphiconTypes.Header:
                    glyphicon = "glyphicon-header";
                    break;
                case GlyphiconTypes.Compressed:
                    glyphicon = "glyphicon-compressed";
                    break;
                case GlyphiconTypes.Earphone:
                    glyphicon = "glyphicon-earphone";
                    break;
                case GlyphiconTypes.PhoneAlt:
                    glyphicon = "glyphicon-phone-alt";
                    break;
                case GlyphiconTypes.Tower:
                    glyphicon = "glyphicon-tower";
                    break;
                case GlyphiconTypes.Stats:
                    glyphicon = "glyphicon-stats";
                    break;
                case GlyphiconTypes.SdVideo:
                    glyphicon = "glyphicon-sd-video";
                    break;
                case GlyphiconTypes.HdVideo:
                    glyphicon = "glyphicon-hd-video";
                    break;
                case GlyphiconTypes.Subtitles:
                    glyphicon = "glyphicon-subtitles";
                    break;
                case GlyphiconTypes.SoundStereo:
                    glyphicon = "glyphicon-sound-stereo";
                    break;
                case GlyphiconTypes.SoundDolby:
                    glyphicon = "glyphicon-sound-dolby";
                    break;
                case GlyphiconTypes.Sound51:
                    glyphicon = "glyphicon-sound-5-1";
                    break;
                case GlyphiconTypes.Sound61:
                    glyphicon = "glyphicon-sound-6-1";
                    break;
                case GlyphiconTypes.Sound71:
                    glyphicon = "glyphicon-sound-7-1";
                    break;
                case GlyphiconTypes.CopyrightMark:
                    glyphicon = "glyphicon-copyright-mark";
                    break;
                case GlyphiconTypes.RegistrationMark:
                    glyphicon = "glyphicon-registration-mark";
                    break;
                case GlyphiconTypes.CloudDownload:
                    glyphicon = "glyphicon-cloud-download";
                    break;
                case GlyphiconTypes.CloudUpload:
                    glyphicon = "glyphicon-cloud-upload";
                    break;
                case GlyphiconTypes.TreeConifer:
                    glyphicon = "glyphicon-tree-conifer";
                    break;
                case GlyphiconTypes.TreeDeciduous:
                    glyphicon = "glyphicon-tree-deciduous";
                    break;
                default:
                    break;
            }
            return glyphicon;
        }
    }
}