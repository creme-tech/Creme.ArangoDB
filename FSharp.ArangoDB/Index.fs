namespace FSharp.ArangoDB

module internal Index =
    open Client
    open Helper
    open Types

    let createIndex collectionName (record: IndexOptions) =
        let target =
            host [| "_api"
                    "index"
                    "?collection=" + collectionName |]

        let response =
            defaultConfig.client.PostAsync(target, serialize record)
            |> Async.AwaitTask
            |> Async.RunSynchronously

        int response.StatusCode
