namespace FSharp.ArangoDB

module Helper =
    open Client

    open Flurl
    open FSharp.Json
    open System.Net.Http
    open System.Text

    let deserialize<'T> (content: HttpContent) =
        content.ReadAsStringAsync()
        |> Async.AwaitTask
        |> Async.RunSynchronously
        |> Json.deserialize

    let host action =
        Url.Combine(action |> Array.append [| defaultConfig.target |])

    let serialize record =
        new StringContent(Json.serialize record, Encoding.UTF8, "application/json")
