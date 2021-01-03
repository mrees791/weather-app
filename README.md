<!-- PROJECT SHIELDS -->
<!--
*** I'm using markdown "reference style" links for readability.
*** Reference links are enclosed in brackets [ ] instead of parentheses ( ).
*** See the bottom of this document for the declaration of the reference variables
*** for contributors-url, forks-url, etc. This is an optional, concise syntax you may use.
*** https://www.markdownguide.org/basic-syntax/#reference-style-links
-->



<!-- PROJECT LOGO -->
<br />
<p align="center">
  <a href="https://github.com/mrees791/weather-app">
    <img src="Images/logo.png" alt="Logo" width="80" height="80">
  </a>

  <h3 align="center">Weather App</h3>

  <p align="center">
    A weather application which provides a daily and hourly forecast.
  </p>
</p>



<!-- TABLE OF CONTENTS -->
<details open="open">
  <summary>Table of Contents</summary>
  <ol>
    <li>
      <a href="#about-the-project">About The Project</a>
      <ul>
        <li><a href="#built-with">Built With</a></li>
      </ul>
    </li>
    <li>
      <a href="#getting-started">Getting Started</a>
      <ul>
        <li><a href="#prerequisites">Prerequisites</a></li>
        <li><a href="#installation">Installation</a></li>
      </ul>
    </li>
    <li><a href="#contact">Contact</a></li>
    <li><a href="#acknowledgements">Acknowledgements</a></li>
  </ol>
</details>



<!-- ABOUT THE PROJECT -->
## About The Project

[![Product Name Screen Shot][product-screenshot]](https://github.com/mrees791/weather-app)

This application provides the daily and hourly forecast for any zip-code within the United States. It was made with Windows Presentation Foundation (WPF) and the Model-View-ViewModel (MVVM) design pattern. The OpenWeatherMap Weather API was used for collecting weather data and the Zippopotamus Zip API was used for collecting zip-code data. It was made for my CPSC-400 Programming Projects course. My primary goal during the development of this project was to gain experience working with JavaScript Object Notation (JSON) APIs and to learn how to implement automatic update functionality for desktop applications.

### Modules
* WeatherApp - The Weather app itself including the GUI. Uses MVVM design pattern along with the MVVM Light Toolkit.
* WeatherLibrary - A small, reusable library with models needed for the OpenWeatherMap and Zippopotamus APIs.
* WeatherTests - Contains unit tests for the WeatherLibrary.

### Built With

* [Visual Studio 2019](https://visualstudio.microsoft.com/downloads/)
* [.NET Framework 4.7.2](https://dotnet.microsoft.com/download/dotnet-framework/net472)
* [Windows Presentation Foundation](https://docs.microsoft.com/en-us/dotnet/desktop/wpf/overview/?view=netdesktop-5.0)

<!-- GETTING STARTED -->
## Getting Started

To get a local copy up and running follow these simple steps.

### Prerequisites

Download [Visual Studio 2019.](https://visualstudio.microsoft.com/downloads/)<br/>
In the Visual Studio Installer, install the .NET desktop development workload.

### Installation

1. Clone the repo
   ```sh
   git clone https://github.com/mrees791/weather-app.git
   ```
2. Open the WeatherApp.sln solution file with Visual Studio.
3. Build the solution in Visual Studio.

<!-- CONTACT -->
## Contact

Michael Rees - mrees791@gmail.com

Project Link: [https://github.com/mrees791/weather-app](https://github.com/mrees791/weather-app)



<!-- ACKNOWLEDGEMENTS -->
## Acknowledgements
* [MVVM Light Toolkit](http://www.mvvmlight.net/)
* [NLog](https://nlog-project.org/)
* [RestoreWindowPlace](https://www.nuget.org/packages/RestoreWindowPlace)
* [LiveCharts](https://lvcharts.net/)
* [AutoUpdater.NET](https://github.com/ravibpatel/AutoUpdater.NET)
* [OpenWeatherMap Weather API](https://openweathermap.org/api)
* [Zippopotam.us Zip API](https://www.zippopotam.us/)


<!-- MARKDOWN LINKS & IMAGES -->
[product-screenshot]: Images/screenshot.png
