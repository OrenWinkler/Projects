using Microsoft.Azure.ServiceBus;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace QueueService
{
    public interface IQueueConnector : IDisposable
    {
        Task SendMessage(object msg);
        Task RegisterOnMessageHandlerAndReceiveMessages(Func<Message, CancellationToken, Task> handler);
    }
}