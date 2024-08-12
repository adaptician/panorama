using Microsoft.AspNetCore.Mvc;
using Panorama.Causation.Producers;
using Panorama.Causation.Services;

namespace Panorama.Causation.Controllers;

[ApiController]
[Route("[controller]")]
public class ValidationController : ControllerBase
{
    private readonly ILogger<ValidationController> _logger;
    private readonly IProductService _productService;
    private readonly IRabbitMqProducer _rabbitMqProducer;

    public ValidationController(ILogger<ValidationController> logger, 
        IProductService productService,
        IRabbitMqProducer rabbitMqProducer)
    {
        _logger = logger;
        _productService = productService;
        _rabbitMqProducer = rabbitMqProducer;
    }

    [HttpGet("productlist")]
    public IEnumerable < Product > ProductList() {
        var productList = _productService.GetProductList();
        return productList;
    }
    [HttpGet("getproductbyid")]
    public Product GetProductById(int Id) {
        return _productService.GetProductById(Id);
    }
    [HttpPost("addproduct")]
    public Product AddProduct(Product product) {
        var productData = _productService.AddProduct(product);
        //send the inserted product data to the queue and consumer will listening this data from queue
        _rabbitMqProducer.SendProductMessage(productData);
        return productData;
    }
    
    // TODO: POST submit cause
    // - can the event key be found?
    // - are the parameters valid according to the event map?
    // NO? - fail fast
    // YES? - post to service bus for consumption
}