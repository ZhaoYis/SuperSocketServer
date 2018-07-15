using SuperSocket.SocketBase.Command;
using SuperSocketServer.AppBase;
using System;
using System.Linq;
using SuperSocketServer.Commands.BaseService;

namespace SuperSocketServer.Commands
{
    public class Test : CommandBase<CustomSession, CustomRequestInfo>
    {
        private int Action = (int)CustomCommand.Test;

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

                session.SN = Guid.NewGuid().ToString();

                session.Push((ushort)Action, $"收到[{((CustomCommand)Action).GetDescription()}]信息");
            }
            else
            {
                
            }
        }
    }
}