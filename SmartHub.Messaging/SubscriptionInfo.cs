using System;
using System.Collections.Generic;
using System.Text;

namespace SmartHub.Messaging
{
  public class SubscriptionInfo
  {

    public Type HandlerType { get; }

    public SubscriptionInfo(Type handlerType) {
      HandlerType = handlerType;
    }
  }
}
