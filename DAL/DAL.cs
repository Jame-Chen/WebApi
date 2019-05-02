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

namespace DAL
{
	
	public partial class FileUploaderRepository : BaseRepository<FileUploader>,IFileUploaderRepository
    {
    }
	
	public partial class SysAnnouncementRepository : BaseRepository<SysAnnouncement>,ISysAnnouncementRepository
    {
    }
	
	public partial class SysDepartmentRepository : BaseRepository<SysDepartment>,ISysDepartmentRepository
    {
    }
	
	public partial class SysDocumentSysDepartmentRepository : BaseRepository<SysDocumentSysDepartment>,ISysDocumentSysDepartmentRepository
    {
    }
	
	public partial class SysDocumentSysPersonRepository : BaseRepository<SysDocumentSysPerson>,ISysDocumentSysPersonRepository
    {
    }
	
	public partial class SysEmailRepository : BaseRepository<SysEmail>,ISysEmailRepository
    {
    }
	
	public partial class SysEmailTempRepository : BaseRepository<SysEmailTemp>,ISysEmailTempRepository
    {
    }
	
	public partial class SysExceptionRepository : BaseRepository<SysException>,ISysExceptionRepository
    {
    }
	
	public partial class SysFieldRepository : BaseRepository<SysField>,ISysFieldRepository
    {
    }
	
	public partial class SysLogRepository : BaseRepository<SysLog>,ISysLogRepository
    {
    }
	
	public partial class SysMenuRepository : BaseRepository<SysMenu>,ISysMenuRepository
    {
    }
	
	public partial class SysMenuSysOperationRepository : BaseRepository<SysMenuSysOperation>,ISysMenuSysOperationRepository
    {
    }
	
	public partial class SysMenuSysRoleSysOperationRepository : BaseRepository<SysMenuSysRoleSysOperation>,ISysMenuSysRoleSysOperationRepository
    {
    }
	
	public partial class SysMessageRepository : BaseRepository<SysMessage>,ISysMessageRepository
    {
    }
	
	public partial class SysMessageTempRepository : BaseRepository<SysMessageTemp>,ISysMessageTempRepository
    {
    }
	
	public partial class SysNoticeRepository : BaseRepository<SysNotice>,ISysNoticeRepository
    {
    }
	
	public partial class SysOperationRepository : BaseRepository<SysOperation>,ISysOperationRepository
    {
    }
	
	public partial class SysPersonRepository : BaseRepository<SysPerson>,ISysPersonRepository
    {
    }
	
	public partial class SysRoleRepository : BaseRepository<SysRole>,ISysRoleRepository
    {
    }
	
	public partial class SysRoleSysPersonRepository : BaseRepository<SysRoleSysPerson>,ISysRoleSysPersonRepository
    {
    }
	    
}