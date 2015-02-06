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
        /* 在这下面定义变量
         * 注意访问权限
         */
        private EngineCore.Base.FPSCounter FPSObj;
        private Debug Debug_Obj;
        private EngineCore.STG.STGCore STG;

        float VValue = (float)Math.PI / 4f;
        float HValue = 0f;
        float TValue = 0f;
        float XValue = 960f;
        float YValue = 1035f;
        float ZValue = -150f;

        public Launcher()
        {
            InitializeComponent();
        }

        private void Launcher_Load(object sender, EventArgs e)
        {
            /* 将来这里可以进行启动器和类的初始化
             * 现在没必要这么做
             * 将来可以执行诸如网络检测等的事情
             */
            FPSObj = new EngineCore.Base.FPSCounter();
            Debug_Obj = new Debug();
            STG = new EngineCore.STG.STGCore(15, 30, 960, 100, 960, 960, 960, 100, 200, 10);
        }

        private void Launch_Click(object sender, EventArgs e)
        {
            /* 显示设置以及其他
             * 目前只有显示设置
             */

            this.Hide();

            /* 显示一个‘加载中’的窗口
             */
            Loading Load_Obj = new Loading();
            Load_Obj.Show();

            if (FullScreen.Checked)
            {
                if (DMode1.Checked)
                    EngineCore.DxCS.DxInit(NsOL.Properties.Resources.icon_s.Handle, NsOL.Properties.Resources.GameTitle, true, 1280, 720);
                if (DMode2.Checked)
                    EngineCore.DxCS.DxInit(NsOL.Properties.Resources.icon_s.Handle, NsOL.Properties.Resources.GameTitle, true, 1600, 900);
                if (DMode3.Checked)
                    EngineCore.DxCS.DxInit(NsOL.Properties.Resources.icon_s.Handle, NsOL.Properties.Resources.GameTitle, true, 1920, 1080);
                if (DMode4.Checked)
                    EngineCore.DxCS.DxInit(NsOL.Properties.Resources.icon_s.Handle, NsOL.Properties.Resources.GameTitle, true, 2560, 1440);
            }
            else
            {
                if (DMode1.Checked)
                    EngineCore.DxCS.DxInit(NsOL.Properties.Resources.icon_s.Handle, NsOL.Properties.Resources.GameTitle, false, 1280, 720);
                if (DMode2.Checked)
                    EngineCore.DxCS.DxInit(NsOL.Properties.Resources.icon_s.Handle, NsOL.Properties.Resources.GameTitle, false, 1600, 900);
                if (DMode3.Checked)
                    EngineCore.DxCS.DxInit(NsOL.Properties.Resources.icon_s.Handle, NsOL.Properties.Resources.GameTitle, false, 1920, 1080);
                if (DMode4.Checked)
                    EngineCore.DxCS.DxInit(NsOL.Properties.Resources.icon_s.Handle, NsOL.Properties.Resources.GameTitle, false, 2560, 1440);
            }

            /* 资源加载函数调用
             * 似乎必须要加载窗口后才能调用加载函数？
             */

            if (!(ResourcesLoad()))
            {
                MessageBox.Show(NsOL.Properties.Resources.ResNoticeBody, NsOL.Properties.Resources.ResNoticeHead, MessageBoxButtons.OK);
                goto ExitFlag;
            }

            /* 关闭Loading窗体
             */
            Load_Obj.Dispose();

            /* 开启Debug窗体
             */
            //Debug_Obj.Initilize("V:", "H:", "T:");
            //Debug_Obj.Show();

            /* 相机设置相关
             */
            DxLibDLL.DX.SetCameraPositionAndAngle(DxLibDLL.DX.VGet(960f, 1035f, -150f), (float)Math.PI / 4f, 0, 0);
            DxLibDLL.DX.SetGlobalAmbientLight(DxLibDLL.DX.GetColorF(1f, 1f, 1f, 1f));
            DxLibDLL.DX.SetLightDirection(DxLibDLL.DX.VGet(0f, 1f, -1f));

            /* 启动STG线程
             */
            STG.Run();

            while (!((DxLibDLL.DX.ProcessMessage() == -1) | EngineCore.Base.GetKey(Keys.Escape)))
            {
                /* 主绘制循环
                 * 游戏相关实时计算也应该在这里面
                 * 对循环跳出的控制应该细化
                 */
                EngineCore.DxCS.DxFlip();

                /* 视角测试
                 */
                //DebugCamera();

                /* 主游戏循环
                 */
                MainRender();

                /* 参考线绘制
                 */
                //RefLine();

                /* FPS绘制
                 */
                FPSObj.Update();
                EngineCore.DxCS.DrawString(Math.Round(FPSObj.GetFPS(), 2) + " fps", 1800, 1060, 20);
                FPSObj.WaitTime();
            }

        ExitFlag:
            STG.Stop();
            EngineCore.DxCS.DxEnd();
            this.Dispose();
        }

        private bool ResourcesLoad()
        {
            /* 资源加载函数
             * 注意检查加载是否成功
             * 不成功应当返回false
             * 并处理这个错误
             */

            return true;
        }

        private void MainRender()
        {
            /* 主绘制函数
             * 其他的工作可以试试多线程
             */
            STG.MainCircle();
            STG.GetInfo(1800, 1000);
            
        }

        private void DebugCamera()
        {
            /* 相机测试
             */
            //float VValue = (float)Math.PI / 4f;
            //float HValue = 0f;
            //float TValue = 0f;
            //float XValue = 960f;
            //float YValue = 1035f;
            //float ZValue = 150f;

            if (EngineCore.Base.GetKey(Keys.Q)) VValue += 0.01f;
            if (EngineCore.Base.GetKey(Keys.A)) VValue -= 0.01f;

            if (EngineCore.Base.GetKey(Keys.W)) HValue += 0.01f;
            if (EngineCore.Base.GetKey(Keys.S)) HValue -= 0.01f;

            if (EngineCore.Base.GetKey(Keys.E)) TValue += 0.01f;
            if (EngineCore.Base.GetKey(Keys.D)) TValue -= 0.01f;


            if (EngineCore.Base.GetKey(Keys.ShiftKey))
            {
                if (EngineCore.Base.GetKey(Keys.R)) YValue += 1f;
                if (EngineCore.Base.GetKey(Keys.F)) YValue -= 1f;

                if (EngineCore.Base.GetKey(Keys.T)) ZValue += 1f;
                if (EngineCore.Base.GetKey(Keys.G)) ZValue -= 1f;
            }
            else
            {
                if (EngineCore.Base.GetKey(Keys.R)) YValue += 10f;
                if (EngineCore.Base.GetKey(Keys.F)) YValue -= 10f;

                if (EngineCore.Base.GetKey(Keys.T)) ZValue += 10f;
                if (EngineCore.Base.GetKey(Keys.G)) ZValue -= 10f;
            }

            DxLibDLL.DX.SetCameraPositionAndAngle(DxLibDLL.DX.VGet(XValue, YValue, ZValue), VValue, HValue, TValue);

            EngineCore.DxCS.DrawString(
                    "V:" + VValue.ToString() + " " +
                    "H:" + HValue.ToString() + " " +
                    "T:" + TValue.ToString() + " " +
                    "Y:" + YValue.ToString() + " " +
                    "Z:" + ZValue.ToString(), 0, 1040, 18);
        }

        private void RefLine()
        {
            DxLibDLL.DX.DrawLine3D(DxLibDLL.DX.VGet(200, 200, 200), DxLibDLL.DX.VGet(1000, 200, 200), DxLibDLL.DX.GetColor(255, 0, 0));
            DxLibDLL.DX.DrawLine3D(DxLibDLL.DX.VGet(200, 200, 200), DxLibDLL.DX.VGet(200, 1000, 200), DxLibDLL.DX.GetColor(0, 255, 0));
            DxLibDLL.DX.DrawLine3D(DxLibDLL.DX.VGet(200, 200, 200), DxLibDLL.DX.VGet(200, 200, 1000), DxLibDLL.DX.GetColor(0, 0, 255));

            DxLibDLL.DX.DrawLine3D(DxLibDLL.DX.VGet(100, 100, 100), DxLibDLL.DX.VGet(1820, 100, 100), DxLibDLL.DX.GetColor(255, 255, 255));
            DxLibDLL.DX.DrawLine3D(DxLibDLL.DX.VGet(1820, 100, 100), DxLibDLL.DX.VGet(1820, 980, 100), DxLibDLL.DX.GetColor(255, 255, 255));
            DxLibDLL.DX.DrawLine3D(DxLibDLL.DX.VGet(100, 100, 100), DxLibDLL.DX.VGet(100, 980, 100), DxLibDLL.DX.GetColor(255, 255, 255));
            DxLibDLL.DX.DrawLine3D(DxLibDLL.DX.VGet(100, 980, 100), DxLibDLL.DX.VGet(1820, 980, 100), DxLibDLL.DX.GetColor(255, 255, 255));

            DxLibDLL.DX.DrawLine3D(DxLibDLL.DX.VGet(100, 100, 2000), DxLibDLL.DX.VGet(1820, 100, 2000), DxLibDLL.DX.GetColor(255, 255, 255));
            DxLibDLL.DX.DrawLine3D(DxLibDLL.DX.VGet(1820, 100, 2000), DxLibDLL.DX.VGet(1820, 980, 2000), DxLibDLL.DX.GetColor(255, 255, 255));
            DxLibDLL.DX.DrawLine3D(DxLibDLL.DX.VGet(100, 100, 2000), DxLibDLL.DX.VGet(100, 980, 2000), DxLibDLL.DX.GetColor(255, 255, 255));
            DxLibDLL.DX.DrawLine3D(DxLibDLL.DX.VGet(100, 980, 2000), DxLibDLL.DX.VGet(1820, 980, 2000), DxLibDLL.DX.GetColor(255, 255, 255));

            DxLibDLL.DX.DrawLine3D(DxLibDLL.DX.VGet(100, 100, 100), DxLibDLL.DX.VGet(100, 100, 2000), DxLibDLL.DX.GetColor(255, 255, 255));
            DxLibDLL.DX.DrawLine3D(DxLibDLL.DX.VGet(1820, 100, 100), DxLibDLL.DX.VGet(1820, 100, 2000), DxLibDLL.DX.GetColor(255, 255, 255));
            DxLibDLL.DX.DrawLine3D(DxLibDLL.DX.VGet(1820, 980, 100), DxLibDLL.DX.VGet(1820, 980, 2000), DxLibDLL.DX.GetColor(255, 255, 255));
            DxLibDLL.DX.DrawLine3D(DxLibDLL.DX.VGet(100, 980, 100), DxLibDLL.DX.VGet(100, 980, 2000), DxLibDLL.DX.GetColor(255, 255, 255));
        }
    }
}
