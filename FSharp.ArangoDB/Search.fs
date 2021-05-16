namespace FSharp.ArangoDB

module internal Search =
    open Client
    open Helper
    open Types

    let getSearch name =
        let response =
            defaultConfig.client.GetAsync(host [| "_api"; "view"; name |])
            |> Async.AwaitTask
            |> Async.RunSynchronously

        int response.StatusCode

    let createSearch (record: SearchOptions) =
        let response =
            defaultConfig.client.PostAsync(host [| "_api"; "view#arangosearch" |], serialize record)
            |> Async.AwaitTask
            |> Async.RunSynchronously

        int response.StatusCode
