using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aaron.Common
{
    public class XMLPath
    {

        //要能反射必须使用字段  属性的话就反射不出来

        //数据库连接
        public const string SQLConnection = "configuration/SQLConnection";

        //添加IIS,新厂家相关
        public const string CompanyProject = "configuration/CompanyProject";
        public const string Host = "configuration/Host";
        public const string HostIp = "configuration/HostIp";
        public const string IISWebUrl = "configuration/IISWebUrl";
        public const string IISMobileUrl = "configuration/IISMobileUrl";
        public const string IISWebIp = "configuration/IISWebIp";
        public const string IISMobileIp = "configuration/IISMobileIp";

        //更新全部代码段
        public const string OldMadnuId = "configuration/OldMadnuId";

        //更新全部Ftp路径
        public const string Ftp = "configuration/Ftp";
        public const string OldFtp = "configuration/OldFtp";

        //解析二维码路径
        public const string ErWeiMaPath = "configuration/ErWeiMaPath";

        //代码段路径    
        public const string SQLShortCut = "configuration/SQLShortCut";
        public const string NavicatShortPath = "configuration/NavicatShortPath";
        public const string NavicatOldManuId = "configuration/NavicatOldManuId";

        //添加新代码段  
        public const string StandardSQLShortCut = "configuration/StandardSQLShortCut";

        //所有程序的路径
        public const string UpdateTCFtpConfigExe = "configuration/UpdateTCFtpConfigExe";
        public const string UpdateShortCodeExe = "configuration/UpdateShortCodeExe";
        public const string UpdateOneLineExe = "configuration/UpdateOneLineExe";
        public const string QueryCodeExe = "configuration/QueryCodeExe";
        public const string AddNewManuExe = "configuration/AddNewManuExe";
        public const string OpenMyTools = "configuration/OpenMyToolsExe";
        public const string CheckLogExe = "configuration/CheckLogExe";
        public const string WsBuildExe = "configuration/WsBuildExe";
        public const string OpenBrowserExe = "configuration/OpenBrowserExe";
        public const string OpenBatchSoftwareExe = "configuration/OpenBatchSoftwareExe";
        public const string CopyViewExe = "configuration/CopyViewExe";
        public const string CreateSQLExe = "configuration/CreateSQLExe";
        public const string CopyGitItemExe = "configuration/CopyGitItemExe";        
        public const string RestartAhkExe = "configuration/RestartAhkExe";
        //所有程序路径结束

        public const string OldPwdQuery = "configuration/OldPwdQuery";
        public const string ChromePath = "configuration/ChromePath";

        public const string CopyViewManuName = "configuration/CopyViewManuName";
        public const string CopyViewNameList = "configuration/CopyViewNameList";
        public const string CopyViewExeCheckBox = "configuration/CopyViewExeCheckBox";

        public const string NavicatExe = "configuration/NavicatExe";

    }
}
