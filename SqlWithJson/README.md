# EF with JSON in SQLite

This is a simple example of how to use Entity Framework with JSON in SQLite.

## 注意事項

`User` 中的 `Goods` 會被自動轉換為 JSON 格式，並且存入 SQLite 中。

並且可以直接正常使用 `LINQ` 來查詢 `Goods` 中的 `Items`。

```csharp
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

/*
 * Item 裡面不可以有 Id 或者後綴為 Id 的欄位，不然會因此自動抓取為被跟 Goods 配為主鍵
 * 如果真的需要使用例如 `ItemId` 為資料庫中的欄位名稱，可以改用 JsonPropertyName 來指定，或者使用 Fluent API 來指定
 * 相關 Issue: https://github.com/dotnet/efcore/issues/29380
 */
public class Item
{
    public required int ItemKey { get; set; }
    public required int Count { get; set; }
}
```