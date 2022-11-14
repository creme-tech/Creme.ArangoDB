namespace Creme.ArangoDB

module internal Client =
    open ArangoDB

    open Flurl
    open System
    open System.Net
    open System.Net.Http

    let internal httpHandler =
        let handler = new SocketsHttpHandler ()

        handler.ConnectTimeout <- TimeSpan.FromSeconds 30
        handler.MaxConnectionsPerServer <- 30
        handler.PooledConnectionLifetime <- TimeSpan.FromMinutes 15
        handler.PooledConnectionIdleTimeout <- TimeSpan.FromMinutes 5
        handler

    let internal httpClient =
        let client = new HttpClient (httpHandler)

        (* TODO: Check if HTTP/2.0 works with ArangoDB *)
        client.DefaultRequestVersion <- HttpVersion.Version11
        client.DefaultVersionPolicy <- HttpVersionPolicy.RequestVersionOrHigher
        client

    let mutable defaultConfig =
        {
            Authorization = "Basic cm9vdDpyb290"
            Client = httpClient
            Database = "_system"
            Debug = false
            Host = "http://127.0.0.1:8529/"
            Target = "http://127.0.0.1:8529/_db/_system"
        }

    let SetConfig setter =
        let setter : Client -> Client = setter
        let config = setter defaultConfig

        let config =
            { defaultConfig with
                Authorization = config.Authorization
                Client =
                    config.Client.DefaultRequestHeaders.Clear ()
                    config.Client.DefaultRequestHeaders.Add ("Authorization", config.Authorization)
                    config.Client

                Database = config.Database
                Debug = config.Debug
                Host = config.Host
                Target = Url.Combine (config.Host, "_db", config.Database)
            }

        defaultConfig <- config
