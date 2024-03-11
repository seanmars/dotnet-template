namespace SqlWithJson;

public class User
{
    public required int Id { get; set; }
    public required string Name { get; set; }
    public required Goods Goods { get; set; }
}

public class Goods
{
    public List<Item>? Items { get; set; }
}

public class Item
{
    public required int ItemKey { get; set; }
    public required int Count { get; set; }
}