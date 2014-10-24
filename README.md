ArcMap Raster Editor
====================

Sometimes we need to do some minor modification on our raster file. For example, we want to remove some classification errors on the classification result or noise pixels on the remote sensing image. Generally, we use the GIS platform like ArcMap and QGIS, or remote sensing software like ENVI. However, none of them is satisfactory. While the remote sensing software is not good at visualization, the GIS platform usually requires using the Raster Calculator or extra vector layers. There is no need to take extra steps for a simple task. That is why I started this project.
***

## Introduction

The ArcMap Raster Editor is a toolbar for ArcMap 10.x, providing a set of tools in order to improve the efficiency of minor raster editing. Its main features include:

+ **Click-and-edit, a simple process for pixel editing**
+ **Regional identification of pixel values and simple statistic**
+ **Quick locating interested pixels using row and column index**

The toolbar is only compatible for ArcMap with version above 10.0. To install it, just download the zip file below, and double click the RasterEditor.esriAddIn to install after unzipping the package. 

A detailed user guide could be found at: **[ArcMap Raster Editor](https://github.com/dz316424/arcmap-raster-editor/wiki)**

**In case of possible bugs, it is recommanded to use the .tiff formart as your primary raster file format while applying this addin.**

## Download

|ArcMap Version|Download|
|--------------|--------|
|10.0/10.1|[Raster Editor 1.1.5408 for ArcMap 10.0/10.1](https://github.com/dz316424/arcmap-raster-editor/blob/10.0/Release%20for%2010.0/1.1.5408.zip?raw=true)|
|10.2|[Raster Editor 1.1.5408 for ArcMap 10.2](https://github.com/dz316424/arcmap-raster-editor/blob/master/Release%20for%2010.2/1.1.5408.zip?raw=true)| 

The listed packages are stable releases and may not include up-to-date modification. To have lastest version, please download the **RasterEditor.dll** and **RasterEditor.esriAddIn** at */RasterEditor/bin/Debug*. **The lastest version may not be fully tested**.

## Installation
Simply double-click the **RasterEditor.esriAddIn** in the package and ArcGIS AddIn installation wizzard will guide you. for more detail, check the wiki page [Install and Uninstall](https://github.com/dz316424/arcmap-raster-editor/wiki/Install-and-Uninstall).
