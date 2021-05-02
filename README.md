# FSharp.ArangoDB

⚠️ This project is in [alpha release](https://www.nuget.org/packages/FSharp.ArangoDB)  
FSharp.ArangoDB is a ArangoDB driver for F# focused on consistency and minimalist

## Usage

```fsharp
open FSharp.ArangoDB

ArangoDB.SetConfig
    (fun config ->
        { config with
              Authorization = "Basic S2lsbHVhOlpvbGR5Y2s="
              Database = "_system"
              Host = "http://127.0.0.1:8529/" })

let collection =
    { ArangoDB.CollectionOptions with
          Name = "MyCollection"
          KeyOptions =
              { ArangoDB.CollectionKeyOptions with
                    Type = ArangoDB.CollectionKeyTypeUUID } }

match ArangoDB.CreateCollection collection with
| ArangoDB.OK -> printfn "Success"
| ArangoDB.Conflict -> printfn "Collection already exists"
| _ -> printfn "Unknown error"

match ArangoDB.GetCollection "MyCollection" with
| (ArangoDB.OK, Some collection) -> printfn "Collection ID: %s" collection.ID
| (_, _) -> printfn "Unknown error"

let document =
    {| firstName = "Weslen"
        lastName = "Guerreiro" |}

let query =
    { ArangoDB.QueryOptions with
          Query = "INSERT @document INTO MyCollection"
          BindVars = Map([ ("document", box document) ]) }

match ArangoDB.Query query with
| (ArangoDB.Created, _) -> printfn "Success"
| _ -> printfn "Unknown error"
```

## License

This project is distributed under the [Apache License 2.0](LICENSE)
