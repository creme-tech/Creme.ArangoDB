namespace FSharp.ArangoDB

module internal Index =
    open Client
    open Helper
    open Types

    let createIndex collectionName (record: IndexOptions) =
        let response =
            defaultConfig.client.PostAsync(
                host [| "_api"
                        "index"
                        "?collection=" + collectionName |],
                serialize record
            )
            |> Async.AwaitTask
            |> Async.RunSynchronously

        int response.StatusCode
