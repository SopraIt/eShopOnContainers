using Catalog.BackgroundTasks.Configuration;
using Catalog.BackgroundTasks.Models;
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

namespace Catalog.BackgroundTasks.Tasks
{
    public class TB_ProductImportManagerTask : BackgroundService
    {
        private readonly ILogger<TB_ProductImportManagerTask> _logger;
        private readonly BackgroundTaskSettings _settings;
        private readonly IEventBus _eventBus;
        private readonly IHttpClientFactory _clientFactory;
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly ICatalogDataRepository _repo;

        public TB_ProductImportManagerTask(
            IOptions<BackgroundTaskSettings> settings,
            IEventBus eventBus,
            ILogger<TB_ProductImportManagerTask> logger,
            IHttpClientFactory clientFactory,
            IHostingEnvironment hostingEnvironment,
            ICatalogDataRepository dataReopsitory)
        {
            _settings = settings?.Value ?? throw new ArgumentNullException(nameof(settings));
            _eventBus = eventBus ?? throw new ArgumentNullException(nameof(eventBus));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _clientFactory = clientFactory ?? throw new ArgumentNullException(nameof(clientFactory));
            _hostingEnvironment = hostingEnvironment ?? throw new ArgumentNullException(nameof(hostingEnvironment));
            _repo = dataReopsitory ?? throw new ArgumentNullException(nameof(dataReopsitory));
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogDebug("GracePeriodManagerService is starting.");

            List<ProductIdImport> productIds = GetProductIdFromCSV();

            stoppingToken.Register(() => _logger.LogDebug("#1 GracePeriodManagerService background task is stopping."));

            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("GracePeriodManagerService background task is doing background work.");

                foreach (var product in productIds.Where(x => !x.Imported))
                {
                    _logger.LogInformation(string.Format("Elaboration of {0} Product", product.ID));

                    string token = await this.GetAuthToken();
                    _logger.LogInformation("Token: " + token);

                    string productDetail = await GetProductDetail(product.ID, token);
                    productDetail = productDetail.Replace("-","_");

                    if (productDetail != string.Empty)
                    {
                        string result_search = await PutInSearch(product.ID, productDetail);
                        if (result_search == string.Empty) {
                            _logger.LogInformation(string.Format("Elaboration of {0} Product KO", product.ID));
                        }

                        string result_nosql = await putInNoSql(product.ID, productDetail);
                        if (result_nosql == string.Empty) {
                            _logger.LogInformation(string.Format("Elaboration of {0} Product KO", product.ID));
                        }

                        if(!string.IsNullOrEmpty(result_search) && !string.IsNullOrEmpty(result_nosql)){
                            product.Imported = true;
                        }
                    }

                }


                await Task.Delay(_settings.CheckUpdateTime, stoppingToken);
            }

            _logger.LogInformation("GracePeriodManagerService background task is stopping.");

            await Task.CompletedTask;
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

                //client.DefaultRequestHeaders
                //      .Accept
                //      .Add(new MediaTypeWithQualityHeaderValue("application/json"));//ACCEPT header

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
                _logger.LogError(string.Format( "Exception message {0} - stack {1}", e.Message, e.StackTrace));
                return string.Empty;
            }


        }

        private List<ProductIdImport> GetProductIdFromCSV()
        {
            string csvFile = Path.Combine(_hostingEnvironment.ContentRootPath, "CsvData", "ToscaBluProductsID.csv");

            if (!File.Exists(csvFile))
            {
                _logger.LogError("EXCEPTION ERROR: CSV non trovato");
                //return GetPreconfiguredCatalogBrands();
            }

            string[] csvheaders;
            try
            {
                string[] requiredHeaders = { "catalogbrand" };
                csvheaders = File.ReadLines(csvFile).First().ToLowerInvariant().Split(',');
                //(csvheaders = GetHeaders(csvFileCatalogBrands, requiredHeaders);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "EXCEPTION ERROR: {Message}", ex.Message);
                //return GetPreconfiguredCatalogBrands();
            }

            List<string> csvdata = File.ReadAllLines(csvFile).Skip(1).ToList();
            List<ProductIdImport> productIds = csvdata.Select(p => new ProductIdImport() { ID = p.Trim(), Imported = false }).ToList();

            return productIds;

            //return new List<ProductIdImport>();
            //return File.ReadAllLines(csvFile)
            //                            .Skip(1) // skip header row
            //                            .SelectTry(x => CreateCatalogBrand(x))
            //                            .OnCaughtException(ex => { logger.LogError(ex, "EXCEPTION ERROR: {Message}", ex.Message); return null; })
            //                            .Where(x => x != null);
        }

        private async Task<string> PutInSearch(string productId, string json)
        {
            try
            {
                PutSearchResult result = null;

                var client = _clientFactory.CreateClient();
                client.BaseAddress = new Uri(string.Format("{0}/tb_products/_doc/{1}", _settings.SearchBaseUrl, productId));

                //client.DefaultRequestHeaders
                //      .Accept
                //      .Add(new MediaTypeWithQualityHeaderValue("application/json"));//ACCEPT header

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
                _logger.LogError(string.Format( "Exception message {0} - stack {1}", e.Message, e.StackTrace));
                return string.Empty;
            }
        }

        private async Task<string> putInNoSql(string productId, string json){
            try
            {
                ProductDetail product = new ProductDetail(){
                    //TODO...
                };

                string _id = await _repo.UpsertAsync(product);

                if(_id != string.Empty){
                    return _id;
                }
                else
                {
                    _logger.LogError(string.Format( "Exception message: Errore nell'inserimento nel nosql db"));
                    return string.Empty;
                }
            }
            catch (Exception e)
            {
                _logger.LogError(string.Format( "Exception message {0} - stack {1}", e.Message, e.StackTrace));
                return string.Empty;
            }
        }

    }
}
