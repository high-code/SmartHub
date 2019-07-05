import { Component, OnInit, ElementRef, ViewChild, AfterViewInit, AfterContentInit } from '@angular/core';
import { Router, ActivatedRoute, ParamMap, Route } from '@angular/router';
import { switchMap } from 'rxjs/operators';
import { Device } from '../models/device';
import { DeviceService } from '../services/device.service';
import { Observable } from 'rxjs';
import { NgxSpinnerService } from 'ngx-spinner';
import * as signalR from '@aspnet/signalr';
import * as Chart from "chart.js";
import { Telemetry } from '../models/telemetry.model';
import { ConfigurationService } from '../services/configuration.service';

@Component({
  selector: 'app-device-page',
  templateUrl: './device-page.component.html',
  styleUrls: ['./device-page.component.less']
})
export class DevicePageComponent implements OnInit, AfterViewInit {

  device: Device;
  measurements: number[];
  connection: signalR.HubConnection;

  canvas: any;
  ctx: any;
  chart: Chart;
  values: Chart.ChartPoint[] = [];
  speed: number = 100;

  constructor(private route: ActivatedRoute,
    private router: Router,
    private deviceService: DeviceService,
    private configurationService: ConfigurationService) {

    if (this.configurationService.isLoaded) {
      this.setupSignalRConnection(this.configurationService.configuration.notificationServiceUrl)
    } else {
      this.configurationService.configurationLoaded$.subscribe(conf => {
        this.setupSignalRConnection(this.configurationService.configuration.notificationServiceUrl)
      })
    }
    this.measurements = [1];
  }

  ngOnInit() {
    this.route.paramMap.pipe(
      switchMap((params: ParamMap) => this.deviceService.getDevice(+params.get('id')))
    ).subscribe(device => {
      this.device = device;
    });

    this.connection.start()
      .then(() => {
        this.connection.invoke("subscribe", this.device.deviceId)
        this.connection.on("sendMeasurement", (data) => {
          this.receiveTelemetry(data);
        })
      })
      .catch(error => console.log(error));
  }

  setupSignalRConnection(baseUrl: string) {
    this.connection = new signalR.HubConnectionBuilder()
      .withUrl(`${baseUrl}/telemetry`)
      .configureLogging(signalR.LogLevel.Debug)
      .build();
  }

  ngAfterViewInit() {

    this.canvas = document.getElementById("chart");
    this.ctx = this.canvas.getContext('2d');

    this.chart = new Chart(this.ctx,
      {
        type: 'line',
        data: {
          datasets: [{
            label: 'Temperature',
            borderColor: 'rgb(255, 99, 132)',
            data: this.values
          }],

        },

        options: {
          responsive: true,
          animation: {
            duration: this.speed * 1.5,
            easing: "linear"
          },
          scales: {
            xAxes: [{
              type: "time",
              display: true
            }],
            yAxes: [{
              ticks: {
                max: 50,
                min: -30
              }
            }]

          }
        }
      });
  }


  deleteDevice(id: string) {
    this.deviceService.deleteDevice(id)
      .subscribe(_ => this.goBack());

  }
  goBack() {
    this.router.navigate(['/devices'])
  }


  receiveTelemetry(data: Telemetry) {


    this.values.push({ x: new Date(), y: data.value });

    requestAnimationFrame(() => {
      if (this.values[0] != null) {
        this.chart.update();
      }

      this.chart.update();
    });

    if (this.values.length > 10)
      this.values.shift();


  }




}
