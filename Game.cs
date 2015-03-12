using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace NsOL
{
    class Game
    {
        /* 在这下面定义变量
         * 注意访问权限
         */
        private EngineCore.Base.FPSCounter FPSObj;
        private Debug Debug_Obj;
        //private Bullet01 Bullet;
        private SC001 SC001_Obj;

        float VValue = (float)Math.PI / 4f;
        float HValue = 0f;
        float TValue = 0f;
        float XValue = 960f;
        float YValue = 1035f;
        float ZValue = -150f;

        public Game()
        {
            /* 将来这里可以进行子类的初始化
             * 将来可以执行诸如网络检测等的事情
             */
            FPSObj = new EngineCore.Base.FPSCounter();
            Debug_Obj = new Debug();
            //Bullet = new Bullet01(40, 80, 960, 100, 540, 1260, 960, 100, 960, 5, 15, EngineCore.STG.TEX_CIRCLE);
            SC001_Obj = new SC001(10, 10, 10, 20, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 960, 0, 540, 1080, EngineCore.STG.TEX_CIRCLE);

            /* 资源加载函数调用
             * 似乎必须要加载窗口后才能调用加载函数？
             */
            if (!(ResourcesLoad()))
            {
                MessageBox.Show(NsOL.Properties.Resources.ResNoticeBody, NsOL.Properties.Resources.ResNoticeHead, MessageBoxButtons.OK);
            }

            /* 相机设置相关
             */
            //EngineCore.DxCS.Dx3DCamera();


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

        public void Work()
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
            EngineCore.DxCS.DrawString(FPSObj.GetFPS().ToString("f2") + " fps", 1800, 1060, 20);
            FPSObj.WaitTime();
        }

        private void MainRender()
        {
            /* 主绘制函数
             * 其他的工作可以试试多线程
             */
            //Bullet.MainCircle();
            //Bullet.GetInfo(1750, 900);
            SC001_Obj.MainCircle();
            //DebugCamera();
            //RefLine();
        }

        private void DebugCamera()
        {
            /* 相机测试
             */

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

    //class Bullet01 : EngineCore.STG.STGCore
    //{
    //    public Bullet01(int ArrayNum1, int ArrayNum2, double CenterX, double CenterY, double CenterZ, double Range, double PlayerX, double PlayerY, double PlayerZ, double PlayerSize, double BulletSize, int DefaultTex)
    //        : base(ArrayNum1, ArrayNum2, CenterX, CenterY, CenterZ, Range, PlayerX, PlayerY, PlayerZ, PlayerSize, BulletSize, DefaultTex)
    //    {

    //    }

    //    protected override void GenBullet()
    //    {
    //        EngineCore.Base.Point3D TmpPoint;
    //        for (int i = 0; i < ArrayNumA; i++)
    //            for (int j = 0; j < ArrayNumB; j++)
    //            {
    //                if (!Bullets[i, j].IsEnabled)
    //                {
    //                    //TmpPoint.x = Math.Cos(i) + i * Math.Cos(0.25 * j) + CenterPos.GetPosition().x;
    //                    //TmpPoint.y = CenterPos.GetPosition().y;
    //                    //TmpPoint.z = Math.Sin(i) - i * Math.Sin(0.25 * j) + CenterPos.GetPosition().z;
    //                    TmpPoint.x = Math.Cos(i) + j * Math.Cos(0.25 * j) + CenterPos.GetPosition().x;
    //                    TmpPoint.y = CenterPos.GetPosition().y;
    //                    TmpPoint.z = Math.Sin(i) - j * Math.Sin(0.25 * j) + CenterPos.GetPosition().z;
    //                    Bullets[i, j].SetPosition(TmpPoint);
    //                    Bullets[i, j].SetScale(20, 20, 20);
    //                    Bullets[i, j].IsEnabled = true;
    //                }
    //                TmpPoint.x = (Bullets[i, j].GetPosition().x - CenterPos.GetPosition().x) / 100;
    //                TmpPoint.y = 0;
    //                TmpPoint.z = (Bullets[i, j].GetPosition().z - CenterPos.GetPosition().z) / 100;
    //                TmpPoint.x = Bullets[i, j].GetPosition().x + TmpPoint.x;
    //                TmpPoint.y = Bullets[i, j].GetPosition().y + TmpPoint.y;
    //                TmpPoint.z = Bullets[i, j].GetPosition().z + TmpPoint.z;
    //                Bullets[i, j].SetPosition(TmpPoint);

    //            }
    //    }
    //}

    class SC001 : EngineCore.STG.SpellCard
    {
        public SC001(int EnemyArray1, int EnemyArray2, int EnemyBulletArray1, int EnemyBulletArray2, int EnemyObjArray1, int EnemyObjArray2, int EnemyLife,
                                      int BossArray, int BossBulletArray1, int BossBulletArray2, int BossObjArray1, int BossObjArray2, int BossLife,
                                     int PlayerArray, int PlayerBulletArray1, int PlayerBulletArray2, int PlayerObjArray1, int PlayerObjArray2, int PlayerLife, int PlayerBomb,
                     double CenterX, double CenterY, double CenterZ, double Range, int DefaultTex) :
            base(EnemyArray1, EnemyArray2, EnemyBulletArray1, EnemyBulletArray2, EnemyObjArray1, EnemyObjArray2, EnemyLife, 
                              BossArray, BossBulletArray1, BossBulletArray2, BossObjArray1, BossObjArray2, BossLife, 
                              PlayerArray, PlayerBulletArray1, PlayerBulletArray2, PlayerObjArray1, PlayerObjArray2, PlayerLife, PlayerBomb, 
                 CenterX, CenterY, CenterZ, Range, DefaultTex)
        {

        }

    }

}
