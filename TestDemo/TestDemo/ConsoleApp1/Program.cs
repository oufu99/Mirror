using Aaron.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            string dirPath = @"E:\转换后的js\测试文件1.js";
            FileStream fs = new FileStream(dirPath, FileMode.Create);
            StreamWriter wr = null;
            wr = new StreamWriter(fs);

            string str = @"/*
 * 加密工具已经升级了一个版本，目前为 sojson.v5 ，主要加强了算法，以及防破解【绝对不可逆】配置，耶稣也无法100 % 还原，我说的。;
            *已经打算把这个工具基础功能一直免费下去。还希望支持我。
 *另外 sojson.v5 已经强制加入校验，注释可以去掉，但是 sojson.v5 不能去掉（如果你开通了VIP，可以手动去掉），其他都没有任何绑定。
 *誓死不会加入任何后门，sojson JS 加密的使命就是为了保护你们的Javascript 。
 *警告：如果您恶意去掉 sojson.v5 那么我们将不会保护您的JavaScript代码。请遵守规则 */


; var encode_version = 'sojson.v5', dfejf = '', _0x3c82 =['6L2i5puQ5LuY5Lqg5LiL57KC5YmnwqZj5pK65L+S44Od', 'XWhzw5J3', 'wrMowrLCo8K/w5LDjQ==', 'bkVNw7J1', 'dsKrwpTDssKSwqQrw6vDtA==', 'NMOpBsOmwqXDvydywoQ=', 'w5rCk8K9w7h/', '5Lq46IKg5Yi96ZqqBMK4w6bCqy3CpMKqCsOe', '56uI6ZSQ5oyD6aiu57mmUOKDjxYO5Yq75ayX4oGEwovlkoho4oOFMcOk6Ka45a2q4oKvw5DvvIzkvanljJHkvbbnm4R9esOI44Ci', 'VQYvwpk=']; (function(_0x49c6bf, _0x3032d9){ var _0x19a4d6 = function(_0x32a493){ while (--_0x32a493) { _0x49c6bf['push'](_0x49c6bf['shift']()); } }; _0x19a4d6(++_0x3032d9); }
            (_0x3c82, 0x12a)); var _0xa087 = function(_0x29fbd0, _0x17bb06){ _0x29fbd0 = _0x29fbd0 - 0x0; var _0x5cd8e3 = _0x3c82[_0x29fbd0]; if (_0xa087['initialized'] === undefined) { (function(){ var _0x377f8e = typeof window!== 'undefined' ? window : typeof process=== 'object' && typeof require=== 'function' && typeof global=== 'object' ? global : this; var _0x31c236 = 'ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/='; _0x377f8e['atob'] || (_0x377f8e['atob'] = function(_0x25bd29){ var _0x36b7d1 = String(_0x25bd29)['replace'](/= +$/, ''); for (var _0x1060e5 = 0x0, _0x366a12, _0x1a565c, _0x5d14a4 = 0x0, _0x16da48 = ''; _0x1a565c = _0x36b7d1['charAt'](_0x5d14a4++); ~_0x1a565c && (_0x366a12 = _0x1060e5 % 0x4 ? _0x366a12 * 0x40 + _0x1a565c : _0x1a565c, _0x1060e5++ % 0x4) ? _0x16da48 += String['fromCharCode'](0xff & _0x366a12 >> (-0x2 * _0x1060e5 & 0x6)) : 0x0) { _0x1a565c = _0x31c236['indexOf'](_0x1a565c); } return _0x16da48; }); } ()); var _0x3b5f97 = function(_0x57d77a, _0x568a91){ var _0x389b0e =[], _0x167f1e = 0x0, _0x52eb97, _0x3f642e = '', _0x2052f7 = ''; _0x57d77a = atob(_0x57d77a); for (var _0x22421d = 0x0, _0x17f282 = _0x57d77a['length']; _0x22421d < _0x17f282; _0x22421d++) { _0x2052f7 += '%' + ('00' + _0x57d77a['charCodeAt'](_0x22421d)['toString'](0x10))['slice'](-0x2); } _0x57d77a = decodeURIComponent(_0x2052f7); for (var _0xaec022 = 0x0; _0xaec022 < 0x100; _0xaec022++) { _0x389b0e[_0xaec022] = _0xaec022; } for (_0xaec022 = 0x0; _0xaec022 < 0x100; _0xaec022++) { _0x167f1e = (_0x167f1e + _0x389b0e[_0xaec022] + _0x568a91['charCodeAt'](_0xaec022 % _0x568a91['length'])) % 0x100; _0x52eb97 = _0x389b0e[_0xaec022]; _0x389b0e[_0xaec022] = _0x389b0e[_0x167f1e]; _0x389b0e[_0x167f1e] = _0x52eb97; } _0xaec022 = 0x0; _0x167f1e = 0x0; for (var _0x15c614 = 0x0; _0x15c614 < _0x57d77a['length']; _0x15c614++) { _0xaec022 = (_0xaec022 + 0x1) % 0x100; _0x167f1e = (_0x167f1e + _0x389b0e[_0xaec022]) % 0x100; _0x52eb97 = _0x389b0e[_0xaec022]; _0x389b0e[_0xaec022] = _0x389b0e[_0x167f1e]; _0x389b0e[_0x167f1e] = _0x52eb97; _0x3f642e += String['fromCharCode'](_0x57d77a['charCodeAt'](_0x15c614) ^ _0x389b0e[(_0x389b0e[_0xaec022] + _0x389b0e[_0x167f1e]) % 0x100]); } return _0x3f642e; }; _0xa087['rc4'] = _0x3b5f97; _0xa087['data'] ={ }; _0xa087['initialized'] = !![]; } var _0xb727c8 = _0xa087['data'][_0x29fbd0]; if (_0xb727c8 === undefined) { if (_0xa087['once'] === undefined) { _0xa087['once'] = !![]; } _0x5cd8e3 = _0xa087['rc4'](_0x5cd8e3, _0x17bb06); _0xa087['data'][_0x29fbd0] = _0x5cd8e3; } else { _0x5cd8e3 = _0xb727c8; } return _0x5cd8e3; }; (function(_0x2145e7, _0x5189f4){ var _0x14c4c5 = { 'gGFIC':_0xa087('0x0', '[ArW'),'TjxiA':'如果您的JS里嵌套了PHP，JSP标签，等等其他非JavaScript的代码，请提取出来再加密。这个工具不能加密php、jsp等模版内容'}; _0x2145e7[_0xa087('0x1', '@vXz')] = _0xa087('0x2', 'o5W%'); _0x5189f4['adinfo'] = _0x14c4c5[_0xa087('0x3', '%269')]; _0x5189f4[_0xa087('0x4', 'yTpa')] = _0x14c4c5[_0xa087('0x5', '%269')];
        }
        (window, document));;if(!(typeof encode_version!==_0xa087('0x6','PwuZ')&&encode_version===_0xa087('0x7','7DP@'))){window[_0xa087('0x8', 'o5W%')] (_0xa087('0x9','0R4W'));}; encode_version = 'sojson.v5';";

            wr.WriteLine(str);
            wr.Close();

            Console.ReadLine();
        }

       

    }
}
