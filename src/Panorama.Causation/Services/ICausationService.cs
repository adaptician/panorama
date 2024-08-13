namespace Panorama.Causation.Services;

public interface ICausationService {
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

public class Cause<TEffect>
where TEffect : Effect
{
    public Catalyst Catalyst { get; set; }
    
    public AuthenticationMethod AuthenticationMethod { get; set; }
    
    // TODO: need to figure out what this payload looks like.
    // Spec out in detail creation / versus usage of effects.
    
    public Subject Subject { get; set; }
    
    public TEffect Effect { get; set; }
}

// An engine is either wired to fire on a trigger,
// or is configured to listen for catalytic events that match a Catalyst.
public class Catalyst
{
    public Trigger Trigger { get; set; }
    
    // If the following users trigger this action ...
    public IEnumerable<Guid> UserGuids { get; set; }
    
    // If any of the following effects are caused ...
    public IEnumerable<Guid> EffectGuids { get; set; }
}

public class Trigger
{
    public string Key { get; set; }
    
    // Here, a trigger could be something simple, like a click.
    // In which case, the trigger can be bound to an input device.
    
    // OR, as events are streamed into the headless space, they can be analysed.
    // Should something match a catalyst, it will trigger the effect.
    
    // TODO: note this in Confluence.
    // If you think about, this is critical for an organic learning environment.
    // Because if you can set things up to react upon something that is to the effect of ie. approximate,
    // then there is so much more room for freedom of expression and experimentation.
}

public class Effect
{
    public long Id { get; set; }
    
    public Guid Guid { get; set; }
    
    public string StateChanged { get; set; }
}

public class AuthenticationMethod
{
    public string Key { get; set; }
    
    public string CredentialKey { get; set; }
    
    public string CredentialValue { get; set; }
}

public class Subject
{
    public Guid Guid { get; set; }
    
    // TODO: how to make this effect broader eg. category based.
}