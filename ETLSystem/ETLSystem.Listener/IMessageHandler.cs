using System;
using System.Threading.Tasks;

namespace ETLSystem.Listener
{
    public interface IMessageHandler
    {
        Task<bool> ExecuteAsync(string message);
    }
}
