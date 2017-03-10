namespace ConsoleApplication1
{

    public class OlxProduct
    {
        public string Name { get; set; }

        public string[] Categories { get; set; }

        public float Price { get; set; }

        public OlxProductState State { get; set; }

        public OlxOfferType OlxOfferType { get; set; }

        public string Description { get; set; }

        public string[] PicturesUrls { get; set; }

    }

}