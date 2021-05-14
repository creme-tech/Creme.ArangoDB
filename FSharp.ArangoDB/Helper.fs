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
        |> Json.deserialize<'T>

    let host action =
        Url.Combine(action |> Array.append [| defaultConfig.target |])

    let serialize record =
        let serialized = Json.serialize record

        if defaultConfig.debug then
            printfn "Serialized payload: %s" serialized

        new StringContent(serialized, Encoding.UTF8, "application/json")
