using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using Dapper;
using Npgsql;
using SmartHub.SaverService.DbEntities;
using SmartHub.SaverService.DTO;

namespace SmartHub.SaverService
{
  public interface IRepository<in T>
  {
    void Add(T entity);
  }


  public class StorageRepository : IRepository<DbMeasurement>, IRepository<DbStatus>
  {

    private readonly string _connectionString;

    private const string _measurementInsertQuery = "INSERT INTO measurements (id, type, value, dtsend, deviceid) VALUES(DEFAULT, @Type, @Value, @DtSend, @DeviceId)";


    public StorageRepository(string connectionString)
    {
      _connectionString = connectionString;
    }

    public void Add(DbMeasurement measurement)
    {


      using(var conn = new NpgsqlConnection(_connectionString))
      {
        conn.Open();

        conn.Execute(_measurementInsertQuery, measurement);

        conn.Close();
      }
      
    }

    public void Add(DbStatus status)
    {
      using (var conn = new NpgsqlConnection(_connectionString))
      {
        conn.Open();

        // implement saving device status

        conn.Close();
      }
    }
  }
}
