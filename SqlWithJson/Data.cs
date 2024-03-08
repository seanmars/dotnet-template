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
    public List<Equipment>? Equipments { get; set; }
}

public class Item
{
    // 因為 JSON 需要 Id 來識別唯一
    public int Id { get; set; }
    public required int ItemId { get; set; }
    public required int Count { get; set; }
}

public class Equipment
{
    // 因為 JSON 需要 Id 來識別唯一
    public int Id { get; set; }
    public required int ItemId { get; set; }
    public required int Str { get; set; }
    public required int Dex { get; set; }
    public required int Wis { get; set; }
}