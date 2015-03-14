ArcMap Raster Edit Suite
====================

Sometimes we need to edit just a few pixels of a raster layer in ArcMap, not all. For example, removing several misclassified pixels from a classification result is a very common situation. In ArcMap, tools we usually use to edit raster layer are Reclassify and Raster Calculator. However, both of them aim at editing the raster layer as a whole and it takes a lot of additional work if only a few pixels are of interest. Editing a raster layer in ArcMap should be as simple and quick as what we deal with a feature layer. This addin makes this possible.

## Introduction

ArcMap Raster Edit Suite (ARES), previously called ArcMap Raster Editor, is an ArcMap Add-In providing a set of tools in order to improve the convenience of minor raster editing. Its main features include:

It includes two toolbars:

* **Raster Editor** for editing and identifying pixels with given rows and columns.

![Raster Editor](http://haoliangyu.net/images/GIS/ares_editing/eidtor_toolbar.png)

* **Raster Painter** for painting values on the raster layer.

![Raster Painter](http://i59.tinypic.com/25fppig.png)

This addin works in ArcMap 10.0/10.1/10.2, currently not in 10.3. 

## Download & Install

* Download the package at [ARES 0.2.0](https://github.com/dz316424/ares/releases/download/0.2.0/ARES.0.2.0.zip)

* Unzip the installation file and get into the folder that mathces your ArcMap version.
 
* Double-click the *RasterEditor.esriAddIn* and click *Install Add-In* in the wizzard.

Now you have it. A detailed user guide could be found at [Wiki](https://github.com/dz316424/arcmap-raster-editor/wiki) or my blog articles:

* Raster Editor: ![Editing single pixels of raster layer in ArcMap with just a few clicks](http://haoliangyu.net/editing-single-pixels-of-raster-layer-in-arcmap-with-just-a-few-clicks.html#.VQOzWuFp4S8)

* Raster Painter: ![Yet another way to edit your raster layer in ArcMap: Paint on it!](Yet another way to edit your raster layer in ArcMap: Paint on it!)

In case of possible bugs, it is recommanded to use the .tiff formart as your primary raster file format while using this addin.

## Contributor

Special thanks to these people who contribute to this project:

* Haoliang Yu
* Xuan Wang
* Jian Qing
* Hancheng Nie

And those who support and enourage us to continue this project.
