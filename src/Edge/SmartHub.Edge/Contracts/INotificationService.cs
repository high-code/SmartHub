using System;
using System.Collections.Generic;
using SmartHub.Edge.Models;

namespace SmartHub.Edge.Contracts
{
  public interface INotificationService
  {
    void Save(Telemetry telemetry);
  }
}
