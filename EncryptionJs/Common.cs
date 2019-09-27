using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EncryptionJs
{
    public class Common
    {
        public static string FormaterJS(string jsText, string jsFileName)
        {
            jsText = jsText.Replace("\"", "!`!");
            jsText = jsText.Replace("'", "&!&");

            jsText = jsText.Replace("\\r", "\\\\r");
            jsText = jsText.Replace("\\n", "\\\\n");

            var text = "var myZpStr='" + jsText + "';  ";
            text += " myZpStr=myZpStr.replace(/!`!/g, \"\\\"\").replace(/&!&/g, \"'\");";
            text += "$(\"#source\").text(myZpStr); ";
            text += "$(\".execute\").click();";
            var time = @"var timer = setInterval(function () {
                var text = $('#resultSource').val().trim();
                if (text == '') {

                }
                else {" +
              @"bound.myMethod('" + jsFileName + @"', document.getElementById('resultSource').value); 
                        clearInterval(timer);
                }
            }, 1000);";
            return text;
        }

    }
}
