using System;
using System.Reflection;
using System.IO;
using System.Xml.Linq;
using System.Drawing;
using System.Runtime.InteropServices;

using System.Windows.Forms;

using ESRI.ArcGIS.Display;
using ESRI.ArcGIS;

namespace RasterEditor
{
    /// <summary>
    /// Provides access to members of the editor configuration.
    /// </summary>
    static class Config
    {
        #region Attributes

        private static bool isLoaded = false;
        private static bool customEditRender = false;
        private static ISimpleFillSymbol selectionSymbol = null;
        private static ISimpleFillSymbol editSymbol = null;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets a value indicating whether the configuration has benn loaded.
        /// </summary>
        public static bool IsLoaded
        {
            get { return isLoaded; }
            set { isLoaded = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the edtis will be rendered as the ArcGis styple.
        /// </summary>
        public static bool CustormEditColor
        {
            get { return customEditRender; }
            set { customEditRender = value; }
        }

        /// <summary>
        /// Gets or sets the render symbol of the selection box.
        /// </summary>
        public static ISimpleFillSymbol SelectionSmbol
        {
            get { return selectionSymbol; }
            set 
            {
                if (value != null)
                {
                    selectionSymbol = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the render symbol of the edit box.
        /// </summary>
        public static ISimpleFillSymbol EditSymbol
        {
            get { return editSymbol; }
            set 
            {
                if (value != null)
                {
                    editSymbol = value;    
                }
            }
        }

        /// <summary>
        /// Gets the path of the configuration file.
        /// </summary>
        public static string ConfigFile
        {
            get 
            {
                Assembly assembly = Assembly.GetExecutingAssembly();
                var attribute = (GuidAttribute)assembly.GetCustomAttributes(typeof(GuidAttribute), true)[0];
                string guid = attribute.Value;
                string version = RuntimeManager.ActiveRuntime.Version;
                return Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\ArcGIS\\AddIns\\Desktop" + 
                                     version + "\\{" + guid + "}\\" + "editorconfig.xml";
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Resets the default configuration.
        /// </summary>
        public static void SetDefault()
        {
            selectionSymbol = new SimpleFillSymbolClass();
            selectionSymbol.Color = new RgbColorClass() { NullColor = true, Transparency = 0 };
            ISimpleLineSymbol selectionOutlineSymbol = new SimpleLineSymbolClass();
            selectionOutlineSymbol.Color = new RgbColorClass() { Red = 0, Green = 255, Blue = 255};
            selectionOutlineSymbol.Width = 2;
            selectionSymbol.Outline = selectionOutlineSymbol;

            customEditRender = true;
            editSymbol = new SimpleFillSymbolClass();
            editSymbol.Color = new RgbColorClass() { Red = 255, Green = 255, Blue = 90, Transparency = 127 };
            ISimpleLineSymbol editOutlineSymbol = new SimpleLineSymbolClass();
            editOutlineSymbol.Color = new RgbColorClass() { Red = 255, Green = 255, Blue = 0 };
            editOutlineSymbol.Width = 2;
            editSymbol.Outline = editOutlineSymbol;
        }

        /// <summary>
        /// Saves the configuration to file.
        /// </summary>
        public static void Save()
        {
            if (File.Exists(ConfigFile))
            {
                File.Delete(ConfigFile);
            }

            XDocument config = new XDocument(
                new XElement("root",
                    new XElement("symbol",
                        GetBoxSymbolXElement("selection", selectionSymbol),
                        GetBoxSymbolXElement("edit", editSymbol, customEditRender))));

            config.Save(ConfigFile);
        }

        /// <summary>
        /// Loads the editor configuration from file.
        /// </summary>
        public static void Load()
        {
            if (!File.Exists(ConfigFile))
            {
                Create();
                isLoaded = true;
                return;
            }

            XDocument config = XDocument.Load(ConfigFile);
            
            Color wColor;
            XElement selectionSymbolNode = config.Root.Element("symbol").Element("selection");
            selectionSymbol = new SimpleFillSymbolClass();
            ISimpleLineSymbol selectionOutlineSymbol = new SimpleLineSymbolClass();
            foreach (XElement xmlColor in selectionSymbolNode.Elements("color"))
            {
                wColor = ColorTranslator.FromHtml(xmlColor.Value);
                IColor arcColor = new RgbColorClass()
                    {
                        Red = wColor.R,
                        Green = wColor.G,
                        Blue = wColor.B,
                        Transparency = Convert.ToByte(xmlColor.Attribute("transparency").Value),
                        NullColor = Convert.ToBoolean(xmlColor.Attribute("nullcolor").Value)
                    };
                if (xmlColor.Attribute("type").Value == "fill")
                {
                    selectionSymbol.Color = arcColor;
                }
                else
                {
                    selectionOutlineSymbol.Color = arcColor;
                }
            }
            selectionOutlineSymbol.Width = Convert.ToDouble(selectionSymbolNode.Element("outlinewidth").Value);
            selectionSymbol.Outline = selectionOutlineSymbol;
            selectionSymbol.Style = (esriSimpleFillStyle)(Convert.ToInt32(selectionSymbolNode.Element("style").Value));

            XElement editSymbolNode = config.Root.Element("symbol").Element("edit");
            editSymbol = new SimpleFillSymbolClass();
            ISimpleLineSymbol editOutlieSymbol = new SimpleLineSymbolClass();
            foreach (XElement xmlColor in editSymbolNode.Elements("color"))
            {
                wColor = ColorTranslator.FromHtml(xmlColor.Value);
                IColor arcColor = new RgbColorClass()
                {
                    Red = wColor.R,
                    Green = wColor.G,
                    Blue = wColor.B,
                    Transparency = Convert.ToByte(xmlColor.Attribute("transparency").Value),
                    NullColor = Convert.ToBoolean(xmlColor.Attribute("nullcolor").Value)
                };
                if (xmlColor.Attribute("type").Value == "fill")
                {
                    editSymbol.Color = arcColor;
                }
                else
                {
                    editOutlieSymbol.Color = arcColor;
                }
            }
            editOutlieSymbol.Width = Convert.ToDouble(editSymbolNode.Element("outlinewidth").Value);
            editSymbol.Outline = editOutlieSymbol;
            editSymbol.Style = (esriSimpleFillStyle)(Convert.ToInt32(editSymbolNode.Element("style").Value));
            customEditRender = Convert.ToBoolean(editSymbolNode.Element("customrender").Value);

            isLoaded = true;
        }

        /// <summary>
        /// Create a empty configuration file.
        /// </summary>
        private static void Create()
        {
            SetDefault();
            Save();
        }

        /// <summary>
        /// Gets a XElement class representing a symbol of a graphic box.
        /// </summary>
        /// <param name="type">Type of symbol</param>
        /// <param name="symbol">Render symbol</param>
        /// <param name="customRender">A value indicating whether the custom render is used.</param>
        /// <returns></returns>
        private static XElement GetBoxSymbolXElement(string type, ISimpleFillSymbol symbol, bool customRender = false)
        {
            return new XElement(type,
                                GetColorXElement("fill", symbol.Color),
                                GetColorXElement("outline", symbol.Outline.Color),
                                new XElement("outlinewidth", symbol.Outline.Width.ToString()),
                                new XElement("style", Convert.ToString((int)symbol.Style)),
                                new XElement("customrender", customRender.ToString()));
        }

        /// <summary>
        /// Gets a XElement class representing a color type.
        /// </summary>
        /// <param name="type"></param>
        /// <param name="color"></param>
        private static XElement GetColorXElement(string type, IColor color)
        {
            // May need extra conversion failure handling
            IRgbColor rgbColor = (IRgbColor)color;

            return new XElement("color",
                        new XAttribute("type", type),
                        new XAttribute("transparency", color.Transparency.ToString()),
                        new XAttribute("nullcolor", color.NullColor.ToString()),
                        ColorTranslator.ToHtml(Color.FromArgb(rgbColor.Transparency,
                                                              rgbColor.Red,
                                                              rgbColor.Green,
                                                              rgbColor.Blue)));
        }

        #endregion
    }
}
