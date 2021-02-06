using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DP_WPF_MVVM_LoginReg_20190128.Services
{
    public class TextService
    {
        public static string StrDeleteWhat(string what)
        {
            return "Do you want to delete the " + what + "?";
        }
        public static string StrDeleteCategory()
        {
            return "Delete category";
        }
        public static string StrResult(string result, string testAnsRightCount, string testAnsWrongCount)
        {
            return "Результат теста " + result + "%. Правильный: " + testAnsRightCount + ", Неправильных: " + testAnsWrongCount;
        }
    }
}
