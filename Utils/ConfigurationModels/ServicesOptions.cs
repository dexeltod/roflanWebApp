namespace Utils.ConfigurationModels;

public class ServicesOptions
{
	public ProductsServiceOptions Products { get; set; }
}

public class ProductsServiceOptions
{
	public string Address { get; set; } = string.Empty;
}