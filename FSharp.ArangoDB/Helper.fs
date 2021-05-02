namespace FSharp.ArangoDB

module internal Helper =
    open Client

    open Flurl
    open Newtonsoft.Json
    open Newtonsoft.Json.Serialization
    open System.Net.Http
    open System.Text

    let Deserialize<'T> (content: HttpContent) =
        content.ReadAsStringAsync()
        |> Async.AwaitTask
        |> Async.RunSynchronously
        |> JsonConvert.DeserializeObject<'T>

    let Host action =
        Url.Combine(
            action
            |> Array.append [| defaultConfig.__Target |]
        )

    let Serialize record =
        new StringContent(
            JsonConvert.SerializeObject(
                record,
                JsonSerializerSettings(
                    ContractResolver = DefaultContractResolver(NamingStrategy = CamelCaseNamingStrategy())
                )
            ),
            Encoding.UTF8,
            "application/json"
        )
