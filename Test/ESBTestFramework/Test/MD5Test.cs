using NewLife.Security;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace ESB.TestFramework.Test
{
    class MD5Test
    {
        public static void DoTest()
        {
            Stopwatch stopWatch = new Stopwatch();

            stopWatch.Start();
            String hashString = DataHelper.Hash(@"MD5具有很好的安全性(因为它具有不可逆的特征,加过密的密文经过解密后和加密前的东东相同的可能性极小)

引用
using System.Security.Cryptography;D5具有很好的安全性(因为它具有不可逆的特征,加过密的密文经过解密后和加密前的东东相同的可能性极小)

引用
using System.Security.Cryptography;D5具有很好的安全性(因为它具有不可逆的特征,加过密的密文经过解密后和加密前的东东相同的可能性极小)

引用
using System.Security.Cryptography;D5具有很好的安全性(因为它具有不可逆的特征,加过密的密文经过解密后和加密前的东东相同的可能性极小)

引用
using System.Security.Cryptography;D5具有很好的安全性(因为它具有不可逆的特征,加过密的密文经过解密后和加密前的东东相同的可能性极小)

引用
using System.Security.Cryptography;D5具有很好的安全性(因为它具有不可逆的特征,加过密的密文经过解密后和加密前的东东相同的可能性极小)

引用
using System.Security.Cryptography;D5具有很好的安全性(因为它具有不可逆的特征,加过密的密文经过解密后和加密前的东东相同的可能性极小)

引用
using System.Security.Cryptography;D5具有很好的安全性(因为它具有不可逆的特征,加过密的密文经过解密后和加密前的东东相同的可能性极小)

引用
using System.Security.Cryptography;MD5具有很好的安全性(因为它具有不可逆的特征,加过密的密文经过解密后和加密前的东东相同的可能性极小)

引用
using System.Security.Cryptography;D5具有很好的安全性(因为它具有不可逆的特征,加过密的密文经过解密后和加密前的东东相同的可能性极小)

引用
using System.Security.Cryptography;D5具有很好的安全性(因为它具有不可逆的特征,加过密的密文经过解密后和加密前的东东相同的可能性极小)

引用
using System.Security.Cryptography;D5具有很好的安全性(因为它具有不可逆的特征,加过密的密文经过解密后和加密前的东东相同的可能性极小)

引用
using System.Security.Cryptography;D5具有很好的安全性(因为它具有不可逆的特征,加过密的密文经过解密后和加密前的东东相同的可能性极小)

引用
using System.Security.Cryptography;D5具有很好的安全性(因为它具有不可逆的特征,加过密的密文经过解密后和加密前的东东相同的可能性极小)

引用
using System.Security.Cryptography;D5具有很好的安全性(因为它具有不可逆的特征,加过密的密文经过解密后和加密前的东东相同的可能性极小)

引用
using System.Security.Cryptography;D5具有很好的安全性(因为它具有不可逆的特征,加过密的密文经过解密后和加密前的东东相同的可能性极小)

引用
using System.Security.Cryptography;MD5具有很好的安全性(因为它具有不可逆的特征,加过密的密文经过解密后和加密前的东东相同的可能性极小)

引用
using System.Security.Cryptography;D5具有很好的安全性(因为它具有不可逆的特征,加过密的密文经过解密后和加密前的东东相同的可能性极小)

引用
using System.Security.Cryptography;D5具有很好的安全性(因为它具有不可逆的特征,加过密的密文经过解密后和加密前的东东相同的可能性极小)

引用
using System.Security.Cryptography;D5具有很好的安全性(因为它具有不可逆的特征,加过密的密文经过解密后和加密前的东东相同的可能性极小)

引用
using System.Security.Cryptography;D5具有很好的安全性(因为它具有不可逆的特征,加过密的密文经过解密后和加密前的东东相同的可能性极小)

引用
using System.Security.Cryptography;D5具有很好的安全性(因为它具有不可逆的特征,加过密的密文经过解密后和加密前的东东相同的可能性极小)

引用
using System.Security.Cryptography;D5具有很好的安全性(因为它具有不可逆的特征,加过密的密文经过解密后和加密前的东东相同的可能性极小)

引用
using System.Security.Cryptography;D5具有很好的安全性(因为它具有不可逆的特征,加过密的密文经过解密后和加密前的东东相同的可能性极小)

引用
using System.Security.Cryptography;");
            stopWatch.Stop();


            Console.WriteLine("MD5 Hash 耗时：{0}ms。", stopWatch.ElapsedTicks);
            Console.WriteLine("Hash String: {0}", hashString);

            Console.ReadKey();

        }
    }
}
