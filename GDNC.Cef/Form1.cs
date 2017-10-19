using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using CefSharp;
using CefSharp.SchemeHandler;
using CefSharp.WinForms;

namespace GDNC.Cef
{
    public partial class Form1 : Form
    {
        private const int WmSysCommand = 0x112;
        private const int MfSeparator = 0x800;
        private const int MfByposition = 0x400;
        private const int ShowDevToolsSysMenuId = 1000;
        private const int ReloadSysMenuId = 1001;
        private const int ReloadIgnoreCacheSysMenuId = 1002;

        private readonly ChromiumWebBrowser _browser;

        public Form1()
        {
            InitializeCef();
            InitializeComponent();

            _browser = new ChromiumWebBrowser("dotnet://index.html")
            {
                Dock = DockStyle.Fill
            };
            Controls.Add(_browser);
        }

        protected override void OnLoad(EventArgs e)
        {
            var systemMenuHandle = GetSystemMenu(Handle, false);

            InsertMenu(systemMenuHandle, 5, MfByposition | MfSeparator, 0, string.Empty);
            InsertMenu(systemMenuHandle, 6, MfByposition, ShowDevToolsSysMenuId, "Show Dev Tools");
            InsertMenu(systemMenuHandle, 7, MfByposition, ReloadSysMenuId, "Reload Page");
            InsertMenu(systemMenuHandle, 8, MfByposition, ReloadIgnoreCacheSysMenuId, "Reload Page Ignore Cache");
        }

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);

            if (m.Msg == WmSysCommand)
            {
                switch (m.WParam.ToInt32())
                {
                    case ShowDevToolsSysMenuId:
                        _browser.ShowDevTools();
                        break;
                    case ReloadSysMenuId:
                        _browser.Reload();
                        break;
                    case ReloadIgnoreCacheSysMenuId:
                        _browser.Reload(true);
                        break;
                }
            }
        }

        [DllImport("user32.dll")]
        private static extern bool InsertMenu(IntPtr handleMenu, int windowPosition, int windowFlags, int windowIdNewItem, string newItem);

        [DllImport("user32.dll")]
        private static extern IntPtr GetSystemMenu(IntPtr handleWindow, bool revert);

        private static void InitializeCef()
        {
            var settings = new CefSettings();

            settings.RegisterScheme(new CefCustomScheme
            {
                SchemeName = "dotnet",
                SchemeHandlerFactory = new FolderSchemeHandlerFactory("dist", "dotnet")
            });

            CefSharp.Cef.Initialize(settings);
        }
    }
}
