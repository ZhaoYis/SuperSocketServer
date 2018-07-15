using System;
using SuperSocket.SocketBase.Command;
using SuperSocketServer.AppBase;
using SuperSocketServer.Commands.BaseService;

namespace SuperSocketServer.Commands
{
    public class Heartbeat : CommandBase<CustomSession, CustomRequestInfo>
    {
        private int Action = (int)CustomCommand.Heartbeat;

        /// <summary>Gets the name.</summary>
        public override string Name
        {
            get { return Action.ToString(); }
        }

        /// <summary>
        /// 上行（来自客户端的信息）
        /// </summary>
        /// <param name="session"></param>
        /// <param name="requestInfo"></param>
        public override void ExecuteCommand(CustomSession session, CustomRequestInfo requestInfo)
        {
            int key = int.Parse(requestInfo.Key);
            if (key == Action)
            {
                Console.WriteLine($"[{((CustomCommand)Action).GetDescription()}]命令被执行");
                Console.WriteLine("内容是：" + requestInfo.Body);

                if (requestInfo.Body.Equals("&"))
                {
                    session.Push((ushort)Action, "$");
                }
            }
            else
            {

            }
        }
    }
}