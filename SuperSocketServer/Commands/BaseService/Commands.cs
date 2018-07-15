using System;
using System.ComponentModel;

namespace SuperSocketServer.Commands.BaseService
{
    /// <summary>
    /// 自定义命令
    /// </summary>
    public enum CustomCommand : ushort
    {
        [Description("用于测试的命令")]
        Test = 1000,

        [Description("心跳检测")]
        Heartbeat = 1001
    }

    public static class EnumUtil
    {
        public static string GetDescription(this Enum value, bool nameInstead = true)
        {
            var type = value.GetType();
            var name = Enum.GetName(type, value);
            if (name == null)
                return null;

            var field = type.GetField(name);
            var attribute = Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) as DescriptionAttribute;

            if (attribute == null && nameInstead)
                return name;
            return attribute?.Description;
        }
    }
}