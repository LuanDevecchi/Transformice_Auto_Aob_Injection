﻿using System;
using System.Runtime.InteropServices;


public class EnablePrivileges
{

    [DllImport("advapi32.dll", SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool OpenProcessToken(IntPtr ProcessHandle, uint DesiredAccess, out IntPtr TokenHandle);


    [DllImport("kernel32.dll", SetLastError = true)]
    private static extern IntPtr GetCurrentProcess();


    [DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool LookupPrivilegeValue(string lpSystemName, string lpName, out EnablePrivileges.LUID lpLuid);


    [DllImport("kernel32.dll", SetLastError = true)]
    private static extern bool CloseHandle(IntPtr hHandle);


    [DllImport("advapi32.dll", SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool AdjustTokenPrivileges(IntPtr TokenHandle, [MarshalAs(UnmanagedType.Bool)] bool DisableAllPrivileges, ref EnablePrivileges.TOKEN_PRIVILEGES NewState, uint Zero, IntPtr Null1, IntPtr Null2);


    public static void GoDebugPriv()
    {
        IntPtr intPtr;
        if (!EnablePrivileges.OpenProcessToken(EnablePrivileges.GetCurrentProcess(), EnablePrivileges.TOKEN_ADJUST_PRIVILEGES | EnablePrivileges.TOKEN_QUERY, out intPtr))
        {
            return;
        }
        EnablePrivileges.LUID luid;
        if (!EnablePrivileges.LookupPrivilegeValue(null, "SeDebugPrivilege", out luid))
        {
            EnablePrivileges.CloseHandle(intPtr);
            return;
        }
        EnablePrivileges.TOKEN_PRIVILEGES token_PRIVILEGES;
        token_PRIVILEGES.PrivilegeCount = 1u;
        token_PRIVILEGES.Luid = luid;
        token_PRIVILEGES.Attributes = 2u;
        EnablePrivileges.AdjustTokenPrivileges(intPtr, false, ref token_PRIVILEGES, 0u, IntPtr.Zero, IntPtr.Zero);
        EnablePrivileges.CloseHandle(intPtr);
    }


    private static uint STANDARD_RIGHTS_REQUIRED = 983040u;


    private static uint STANDARD_RIGHTS_READ = 131072u;


    private static uint TOKEN_ASSIGN_PRIMARY = 1u;


    private static uint TOKEN_DUPLICATE = 2u;


    private static uint TOKEN_IMPERSONATE = 4u;


    private static uint TOKEN_QUERY = 8u;


    private static uint TOKEN_QUERY_SOURCE = 16u;


    private static uint TOKEN_ADJUST_PRIVILEGES = 32u;


    private static uint TOKEN_ADJUST_GROUPS = 64u;


    private static uint TOKEN_ADJUST_DEFAULT = 128u;


    private static uint TOKEN_ADJUST_SESSIONID = 256u;


    private static uint TOKEN_READ = EnablePrivileges.STANDARD_RIGHTS_READ | EnablePrivileges.TOKEN_QUERY;


    private static uint TOKEN_ALL_ACCESS = EnablePrivileges.STANDARD_RIGHTS_REQUIRED | EnablePrivileges.TOKEN_ASSIGN_PRIMARY | EnablePrivileges.TOKEN_DUPLICATE | EnablePrivileges.TOKEN_IMPERSONATE | EnablePrivileges.TOKEN_QUERY | EnablePrivileges.TOKEN_QUERY_SOURCE | EnablePrivileges.TOKEN_ADJUST_PRIVILEGES | EnablePrivileges.TOKEN_ADJUST_GROUPS | EnablePrivileges.TOKEN_ADJUST_DEFAULT | EnablePrivileges.TOKEN_ADJUST_SESSIONID;

    public const string SE_ASSIGNPRIMARYTOKEN_NAME = "SeAssignPrimaryTokenPrivilege";

    public const string SE_AUDIT_NAME = "SeAuditPrivilege";

    public const string SE_BACKUP_NAME = "SeBackupPrivilege";

    public const string SE_CHANGE_NOTIFY_NAME = "SeChangeNotifyPrivilege";

    public const string SE_CREATE_GLOBAL_NAME = "SeCreateGlobalPrivilege";

    public const string SE_CREATE_PAGEFILE_NAME = "SeCreatePagefilePrivilege";


    public const string SE_CREATE_PERMANENT_NAME = "SeCreatePermanentPrivilege";


    public const string SE_CREATE_SYMBOLIC_LINK_NAME = "SeCreateSymbolicLinkPrivilege";

    public const string SE_CREATE_TOKEN_NAME = "SeCreateTokenPrivilege";


    public const string SE_DEBUG_NAME = "SeDebugPrivilege";


    public const string SE_ENABLE_DELEGATION_NAME = "SeEnableDelegationPrivilege";

 
    public const string SE_IMPERSONATE_NAME = "SeImpersonatePrivilege";


    public const string SE_INC_BASE_PRIORITY_NAME = "SeIncreaseBasePriorityPrivilege";

    public const string SE_INCREASE_QUOTA_NAME = "SeIncreaseQuotaPrivilege";

    public const string SE_INC_WORKING_SET_NAME = "SeIncreaseWorkingSetPrivilege";


    public const string SE_LOAD_DRIVER_NAME = "SeLoadDriverPrivilege";


    public const string SE_LOCK_MEMORY_NAME = "SeLockMemoryPrivilege";


    public const string SE_MACHINE_ACCOUNT_NAME = "SeMachineAccountPrivilege";


    public const string SE_MANAGE_VOLUME_NAME = "SeManageVolumePrivilege";

   
    public const string SE_PROF_SINGLE_PROCESS_NAME = "SeProfileSingleProcessPrivilege";


    public const string SE_RELABEL_NAME = "SeRelabelPrivilege";

    
    public const string SE_REMOTE_SHUTDOWN_NAME = "SeRemoteShutdownPrivilege";


    public const string SE_RESTORE_NAME = "SeRestorePrivilege";

    
    public const string SE_SECURITY_NAME = "SeSecurityPrivilege";


    public const string SE_SHUTDOWN_NAME = "SeShutdownPrivilege";


    public const string SE_SYNC_AGENT_NAME = "SeSyncAgentPrivilege";

    public const string SE_SYSTEM_ENVIRONMENT_NAME = "SeSystemEnvironmentPrivilege";

    public const string SE_SYSTEM_PROFILE_NAME = "SeSystemProfilePrivilege";


    public const string SE_SYSTEMTIME_NAME = "SeSystemtimePrivilege";


    public const string SE_TAKE_OWNERSHIP_NAME = "SeTakeOwnershipPrivilege";

   
    public const string SE_TCB_NAME = "SeTcbPrivilege";


    public const string SE_TIME_ZONE_NAME = "SeTimeZonePrivilege";

    
    public const string SE_TRUSTED_CREDMAN_ACCESS_NAME = "SeTrustedCredManAccessPrivilege";


    public const string SE_UNDOCK_NAME = "SeUndockPrivilege";


    public const string SE_UNSOLICITED_INPUT_NAME = "SeUnsolicitedInputPrivilege";


    public const uint SE_PRIVILEGE_ENABLED_BY_DEFAULT = 1u;


    public const uint SE_PRIVILEGE_ENABLED = 2u;


    public const uint SE_PRIVILEGE_REMOVED = 4u;


    public const uint SE_PRIVILEGE_USED_FOR_ACCESS = 2147483648u;


    public struct LUID
    {
     
        public uint LowPart;

      
        public int HighPart;
    }

   
    public struct TOKEN_PRIVILEGES
    {
     
        public uint PrivilegeCount;


        public EnablePrivileges.LUID Luid;


        public uint Attributes;
    }

    public struct LUID_AND_ATTRIBUTES
    {

        public EnablePrivileges.LUID Luid;

        public uint Attributes;
    }
}