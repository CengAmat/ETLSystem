using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ETLSystem.Service;

namespace ETLSystem.Listener
{
    public interface IListener
    {
        Task ExecuteAsync(CancellationToken stoppingToken);
    }


}
