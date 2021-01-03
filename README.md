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
  <a href="https://www.vecteezy.com/free-vector/atom">
    <img src="Images/logo.png" alt="Logo" width="80" height="80">
  </a>

  <h3 align="center">Periodic Table App</h3>

  <p align="center">
    An interactive periodic table of elements made for educational purposes.
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

[![Product Name Screen Shot][product-screenshot]](https://github.com/mrees791/chemistry-app)

This app is an interactive periodic table of elements where the user may click on an element to view more details. I made this to learn the fundamentals of Windows Presentation Foundation (WPF) and the Model-View-ViewModel (MVVM) design pattern.

### Modules
* ChemistryApp - The Periodic Table app itself including the GUI. Uses MVVM design pattern along with the MVVM Light Toolkit.
* ChemistryLibrary - A small, reusable library with models needed for the periodic table of elements.
* ChemistryTests - Contains unit tests for the ChemistryLibrary.

### Built With

* [Visual Studio 2019](https://visualstudio.microsoft.com/downloads/)
* [.NET Framework 4.5.2](https://www.microsoft.com/en-us/download/details.aspx?id=42642)
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
   git clone https://github.com/mrees791/chemistry-app.git
   ```
2. Open the ChemistryApp.sln solution file with Visual Studio.
3. Build the solution in Visual Studio.

<!-- CONTACT -->
## Contact

Michael Rees - mrees791@gmail.com

Project Link: [https://github.com/mrees791/chemistry-app](https://github.com/mrees791/chemistry-app)



<!-- ACKNOWLEDGEMENTS -->
## Acknowledgements
* [MVVM Light Toolkit](http://www.mvvmlight.net/)
* [NLog](https://nlog-project.org/)
* [RestoreWindowPlace](https://www.nuget.org/packages/RestoreWindowPlace)
* [Project Logo](https://www.vecteezy.com/free-vector/atom)

<!-- MARKDOWN LINKS & IMAGES -->
[product-screenshot]: Images/screenshot.png
