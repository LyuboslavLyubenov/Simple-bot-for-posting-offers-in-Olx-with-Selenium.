namespace ConsoleApplication1
{

    public class AliExpressProduct
    {
        public string Name { get; set; }

        public string ImageUrl { get; set; }

        public string PriceRange { get; set; }

        public override string ToString()
        {
            return $"{this.Name}|{this.ImageUrl}|{this.PriceRange}";
        }
    }

}