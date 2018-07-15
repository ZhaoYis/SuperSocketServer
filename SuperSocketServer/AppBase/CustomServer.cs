using SuperSocket.SocketBase;
using SuperSocket.SocketBase.Protocol;
using System;
using SuperSocket.SocketBase.Config;
using SuperSocketServer.Commands;

namespace SuperSocketServer.AppBase
{
    /// <summary>
    /// 自定义监听客户端连接，承载TCP连接的服务器实例
    /// </summary>
    [CheckCommandFilter]
    public sealed class CustomServer : AppServer<CustomSession, CustomRequestInfo>
    {
        /// <summary>
        /// 通过配置文件安装服务从这里启动
        /// </summary>
        public CustomServer() : base(new DefaultReceiveFilterFactory<CustomReceiveFilter, CustomRequestInfo>())
        {
            this.NewSessionConnected += OnNewSessionConnected;
            this.SessionClosed += OnSessionClosed;
            //this.NewRequestReceived += OnNewRequestReceived;
        }

        protected override bool Setup(IRootConfig rootConfig, IServerConfig config)
        {
            string ip = config.Ip;

            return base.Setup(rootConfig, config);
        }

        protected override void OnStarted()
        {
            Console.WriteLine("启动成功");
        }

        /// <summary>Called when [stopped].</summary>
        protected override void OnStopped()
        {
            Console.WriteLine("服务停止");
        }

        /// <summary>Called when [new session connected].</summary>
        /// <param name="session">The session.</param>
        protected override void OnNewSessionConnected(CustomSession session)
        {
            Console.WriteLine($"新的客户端已经连接,{session.RemoteEndPoint.Address}:{session.RemoteEndPoint.Port}");
        }

        /// <summary>Called when [socket session closed].</summary>
        /// <param name="session">The session.</param>
        /// <param name="reason">The reason.</param>
        protected override void OnSessionClosed(CustomSession session, CloseReason reason)
        {
            Console.WriteLine($"客户端[{session.RemoteEndPoint.Address}:{session.RemoteEndPoint.Port}]关闭,原因：{reason.ToString()}");
        }

        private void OnNewRequestReceived(CustomSession session, CustomRequestInfo requestinfo)
        {
            string key = requestinfo.Key;

            Test test = new Test();
            test.ExecuteCommand(session, requestinfo);

            Console.WriteLine("新的请求到达");
        }
    }
}