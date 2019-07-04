using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Confluent.Kafka;
using Microsoft.Extensions.Logging;


namespace SmartHub.KafkaConsumer
{
  public class Consumer : IKafkaConsumer
  {
    private readonly ILogger<Consumer> _logger;
    private readonly ConsumerConfig _consumerConfig;

    public Consumer(ILogger<Consumer> logger, ConsumerConfig consumerConfiguration)
    {
      _logger = logger;
      _consumerConfig = consumerConfiguration;
    }

    public void Listen(Dictionary<string, Action<string>> consumersCallbacks, IEnumerable<string> topics, CancellationToken token)
    {


      using (var c = new ConsumerBuilder<Ignore, string>(_consumerConfig).Build())
      {

        c.Subscribe(topics);

        try
        {
          while (true)
          {
            try
            {
              var cr = c.Consume(token);

              var actionToExecute = consumersCallbacks[cr.Topic];

              actionToExecute.Invoke(cr.Value);

            }
            catch (ConsumeException e)
            {
              _logger.LogError(e, "Exception occured");
            }
          }
        }
        catch (Exception)
        {
          c.Close();
          throw;
        }
      }
    }

  }
}
