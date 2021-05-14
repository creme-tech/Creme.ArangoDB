namespace FSharp.ArangoDB

module internal Search =
    open Client
    open Helper
    open Types

    let createSearch (record: ViewOptions) =
        let response =
            defaultConfig.client.PostAsync(host [| "_api"; "view#arangosearch" |], serialize record)
            |> Async.AwaitTask
            |> Async.RunSynchronously

        int response.StatusCode
