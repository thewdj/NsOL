using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace NsOL
{
    public partial class Launcher : Form
    {
        private Game GameObj;

        public Launcher()
        {
            InitializeComponent();
        }

        private void Launcher_Load(object sender, EventArgs e)
        {

        }

        private void Launch_Click(object sender, EventArgs e)
        {
            this.Hide();

            /* 显示一个‘加载中’的窗口
             */
            Loading Load_Obj = new Loading();
            Load_Obj.Show();

            if (FullScreen.Checked)
            {
                if (DMode1.Checked) if (!EngineCore.DxCS.DxInit(NsOL.Properties.Resources.icon_s.Handle, NsOL.Properties.Resources.GameTitle, true, 1280, 720))
                        goto ExitFlag;
                if (DMode2.Checked) if (!EngineCore.DxCS.DxInit(NsOL.Properties.Resources.icon_s.Handle, NsOL.Properties.Resources.GameTitle, true, 1600, 900))
                        goto ExitFlag;
                if (DMode3.Checked) if (!EngineCore.DxCS.DxInit(NsOL.Properties.Resources.icon_s.Handle, NsOL.Properties.Resources.GameTitle, true, 1920, 1080))
                        goto ExitFlag;
                if (DMode4.Checked) if (!EngineCore.DxCS.DxInit(NsOL.Properties.Resources.icon_s.Handle, NsOL.Properties.Resources.GameTitle, true, 2560, 1440))
                        goto ExitFlag;
            }
            else
            {
                if (DMode1.Checked) if (!EngineCore.DxCS.DxInit(NsOL.Properties.Resources.icon_s.Handle, NsOL.Properties.Resources.GameTitle, false, 1280, 720))
                        goto ExitFlag;
                if (DMode2.Checked) if (!EngineCore.DxCS.DxInit(NsOL.Properties.Resources.icon_s.Handle, NsOL.Properties.Resources.GameTitle, false, 1600, 900))
                        goto ExitFlag;
                if (DMode3.Checked) if (!EngineCore.DxCS.DxInit(NsOL.Properties.Resources.icon_s.Handle, NsOL.Properties.Resources.GameTitle, false, 1920, 1080))
                        goto ExitFlag;
                if (DMode4.Checked) if (!EngineCore.DxCS.DxInit(NsOL.Properties.Resources.icon_s.Handle, NsOL.Properties.Resources.GameTitle, false, 2560, 1440))
                        goto ExitFlag;
            }

            /* 实例化游戏类
             */
            GameObj = new Game();

            /* 关闭Loading窗体
             */
            Load_Obj.Dispose();

            /* 开启Debug窗体
             */
            //Debug_Obj.Initilize("V:", "H:", "T:");
            //Debug_Obj.Show();

            while (!((DxLibDLL.DX.ProcessMessage() == -1) | EngineCore.Base.GetKey(Keys.Escape)))
            {
                GameObj.Work();
            }

        ExitFlag:

            EngineCore.DxCS.DxEnd();
            this.Dispose();
        }
    }
}
