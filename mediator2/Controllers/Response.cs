using mediator2.dom;

namespace mediator2.Controllers
{
    public class Response
    {
        public string Name { get; init; }
        public int Number { get; init; }
        public int Size { get; init; }
    }

    public static class ExtentionsMappers
    {
        public static Response ToResponse(this DomeinClass domeinClass)
        {
            return new Response
            {
                Name = domeinClass.Name,
                Number = domeinClass.Number,
                Size = domeinClass.Size
            };
        }
    }
}
