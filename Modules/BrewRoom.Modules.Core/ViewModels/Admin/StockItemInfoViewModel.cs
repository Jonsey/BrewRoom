using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using BrewRoom.Modules.Core.Interfaces.Repositories;
using BrewRoom.Modules.Core.Interfaces.ViewModels.Admin;
using BrewRoom.Modules.Core.Models;
using HtmlAgilityPack;
using Microsoft.Practices.Prism.Commands;

namespace BrewRoom.Modules.Core.ViewModels.Admin
{
    public class StockItemInfoViewModel : IStockItemInfoViewModel
    {
        private readonly IStockItemsRepository _repository;
        List<Hop> hopsBYO;

        public StockItemInfoViewModel(IStockItemsRepository repository)
        {
            _repository = repository;
        }

        public DelegateCommand GetHopsCommand
        {
            get { return new DelegateCommand(GetHopsOld); }
        }

        private void GetHops()
        {
            var hops = new List<Hop>();

            var baseUrl = "http://www.wellhopped.co.uk/";
            var result = new System.Net.WebClient().DownloadString(string.Format("{0}Variety.asp", baseUrl));

            var doc = new HtmlDocument();
            doc.LoadHtml(result);

            var nameXPath = "td[2]";
            var linkXPath = "td[2]/a[1]";
            var aaXPath = "td[3]";

            var hopTable =
                doc.DocumentNode.SelectSingleNode(
                    "/html[1]/body[1]/table[1]/tr[2]/td[2]/table[2]");

            int iter = 2;
            HtmlNode row = hopTable.SelectSingleNode("tr[" + iter + "]");
            while (row != null)
            {
                var name = row.SelectSingleNode(nameXPath).InnerText.Replace("\r\n", "").Trim();
                string detailsLink = null;
                try
                {
                    detailsLink = row.SelectSingleNode(linkXPath).Attributes["href"].Value.Trim();
                }
                catch (Exception)
                {
                }

                var alphaAcid = row.SelectSingleNode(aaXPath).InnerText.Replace("\r\n", "");
                alphaAcid = alphaAcid.Replace("%", String.Empty);
                var alphaAcidRange = alphaAcid.Split('-');

                var hop = new Hop(name);

                if (detailsLink != null)
                {
                    var resultDetails =
                        new System.Net.WebClient().DownloadString(string.Format("{0}{1}", baseUrl, detailsLink));

                    var docDetails = new HtmlDocument();
                    docDetails.LoadHtml(resultDetails);

                    hop.Description =
                        docDetails.DocumentNode.SelectSingleNode(
                            "/html[1]/body[1]/table/tr[2]/td[2]/table/tr[4]/td[2]").InnerText;

                    var analyticsTable =
                        docDetails.DocumentNode.SelectSingleNode(
                            "/html[1]/body[1]/table[1]/tr[2]/td[2]/table[3]/tr/td[2]/table[1]");

                    var oilsTable =
                        docDetails.DocumentNode.SelectSingleNode(
                            "/html[1]/body[1]/table[1]/tr[2]/td[2]/table[3]/tr/td[4]/table[1]");

                    try
                    {
                        hop.AddOilCharacteristics(new HopOilCharacteristics
                                                      {
                                                          Carophyllene =
                                                              Convert.ToDecimal(
                                                                  oilsTable.SelectSingleNode("tr[2]/td[2]").InnerText.Replace("%","")),
                                                          Farnesene =
                                                              Convert.ToDecimal(
                                                                  oilsTable.SelectSingleNode("tr[3]/td[2]").InnerText.Replace("%", "")),
                                                          Humulene =
                                                              Convert.ToDecimal(
                                                                  oilsTable.SelectSingleNode("tr[4]/td[2]").InnerText.Replace("%", "")),
                                                          Myrcene =
                                                              Convert.ToDecimal(
                                                                  oilsTable.SelectSingleNode("tr[5]/td[2]").InnerText.Replace("%", "")),
                                                          OtherAcids =
                                                              Convert.ToDecimal(
                                                                  oilsTable.SelectSingleNode("tr[6]/td[2]").InnerText.Replace("%", "")),
                                                          PercentageOfTotalWeight =
                                                              GetAverageFromTextRange(
                                                                  analyticsTable.SelectSingleNode("tr[6]/td[2]").
                                                                      InnerText),
                                                          TotalAlphaAcid = GetTotalAlphaAcid(alphaAcidRange)
                                                      });
                    }
                    catch(Exception ex)
                    {
                    }
                }

                hops.Add(hop);
                row = hopTable.SelectSingleNode("tr[" + iter++ + "]");
            }

            hops.ForEach(x => _repository.Save(x));
        }

        private void GetHopsOld()
        {
            GetHopsBYO();

            var hops = new List<Hop>();


            var result = new System.Net.WebClient().DownloadString("http://www.trystan.org/res/beer/hops.html");

            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(result);

            var hopTable =
                doc.DocumentNode.SelectSingleNode(
                    "/html[1]/body[1]/table[2]");

            if (hopTable != null)
            {
                int iter = 2;
                HtmlNode row = hopTable.SelectSingleNode("tr[" + iter + "]");
                while (row != null)
                {

                    var name = row.SelectSingleNode("td[1]").InnerText.Replace("\\r\\n", "").Trim();
                    var hop = new Hop(name);

                    var matchingBYOHop = hopsBYO.FirstOrDefault(x => x.Name.Contains(name));
                    decimal alphaAcid = 0;
                    if (matchingBYOHop != null)
                    {
                        alphaAcid = matchingBYOHop.GetAlphaAcid();
                        hop.Description = matchingBYOHop.Description;
                    }



                    hop.AddOilCharacteristics(new HopOilCharacteristics
                                                  {
                                                      Carophyllene = Convert.ToDecimal(row.SelectSingleNode("td[5]").InnerText),
                                                      Farnesene = row.SelectSingleNode("td[6]").InnerText == "--" ? 0M : Convert.ToDecimal(row.SelectSingleNode("td[6]").InnerText),
                                                      Humulene = Convert.ToDecimal(row.SelectSingleNode("td[4]").InnerText),
                                                      Myrcene = Convert.ToDecimal(row.SelectSingleNode("td[3]").InnerText),
                                                      OtherAcids = Convert.ToDecimal(row.SelectSingleNode("td[7]").InnerText),
                                                      PercentageOfTotalWeight = Convert.ToDecimal(row.SelectSingleNode("td[2]").InnerText),
                                                      TotalAlphaAcid = alphaAcid
                                                  });

                    hops.Add(hop);
                    row = hopTable.SelectSingleNode("tr[" + iter++ + "]");
                }
            }

            hops.ForEach(x => _repository.Save(x));
        }

        private void GetHopsBYO()
        {
            hopsBYO = new List<Hop>();

            for (int x = 1; x < 25; x++)
            {
                var result = new System.Net.WebClient().DownloadString("http://byo.com/resources/hops?style=" + x + "");
                //result = result.Remove(0, 123);

                HtmlDocument doc = new HtmlDocument();
                doc.LoadHtml(result);

                int i = 1;
                bool isTable = true;
                while (isTable)
                {
                    var hopTable =
                        doc.DocumentNode.SelectSingleNode(
                            "/html[1]/body[1]/table[1]/tr[4]/td[2]/div[3]/table[1]/tbody[1]/tr[" + i + "]");

                    if (hopTable != null)
                    {
                        var name = hopTable.SelectSingleNode("td[1]").InnerText.Replace("\\r\\n", "").Trim();
                        var alphaAcid = hopTable.SelectSingleNode("td[2]").InnerText.Replace("\r\n", "");
                        alphaAcid = alphaAcid.Replace("%", String.Empty);
                        var alphaAcidRange = alphaAcid.Split('-');
                        var description = hopTable.SelectSingleNode("td[4]").InnerText.Trim();

                        var hop = new Hop(name);
                        hop.Description = description;

                        hop.AddOilCharacteristics(new HopOilCharacteristics
                                                      {
                                                          Carophyllene = 20M,
                                                          Farnesene = 20M,
                                                          Humulene = 20M,
                                                          Myrcene = 20M,
                                                          OtherAcids = 20M,
                                                          PercentageOfTotalWeight = 20,
                                                          TotalAlphaAcid =
                                                              GetTotalAlphaAcid(alphaAcidRange)
                                                      });
                        if (hopsBYO.FirstOrDefault(h => h.Name == name) == null)
                            hopsBYO.Add(hop);

                        i = i + 2;
                    }
                    else
                    {
                        isTable = false;
                    }
                }
            }

            //hopsBYO.ForEach(x => _repository.Save(x));
        }

        private static decimal GetAverageFromTextRange(string range)
        {
            var alphaAcid = range.Replace("%", String.Empty);
            var alphaAcidRange = alphaAcid.Split('-');

            return GetTotalAlphaAcid(alphaAcidRange);
        }
        private static decimal GetTotalAlphaAcid(string[] alphaAcidRange)
        {
            if (alphaAcidRange.Count() == 1)
                return Convert.ToDecimal(alphaAcidRange[0]);

            return (Convert.ToDecimal(alphaAcidRange[0]) +
                    Convert.ToDecimal(alphaAcidRange[1])) / 2;
        }
    }
}
