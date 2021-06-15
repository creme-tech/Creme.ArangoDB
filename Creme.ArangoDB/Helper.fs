namespace Creme.ArangoDB

module internal Helper =
    open ArangoDB
    open Client

    open Creme.Json
    open Flurl
    open FSharp.Control.Tasks
    open System.Net.Http
    open System.Text

    let private JsonConfig =
        JsonConfig.create (Unformatted = true, JsonFieldNaming = Json.CamelCase)

    let deserialize<'T> (content: HttpContent) =
        task {
            let! payload = content.ReadAsStringAsync()

            if defaultConfig.Debug then
                printfn "Deserialized payload: %s" payload

            return Json.DeserializeEx<'T> JsonConfig payload
        }

    let internal EmptyQueryResult = { Id = None; Result = [] }

    let internal EmptyTransactionResult = { result = { Id = "#"; status = "#" } }

    let host action =
        Url.Combine(action |> Array.append [| defaultConfig.Target |])

    let serialize record =
        let payload = Json.SerializeEx JsonConfig record

        if defaultConfig.Debug then
            printfn "Serialized payload: %s" payload

        new StringContent(payload, Encoding.UTF8, "application/json")
