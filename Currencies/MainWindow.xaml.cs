using System.Net;
using System.Net.Http;
using System.Windows;
using System.Xml.Linq;
using System.Text.Json;
using ScottPlot;
using System.Text.Json.Nodes;
using System.Net.Http.Json;
using System;
using System.Text.Json.Serialization;

namespace Currencies
{
    public partial class MainWindow : Window
    {
        private Dictionary<string, string> necessaryCurrencies = new Dictionary<string, string>
        {
            {"Доллар США", "R01235" },
            {"Евро", "R01239"},
            {"Китайский юань", "R01375" }
        };

        public MainWindow()
        {
            InitializeComponent();

            try
            {
                var xmlDocument = XDocument.Load("https://www.cbr-xml-daily.ru/daily_utf8.xml");

                var elementValCurs = xmlDocument.Element("ValCurs");

                var listCurrencies = elementValCurs.Elements("Valute");

                var searchedCurrencies = new List<XElement>();

                foreach (XElement currency in listCurrencies)
                {
                    string id = currency.Attribute("ID").Value.ToString();

                    if (necessaryCurrencies.ContainsValue(id.ToString()))
                    {
                        searchedCurrencies.Add(currency);
                    }
                }

                var tick = new Tick[necessaryCurrencies.Count()];

                var xValues = new double[necessaryCurrencies.Count()];

                int index = 0;

                foreach (var xmlCurrencies in searchedCurrencies)
                {
                    string title = xmlCurrencies.Element("Name").Value.ToString();

                    double value = Convert.ToDouble(xmlCurrencies.Element("Value")?.Value.ToString());

                    tick[index] = new(index, title);

                    xValues[index] = value;

                    index += 1;
                }

                WpfPlot.Plot.Add.Bars(xValues);

                WpfPlot.Plot.YLabel("Рубли");
                WpfPlot.Plot.XLabel("Иностранная валюта");

                WpfPlot.Plot.Axes.Bottom.TickGenerator = new ScottPlot.TickGenerators.NumericManual(tick);

                WpfPlot.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (!int.TryParse(textBoxCurrency.Text, out int i))
            {
                MessageBox.Show("Строка пустая или содержит буквы.");

                return;
            }

            try
            {
                WpfPlot.Plot.Clear();

                var httpClient = new HttpClient();

                var request = new HttpRequestMessage(HttpMethod.Get, "https://www.cbr-xml-daily.ru/daily_json.js");

                var response = httpClient.Send(request);

                if (!response.IsSuccessStatusCode)
                    throw new ArgumentException("Сервер не отвечает");

                var jsonString = response.Content.ReadAsStringAsync().Result.ToString();

                var json = JsonDocument.Parse(jsonString);

                var rootElement = json.RootElement;

                var valuties = rootElement.GetProperty("Valute");

                var searchedCurrencies = new List<JsonProperty>();

                foreach (var currency in valuties.EnumerateObject())
                {
                    var id = currency.Value.GetProperty("ID").ToString();

                    if (necessaryCurrencies.ContainsValue(id))
                    {
                        searchedCurrencies.Add(currency);
                    }
                }

                var tick = new Tick[necessaryCurrencies.Count()];

                var xValues = new double[necessaryCurrencies.Count()];

                int index = 0;

                foreach (var jsonCurrency in searchedCurrencies)
                {
                    string title = jsonCurrency.Value.GetProperty("Name").ToString();

                    double value = jsonCurrency.Value.GetProperty("Value").GetDouble();

                    tick[index] = new(index, title);

                    xValues[index] = value * double.Parse(textBoxCurrency.Text);

                    index += 1;
                }

                WpfPlot.Plot.Add.Bars(xValues);

                WpfPlot.Plot.Axes.Bottom.TickGenerator = new ScottPlot.TickGenerators.NumericManual(tick);

                WpfPlot.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}