using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using DxLibDLL;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Threading;


namespace NsOL
{
    public class EngineCore
    {
        public class Base
        {
            /* Base类用于放置基础函数以及数据类型
             * 对象比较小的使用结构体，大的使用类
             * 此类不可实例化
             */
            public struct Point3D
            {
                /* 因为Drawing里还有一个二维的Point
                 * 这里如此定义
                 */
                public double x;
                public double y;
                public double z;
            }

            public static double Distance3D(Point3D Point1, Point3D Point2)
            {
                /* 距离计算公式
                 */
                return Math.Sqrt(Math.Pow(Point2.x - Point1.x, 2)
                    + Math.Pow(Point2.y - Point1.y, 2)
                    + Math.Pow(Point2.z - Point1.z, 2));
            }

            public static double Average3D(Point3D Point)
            {
                return (Point.x + Point.y + Point.z) / 3d;
            }

            [DllImport("user32.dll")]
            static extern int GetKeyState(int nVirtKey);
            /* 键盘处理函数，暂时这样导入API
             * 应该C#有其他方式记录键盘输入
             * 下面会对这个函数进行封装
             */

            public static bool GetKey(Keys Key)
            {
                /* 对上面的函数的封装
                 * 下面的条件是按位运算的
                 */
                if (0x8000 == (0x8000 & GetKeyState((int)Key)))
                    return true;
                else
                    return false;
            }

            public class FPSCounter
            {
                /* 进行帧率计算和控制的类
                 * 此类需要实例化
                 * 精度为小数点后一位
                 * 其中的Update要加入主游戏循环
                 */
                private int StartTime;
                private int Count;
                private float FPSNum;
                private const int N = 60;
                private const int FPS = 60;

                public FPSCounter()
                {
                    StartTime = 0;
                    Count = 0;
                    FPSNum = 0;
                }

                public void Update()
                {
                    if (Count == 0)
                        StartTime = System.Environment.TickCount;
                    if (Count == N)
                    {
                        int TmpTime = System.Environment.TickCount;
                        FPSNum = 1000f / (((float)TmpTime - (float)StartTime) / (float)N);
                        //FPSNum = 1000 / ((TmpTime - StartTime) / N);
                        Count = 0;
                        StartTime = TmpTime;
                    }
                    Count++;
                }

                public void WaitTime()
                {
                    int TookTime = System.Environment.TickCount - StartTime;
                    int WaitTime = Count * 1000 / FPS - TookTime;
                    if (WaitTime > 0)
                        DX.WaitTimer(WaitTime);
                }

                public float GetFPS()
                {
                    return FPSNum;
                }
            }

        }

        public class DxCS
        {
            /* 对DxLib的二次封装
             * 操作DxLib的函数尽量在这里进行封装
             */
            public struct DxImage
            {
                /* 这里是一个图片的结构体
                 * 包含资源句柄和图片尺寸
                 */
                public int Handle;
                public int Width;
                public int Height;
            }

            public static bool DxInit(IntPtr IconHandle, string Title, bool IsFullScreen, int Width, int Height)
            {
                /* 游戏窗口创建
                 * 注意当返回值为false的时候
                 * 需要提示信息，DirectX加载失败
                 * 这里的全屏是一种比较友好的全屏
                 * 可以随时切换回桌面
                 */
                DX.SetWindowIconHandle(IconHandle);

                if (IsFullScreen)
                {
                    DX.ChangeWindowMode(1);
                    DX.SetWindowStyleMode(2);
                    DX.SetWindowSize(System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width,
                        System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height);
                }
                else
                {
                    DX.ChangeWindowMode(1);
                    DX.SetWindowStyleMode(5);
                    DX.SetWindowSize(Width, Height);
                }

                DX.SetGraphMode(1920, 1080, 32);
                DX.SetOutApplicationLogValidFlag(0);
                DX.SetWindowText(Title);
                DX.SetDisplayRefreshRate(60);
                if (DX.DxLib_Init() == -1)
                    return false;
                DX.SetDrawScreen(DX.DX_SCREEN_BACK);
                return true;
            }

            public static void DxEnd()
            {
                /* 关闭游戏窗口
                 * 处理这条信息
                 * DX.ProcessMessage == -1
                 * 退出主游戏循环后执行这个函数
                 */
                DX.DxLib_End();
            }

            public static void DxFlip()
            {
                /* 翻转调色板
                 * 并清除另一面
                 */
                DX.ScreenFlip();
                DX.ClearDrawScreen();
            }

            public static void Dx3DCamera()
            {
                DX.SetCameraPositionAndAngle(DxLibDLL.DX.VGet(960f, 1035f, -150f), (float)Math.PI / 4f, 0, 0);
                DX.SetGlobalAmbientLight(DxLibDLL.DX.GetColorF(1f, 1f, 1f, 1f));
                DX.SetLightDirection(DxLibDLL.DX.VGet(0f, 1f, -1f));
            }

            public static void DxWait(int msTime)
            {
                /* 延时的函数
                 */
                DX.WaitTimer(msTime);
            }

            public static bool LoadTexture(string Path, ref DxImage Image)
            {
                /* 这里的路径是相对路径
                 * 不带头部的斜杠
                 * 注意处理返回值
                 */
                if (System.IO.File.Exists(Environment.CurrentDirectory + "\\" + Path))
                {
                    Image.Handle = DX.LoadGraph(Environment.CurrentDirectory + "\\" + Path);
                    Bitmap ImgTmp = new Bitmap(Environment.CurrentDirectory + "\\" + Path);
                    Image.Width = ImgTmp.Width;
                    Image.Height = ImgTmp.Height;
                    ImgTmp.Dispose();
                    return true;
                }
                else
                    return false;
            }

            public static int LoadString(string Context, Brush Brush, int Size, int Width, int Height)
            {
                Bitmap Image = new Bitmap(Width, Height);
                Graphics GDI = Graphics.FromImage(Image);
                FontFamily FontFamily = new FontFamily("微软雅黑");
                Font Font = new Font(FontFamily, Size, FontStyle.Regular, GraphicsUnit.Pixel);
                GDI.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                GDI.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;
                GDI.DrawString(Context, Font, Brush, 0, 0);
                Image.Save("String.dat", System.Drawing.Imaging.ImageFormat.Png);
                GDI.Dispose(); Image.Dispose(); Font.Dispose(); FontFamily.Dispose();
                return DX.LoadGraph("String.dat");
            }

            public static void DrawPic(DxImage Image, float x, float y)
            {
                DX.DrawGraphF(x - (Image.Width / 2), y - (Image.Height / 2), Image.Handle, 1);
            }

            public static void DrawSphere(float x, float y, float z, float r, int DivNum, int DifColorR, int DifColorG, int DifColorB, int SpcColorR, int SpcColorG, int SpcColorB, bool IsFill)
            {
                if (IsFill) DX.DrawSphere3D(DX.VGet(x, y, z), r, DivNum, DX.GetColor(DifColorR, DifColorG, DifColorB), DX.GetColor(SpcColorR, SpcColorG, SpcColorB), 1);
                else DX.DrawSphere3D(DX.VGet(x, y, z), r, DivNum, DX.GetColor(DifColorR, DifColorG, DifColorB), DX.GetColor(SpcColorR, SpcColorG, SpcColorB), 0);
            }

            public static void DrawSphere(double x, double y, double z, double r, int DivNum, int DifColorR, int DifColorG, int DifColorB, int SpcColorR, int SpcColorG, int SpcColorB, bool IsFill)
            {
                if (IsFill) DX.DrawSphere3DD(DX.VGetD(x, y, z), r, DivNum, DX.GetColor(DifColorR, DifColorG, DifColorB), DX.GetColor(SpcColorR, SpcColorG, SpcColorB), 1);
                else DX.DrawSphere3DD(DX.VGetD(x, y, z), r, DivNum, DX.GetColor(DifColorR, DifColorG, DifColorB), DX.GetColor(SpcColorR, SpcColorG, SpcColorB), 0);
            }

            public static void DrawString(string Context, float x, float y, int Size)
            {
                DX.SetFontSize(Size);
                DX.DrawStringF(x, y, Context, 0xFFFFFF);
            }

            public static void DrawString(int Handle, float x, float y)
            {
                DX.DrawGraphF(x, y, Handle, 1);
            }
        }

        public class GdiCS
        {
            /* GDI操作函数
             * 此类不可实例化
             */

            public static int GenBulletTex(Brush BulletColor, int Width, int Height)
            {
                /* 弹幕图片生成
                 */

                Bitmap Pic = new Bitmap(Width, Height);
                Graphics Gdi = Graphics.FromImage(Pic);
                Rectangle Rect = new Rectangle(0, 0, Width, Height);
                Gdi.FillPie(BulletColor, Rect, 0f, 360f);
                Gdi.Dispose();
                Pic.Save("Bullet.dat", System.Drawing.Imaging.ImageFormat.Png);
                Pic.Dispose();
                return DX.LoadGraph("Bullet.dat");
            }
        }

        public class STG
        {
            /* STG相关的数据结构和函数
             * 此类不可实例化
             */

            public const int DivideNum = 12;

            public const int JUDGE_GRAZE = 1;
            public const int JUDGE_MISS = 2;
            public const int JUDGE_AWAY = 3;

            public const int TEX_SPHERE = -2;
            public const int TEX_CIRCLE = -1;

            public class Bullet
            {
                /* 抽象的弹幕类
                 * 也可作为物体的基类
                 */

                private Base.Point3D Position;
                private Base.Point3D Speed;
                private Base.Point3D Rotate;
                private Base.Point3D Scale;
                private int TexPtr;
                public bool IsEnabled;
                public bool IsGrazed;

                public Bullet()
                {
                    Position.x = 0; Position.y = 0; Position.z = 0;
                    Speed.x = 0; Speed.y = 0; Speed.z = 0;
                    Rotate.x = 0; Rotate.y = 0; Rotate.z = 0;
                    Scale.x = 0; Scale.y = 0; Scale.z = 0;
                    TexPtr = 0; IsEnabled = false; IsGrazed = true;
                }

                public Bullet(int Texture)
                {
                    Position.x = 0; Position.y = 0; Position.z = 0;
                    Speed.x = 0; Speed.y = 0; Speed.z = 0;
                    Rotate.x = 0; Rotate.y = 0; Rotate.z = 0;
                    Scale.x = 0; Scale.y = 0; Scale.z = 0;
                    TexPtr = Texture; IsEnabled = false; IsGrazed = true;
                }

                public Base.Point3D GetPosition()
                {
                    return Position;
                }

                public void SetPosition(double x, double y, double z)
                {
                    Position.x = x; Position.y = y; Position.z = z;
                }

                public void SetPosition(Base.Point3D Point)
                {
                    Position = Point;
                }

                public void SetSpeed(double x, double y, double z)
                {
                    Speed.x = x; Speed.y = y; Speed.z = z;
                }

                public void SetRotate(double x, double y, double z)
                {
                    Rotate.x = x; Rotate.y = y; Rotate.z = z;
                }

                public void SetScale(double x, double y, double z)
                {
                    Scale.x = x; Scale.y = y; Scale.z = z;
                }

                public bool PreJudge(Base.Point3D Point, double Range)
                {
                    return Math.Abs(Point.x - Position.x) < Range && Math.Abs(Point.y - Position.y) < Range && Math.Abs(Point.z - Position.z) < Range;
                }

                public int Judge(Base.Point3D Point)
                {
                    if (Base.Distance3D(Point, Position) > Base.Average3D(Scale) * 2d) return JUDGE_AWAY;
                    else if (Base.Distance3D(Point, Position) > Base.Average3D(Scale)) return JUDGE_GRAZE;
                    else return JUDGE_MISS;
                }

                public void Draw()
                {
                    if (TexPtr == TEX_SPHERE) DxCS.DrawSphere(Position.x, Position.y, Position.z, Base.Average3D(Scale), DivideNum, 255, 255, 255, 0, 0, 0, true);
                    else
                    {
                        DX.DrawRotaGraphF((float)Position.x, (float)Position.z, 1, 0, TexPtr, 1);
                        //DX.DrawPixel((int)(Position.x ), (int)(Position.z ), 0xFFFFFF);
                    }
                }

            }

            public class STGCore
            {
                /* STG的核心函数在这里
                 * 包括绘制，判定和控制
                 * 弹幕生成需要继承这个类
                 * 此类需要实例化
                 */
                protected int ArrayNumA;
                protected int ArrayNumB;
                public Bullet[,] Bullets;
                private static Bullet PlayerPos;
                private static Base.Point3D OriPlayer;
                protected static int Graze, Miss, Num;
                public Bullet CenterPos;
                public static double GameRange;
                protected int DefaultTexture;
                //Thread STGThread;

                public STGCore(int ArrayNum1, int ArrayNum2, double CenterX, double CenterY, double CenterZ, double Range,
                                                             double PlayerX, double PlayerY, double PlayerZ, double PlayerSize, double BulletSize, int DefaultTex)
                {
                    ArrayNumA = ArrayNum1;
                    ArrayNumB = ArrayNum2;
                    Bullets = new Bullet[ArrayNumA, ArrayNumB];
                    if (DefaultTex == TEX_CIRCLE)
                    {
                        PlayerPos = new Bullet(GdiCS.GenBulletTex(Brushes.White, (int)PlayerSize, (int)PlayerSize));
                        DefaultTexture = GdiCS.GenBulletTex(Brushes.Red, (int)BulletSize, (int)BulletSize);
                    }
                    else
                    {
                        DefaultTexture = DefaultTex;
                    }
                    InitBullet();
                    PlayerPos.SetPosition(PlayerX, PlayerY, PlayerZ);
                    PlayerPos.SetScale(PlayerSize, PlayerSize, PlayerSize);
                    PlayerPos.IsEnabled = true;
                    OriPlayer = PlayerPos.GetPosition();
                    CenterPos = new Bullet();
                    CenterPos.SetPosition(CenterX, CenterY, CenterZ);
                    GameRange = Range;
                    Graze = 0; Miss = 0;

                    //STGThread = new Thread(JudgeACtrl);
                }

                public void Run()
                {
                    //STGThread.Start();
                }

                public void Stop()
                {
                    //STGThread.Abort();
                }

                public void GetInfo(float x, float y)
                {
                    DxCS.DrawString("Graze: " + Graze.ToString(), x, y, 20);
                    DxCS.DrawString("Miss:  " + Miss.ToString(), x, y + 25, 20);
                    DxCS.DrawString("Num: " + Num.ToString(), x, y + 50, 20);
                }

                public void GetInfo(int Graze, int Miss, int Num)
                {
                    Graze = STGCore.Graze;
                    Miss = STGCore.Miss;
                    Num = STGCore.Num;
                }

                private void InitBullet()
                {
                    for (int i = 0; i < ArrayNumA; i++)
                        for (int j = 0; j < ArrayNumB; j++)
                            Bullets[i, j] = new Bullet(DefaultTexture);
                }

                public void MainCircle()
                {
                    GenBullet();
                    JudgeBullet();
                    DrawBullet();
                }

                protected virtual void GenBullet()
                {
                    Base.Point3D TmpPoint;
                    for (int i = 0; i < ArrayNumA; i++)
                        for (int j = 0; j < ArrayNumB; j++)
                        {
                            //if (!Bullets[i, j].IsEnabled)
                            //{
                            //    TmpPoint.x = CenterPos.GetPosition().x;
                            //    TmpPoint.y = CenterPos.GetPosition().y;
                            //    TmpPoint.z = CenterPos.GetPosition().z;
                            //    Bullets[i, j].SetPosition(TmpPoint);
                            //    Bullets[i, j].SetScale(20, 20, 20);
                            //    Bullets[i, j].IsEnabled = true;
                            //}
                            //TmpPoint.x = 0;
                            //TmpPoint.y = 0;
                            //TmpPoint.z = 0;
                            //TmpPoint.x = Bullets[i, j].GetPosition().x + TmpPoint.x;
                            //TmpPoint.y = Bullets[i, j].GetPosition().y + TmpPoint.y;
                            //TmpPoint.z = Bullets[i, j].GetPosition().z + TmpPoint.z;
                            //Bullets[i, j].SetPosition(TmpPoint);
                            if (!Bullets[i, j].IsEnabled)
                            {
                                TmpPoint.x = Math.Cos(i) + i * Math.Cos(0.25 * j) + CenterPos.GetPosition().x;
                                TmpPoint.y = CenterPos.GetPosition().y;
                                TmpPoint.z = Math.Sin(i) - i * Math.Sin(0.25 * j) + CenterPos.GetPosition().z;
                                //TmpPoint.x = Math.Cos(i) + j * Math.Cos(0.25 * j) + 960;
                                //TmpPoint.y = CenterPos.GetPosition().y;
                                //TmpPoint.z = Math.Sin(i) - j * Math.Sin(0.25 * j) + 540;
                                Bullets[i, j].SetPosition(TmpPoint);
                                Bullets[i, j].SetScale(20, 20, 20);
                                Bullets[i, j].IsEnabled = true;
                            }
                            TmpPoint.x = (Bullets[i, j].GetPosition().x - CenterPos.GetPosition().x) / 100;
                            TmpPoint.y = 0;
                            TmpPoint.z = (Bullets[i, j].GetPosition().z - CenterPos.GetPosition().z) / 100;
                            TmpPoint.x = Bullets[i, j].GetPosition().x + TmpPoint.x;
                            TmpPoint.y = Bullets[i, j].GetPosition().y + TmpPoint.y;
                            TmpPoint.z = Bullets[i, j].GetPosition().z + TmpPoint.z;
                            Bullets[i, j].SetPosition(TmpPoint);
                        }
                }

                private void DrawBullet()
                {
                    for (int i = 0; i < ArrayNumA; i++)
                        for (int j = 0; j < ArrayNumB; j++)
                            if (Bullets[i, j].IsEnabled) Bullets[i, j].Draw();
                    DrawPlayer();
                }

                private static void DrawPlayer()
                {
                    PlayerPos.Draw();
                }

                private void JudgeBullet()
                {
                    Num = 0;
                    for (int i = 0; i < ArrayNumA; i++)
                        for (int j = 0; j < ArrayNumB; j++)
                            if (Bullets[i, j].IsEnabled)
                            {
                                if (Base.Distance3D(Bullets[i, j].GetPosition(), CenterPos.GetPosition()) > GameRange)
                                    Bullets[i, j] = new Bullet(DefaultTexture);
                                //if (!CenterPos.PreJudge(Bullets[i, j].GetPosition(), GameRange))
                                //    Bullets[i, j] = new Bullet(DefaultTexture);
                                Num++;
                            }

                    //for (int i = 0; i < ArrayNumA; i++)
                    //    for (int j = 0; j < ArrayNumB; j++)
                    //        if (Bullets[i, j].IsEnabled)
                    //            if (Bullets[i, j].IsEnabled && PlayerPos.PreJudge(Bullets[i, j].GetPosition(), GameRange))
                    //            {
                    //                if (PlayerPos.Judge(Bullets[i, j].GetPosition()) == JUDGE_AWAY && !Bullets[i, j].IsGrazed) Bullets[i, j].IsGrazed = true;
                    //                if (PlayerPos.Judge(Bullets[i, j].GetPosition()) == JUDGE_GRAZE && Bullets[i, j].IsGrazed)
                    //                {
                    //                    Bullets[i, j].IsGrazed = false;
                    //                    Graze++;
                    //                }
                    //                if (PlayerPos.Judge(Bullets[i, j].GetPosition()) == JUDGE_MISS)
                    //                {
                    //                    Bullets[i, j] = new Bullet(DefaultTexture);
                    //                    Miss++;
                    //                    PlayerPos.SetPosition(OriPlayer);
                    //                }
                    //            }
                }

                public void Control()
                {
                    Base.Point3D TmpPos;
                    TmpPos = PlayerPos.GetPosition();
                    if (DefaultTexture == TEX_SPHERE)
                    {
                        if (Base.GetKey(Keys.ShiftKey))
                        {
                            if (Base.GetKey(Keys.Up)) TmpPos.z += 3;
                            if (Base.GetKey(Keys.Down)) TmpPos.z -= 3;
                            if (Base.GetKey(Keys.Left)) TmpPos.x -= 3;
                            if (Base.GetKey(Keys.Right)) TmpPos.x += 3;
                        }
                        else
                        {
                            if (Base.GetKey(Keys.Up)) TmpPos.z += 5;
                            if (Base.GetKey(Keys.Down)) TmpPos.z -= 5;
                            if (Base.GetKey(Keys.Left)) TmpPos.x -= 5;
                            if (Base.GetKey(Keys.Right)) TmpPos.x += 5;
                        }
                    }
                    else
                    {
                        if (Base.GetKey(Keys.ShiftKey))
                        {
                            if (Base.GetKey(Keys.Up)) TmpPos.z -= 2;
                            if (Base.GetKey(Keys.Down)) TmpPos.z += 2;
                            if (Base.GetKey(Keys.Left)) TmpPos.x -= 2;
                            if (Base.GetKey(Keys.Right)) TmpPos.x += 2;
                        }
                        else
                        {
                            if (Base.GetKey(Keys.Up)) TmpPos.z -= 5;
                            if (Base.GetKey(Keys.Down)) TmpPos.z += 5;
                            if (Base.GetKey(Keys.Left)) TmpPos.x -= 5;
                            if (Base.GetKey(Keys.Right)) TmpPos.x += 5;
                        }
                    }


                    if (TmpPos.x > 0 & TmpPos.x < 1920 & TmpPos.z > 0 & TmpPos.z < 1080) PlayerPos.SetPosition(TmpPos);
                }
            }

            public class Enemy
            {
                public STGCore BulletGen;
                public STGCore ObjGen;
                protected int Life;
                protected int[] Texture;

                public Enemy(int BulletArray1, int BulletArray2, int ObjArray1, int ObjArray2, int EnemyLife, double Range, int DefaultTex)
                {
                    BulletGen = new STGCore(BulletArray1, BulletArray2, 0, 0, 0, Range, 0, 0, 0, 10, 5, DefaultTex);
                    ObjGen = new STGCore(ObjArray1, ObjArray2, 0, 0, 0, Range, 0, 0, 0, 10, 5, DefaultTex);
                    Life = EnemyLife;
                    Texture = new int[128];
                    Texture[0] = DefaultTex;
                }

                public void MainCircle()
                {
                    BulletGen.MainCircle();
                    //ObjGen.MainCircle();
                }

            }

            public class Boss
            {
                public STGCore BulletGen;
                public STGCore ObjGen;
                protected int Life;
                protected int[] Texture;

                public Boss(int BulletArray1, int BulletArray2, int ObjArray1, int ObjArray2, int BossLife, double Range, int DefaultTex)
                {
                    BulletGen = new STGCore(BulletArray1, BulletArray2, 0, 0, 0, Range, 0, 0, 0, 10, 5, DefaultTex);
                    ObjGen = new STGCore(ObjArray1, ObjArray2, 0, 0, 0, Range, 0, 0, 0, 10, 5, DefaultTex);
                    Life = BossLife;
                    Texture = new int[128];
                    Texture[0] = DefaultTex;
                }

                public void MainCircle()
                {
                    BulletGen.MainCircle();
                    //ObjGen.MainCircle();
                }

            }

            public class Player
            {
                public STGCore BulletGen;
                public STGCore ObjGen;
                protected int Life, Bomb, Graze;
                protected long Score;
                protected object Task;
                protected object Objs;
                protected int[] Texture;

                public Player(int BulletArray1, int BulletArray2, int ObjArray1, int ObjArray2, int PlayerLife, int PlayerBomb, double Range, int DefaultTex)
                {
                    BulletGen = new STGCore(BulletArray1, BulletArray2, 0, 0, 0, Range, 0, 0, 0, 10, 5, DefaultTex);
                    ObjGen = new STGCore(ObjArray1, ObjArray2, 0, 0, 0, Range, 0, 0, 0, 10, 5, DefaultTex);
                    Life = PlayerLife;
                    Bomb = PlayerBomb;
                    Graze = 0;
                    Texture = new int[128];
                    Texture[0] = DefaultTex;
                }

                public void GetInfo()
                {
                    int tmp = 0;
                    BulletGen.GetInfo(Graze, Bomb, tmp);
                }

                private void MovePlayer()
                {
                    BulletGen.Control();
                }

                public void MainCircle()
                {
                    BulletGen.MainCircle();
                    //ObjGen.MainCircle();
                    MovePlayer();
                }

            }


            public class SpellCard
            {
                protected Enemy[,] Enemies;
                protected Boss[] Bosses;
                protected Player[] Players;
                protected Bullet Center;
                protected double Range;

                public SpellCard(int EnemyArray1, int EnemyArray2, int EnemyBulletArray1, int EnemyBulletArray2, int EnemyObjArray1, int EnemyObjArray2, int EnemyLife,
                                                  int BossArray, int BossBulletArray1, int BossBulletArray2, int BossObjArray1, int BossObjArray2, int BossLife,
                                                  int PlayerArray, int PlayerBulletArray1, int PlayerBulletArray2, int PlayerObjArray1, int PlayerObjArray2, int PlayerLife, int PlayerBomb,
                                 double CenterX, double CenterY, double CenterZ, double Range, int DefaultTex)
                {
                    Enemies = new Enemy[EnemyArray1, EnemyArray2];
                    Bosses = new Boss[BossArray];
                    Players = new Player[PlayerArray];

                    Center = new Bullet();
                    Center.SetPosition(CenterX, CenterY, CenterZ);
                    this.Range = Range;

                    for (int i = 0; i < EnemyArray1; i++)
                        for (int j = 0; j < EnemyArray2; j++)
                            Enemies[i, j] = new Enemy(EnemyBulletArray1, EnemyBulletArray2, EnemyObjArray1, EnemyObjArray2, EnemyLife, Range, DefaultTex);
                    for (int i = 0; i < BossArray; i++)
                        Bosses[i] = new Boss(BossBulletArray1, BossBulletArray2, BossObjArray1, BossObjArray2, BossLife, Range, DefaultTex);
                    for (int i = 0; i < PlayerArray; i++)
                    {
                        Players[i] = new Player(PlayerBulletArray1, PlayerBulletArray2, PlayerObjArray1, PlayerObjArray2, PlayerLife, PlayerBomb, Range, DefaultTex);
                        Players[i].BulletGen.CenterPos = Center;
                    }

                    STGCore.GameRange = Range;

                }

                public void MainCircle()
                {
                    EnemyMove();
                    for (int i = 0; i < Enemies.GetUpperBound(0); i++)
                        for (int j = 0; j < Enemies.GetUpperBound(1); j++)
                            Enemies[i, j].MainCircle();

                    for (int i = 0; i < Bosses.GetUpperBound(0); i++)
                        Bosses[i].MainCircle();

                    for (int i = 0; i < Players.GetUpperBound(0); i++)
                        Players[i].MainCircle();
                }

                private void EnemyMove()
                {
                    Base.Point3D TmpPoint;
                    for (int i = 0; i < Enemies.GetUpperBound(0); i++)
                        for (int j = 0; j < Enemies.GetUpperBound(1); j++)
                        {
                            if (!Enemies[i, j].BulletGen.CenterPos.IsEnabled)
                            {
                                //TmpPoint.x = Math.Cos(i) + i * Math.Cos(0.25 * j) + Center.GetPosition().x;
                                //TmpPoint.y = Center.GetPosition().y;
                                //TmpPoint.z = Math.Sin(i) - i * Math.Sin(0.25 * j) + Center.GetPosition().z;

                                TmpPoint.x = (i + 1)  * Math.Cos(2 * Math.PI / 10 + j) + Center.GetPosition().x;
                                TmpPoint.y = Center.GetPosition().y;
                                TmpPoint.z = (i + 1)  * Math.Sin(2 * Math.PI / 10 + j) + Center.GetPosition().z;

                                //TmpPoint.x = Math.Cos(i) + j * Math.Cos(0.25 * j) + Center.GetPosition().x;
                                //TmpPoint.y = Center.GetPosition().y;
                                //TmpPoint.z = Math.Sin(i) - j * Math.Sin(0.25 * j) + Center.GetPosition().z;
                                Enemies[i, j].BulletGen.CenterPos.SetPosition(TmpPoint);
                                Enemies[i, j].BulletGen.CenterPos.SetScale(20, 20, 20);
                                Enemies[i, j].BulletGen.CenterPos.IsEnabled = true;
                            }
                            TmpPoint.x = (Center.GetPosition().x - Enemies[i, j].BulletGen.CenterPos.GetPosition().x) / 100;
                            TmpPoint.y = 0;
                            TmpPoint.z = (Center.GetPosition().z - Enemies[i, j].BulletGen.CenterPos.GetPosition().z) / 100;
                            TmpPoint.x = Enemies[i, j].BulletGen.CenterPos.GetPosition().x + TmpPoint.x;
                            TmpPoint.y = Enemies[i, j].BulletGen.CenterPos.GetPosition().y + TmpPoint.y;
                            TmpPoint.z = Enemies[i, j].BulletGen.CenterPos.GetPosition().z + TmpPoint.z;
                            if (Center.PreJudge(TmpPoint, Range))
                                Enemies[i, j].BulletGen.CenterPos.SetPosition(TmpPoint);
                        }
                }
            }
        }
    }
}
