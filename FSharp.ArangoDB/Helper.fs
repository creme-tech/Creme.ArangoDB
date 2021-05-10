namespace FSharp.ArangoDB

module internal Helper =
    open Client

    open Flurl
    open FSharp.Json
    open System.Net.Http
    open System.Text

    let Deserialize<'T> (content: HttpContent) =
        content.ReadAsStringAsync()
        |> Async.AwaitTask
        |> Async.RunSynchronously
        |> Json.deserialize<'T>

    let Host action =
        Url.Combine(
            action
            |> Array.append [| defaultConfig.__Target |]
        )

    let Serialize record =
        new StringContent(Json.serialize record, Encoding.UTF8, "application/json")
