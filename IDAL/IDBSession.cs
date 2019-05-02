 //TT模板命名空间
	//生成的文件后缀名为.cs
//使用TT模板自动生成的代码片段
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDAL;

namespace IDAL
{
	//统一管理上下文的各种操作
    public interface IDbSession
    {  
				IFileUploaderRepository FileUploaderRepository{ get; }

				ISysAnnouncementRepository SysAnnouncementRepository{ get; }

				ISysDepartmentRepository SysDepartmentRepository{ get; }

				ISysDocumentSysDepartmentRepository SysDocumentSysDepartmentRepository{ get; }

				ISysDocumentSysPersonRepository SysDocumentSysPersonRepository{ get; }

				ISysEmailRepository SysEmailRepository{ get; }

				ISysEmailTempRepository SysEmailTempRepository{ get; }

				ISysExceptionRepository SysExceptionRepository{ get; }

				ISysFieldRepository SysFieldRepository{ get; }

				ISysLogRepository SysLogRepository{ get; }

				ISysMenuRepository SysMenuRepository{ get; }

				ISysMenuSysOperationRepository SysMenuSysOperationRepository{ get; }

				ISysMenuSysRoleSysOperationRepository SysMenuSysRoleSysOperationRepository{ get; }

				ISysMessageRepository SysMessageRepository{ get; }

				ISysMessageTempRepository SysMessageTempRepository{ get; }

				ISysNoticeRepository SysNoticeRepository{ get; }

				ISysOperationRepository SysOperationRepository{ get; }

				ISysPersonRepository SysPersonRepository{ get; }

				ISysRoleRepository SysRoleRepository{ get; }

				ISysRoleSysPersonRepository SysRoleSysPersonRepository{ get; }

		
        int Save();       

        
    }
}