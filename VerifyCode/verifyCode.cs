using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace VerifyCode
{
    public class verifyCode
    {
        /*调用方式
         * 引用这个VerifyCode.dll
        1、主程序先定义一个string类 = verifyCode.getVerifyCode(X);  X是数字，生成验证码的位数
        2、然后让你的pictureBox控件的Image = verifyCode.amap; 就会显示验证码了

        (比较验证码)
        可以自己比较，也可以调用 verifyCode.authCode(input, code);
        input是用户输入的，code是上面第1步获取的生成的code
        返回类型是true/false

        */
        public static Bitmap amap;
        //匹配字符的临时变量
        private static string strTemp;

        public static string getVerifyCode(int iVerifyCodeLength)
        {
            string strVerifyCode = "";
            strVerifyCode = CreateRandomCode(iVerifyCodeLength);
            if (strVerifyCode == "")
            {
                return "none";
            }
            strTemp = strVerifyCode;
            CreateImage(strVerifyCode);
            return strVerifyCode;
        }

     
        //生成验证码字符串
        private static string CreateRandomCode(int iLength)
        {
            int rand;
            char code;
            string randomCode = String.Empty;
            //生成一定长度的验证码
            System.Random random = new Random();
            for (int i = 0; i < iLength; i++)
            {
                rand = random.Next();
                if (rand % 3 == 0)
                {
                    code = (char)('A' + (char)(rand % 26));
                }
                else
                {
                    code = (char)('0' + (char)(rand % 10));
                }
                randomCode += code.ToString();
            }
            return randomCode;
        }
        ///  创建验证码图片
        private static void CreateImage(string strVerifyCode)
        {
            try
            {
                int iRandAngle = 45;    //随机转动角度
                int iMapWidth = (int)(strVerifyCode.Length * 21);
                Bitmap map = new Bitmap(iMapWidth, 28);    //创建图片背景
                Graphics graph = Graphics.FromImage(map);
                graph.Clear(Color.AliceBlue);//清除画面，填充背景
                graph.DrawRectangle(new Pen(Color.Black, 0), 0, 0, map.Width - 1, map.Height - 1);//画一个边框
                graph.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;//模式
                Random rand = new Random();
                //背景噪点生成
                Pen blackPen = new Pen(Color.LightGray, 0);
                for (int i = 0; i < 50; i++)
                {
                    int x = rand.Next(0, map.Width);
                    int y = rand.Next(0, map.Height);
                    graph.DrawRectangle(blackPen, x, y, 1, 1);
                }
                //验证码旋转，防止机器识别
                char[] chars = strVerifyCode.ToCharArray();//拆散字符串成单字符数组
                //文字距中
                StringFormat format = new StringFormat(StringFormatFlags.NoClip);
                format.Alignment = StringAlignment.Center;
                format.LineAlignment = StringAlignment.Center;
                //定义颜色
                Color[] c = { Color.Black, Color.Red, Color.DarkBlue, Color.Green,Color.Orange, Color.Brown, Color.DarkCyan, Color.Purple };
                //定义字体
                string[] font = { "Verdana", "Microsoft Sans Serif", "Comic Sans MS", "Arial", "宋体" };
                for (int i = 0; i < chars.Length; i++)
                {
                    int cindex = rand.Next(7);
                    int findex = rand.Next(5); Font f = new System.Drawing.Font(font[findex], 13, System.Drawing.FontStyle.Bold);//字体样式(参数2为字体大小)
                    Brush b = new System.Drawing.SolidBrush(c[cindex]);
                    Point dot = new Point(16, 16);
                    float angle = rand.Next(-iRandAngle, iRandAngle);//转动的度数
                    graph.TranslateTransform(dot.X, dot.Y);//移动光标到指定位置
                    graph.RotateTransform(angle);
                    graph.DrawString(chars[i].ToString(), f, b, 1, 1, format);
                    graph.RotateTransform(-angle);//转回去
                    graph.TranslateTransform(2, -dot.Y);//移动光标到指定位置
                }
                amap = map;
            }
            catch (ArgumentException)
            {
                MessageBox.Show("验证码生成错误。");
            }

        }

        public static bool authCode(string input,string autocode)
        {
            bool status = false;
            char[] ch1 = input.ToCharArray();
            char[] ch2 = autocode.ToCharArray();
            int nCount = 0;
            for (int i = 0; i < strTemp.Length; i++)
            {
                if ((ch1[i] >= 'a' && ch1[i] <= 'z') || (ch1[i] >= 'A' && ch1[i] <= 'Z'))
                {
                    if ((ch1[i] - 32 == ch2[i]) || (ch1[i] + 32 == ch2[i])||ch1[i]==ch2[i])
                    {
                        nCount++; 
                    }
                }
                else
                {
                    if (ch1[i] == ch2[i])
                    {
                        nCount++;
                    }
                }
            }
            if (nCount == strTemp.Length)
            {
                status = true;
            }
            else
            {
                status = false;
            }
            return status;
        }

        //dll作者：dreamstation625

    }
}
