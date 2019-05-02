 //TT模板命名空间
	//生成的文件后缀名为.cs

//使用TT模板自动生成的代码片段
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDAL;

namespace DAL
{
	//统一管理上下文的各种操作
    public partial class DbSession : IDbSession
    {  
				public IFileUploaderRepository FileUploaderRepository
        {
            get
            {
                return new FileUploaderRepository();
            }
        }

				public ISysAnnouncementRepository SysAnnouncementRepository
        {
            get
            {
                return new SysAnnouncementRepository();
            }
        }

				public ISysDepartmentRepository SysDepartmentRepository
        {
            get
            {
                return new SysDepartmentRepository();
            }
        }

				public ISysDocumentSysDepartmentRepository SysDocumentSysDepartmentRepository
        {
            get
            {
                return new SysDocumentSysDepartmentRepository();
            }
        }

				public ISysDocumentSysPersonRepository SysDocumentSysPersonRepository
        {
            get
            {
                return new SysDocumentSysPersonRepository();
            }
        }

				public ISysEmailRepository SysEmailRepository
        {
            get
            {
                return new SysEmailRepository();
            }
        }

				public ISysEmailTempRepository SysEmailTempRepository
        {
            get
            {
                return new SysEmailTempRepository();
            }
        }

				public ISysExceptionRepository SysExceptionRepository
        {
            get
            {
                return new SysExceptionRepository();
            }
        }

				public ISysFieldRepository SysFieldRepository
        {
            get
            {
                return new SysFieldRepository();
            }
        }

				public ISysLogRepository SysLogRepository
        {
            get
            {
                return new SysLogRepository();
            }
        }

				public ISysMenuRepository SysMenuRepository
        {
            get
            {
                return new SysMenuRepository();
            }
        }

				public ISysMenuSysOperationRepository SysMenuSysOperationRepository
        {
            get
            {
                return new SysMenuSysOperationRepository();
            }
        }

				public ISysMenuSysRoleSysOperationRepository SysMenuSysRoleSysOperationRepository
        {
            get
            {
                return new SysMenuSysRoleSysOperationRepository();
            }
        }

				public ISysMessageRepository SysMessageRepository
        {
            get
            {
                return new SysMessageRepository();
            }
        }

				public ISysMessageTempRepository SysMessageTempRepository
        {
            get
            {
                return new SysMessageTempRepository();
            }
        }

				public ISysNoticeRepository SysNoticeRepository
        {
            get
            {
                return new SysNoticeRepository();
            }
        }

				public ISysOperationRepository SysOperationRepository
        {
            get
            {
                return new SysOperationRepository();
            }
        }

				public ISysPersonRepository SysPersonRepository
        {
            get
            {
                return new SysPersonRepository();
            }
        }

				public ISysRoleRepository SysRoleRepository
        {
            get
            {
                return new SysRoleRepository();
            }
        }

				public ISysRoleSysPersonRepository SysRoleSysPersonRepository
        {
            get
            {
                return new SysRoleSysPersonRepository();
            }
        }

		
           public  int Save()
        {
            return  EFContextFactory.GetCurrentDbContext().SaveChanges();
        }


    }
}