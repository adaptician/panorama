namespace Panorama.Causation.Services;

public class CausationService: ICausationService {
    public CausationService() {
    }
    
    private List<Product> _dummy = new ()
    {
        new () { ProductId = 1 },
        new () { ProductId = 2 },
        new () { ProductId = 3 }
    };
    
    public IEnumerable < Product > GetProductList()
    {
        return _dummy;
    }
    public Product GetProductById(int id) {
        return _dummy.Where(x => x.ProductId == id).FirstOrDefault();
    }
    public Product AddProduct(Product product)
    {
        _dummy.Add(product);
        return product;
    }
}