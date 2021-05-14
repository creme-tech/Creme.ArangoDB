namespace FSharp.ArangoDB

module internal View =
    open Client
    open Helper
    open Types

    let createView (record: ViewOptions) =
        let response =
            defaultConfig.client.PostAsync(host [| "_api"; "view#arangosearch" |], serialize record)
            |> Async.AwaitTask
            |> Async.RunSynchronously

        int response.StatusCode
