namespace FSharp.ArangoDB

module internal Client =
    open ArangoDB

    open Flurl
    open System
    open System.Net.Http

    let mutable defaultConfig =
        { Authorization = "Basic cm9vdDpyb290"
          Client = new HttpClient(DefaultRequestVersion = Version(2, 0))
          Database = "_system"
          Debug = false
          Host = "http://127.0.0.1:8529/"
          Target = "http://127.0.0.1:8529/_db/_system" }

    let SetConfig (setter: Client -> Client) =
        let next = setter defaultConfig

        defaultConfig.Client.DefaultRequestHeaders.Clear()
        defaultConfig.Client.DefaultRequestHeaders.Add("Authorization", next.Authorization)

        defaultConfig <-
            { next with
                  Target = Url.Combine(next.Host, "_db", next.Database) }
