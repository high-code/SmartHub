using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;

namespace SmartHub.Edge.Configuration
{
  public class KafkaConfiguration
  {
    private readonly IConfiguration _configuration;

    public KafkaConfiguration(IConfiguration configuration)
    {
      _configuration = configuration;
    }

    public string BootstrapServer => _configuration["kafka_endpoint"];
  }
}
