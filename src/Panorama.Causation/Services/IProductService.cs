namespace Panorama.Causation.Services;

public interface IProductService {
    public IEnumerable < Product > GetProductList();
    public Product GetProductById(int id);
    public Product AddProduct(Product product);
}

// TODO: replace and move out.
public class Product
{
    public int ProductId {
        get;
        set;
    }
    public string ProductName {
        get;
        set;
    }
    public string ProductDescription {
        get;
        set;
    }
    public int ProductPrice {
        get;
        set;
    }
    public int ProductStock {
        get;
        set;
    }
}