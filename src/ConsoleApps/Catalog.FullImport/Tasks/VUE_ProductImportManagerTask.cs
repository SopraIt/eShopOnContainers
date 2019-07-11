using Catalog.FullImport.Configuration;
using Catalog.FullImport.Models;
using Microsoft.eShopOnContainers.BuildingBlocks.EventBus.Abstractions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Data.SqlClient;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using System.Text;
using Catalog.Nosql.Infrastructure.Repositories;
using Catalog.Nosql.Model;
using Catalog.FullImport.Models.Magento2;
using Newtonsoft.Json;
using System.Reflection;

namespace Catalog.FullImport.Tasks
{
    public class VUE_ProductImportManagerTask : ITaskAsync
    {
        private readonly ILogger<VUE_ProductImportManagerTask> _logger;
        private readonly FullImportSettings _settings;
        private readonly IEventBus _eventBus;
        private readonly IHttpClientFactory _clientFactory;
        private readonly ICatalogDataRepository _repo;

        public VUE_ProductImportManagerTask(
            IOptions<FullImportSettings> settings,
            ILogger<VUE_ProductImportManagerTask> logger,
            IHttpClientFactory clientFactory,
            ICatalogDataRepository dataReopsitory)
        {
            _settings = settings?.Value ?? throw new ArgumentNullException(nameof(settings));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _clientFactory = clientFactory ?? throw new ArgumentNullException(nameof(clientFactory));
            _repo = dataReopsitory ?? throw new ArgumentNullException(nameof(dataReopsitory));
        }

        public async Task ExecuteAsync()
        {
            _logger.LogDebug("GracePeriodManagerService is starting.");

            //await Task.Delay(_settings.CheckUpdateTime);
            
            BuildCatalog(string.Empty);

            BuildCatalog("_it");

            BuildCatalog("_de");

            _logger.LogInformation("GracePeriodManagerService background task is stopping.");
        }

        private async void BuildCatalog(string local)
        {
            List<Import> rows = GetRowsFromFile(local);

            string mapping_json = string.Empty;
            string result_mapping = string.Empty;

            mapping_json = GetFromFile("catalog_mapping.txt");
            result_mapping = await PutMappingInSearch("product", mapping_json, local);

            mapping_json = GetFromFile("attribute_mapping.txt");
            result_mapping = await PutMappingInSearch("attribute", mapping_json, local);

            mapping_json = GetFromFile("category_mapping.txt");
            result_mapping = await PutMappingInSearch("category", mapping_json, local);



            _logger.LogInformation("GracePeriodManagerService background task is doing background work.");

            foreach (var row in rows.Where(x => !x.Imported))
            {
                _logger.LogInformation(string.Format("Elaboration of {0} Product", row._id));
                string json_source = JsonConvert.SerializeObject(row._source);
                string result_search = string.Empty;
                string result_nosql = string.Empty;

                switch (row._type)
                {
                    case "category":
                        result_search = await PutInSearch("category" + local, row._id, json_source);
                        break;
                    case "product":
                        result_search = await PutInSearch("product" + local, row._id, json_source);
                        result_nosql = await putInNoSql(row._id, json_source);
                        break;
                    case "attribute":
                        result_search = await PutInSearch("attribute" + local, row._id, json_source);
                        break;
                    case "review":
                        result_search = await PutInSearch("review" + local, row._id, json_source);
                        break;
                    default:
                        Console.WriteLine("Default case");
                        break;
                }

                if (result_search == string.Empty)
                {
                    _logger.LogInformation(string.Format("Elaboration of {0} Product KO", row._id));
                }
                else
                {
                    row.Imported = true;
                }



                // string token = await this.GetAuthToken();
                // _logger.LogInformation("Token: " + token);

                // string productDetail = await GetProductDetail(product.ID, token);
                // productDetail = productDetail.Replace("-","_");

                // if (productDetail != string.Empty)
                // {
                //     string result_search = await PutInSearch(product.ID, productDetail);
                //     if (result_search == string.Empty) {
                //         _logger.LogInformation(string.Format("Elaboration of {0} Product KO", product.ID));
                //     }

                //     string result_nosql = await putInNoSql(product.ID, productDetail);
                //     if (result_nosql == string.Empty) {
                //         _logger.LogInformation(string.Format("Elaboration of {0} Product KO", product.ID));
                //     }

                //     if(!string.IsNullOrEmpty(result_search) && !string.IsNullOrEmpty(result_nosql)){
                //         product.Imported = true;
                //     }
                // }

            }




        }

        private async Task<string> GetAuthToken()
        {
            try
            {
                OCCAuth result = null;

                var nvc = new List<KeyValuePair<string, string>>();
                nvc.Add(new KeyValuePair<string, string>("grant_type", "client_credentials"));

                var client = _clientFactory.CreateClient();
                client.BaseAddress = new Uri(string.Format("{0}/ccadmin/v1/login", _settings.ProductSourceBaseUrl));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJzdWIiOiI0YjIxYzcyOC04NDI5LTQ1MDMtYWU5Yi00NmY2OWM1NGNjNjUiLCJpc3MiOiJhcHBsaWNhdGlvbkF1dGgiLCJleHAiOjE1ODQwMDQ1MDAsImlhdCI6MTU1MjQ2ODUwMH0=.WWPKNld/2BCtOcDGiT7DPBbM4t3HqFQepS/eQy6Vg7o=");

                var request = new HttpRequestMessage(HttpMethod.Post, client.BaseAddress);
                request.Content = new FormUrlEncodedContent(nvc);


                var response = await client.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    result = await response.Content
                        .ReadAsAsync<OCCAuth>();

                    return result.access_token;
                }
                else
                {
                    return string.Empty;
                }
            }
            catch (Exception e)
            {
                return string.Empty;
            }


        }

        private async Task<string> GetProductDetail(string productId, string token)
        {
            try
            {
                var client = _clientFactory.CreateClient();
                client.BaseAddress = new Uri(string.Format("{0}/ccstore/v1/products/{1}", _settings.ProductSourceBaseUrl, productId));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var request = new HttpRequestMessage(HttpMethod.Get, client.BaseAddress);

                var response = await client.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    string result = await response.Content.ReadAsStringAsync();
                    return result;
                }
                else
                {
                    _logger.LogInformation("Response status:" + response.StatusCode.ToString());
                    return string.Empty;
                }
            }
            catch (Exception e)
            {
                _logger.LogError(string.Format("Exception message {0} - stack {1}", e.Message, e.StackTrace));
                return string.Empty;
            }


        }

        private List<Import> GetRowsFromFile(string local)
        {
            string csvFile = Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "CsvData", "catalog" + local + ".txt");

            if (!File.Exists(csvFile))
            {
                _logger.LogError("EXCEPTION ERROR: CSV non trovato");
                //return GetPreconfiguredCatalogBrands();
            }

            List<string> txtData = File.ReadAllLines(csvFile).ToList();
            List<Import> result = new List<Import>();

            foreach (var row in txtData)
            {
                Import dataRow = JsonConvert.DeserializeObject<Import>(row);
                result.Add(dataRow);
            }

            return result;
        }

        private string GetFromFile(string file_name)
        {
            string csvFile = Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "CsvData", file_name);

            if (!File.Exists(csvFile))
            {
                _logger.LogError("EXCEPTION ERROR: CSV non trovato");
                //return GetPreconfiguredCatalogBrands();
            }

            string json = File.ReadAllText(csvFile);

            return json;
        }

        private async Task<string> PutMappingInSearch(string index, string json, string local)
        {
            try
            {
                PutSearchResult result = null;

                var client = _clientFactory.CreateClient();
                client.BaseAddress = new Uri(string.Format("{0}/{1}{2}", _settings.SearchBaseUrl, index, local));

                var content = new StringContent(json.ToString(), Encoding.UTF8, "application/json");

                var request = new HttpRequestMessage(HttpMethod.Put, client.BaseAddress);
                request.Content = content;


                var response = await client.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    result = await response.Content
                        .ReadAsAsync<PutSearchResult>();

                    return result.ToString();
                }
                else
                {
                    return string.Empty;
                }
            }
            catch (Exception e)
            {
                _logger.LogError(string.Format("Exception message {0} - stack {1}", e.Message, e.StackTrace));
                return string.Empty;
            }
        }

        private async Task<string> PutInSearch(string index, string id, string json)
        {
            try
            {
                PutSearchResult result = null;

                var client = _clientFactory.CreateClient();
                client.BaseAddress = new Uri(string.Format("{0}/{1}/_doc/{2}", _settings.SearchBaseUrl, index, id));

                var content = new StringContent(json.ToString(), Encoding.UTF8, "application/json");

                var request = new HttpRequestMessage(HttpMethod.Post, client.BaseAddress);
                request.Content = content;


                var response = await client.SendAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    result = await response.Content
                        .ReadAsAsync<PutSearchResult>();

                    return result.ToString();
                }
                else
                {
                    return string.Empty;
                }
            }
            catch (Exception e)
            {
                _logger.LogError(string.Format("Exception message {0} - stack {1}", e.Message, e.StackTrace));
                return string.Empty;
            }
        }

        private async Task<string> putInNoSql(string productId, string json)
        {
            try
            {
                var product = JsonConvert.DeserializeObject<ProductDetail>(json);

                string _id = await _repo.UpsertAsync(product);

                if (_id != string.Empty)
                {
                    return _id;
                }
                else
                {
                    _logger.LogError(string.Format("Exception message: Errore nell'inserimento nel nosql db"));
                    return string.Empty;
                }
            }
            catch (Exception e)
            {
                _logger.LogError(string.Format("Exception message {0} - stack {1}", e.Message, e.StackTrace));
                return string.Empty;
            }
        }

    }
}
