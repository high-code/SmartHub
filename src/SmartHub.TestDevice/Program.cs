﻿// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using System;
using System.Diagnostics;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using IdentityModel;
using IdentityModel.Client;

// TODO Rewrite to emulate a device

namespace Client
{
  class Program
  {
    static IDiscoveryCache _cache = new DiscoveryCache("https://localhost:5001");

    public static void Main()
    {
      Console.Title = "Console Device Flow";

      Thread.Sleep(10000);
      var authorizeResponse = RequestAuthorizationAsync().Result;


      var tokenResponse = RequestTokenAsync(authorizeResponse).Result;
      Console.WriteLine(tokenResponse.AccessToken);
      //tokenResponse.Show();

      Console.ReadLine();
      CallServiceAsync(tokenResponse.AccessToken).Wait();
      Console.ReadLine();
    }

    static async Task<DeviceAuthorizationResponse> RequestAuthorizationAsync()
    {
      var disco = await _cache.GetAsync();
      if (disco.IsError) throw new Exception(disco.Error);

      var client = new HttpClient();
      var response = await client.RequestDeviceAuthorizationAsync(new DeviceAuthorizationRequest
      {
        Address = disco.DeviceAuthorizationEndpoint,
        ClientId = "bb6b646f-9cec-4f15-a7f9-5deb6788d4ee",
        ClientSecret = "device"
      });

      if (response.IsError) throw new Exception(response.Error);

      Console.WriteLine($"user code   : {response.UserCode}");
      Console.WriteLine($"device code : {response.DeviceCode}");
      Console.WriteLine($"URL         : {response.VerificationUri}");
      Console.WriteLine($"Complete URL: {response.VerificationUriComplete}");

      Console.WriteLine($"\nPress enter to launch browser ({response.VerificationUri})");
      Console.ReadLine();

      Process.Start(new ProcessStartInfo(response.VerificationUri) { UseShellExecute = true });
      return response;
    }

    private static async Task<TokenResponse> RequestTokenAsync(DeviceAuthorizationResponse authorizeResponse)
    {
      var disco = await _cache.GetAsync();
      if (disco.IsError) throw new Exception(disco.Error);

      var client = new HttpClient();

      while (true)
      {
        var response = await client.RequestDeviceTokenAsync(new DeviceTokenRequest
        {
          Address = disco.TokenEndpoint,
          ClientId = "bb6b646f-9cec-4f15-a7f9-5deb6788d4ee",
          ClientSecret = "device",
          DeviceCode = authorizeResponse.DeviceCode
        });

        if (response.IsError)
        {
          if (response.Error == OidcConstants.TokenErrors.AuthorizationPending || response.Error == OidcConstants.TokenErrors.SlowDown)
          {
            Console.WriteLine($"{response.Error}...waiting.");
            await Task.Delay(authorizeResponse.Interval * 1000);
          }
          else
          {
            throw new Exception(response.Error);
          }
        }
        else
        {
          return response;
        }
      }
    }

    static async Task CallServiceAsync(string token)
    {
      var client = new HttpClient();

      client.SetBearerToken(token);
      var response = await client.GetStringAsync("https://localhost:5000/api/device");

      Console.WriteLine(response);
    }
  }
}