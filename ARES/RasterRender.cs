using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.DataSourcesRaster;

namespace ARES
{
    /// <summary>
    /// Private assess to members of rendering operation of raster layer.
    /// </summary>
    static class RasterRender
    {
        #region Public Method

        /// <summary>
        /// Gets the render type of the input raster layer.
        /// </summary>
        /// <param name="rasterLayer"></param>
        /// <returns></returns>
        public static RenderType GetRenderType(IRasterLayer rasterLayer)
        {
            IRasterRenderer rasterRenderer = rasterLayer.Renderer;

            if (rasterRenderer is RasterClassifyColorRampRenderer)
            {
                return RenderType.Classified;
            }
            else if (rasterRenderer is RasterDiscreteColorRenderer)
            {
                return RenderType.DiscreteColor;
            }
            else if (rasterRenderer is RasterStretchColorRampRenderer)
            {
                return RenderType.Stretched;
            }
            else if (rasterRenderer is RasterUniqueValueRenderer)
            {
                return RenderType.UniqueValues;
            }
            else
            {
                return RenderType.Unknow;
            }
        }
         
        /// <summary>
        /// Gets the render color of the specific value in the input raster layer.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="rasterLayer"></param>                         
        /// <returns></returns>             
        public static IColor GetRenderColor(IRasterLayer rasterLayer, double value)
        {
            RenderType renderType = GetRenderType(rasterLayer);

            IColor renderColor = null;
            switch (renderType)
            { 
                case RenderType.Classified:
                    renderColor = GetRenderColor_Classified(rasterLayer, value);
                    break;
                case RenderType.DiscreteColor:
                    renderColor = GetRenderColor_DiscreteColor(rasterLayer, value);
                    break;
                case RenderType.Stretched:
                    renderColor = GetRenderColor_Stretched(rasterLayer, value);
                    break;
                case RenderType.UniqueValues:
                    renderColor = GetRenderColor_UniqueValues(rasterLayer, value);
                    break;
                default:
                    break;
            }

            return renderColor;
        }

        /// <summary>
        /// Gets the render color of the specific position in the input raster layer.
        /// </summary>
        /// <param name="rasterLayer"></param>
        /// <param name="position"></param>
        /// <returns></returns>
        public static IColor GetRenderColor(IRasterLayer rasterLayer, Position position)
        {
            //return GetRenderColor(rasterLayer, Editor.GetValue(position, rasterLayer));
            return null;
        }

        #endregion

        #region private Method

        #region GetRenderColor  
      
        /// <summary>
        /// Ge the render color of the input layer rendering with classified color ramp.
        /// </summary>
        /// <param name="layer">Input raster layer</param>
        /// <param name="value">Pixel value</param>
        /// <returns></returns>
        private static IColor GetRenderColor_Classified(ILayer layer, double value)
        {
            IRasterLayer rasterLayer = (IRasterLayer)layer;

            // Check whether the value is NoData value
            IRasterProps rasterProps = (IRasterProps)rasterLayer.Raster;
            System.Array noDataValue = (System.Array)rasterProps.NoDataValue;
            if ((rasterProps.NoDataValue != null) && (Convert.ToDouble(noDataValue.GetValue(0)) == value))
            {
                return null;
            }

            RasterClassifyColorRampRenderer rasterRender = (RasterClassifyColorRampRenderer)rasterLayer.Renderer;
            IRasterRendererColorRamp colorRamp = (IRasterRendererColorRamp)rasterRender;
            for (int index = 0; index < rasterRender.ClassCount - 1; index++)
            {
                if (value <= rasterRender.Break[index])
                {
                    return colorRamp.ColorRamp.Color[index];
                }
            }

            return colorRamp.ColorRamp.Color[rasterRender.ClassCount - 1];
        }

        /// <summary>
        /// Ge the render color of the input layer rendering with discrete color ramp.
        /// </summary>
        /// <param name="layer">Input raster layer</param>
        /// <param name="value">Pixel value</param>
        /// <returns></returns>
        private static IColor GetRenderColor_DiscreteColor(ILayer layer, double value)
        {
            IRasterLayer rasterLayer = (IRasterLayer)layer;

            // Check whether the value is NoData value
            IRasterProps rasterProps = (IRasterProps)rasterLayer.Raster;
            System.Array noDataValue = (System.Array)rasterProps.NoDataValue;
            if ((rasterProps.NoDataValue != null) && (Convert.ToDouble(noDataValue.GetValue(0)) == value))
            {
                return null;
            }

            IRasterRendererColorRamp colorRamp = (IRasterRendererColorRamp)rasterLayer.Renderer;
            Random rnd = new Random();
            return colorRamp.ColorRamp.Color[rnd.Next(colorRamp.ColorRamp.Size)];
        }

        /// <summary>
        /// Ge the render color of the input layer rendering with discrete color ramp.
        /// </summary>
        /// <param name="layer">Input raster layer</param>     
        /// <param name="value">Pixel value</param>
        /// <returns></returns>
        private static IColor GetRenderColor_UniqueValues(ILayer layer, double value)
        {
            IRasterLayer rasterLayer = (IRasterLayer)layer;

            // Check whether the value is NoData value
            IRasterProps rasterProps = (IRasterProps)rasterLayer.Raster;
            System.Array noDataValue = (System.Array)rasterProps.NoDataValue;
            if ((rasterProps.NoDataValue != null) && (Convert.ToDouble(noDataValue.GetValue(0)) == value))
            {
                return null;
            }

            IRasterRendererUniqueValues uniqueValues = (IRasterRendererUniqueValues)rasterLayer.Renderer;
            IRasterRendererColorRamp colorRamp = (IRasterRendererColorRamp)rasterLayer.Renderer;
            for (int i = 0; i < uniqueValues.UniqueValues.Count; i++)
            {
                if (value == Convert.ToDouble(uniqueValues.UniqueValues.UniqueValue[i]))
                    return colorRamp.ColorRamp.Color[i];
            }

            return null;
        }

        /// <summary>
        /// Ge the render color of the input layer rendering with stretched color ramp.
        /// </summary>
        /// <param name="layer">Input raster layer</param>     
        /// <param name="value">Pixel value</param>
        /// <returns></returns>
        private static IColor GetRenderColor_Stretched(ILayer layer, double value)
        {
            IRasterLayer rasterLayer = (IRasterLayer)layer;

            // Check whether the value is NoData value
            IRasterProps rasterProps = (IRasterProps)rasterLayer.Raster;
            System.Array noDataValue = (System.Array)rasterProps.NoDataValue;
            if ((rasterProps.NoDataValue != null) && (Convert.ToDouble(noDataValue.GetValue(0)) == value))
            {
                return null;
            }

            IRasterRendererColorRamp colorRamp = (IRasterRendererColorRamp)rasterLayer.Renderer;
            IRasterStretchMinMax stretchMinMax = (IRasterStretchMinMax)rasterLayer.Renderer;

            double min = 0;
            double max = 0;
            if (stretchMinMax.UseCustomStretchMinMax)
            {
                min = stretchMinMax.CustomStretchMin;
                max = stretchMinMax.CustomStretchMax;
            }
            else
            {
                min = stretchMinMax.StretchMin;
                max = stretchMinMax.StretchMax;
            }

            if (value >= max)
            {
                return colorRamp.ColorRamp.Color[255];
            }

            if (value <= min)
            {
                return colorRamp.ColorRamp.Color[0];
            }

            return colorRamp.ColorRamp.Color[Convert.ToInt32(System.Math.Round(255 * (value - min)))];
        }

        #endregion

        #endregion
    }

    /// <summary>
    /// THe raster layer render Type                                           
    /// </summary>
    enum RenderType
    { 
        UniqueValues,
        Classified,
        Stretched,
        DiscreteColor,
        Unknow
    }
}
