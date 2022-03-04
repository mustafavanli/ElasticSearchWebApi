using ElasticSearchWebApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Nest;

namespace ElasticSearchWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly IElasticClient client;
        public CustomerController(IElasticClient client)
        {
            this.client = client;
        }

        [HttpGet("GetById")]
        public async Task<Customer> GetById(Guid id)
        {
            var response = await client.GetAsync<Customer>(id);
            var customer = response.Source;
            return customer;
        }
        [HttpPut("Update")]
        public async Task<Customer> Update(Customer customer)
        {
            var response = await client.UpdateAsync<Customer>(customer.Id,f=>f.Doc(customer));
            return customer;
        }
        [HttpGet("GetAll")]
        public async Task<List<Customer>> GetAll()
        {
            var response = await client.SearchAsync<Customer>(x=>x.Query(q=>q.MatchAll()));
            var customer = response.Documents;
            return customer.ToList();
        }

        [HttpPost("Add")]
        public async Task<bool> Add(Customer customer)
        {
            customer.Id = Guid.NewGuid();
            var result = await client.CreateDocumentAsync<Customer>(customer);
            return true;
        }
        [HttpGet("Geo")]
        public async Task<List<Customer>> Geo(double lat,double log,int km)
        {
            var geoResult = await client.SearchAsync<Customer>(s => s.Query(q => q.GeoDistance(geo => geo.DistanceType(GeoDistanceType.Plane).Distance(Distance.Kilometers(km)).Location(lat, log))));


            return geoResult.Documents.ToList();
        }
        [HttpDelete("Delete")]
        public async Task<bool> Delete([FromQuery]Guid id)
        {
            await client.DeleteAsync<Customer>(new DocumentPath<Customer>(new Customer() { Id = id}));
            return true;
        }
        [HttpGet("Search")]
        public async Task<object> Search([FromQuery] string keyword)
        {
            //....
            return "null";
        }
    }
}
