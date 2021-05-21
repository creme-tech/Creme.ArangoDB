namespace FSharp.ArangoDB

module Helper =
    open ArangoDB
    open Client

    open Flurl
    open FSharp.Control.Tasks
    open FSharp.Json
    open System.Net.Http
    open System.Text

    let private JsonConfig =
        JsonConfig.create (unformatted = false, jsonFieldNaming = Json.lowerCamelCase)

    let deserialize<'T> (content: HttpContent) =
        task {
            let! payload = content.ReadAsStringAsync()

            if defaultConfig.Debug then
                printfn "Deserialized payload: %s" payload

            return Json.deserializeEx<'T> JsonConfig payload
        }

    let internal EmptyQueryResult = { Id = None; Result = [] }

    let host action =
        Url.Combine(action |> Array.append [| defaultConfig.Target |])

    let serialize record =
        let payload = Json.serializeEx JsonConfig record

        if defaultConfig.Debug then
            printfn "Serialized payload: %s" payload

        new StringContent(payload, Encoding.UTF8, "application/json")
