using CefSharp;
using CefSharp.WinForms;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;

namespace Crowium
{

    public partial class Form1 : Form
    {
        //マウスのクリック位置を記憶
        public CefSettings settings;
        public int disph;
        public int dispw;
        private ToolTip toolTip1;
        public object urlchange;
        ChromiumWebBrowser cefBrowser;


        public Form1()
        {


            InitializeComponent();

            int disph = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height;
            //ディスプレイの幅
            int dispw = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width;

            //結果表示


            this.FormBorderStyle = FormBorderStyle.None;

            this.ControlBox = false;
            this.Text = "";

            this.MaximumSize = Screen.PrimaryScreen.WorkingArea.Size;
            this.WindowState = FormWindowState.Maximized;



            CefSettings cefsettings = new CefSettings();
            Cef.Initialize(cefsettings);
            cefBrowser = new ChromiumWebBrowser("https://google.com/");
            cefBrowser.AddressChanged += OnBrowserAddressChanged;
            cefBrowser.Dock = DockStyle.Bottom;
            cefBrowser.Height = disph - 90;
            
            this.Controls.Add(cefBrowser);            

            int screen_width = Screen.PrimaryScreen.WorkingArea.Width;
            int screen_height = Screen.PrimaryScreen.WorkingArea.Height;
            int MenuHeight = screen_height - cefBrowser.Height;

            Debug.Print("ディスプレイの高さ:{0}ピクセル", disph);
            Debug.Print("ディスプレイの幅:{0}ピクセル", dispw);

            Debug.Print(string.Format("スクリーンの作業領域の幅:{0:d}", screen_width));
            Debug.Print(string.Format("スクリーンの作業領域の高さ:{0:d}", screen_height));

            Debug.Print("this.Height:" + this.Height);
            Debug.Print("cefBrowser.Height:" + cefBrowser.Height);
            Debug.Print("disph:" + disph);
            

            CloseButton.Height = MenuHeight;
            CloseButton.Location = new Point(dispw - CloseButton.Width, 0);

            toolTip1.SetToolTip(CloseButton, "閉じる");

            textBox1.Width = dispw - CloseButton.Width - CloseButton.Width;

            this.Text = "Crowium";
        }

        

        private void Form1_Load(object sender, EventArgs e)
        {
            CloseButton.Enabled = true;
        }


        private void CloseButton_Click(object sender, EventArgs e)
        {
            this.Close();
            Application.Exit();
        }

        private void Minimum_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void ChangeURL(object sender, KeyEventArgs e)
        {
            
            switch (e.KeyData)
            {
                case Keys.Enter:
                    try
                    {
                        cefBrowser.Load(textBox1.Text);
                    }
                    catch(Exception er)
                    {
                        Debug.Print(er.ToString());
                    }
                    break;
            }
        }

        private void OnBrowserAddressChanged(object sender, AddressChangedEventArgs e)
        {
            // this.InvokeOnUiThreadIfRequired(() => Text = e.Address);
            this.Invoke(new Action(() => {
                textBox1.Text = e.Address;
                this.Text = "Crowium:" + e.Address;
            }));
        }

    }
}
