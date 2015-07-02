using System.ComponentModel;

namespace Bootstrap.WebForms.Controls
{
    [Description("Use any of the available button classes to quickly create a styled button.")]
    public enum ButtonTypes
    {
        [Description("Standard button")]
        Default = 0,

        [Description("Provides extra visual weight and identifies the primary action in a set of buttons")]
        Primary = 1,

        [Description("Indicates a successful or positive action")]
        Success = 2,

        [Description("Contextual button for informational alert messages")]
        Info = 3,

        [Description("Indicates caution should be taken with this action")]
        Warning = 4,

        [Description("Indicates a dangerous or potentially negative action")]
        Danger = 5,

        [Description("Deemphasize a button by making it look like a link while maintaining button behavior")]
        Link = 6
    }

    public enum AlertTypes
    {
        Warning = 1,
        Danger = 2,
        Success = 3,
        Info = 4
    }

    public enum ImageTypes
    {
        None = 0,
        Rounded = 1,
        Circle = 2,
        Thumbnail = 3
    }

    public enum LabelTypes
    {
        Default = 0,
        Primary = 1,
        Success = 2,
        Info = 3,
        Warning = 4,
        Danger = 5,
    }

    public enum PanelTypes
    {
        Primary = 0,
        Success = 1,
        Info = 2,
        Warning = 3,
        Danger = 4
    }

    public enum ProgressBarStyles
    {
        Info = 0,
        Success = 1,
        Warning = 2,
        Danger = 3
    }

    public enum TitleTypes
    {
        H1 = 0,
        H2 = 1,
        H3 = 2,
        H4 = 3,
        H5 = 4,
        H6 = 5
    }

    public enum WellTypes
    {
        Normal = 0,
        Small = 1,
        Large = 2
    }

    public enum GlyphiconTypes
    {
        None = 0,
        Asterisk = 1,
        Plus = 2,
        Euro = 3,
        Minus = 4,
        Cloud = 5,
        Envelope = 6,
        Pencil = 7,
        Glass = 8,
        Music = 9,
        Search = 10,
        Heart = 11,
        Star = 12,
        StarEmpty = 13,
        User = 14,
        Film = 15,
        ThLarge = 16,
        Th = 17,
        ThList = 18,
        Ok = 19,
        Remove = 20,
        ZoomIn = 21,
        ZoomOut = 22,
        Off = 23,
        Signal = 24,
        Cog = 25,
        Trash = 26,
        Home = 27,
        File = 28,
        Time = 29,
        Road = 30,
        DownloadAlt = 31,
        Download = 32,
        Upload = 33,
        Inbox = 34,
        PlayCircle = 35,
        Repeat = 36,
        Refresh = 37,
        ListAlt = 38,
        Lock = 39,
        Flag = 40,
        Headphones = 41,
        VolumeOff = 42,
        VolumeDown = 43,
        VolumeUp = 44,
        Qrcode = 45,
        Barcode = 46,
        Tag = 47,
        Tags = 48,
        Book = 49,
        Bookmark = 50,
        Print = 51,
        Camera = 52,
        Font = 53,
        Bold = 54,
        Italic = 55,
        TextHeight = 56,
        TextWidth = 57,
        AlignLeft = 58,
        AlignCenter = 59,
        AlignRight = 60,
        AlignJustify = 61,
        List = 62,
        IndentLeft = 63,
        IndentRight = 64,
        FacetimeVideo = 65,
        Picture = 66,
        MapMarker = 67,
        Adjust = 68,
        Tint = 69,
        Edit = 70,
        Share = 71,
        Check = 72,
        Move = 73,
        StepBackward = 74,
        FastBackward = 75,
        Backward = 76,
        Play = 77,
        Pause = 78,
        Stop = 79,
        Forward = 80,
        FastForward = 81,
        StepForward = 82,
        Eject = 83,
        ChevronLeft = 84,
        ChevronRight = 85,
        PlusSign = 86,
        MinusSign = 87,
        RemoveSign = 88,
        OkSign = 89,
        QuestionSign = 90,
        InfoSign = 91,
        Screenshot = 92,
        RemoveCircle = 93,
        OkCircle = 94,
        BanCircle = 95,
        ArrowLeft = 96,
        ArrowRight = 97,
        ArrowUp = 98,
        ArrowDown = 99,
        ShareAlt = 100,
        ResizeFull = 101,
        ResizeSmall = 102,
        ExclamationSign = 103,
        Gift = 104,
        Leaf = 105,
        Fire = 106,
        EyeOpen = 107,
        EyeClose = 108,
        WarningSign = 109,
        Plane = 110,
        Calendar = 111,
        Random = 112,
        Comment = 113,
        Magnet = 114,
        ChevronUp = 115,
        ChevronDown = 116,
        Retweet = 117,
        ShoppingCart = 118,
        FolderClose = 119,
        FolderOpen = 120,
        ResizeVertical = 121,
        ResizeHorizontal = 122,
        Hdd = 123,
        Bullhorn = 124,
        Bell = 125,
        Certificate = 126,
        ThumbsUp = 127,
        ThumbsDown = 128,
        HandRight = 129,
        HandLeft = 130,
        HandUp = 131,
        HandDown = 132,
        CircleArrowRight = 133,
        CircleArrowLeft = 134,
        CircleArrowUp = 135,
        CircleArrowDown = 136,
        Globe = 137,
        Wrench = 138,
        Tasks = 139,
        Filter = 140,
        Briefcase = 141,
        Fullscreen = 142,
        Dashboard = 143,
        Paperclip = 144,
        HeartEmpty = 145,
        Link = 146,
        Phone = 147,
        Pushpin = 148,
        Usd = 149,
        Gbp = 150,
        Sort = 151,
        SortByAlphabet = 152,
        SortByAlphabetAlt = 153,
        SortByOrder = 154,
        SortByOrderAlt = 155,
        SortByAttributes = 156,
        SortByAttributesAlt = 157,
        Unchecked = 158,
        Expand = 159,
        CollapseDown = 160,
        CollapseUp = 161,
        LogIn = 162,
        Flash = 163,
        LogOut = 164,
        NewWindow = 165,
        Record = 166,
        Save = 167,
        Open = 168,
        Saved = 169,
        Import = 170,
        Export = 171,
        Send = 172,
        FloppyDisk = 173,
        FloppySaved = 174,
        FloppyRemove = 175,
        FloppySave = 176,
        FloppyOpen = 177,
        CreditCard = 178,
        Transfer = 179,
        Cutlery = 180,
        Header = 181,
        Compressed = 182,
        Earphone = 183,
        PhoneAlt = 184,
        Tower = 185,
        Stats = 186,
        SdVideo = 187,
        HdVideo = 188,
        Subtitles = 189,
        SoundStereo = 190,
        SoundDolby = 191,
        Sound51 = 192,
        Sound61 = 193,
        Sound71 = 194,
        CopyrightMark = 195,
        RegistrationMark = 196,
        CloudDownload = 197,
        CloudUpload = 198,
        TreeConifer = 199,
        TreeDeciduous = 200
    }
}