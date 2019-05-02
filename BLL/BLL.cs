 //TT模板命名空间
	//生成的文件后缀名为.cs

//使用TT模板自动生成的代码片段
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using IDAL;
using DAL;
using IBLL;

namespace BLL
{
		
    public partial  class FileUploaderService:BaseService<FileUploader>,IFileUploaderService
    {
        public override void SetCurrentRepository()
        {
            CurrentRepository = DbSessionFactory.GetCurrentDbSession().FileUploaderRepository;
        }
    }
		
    public partial  class SysAnnouncementService:BaseService<SysAnnouncement>,ISysAnnouncementService
    {
        public override void SetCurrentRepository()
        {
            CurrentRepository = DbSessionFactory.GetCurrentDbSession().SysAnnouncementRepository;
        }
    }
		
    public partial  class SysDepartmentService:BaseService<SysDepartment>,ISysDepartmentService
    {
        public override void SetCurrentRepository()
        {
            CurrentRepository = DbSessionFactory.GetCurrentDbSession().SysDepartmentRepository;
        }
    }
		
    public partial  class SysDocumentSysDepartmentService:BaseService<SysDocumentSysDepartment>,ISysDocumentSysDepartmentService
    {
        public override void SetCurrentRepository()
        {
            CurrentRepository = DbSessionFactory.GetCurrentDbSession().SysDocumentSysDepartmentRepository;
        }
    }
		
    public partial  class SysDocumentSysPersonService:BaseService<SysDocumentSysPerson>,ISysDocumentSysPersonService
    {
        public override void SetCurrentRepository()
        {
            CurrentRepository = DbSessionFactory.GetCurrentDbSession().SysDocumentSysPersonRepository;
        }
    }
		
    public partial  class SysEmailService:BaseService<SysEmail>,ISysEmailService
    {
        public override void SetCurrentRepository()
        {
            CurrentRepository = DbSessionFactory.GetCurrentDbSession().SysEmailRepository;
        }
    }
		
    public partial  class SysEmailTempService:BaseService<SysEmailTemp>,ISysEmailTempService
    {
        public override void SetCurrentRepository()
        {
            CurrentRepository = DbSessionFactory.GetCurrentDbSession().SysEmailTempRepository;
        }
    }
		
    public partial  class SysExceptionService:BaseService<SysException>,ISysExceptionService
    {
        public override void SetCurrentRepository()
        {
            CurrentRepository = DbSessionFactory.GetCurrentDbSession().SysExceptionRepository;
        }
    }
		
    public partial  class SysFieldService:BaseService<SysField>,ISysFieldService
    {
        public override void SetCurrentRepository()
        {
            CurrentRepository = DbSessionFactory.GetCurrentDbSession().SysFieldRepository;
        }
    }
		
    public partial  class SysLogService:BaseService<SysLog>,ISysLogService
    {
        public override void SetCurrentRepository()
        {
            CurrentRepository = DbSessionFactory.GetCurrentDbSession().SysLogRepository;
        }
    }
		
    public partial  class SysMenuService:BaseService<SysMenu>,ISysMenuService
    {
        public override void SetCurrentRepository()
        {
            CurrentRepository = DbSessionFactory.GetCurrentDbSession().SysMenuRepository;
        }
    }
		
    public partial  class SysMenuSysOperationService:BaseService<SysMenuSysOperation>,ISysMenuSysOperationService
    {
        public override void SetCurrentRepository()
        {
            CurrentRepository = DbSessionFactory.GetCurrentDbSession().SysMenuSysOperationRepository;
        }
    }
		
    public partial  class SysMenuSysRoleSysOperationService:BaseService<SysMenuSysRoleSysOperation>,ISysMenuSysRoleSysOperationService
    {
        public override void SetCurrentRepository()
        {
            CurrentRepository = DbSessionFactory.GetCurrentDbSession().SysMenuSysRoleSysOperationRepository;
        }
    }
		
    public partial  class SysMessageService:BaseService<SysMessage>,ISysMessageService
    {
        public override void SetCurrentRepository()
        {
            CurrentRepository = DbSessionFactory.GetCurrentDbSession().SysMessageRepository;
        }
    }
		
    public partial  class SysMessageTempService:BaseService<SysMessageTemp>,ISysMessageTempService
    {
        public override void SetCurrentRepository()
        {
            CurrentRepository = DbSessionFactory.GetCurrentDbSession().SysMessageTempRepository;
        }
    }
		
    public partial  class SysNoticeService:BaseService<SysNotice>,ISysNoticeService
    {
        public override void SetCurrentRepository()
        {
            CurrentRepository = DbSessionFactory.GetCurrentDbSession().SysNoticeRepository;
        }
    }
		
    public partial  class SysOperationService:BaseService<SysOperation>,ISysOperationService
    {
        public override void SetCurrentRepository()
        {
            CurrentRepository = DbSessionFactory.GetCurrentDbSession().SysOperationRepository;
        }
    }
		
    public partial  class SysPersonService:BaseService<SysPerson>,ISysPersonService
    {
        public override void SetCurrentRepository()
        {
            CurrentRepository = DbSessionFactory.GetCurrentDbSession().SysPersonRepository;
        }
    }
		
    public partial  class SysRoleService:BaseService<SysRole>,ISysRoleService
    {
        public override void SetCurrentRepository()
        {
            CurrentRepository = DbSessionFactory.GetCurrentDbSession().SysRoleRepository;
        }
    }
		
    public partial  class SysRoleSysPersonService:BaseService<SysRoleSysPerson>,ISysRoleSysPersonService
    {
        public override void SetCurrentRepository()
        {
            CurrentRepository = DbSessionFactory.GetCurrentDbSession().SysRoleSysPersonRepository;
        }
    }
	    
}