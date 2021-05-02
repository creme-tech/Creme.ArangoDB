namespace FSharp.ArangoDB

module internal Collection =
    open Client
    open Helper
    open Types

    let GetCollection name =
        let response =
            defaultConfig.__Client.GetAsync(Host [| "_api"; "collection"; name |])
            |> Async.AwaitTask
            |> Async.RunSynchronously

        let status = int response.StatusCode

        let collection =
            if status <> 200 then
                None
            else
                Some(response.Content |> Deserialize<Collection>)

        (status, collection)

    let CreateCollection (record: CollectionOptions) =
        let response =
            defaultConfig.__Client.PostAsync(Host [| "_api"; "collection" |], Serialize record)
            |> Async.AwaitTask
            |> Async.RunSynchronously

        let status = int response.StatusCode

        status
