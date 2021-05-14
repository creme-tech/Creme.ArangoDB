namespace FSharp.ArangoDB

module internal Collection =
    open Client
    open Helper
    open Types

    let getCollection name =
        let response =
            defaultConfig.client.GetAsync(host [| "_api"; "collection"; name |])
            |> Async.AwaitTask
            |> Async.RunSynchronously

        let status = int response.StatusCode

        let collection =
            if status <> 200 then
                None
            else
                Some(response.Content |> deserialize<Collection>)

        (status, collection)

    let createCollection (record: CollectionOptions) =
        let response =
            defaultConfig.client.PostAsync(host [| "_api"; "collection" |], serialize record)
            |> Async.AwaitTask
            |> Async.RunSynchronously

        int response.StatusCode
