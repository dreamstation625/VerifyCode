# VerifyCode
基于C#的图片验证码生成<br>
>使用方法：<br>
  环境:.net 4.6+<br>
   * 引用这个VerifyCode.dll<br>
   * 1、主程序先定义一个string类 = verifyCode.getVerifyCode(X);  X是数字，生成验证码的位数<br>
   * 2、然后让你的pictureBox控件的Image属性 = verifyCode.amap; 就会显示验证码了<br>

   * (比较验证码)<br>
   * 可以自己比较，也可以调用 verifyCode.authCode(input, code);<br>
   * input是用户输入的，code是上面第1步获取的生成的code<br>
   * 返回类型是true/false<br>
