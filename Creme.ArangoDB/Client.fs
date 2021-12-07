namespace Creme.ArangoDB

module internal Client =
    open ArangoDB

    open Flurl
    open System
    open System.Net.Http

    let mutable defaultConfig =
        { Authorization = "Basic cm9vdDpyb290"
          Client = new HttpClient(DefaultRequestVersion = Version(1, 1))
          Database = "_system"
          Debug = true
          Host = "http://127.0.0.1:8529/"
          Target = "http://127.0.0.1:8529/_db/_system" }

    let SetConfig (setter: Client -> Client) =
        let next = setter defaultConfig

        defaultConfig.Client.DefaultRequestHeaders.Clear()
        defaultConfig.Client.DefaultRequestHeaders.Add("Authorization", next.Authorization)

        defaultConfig <-
            { next with
                  Target = Url.Combine(next.Host, "_db", next.Database) }
