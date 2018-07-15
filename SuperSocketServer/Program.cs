using SuperSocket.SocketBase;
using SuperSocket.SocketEngine;
using System;

namespace SuperSocketServer
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            //通过读取配置文件启动
            IBootstrap bootstrap = BootstrapFactory.CreateBootstrap();
            if (!bootstrap.Initialize())
            {
                Console.WriteLine("初始化失败！");
                Console.ReadKey();
                return;
            }

            Console.WriteLine("服务启动中...");

            var result = bootstrap.Start();
            foreach (var server in bootstrap.AppServers)
            {
                switch (server.State)
                {
                    case ServerState.Running:
                        Console.WriteLine($"{server.Name} 运行中");
                        break;

                    case ServerState.NotInitialized:
                        break;

                    case ServerState.Initializing:
                        break;

                    case ServerState.NotStarted:
                        break;

                    case ServerState.Starting:
                        break;

                    case ServerState.Stopping:
                        break;

                    default:
                        Console.WriteLine($"{server.Name} 启动失败");
                        break;
                }
            }

            switch (result)
            {
                case StartResult.Failed:
                    Console.WriteLine("无法启动服务，更多错误信息请查看日志");
                    break;

                case StartResult.None:
                    Console.WriteLine("没有服务器配置，请检查你的配置！");
                    break;

                case StartResult.PartialSuccess:
                    Console.WriteLine("一些服务启动成功，但是还有一些启动失败，更多错误信息请查看日志");
                    break;

                case StartResult.Success:
                    Console.WriteLine("服务已经开始！");
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }

            Console.WriteLine("输入'quit'以停止服务");

            ReadConsoleCommand(bootstrap);

            bootstrap.Stop();

            Console.WriteLine("The SuperSocket ServiceEndine has been stopped!");
        }

        private static void ReadConsoleCommand(IBootstrap bootstrap)
        {
            while (true)
            {
                var line = Console.ReadLine();
                if (string.IsNullOrEmpty(line))
                {
                    continue;
                }
                if ("quit".Equals(line, StringComparison.OrdinalIgnoreCase))
                    return;
            }
        }
    }
}