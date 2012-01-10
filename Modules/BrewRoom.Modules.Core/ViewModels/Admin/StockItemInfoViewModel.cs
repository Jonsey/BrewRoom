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

        public StockItemInfoViewModel(IStockItemsRepository repository)
        {
            _repository = repository;
        }

        public DelegateCommand GetHopsCommand
        {
            get { return new DelegateCommand(GetHops); }
        }

        private void GetHops()
        {
            var hops = new List<Hop>();


            var result = new System.Net.WebClient().DownloadString("http://www.trystan.org/res/beer/hops.html");

            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(result);

            var hopTable =
                doc.DocumentNode.SelectSingleNode(
                    "/html[1]/body[1]/table[2]");

            if (hopTable != null)
            {
                foreach (var node in hopTable.ChildNodes)
                {
                    // /html[1]/body[1]/table[2]/tr[1]/td[1]
                    var name = hopTable.SelectSingleNode("tr[1]/td[1]").InnerText.Replace("\\r\\n", "").Trim();
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
                    if (hops.FirstOrDefault(h => h.Name == name) == null)
                        hops.Add(hop);
                }
            }

            hops.ForEach(x => _repository.Save(x));
        }

        private void GetHopsOld()
        {
            var hops = new List<Hop>();

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
                        if (hops.FirstOrDefault(h => h.Name == name) == null)
                            hops.Add(hop);

                        i = i + 2;
                    }
                    else
                    {
                        isTable = false;
                    }
                }
            }

            hops.ForEach(x => _repository.Save(x));
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
