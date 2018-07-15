using System;
using SuperSocket.Common;
using SuperSocket.SocketBase;
using SuperSocket.SocketBase.Metadata;
using SuperSocketServer.AppBase;
using SuperSocketServer.Commands.BaseService;

namespace SuperSocketServer.Commands
{
    /// <summary>
    /// 命令监视器-执行命令前调用
    /// </summary>
    public class CheckCommandFilterAttribute : CommandFilterAttribute
    {
        /// <summary>
        /// 执行命令前调用
        /// </summary>
        /// <param name="commandContext"></param>
        public override void OnCommandExecuting(CommandExecutingContext commandContext)
        {
            CustomSession session = (CustomSession)commandContext.Session;
            if (session != null)
            {
                session.Items["StartTime"] = DateTime.Now;

                if (commandContext.RequestInfo.Key.Equals("-1"))
                {
                    session.Push(Convert.ToUInt16(commandContext.RequestInfo.Key), "未知命令！");
                    //取消执行当前命令
                    commandContext.Cancel = true;
                }
            }
        }

        /// <summary>
        /// 命令结束后调用
        /// </summary>
        /// <param name="commandContext"></param>
        public override void OnCommandExecuted(CommandExecutingContext commandContext)
        {
            CustomSession session = (CustomSession)commandContext.Session;
            if (session != null)
            {
                var startTime = session.Items.GetValue<DateTime>("StartTime");
                var ts = DateTime.Now.Subtract(startTime);
                if (ts.TotalSeconds > 5 && session.Logger.IsInfoEnabled)
                {
                    session.Logger.InfoFormat("命令'{0}' 执行时间： {1} 秒！", commandContext.CurrentCommand.Name, ts.ToString());
                }
            }
        }
    }
}