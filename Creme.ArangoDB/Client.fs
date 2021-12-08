namespace Creme.ArangoDB

module internal Client =
    open ArangoDB

    open Flurl
    open System
    open System.Net.Http

    let internal httpHandler =
        let handler = new SocketsHttpHandler()

        handler.ConnectTimeout <- TimeSpan.FromSeconds 32
        handler.MaxConnectionsPerServer <- 32
        handler.PooledConnectionLifetime <- TimeSpan.FromMinutes(16)
        handler.PooledConnectionIdleTimeout <- TimeSpan.FromMinutes(4)
        handler

    let internal httpClient =
        let client = new HttpClient(httpHandler)

        (* TODO: Check if HTTP/2.0 has some connection error with ArangoDB *)
        client.DefaultRequestVersion <- Version(1, 1)
        client

    let mutable defaultConfig =
        { Authorization = "Basic cm9vdDpyb290"
          Client = httpClient
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
