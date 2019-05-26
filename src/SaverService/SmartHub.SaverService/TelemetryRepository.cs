using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using Dapper;

namespace SmartHub.SaverService
{

  public interface ITelemetryRepository
  {
    void Add(Telemetry telemetry);
  }

  public class TelemetryRepository : ITelemetryRepository
  {

    private readonly string _connectionString;
    public TelemetryRepository(string connectionString)
    {
      _connectionString = connectionString;
    }

    public void Add(Telemetry telemetry)
    {

      using (IDbConnection connection = new SqlConnection(_connectionString))
      {
        var sqlQuery = "INSERT INTO Telemetry (DeviceId, Type, Value) VALUES(@DeviceId, @Type, @Value)";

        connection.Execute(sqlQuery, telemetry);
      }
      
    }
  }
}
