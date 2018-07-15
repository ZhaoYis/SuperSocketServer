using System;
using SuperSocket.SocketBase.Command;
using SuperSocketServer.AppBase;
using SuperSocketServer.Commands.BaseService;

namespace SuperSocketServer.Commands.SingleBidDoc
{
    public class Dissent : CommandBase<MySession, MyRequestInfo>, IPush
    {
        private const SuperSocketServer.Commands.BaseService.SingleBidDoc Action = SuperSocketServer.Commands.BaseService.SingleBidDoc.Dissent;
        public override string Name => ((int)Action).ToString();

        /// <inheritdoc />
        /// <summary>
        /// 上行（来自客户端的信息）
        /// </summary>
        /// <param name="session"></param>
        /// <param name="requestInfo"></param>
        public override void ExecuteCommand(MySession session, MyRequestInfo requestInfo)
        {
            Console.WriteLine($"{Action.GetDescription()}命令被执行");
            Console.WriteLine("内容是" + requestInfo.Body);
            this.Push(session, (ushort)Action, $"收到{Action.GetDescription()}信息");
        }


    }
}
