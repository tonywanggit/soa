using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections.Generic;
using System.Collections;

public enum Cloudiness { Low, Partly, Hi }
public enum Rain { None, Drizzle, Heavy, Thunderstorm }
public enum TemperatureUnit { Fahrenheit, Celsius }

public interface IWeatherForecastProvider {
    WeatherInfo GetWeatherInfo(DateTime dateValue);
}

public class RandomWeatherForecastProvider : IWeatherForecastProvider {
    private static RandomWeatherForecastProvider instance;
    private static readonly Dictionary<Month, TemperatureRange> averageMonthTemperatures = new Dictionary<Month, TemperatureRange>();
    private Hashtable weatherInfoCache = new Hashtable();
    private Random random = new Random();

    static RandomWeatherForecastProvider() {
        AddAverageMonthTemperature(Month.January, -4, 3);
        AddAverageMonthTemperature(Month.February, -4, 3);
        AddAverageMonthTemperature(Month.March, -1, 7);
        AddAverageMonthTemperature(Month.April, 6, 14);
        AddAverageMonthTemperature(Month.May, 12, 20);
        AddAverageMonthTemperature(Month.June, 16, 25);
        AddAverageMonthTemperature(Month.July, 19, 28);
        AddAverageMonthTemperature(Month.August, 19, 27);
        AddAverageMonthTemperature(Month.September, 16, 26);
        AddAverageMonthTemperature(Month.October, 9, 21);
        AddAverageMonthTemperature(Month.November, 3, 11);
        AddAverageMonthTemperature(Month.December, -2, 5);
    }
    private RandomWeatherForecastProvider() { }

    public static RandomWeatherForecastProvider Instance {
        get {
            if(instance == null)
                instance = new RandomWeatherForecastProvider();
            return instance;
        }
    }

    public WeatherInfo GetWeatherInfo(DateTime dateValue) {
        WeatherInfo info = weatherInfoCache[dateValue] as WeatherInfo;
        if(info == null) {
            Month month = (Month)(dateValue.Month - 1);
            TemperatureRange temperature = GetRandomDayTemperature(averageMonthTemperatures[month]);
            Cloudiness cloudiness = GetRandomCloudiness();
            Rain rain = GetRandomRain(month, cloudiness);
            info = new WeatherInfo(dateValue, TemperatureUnit.Celsius, temperature.Min.Value, temperature.Max.Value,
                cloudiness, rain, GetRandomSnow(month), GetRandomFog(month), GetRandomHail(rain));
            weatherInfoCache[dateValue] = info;
        }
        return info;
    }

    private static void AddAverageMonthTemperature(Month month, int min, int max) {
        averageMonthTemperatures.Add(month, new TemperatureRange(TemperatureUnit.Celsius, min, max));
    }
    private TemperatureRange GetRandomDayTemperature(TemperatureRange averageMonthTemperature) {
        TemperatureUnit unit = averageMonthTemperature.Min.Unit;
        int rnd1 = 0, rnd2 = 0;
        while(rnd1 == rnd2) {
            rnd1 = this.random.Next(averageMonthTemperature.Min.Value - 2, averageMonthTemperature.Min.Value + 2);
            rnd2 = this.random.Next(averageMonthTemperature.Max.Value - 2, averageMonthTemperature.Max.Value + 2);
        }
        return new TemperatureRange(unit, Math.Min(rnd1, rnd2), Math.Max(rnd1, rnd2));
    }
    private Cloudiness GetRandomCloudiness() {
        double fraction = random.NextDouble();
        if(fraction < 0.5)
            return Cloudiness.Low;
        else if(fraction < 0.8)
            return Cloudiness.Partly;
        else
            return Cloudiness.Hi;
    }
    private Rain GetRandomRain(Month month, Cloudiness cloudiness) {
        double fraction = random.NextDouble();
        switch(month) {
            case Month.January:
            case Month.February:
            case Month.December:
                if(fraction < 0.05 && cloudiness != Cloudiness.Low)
                    return Rain.Heavy;
                else if(fraction < 0.2)
                    return Rain.Drizzle;
                else
                    return Rain.None;
            case Month.July:
            case Month.August:
                if(fraction < 0.05 && cloudiness != Cloudiness.Low)
                    return Rain.Thunderstorm;
                else if(fraction < 0.1)
                    return Rain.Heavy;
                else if(fraction < 0.3)
                    return Rain.Drizzle;
                else
                    return Rain.None;
            default:
                if(fraction < 0.01 && cloudiness != Cloudiness.Low)
                    return Rain.Thunderstorm;
                else if(fraction < 0.03)
                    return Rain.Heavy;
                else if(fraction < 0.1)
                    return Rain.Drizzle;
                else
                    return Rain.None;
        }
    }
    private bool GetRandomSnow(Month month) {
        double fraction = random.NextDouble();
        switch(month) {
            case Month.December:
                return fraction < 0.5;
            case Month.January:
                return fraction < 0.6;
            case Month.February:
                return fraction < 0.7;
            case Month.March:
                return fraction < 0.1;
            default:
                return false;
        }
    }
    private bool GetRandomFog(Month month) {
        double fraction = random.NextDouble();
        if(month >= Month.April && month <= Month.August)
            return fraction < 0.05;
        else
            return false;
    }
    private bool GetRandomHail(Rain rain) {
        double fraction = random.NextDouble();
        switch(rain) {
            case Rain.Thunderstorm:
                return fraction < 0.5;
            case Rain.Heavy:
                return fraction < 0.1;
            case Rain.Drizzle:
                return fraction < 0.03;
            default:
                return false;
        }
    }

    #region Nested types

    private enum Month : byte { January, February, March, April, May, June, July, August, September, October, November, December }

    private class TemperatureRange {
        private Temperature min;
        private Temperature max;

        public TemperatureRange(TemperatureUnit temperatureUnit, int min, int max) {
            this.min = new Temperature(min, temperatureUnit);
            this.max = new Temperature(max, temperatureUnit);
        }

        public Temperature Min { get { return min; } }
        public Temperature Max { get { return max; } }
    }

    #endregion

}

public class WeatherInfoControl : WebControl {
    private const string BaseImagePath = "Images/Calendar/Weather/";
    private static readonly System.Drawing.Size ImageSize = new System.Drawing.Size(24, 24);
    private readonly WeatherInfo info;
    private readonly TemperatureUnit temperatureUnit;

    public WeatherInfoControl(WeatherInfo info, TemperatureUnit temperatureUnit) {
        this.info = info;
        this.temperatureUnit = temperatureUnit;
    }

    public WeatherInfo Info {
        get { return info; }
    }

    private bool ApplyAlphaImageLoader { get { return IsIE && !IsIE7; } }
    private bool DuplicateForeColorDefinition { get { return IsIE55; } }

    #region Utils
    private bool IsIE { get { return BrowserCaps != null && BrowserCaps.Browser.ToLower() == "ie"; } }
    private bool IsIE55 { get { return IsIE && BrowserCaps.Version.ToLower() == "5.5"; } }
    private bool IsIE7 { get { return IsIE && BrowserCaps.Version.ToLower().Contains("7."); } }
    private HttpBrowserCapabilities BrowserCaps {
        get {
            if(HttpContext.Current != null && HttpContext.Current.Request != null)
                return HttpContext.Current.Request.Browser;
            return null;
        }
    }    
    #endregion

    // ViewState
    protected override object SaveViewState() {
        return null;
    }
    protected override void LoadViewState(object savedState) {

    }
    // Render
    protected override void OnPreRender(EventArgs e) {
        base.OnPreRender(e);
        Table table = new Table();
        Controls.Add(table);
        table.CellSpacing = 0;
        table.CellPadding = 0;
        table.Style.Add(HtmlTextWriterStyle.TextAlign, "center");
        TableRow dateRow = new TableRow();
        TableRow imagesRow = new TableRow();
        TableRow textRow = new TableRow();
        table.Rows.Add(dateRow);
        table.Rows.Add(imagesRow);
        table.Rows.Add(textRow);
        TableCell dateCell = new TableCell();
        TableCell imagesCell = new TableCell();
        TableCell textCell = new TableCell();
        dateRow.Cells.Add(dateCell);
        imagesRow.Cells.Add(imagesCell);
        textRow.Cells.Add(textCell);

        CreateDate(dateCell);
        CreateImages(imagesCell);
        CreateText(textCell);
    }
    protected override void Render(HtmlTextWriter writer) {
        RenderContents(writer);
    }

    private void AddImageInfo(List<WeatherImageInfo> images, string imageName, System.Drawing.Size imageSize, string imageTooltip) {
        images.Add(new WeatherImageInfo(BaseImagePath + imageName, imageSize, imageTooltip));
    }
    private List<Image> CreateImageControls() {
        List<Image> imageControls = null;
        List<WeatherImageInfo> imagesInfo = GetImagesInfo();
        if(imagesInfo.Count > 0) {
            imageControls = new List<Image>();
            foreach(WeatherImageInfo imageInfo in imagesInfo) {
                Image image = new Image();
                image.AlternateText = imageInfo.Tooltip;
                image.Width = imageInfo.Size.Width;
                image.Height = imageInfo.Size.Height;
                if(ApplyAlphaImageLoader) {
                    image.ImageUrl = BaseImagePath + "1x1.gif";
                    image.Style.Add("filter",
                        string.Format("progid:DXImageTransform.Microsoft.AlphaImageLoader(src={0}, sizingMethod=scale)", imageInfo.Path));
                } else {
                    image.ImageUrl = imageInfo.Path;
                }
                imageControls.Add(image);
            }
        }
        return imageControls;
    }
    private void CreateDate(WebControl container) {
        Panel frame = new Panel();
        frame.Style.Add(HtmlTextWriterStyle.Padding, "3px");
        if(DuplicateForeColorDefinition)
            frame.ForeColor = GetParentForeColorRecursive(container);
        frame.Font.Size = new FontUnit(20, UnitType.Point);
        frame.Controls.Add(new LiteralControl(Info.Date.Day.ToString()));
        container.Controls.Add(frame);
    }
    private System.Drawing.Color GetParentForeColorRecursive(WebControl parent) {
        if (parent == null)
            return System.Drawing.Color.Empty;
        if(parent.ForeColor != System.Drawing.Color.Empty)
            return parent.ForeColor;
        return GetParentForeColorRecursive(parent.Parent as WebControl);
    }
    private void CreateImages(Control container) {
        Panel div = new Panel();
        div.Width = 72;
        List<Image> images = CreateImageControls();
        foreach(Image image in images)
            div.Controls.Add(image);
        container.Controls.Add(div);
    }
    private void CreateText(Control container) {
        container.Controls.Add(new LiteralControl(string.Format("<span style=\"color: #C00;\">{0}</span> / <span style=\"color: #0A75B3;\">{1}</span>",
            Info.TemperatureHi.ToString(this.temperatureUnit), Info.TemperatureLow.ToString(this.temperatureUnit))));
    }
    private List<WeatherImageInfo> GetImagesInfo() {
        List<WeatherImageInfo> images = new List<WeatherImageInfo>();
        switch(Info.Cloudiness) {
            case Cloudiness.Low:
                AddImageInfo(images, "cloudiness_low.png", ImageSize, "Low cloud");
                break;
            case Cloudiness.Partly:
                AddImageInfo(images, "cloudiness_partly.png", ImageSize, "Partly cloudy");
                break;
            case Cloudiness.Hi:
                AddImageInfo(images, "cloudiness_hi.png", ImageSize, "Hi cloud");
                break;
        }
        switch(Info.Rain) {
            case Rain.Drizzle:
                AddImageInfo(images, "rain_common.png", ImageSize, "Drizzle");
                break;
            case Rain.Heavy:
                AddImageInfo(images, "rain_common.png", ImageSize, "Heavy");
                break;
            case Rain.Thunderstorm:
                AddImageInfo(images, "rain_thunderstorm.png", ImageSize, "Thunderstorms");
                break;
        }
        if(Info.Snow)
            AddImageInfo(images, "snow.png", ImageSize, "Snow");
        if(Info.Fog)
            AddImageInfo(images, "fog.png", ImageSize, "Fog");
        if(Info.Hail)
            AddImageInfo(images, "hail.png", ImageSize, "Hail");
        return images;
    }

    #region Nested types

    private sealed class WeatherImageInfo {
        private string path;
        private System.Drawing.Size size;
        private string tooltip;

        public WeatherImageInfo(string path, System.Drawing.Size size, string tooltip) {
            this.path = path;
            this.size = size;
            this.tooltip = tooltip;
        }

        public string Path { get { return path; } }
        public System.Drawing.Size Size { get { return size; } }
        public string Tooltip { get { return tooltip; } }
    }

    #endregion
}

public class WeatherInfo {
    private DateTime date;
    private Temperature temperatureHi;
    private Temperature temperatureLow;
    private Cloudiness cloudiness;
    private Rain rain;
    private bool snow;
    private bool fog;
    private bool hail;

    public WeatherInfo(DateTime date, TemperatureUnit unit, int temperatureLow, int temperatureHi, Cloudiness cloudiness,
        Rain rain, bool snow, bool fog, bool hail) {
        if(temperatureHi < temperatureLow)
            throw new ArgumentException("temperatureHi < temperatureLow");
        this.date = date;
        this.temperatureLow = new Temperature(temperatureLow, unit);
        this.temperatureHi = new Temperature(temperatureHi, unit);
        this.cloudiness = cloudiness;
        this.rain = rain;
        this.snow = snow;
        this.fog = fog;
        this.hail = hail;
    }

    public DateTime Date { get { return date; } }
    public Temperature TemperatureHi { get { return temperatureHi; } }
    public Temperature TemperatureLow { get { return temperatureLow; } }
    public Cloudiness Cloudiness { get { return cloudiness; } }
    public Rain Rain { get { return rain; } }
    public bool Snow { get { return snow; } }
    public bool Fog { get { return fog; } }
    public bool Hail { get { return hail; } }
}

public class Temperature {
    private int value;
    private TemperatureUnit unit;

    public Temperature(int value, TemperatureUnit unit) {
        this.value = value;
        this.unit = unit;
    }

    public TemperatureUnit Unit { get { return unit; } }
    public int Value { get { return value; } }

    public string GetLabel(TemperatureUnit unit) {
        return "&deg;" + (unit == TemperatureUnit.Celsius ? "C" : "F");
    }
    public int GetTemperature(TemperatureUnit unit) {
        return ConvertTemperature(value, this.unit, unit);
    }
    public override string ToString() {
        return this.value.ToString() + GetLabel(this.unit);
    }
    public string ToString(TemperatureUnit temperatureUnit) {
        return GetTemperature(temperatureUnit).ToString() + GetLabel(temperatureUnit);
    }

    private static int ConvertTemperature(int value, TemperatureUnit inputUnit, TemperatureUnit outputUnit) {
        if(inputUnit == outputUnit)
            return value;
        else
            return (int)Math.Round(inputUnit == TemperatureUnit.Fahrenheit ? (5.0 * (value - 32) / 9.0) : (1.8 * value + 32));
    }
}
