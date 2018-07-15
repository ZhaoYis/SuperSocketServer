using SuperSocket.SocketBase.Protocol;
using System.Text;

namespace SuperSocketServer.AppBase
{
    /// <summary>
    /// 命令行协议
    /// </summary>
    public class CustomRequestInfo : IRequestInfo
    {
        public CustomRequestInfo(byte[] header, byte[] bodyBuffer)
        {
            Key = ((header[0] * 256) + header[1]).ToString();
            Data = bodyBuffer;
        }

        /// <summary>
        /// 协议号对应自定义命令Name,会触发自定义命令
        /// </summary>
        public string Key { get; }

        /// <summary>
        /// 正文字节码
        /// </summary>
        public byte[] Data { get; set; }

        /// <summary>
        /// 正文文本
        /// </summary>
        public string Body
        {
            get { return Encoding.UTF8.GetString(Data); }
        }
    }
}