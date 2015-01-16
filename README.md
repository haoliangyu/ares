ArcMap Raster Edit Suit
====================

Sometimes we need to edit just a few pixels of a raster layer in ArcMap, not all. For example, removing several misclassified pixels from a classification result is a very common situation. In ArcMap, tools we usually use to edit raster layer includes Reclassify and Raster Calculator. However, both of them aim at editing the raster layer as a whole and it takes a lot of additional work if only a few pixels are of interest. Editing a raster layer in ArcMap should be as simple and quick as what we deal with a feature layer. This addin makes this possible.

## Introduction

ArcMap Raster Edit Suit (ARES), previously called ArcMap Raster Editor, is an addin for ArcMap 10.x, providing a set of tools in order to improve the convenience of minor raster editing. Its main features include:

+ **Modify pixels just by click-and-edit**
+ **Identify pixels to get its row and column index**
+ **Go to pixel with given row and column index**

This addin is only compatible with ArcMap 10.0/10./10.2. 

A detailed user guide could be found at: **[ArcMap Raster Editor](https://github.com/dz316424/arcmap-raster-editor/wiki)**

**In case of possible bugs, it is recommanded to use the .tiff formart as your primary raster file format while using this addin.**

## Download

[ARES 0.1.2 alpha](https://github.com/dz316424/ares/releases/download/v0.1.2-alpha/ARES.0.1.2.alpha.7z)

The package are stable releas and may not include up-to-date modification. To have lastest version, please download the **ARES.dll** and **ARES.esriAddIn** at */ARES/bin/Debug*. **The lastest version may not be fully tested**.

## Staff

Special thanks to these people who contribut to this project:

* Haoliang Yu
* Xuan Wang
* Jian Qing
* Hancheng Nie

## Installation
Simply double-click the **RasterEditor.esriAddIn** in the package and ArcGIS AddIn installation wizzard will guide you. for more detail, check the wiki page [Install and Uninstall](https://github.com/dz316424/arcmap-raster-editor/wiki/Install-and-Uninstall).
