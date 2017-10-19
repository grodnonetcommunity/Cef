using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CefSharp;
using CefSharp.SchemeHandler;
using CefSharp.WinForms;

namespace GDNC.Cef
{
    public partial class Form1 : Form
    {
        private readonly ChromiumWebBrowser _browser;

        public Form1()
        {
            InitializeComponent();

            var settings = new CefSettings();

            settings.RegisterScheme(new CefCustomScheme
            {
                SchemeName = "dotnet",
                SchemeHandlerFactory = new FolderSchemeHandlerFactory("dist", "dotnet")
            });

            CefSharp.Cef.Initialize(settings);

            _browser = new ChromiumWebBrowser("dotnet://index.html")
            {
                Dock = DockStyle.Fill
            };
            Controls.Add(_browser);
        }
    }
}
