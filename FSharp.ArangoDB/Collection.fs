namespace FSharp.ArangoDB

module internal Collection =
    open Client
    open Helper
    open Types

    let getCollection collectionName =
        let response =
            defaultConfig.__Client.GetAsync(
                host [| "_api"
                        "collection"
                        collectionName |]
            )
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
            defaultConfig.__Client.PostAsync(host [| "_api"; "collection" |], serialize record)
            |> Async.AwaitTask
            |> Async.RunSynchronously

        let status = int response.StatusCode

        status
