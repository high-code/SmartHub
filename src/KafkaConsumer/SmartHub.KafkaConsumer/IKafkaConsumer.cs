using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SmartHub.KafkaConsumer
{
  public interface IKafkaConsumer
  {
    void Listen(Dictionary<string, Action<string>> callbacks, IEnumerable<string> topics, CancellationToken token = default(CancellationToken));

  }
}
