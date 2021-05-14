namespace FSharp.ArangoDB

module internal Client =
    open Types

    open Flurl
    open System
    open System.Net.Http

    let mutable defaultConfig =
        { authorization = "Basic cm9vdDpyb290"
          client = new HttpClient(DefaultRequestVersion = Version(2, 0))
          database = "_system"
          debug = false
          host = "http://127.0.0.1:8529/"
          target = "http://127.0.0.1:8529/_db/_system" }

    let setConfig (setter: Client -> Client) =
        let next = setter defaultConfig

        defaultConfig.client.DefaultRequestHeaders.Clear()
        defaultConfig.client.DefaultRequestHeaders.Add("Authorization", next.authorization)

        defaultConfig <-
            { next with
                  target = Url.Combine(next.host, "_db", next.database) }
