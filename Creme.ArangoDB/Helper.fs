namespace Creme.ArangoDB

module internal Helper =
    open ArangoDB
    open Client

    open Flurl
    open FSharp.Json
    open System.Net.Http
    open System.Text

    let private config =
        JsonConfig.create (unformatted = true, jsonFieldNaming = Json.lowerCamelCase)

    let deserialize<'T> (content: HttpContent) =
        task {
            let! payload = content.ReadAsStringAsync()

            if defaultConfig.Debug then
                printfn "Deserialized payload: %s" payload

            return Json.deserializeEx<'T> config payload
        }

    let internal eQueryResult =
        { ID = None
          HasMore = false
          Result = [] }

    let internal eTransactionResult = { Result = { ID = "#"; Status = "#" } }

    let host action =
        Url.Combine(action |> Array.append [| defaultConfig.Target |])

    let serialize record =
        let payload = Json.serializeEx config record

        if defaultConfig.Debug then
            printfn "Serialized payload: %s" payload

        new StringContent(payload, Encoding.UTF8, "application/json")
