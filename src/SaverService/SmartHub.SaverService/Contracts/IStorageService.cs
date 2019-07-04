using System;
using System.Collections.Generic;
using System.Text;
using SmartHub.SaverService.DTO;

namespace SmartHub.SaverService
{
  public interface IStorageService
  {
    void StoreMeasurements(string rawData);

    void StoreStatus(string rawData);
  }
}
