﻿using System;

namespace SmartHub.IntegrationEventLog
{
  public enum EventState
  { 
    NotPublished = 0,
    InProgress = 1,
    Published = 2,
    PublishFailed = 3
  }
}
