using SuperSocketServer.AppBase;
using System;
using System.Linq;

namespace SuperSocketServer.Commands.BaseService
{
    public static class ExtensionPush
    {
        /// <summary>
        /// 推送消息
        /// </summary>
        /// <param name="session"></param>
        /// <param name="action"></param>
        /// <param name="text"></param>
        public static void Push(this CustomSession session, ushort action, string text)
        {
            var response = BitConverter.GetBytes(action).Reverse().ToList();
            var arr = System.Text.Encoding.UTF8.GetBytes(text);
            response.AddRange(BitConverter.GetBytes((ushort)arr.Length).Reverse().ToArray());
            response.AddRange(arr);

            session.Send(response.ToArray(), 0, response.Count);
        }
    }
}