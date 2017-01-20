using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Text;

public enum TextType
{
    None,
    //写入同一个file中
    Discrete
    //
}

public enum LogChannel
{
    Error = 1,
    Warning = 2,
    Statistic = 4,
    Default = 8,
    Config = 16,
    Newbie = 32,
    Debug = 64,
    All = 128,
    Protocol = 256,
    Protocol_Core = 512,
    BigWorld_Create = 1024,
    BigWorld_Delete = 2048,
    Memory = 4096,
    SysModel = 8192,
    Managers = 16384,
    WSC = 32768,
    ZACKS = 65536,
    Chanyow = 131072
}


