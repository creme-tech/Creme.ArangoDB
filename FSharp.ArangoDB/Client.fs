namespace FSharp.ArangoDB

module internal Client =
    open Types

    open Flurl
    open System
    open System.Net.Http

    let mutable defaultConfig =
        { Authorization = "Basic cm9vdDpyb290"
          __Client = new HttpClient(DefaultRequestVersion = Version(2, 0))
          Database = "_system"
          __Debug = false
          Host = "http://127.0.0.1:8529/"
          __Target = "http://127.0.0.1:8529/_db/_system" }

    let setConfig (setConfig: Client -> Client) =
        let next = setConfig defaultConfig

        defaultConfig.__Client.DefaultRequestHeaders.Clear()
        defaultConfig.__Client.DefaultRequestHeaders.Add("Authorization", next.Authorization)

        defaultConfig <-
            { next with
                  __Target = Url.Combine(next.Host, "_db", next.Database) }
