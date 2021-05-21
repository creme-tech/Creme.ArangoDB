namespace FSharp.ArangoDB

module internal Collection =
    open ArangoDB
    open Client
    open Helper

    open FSharp.Control.Tasks

    let GetCollection name =
        task {
            let! response = defaultConfig.Client.GetAsync(host [| "_api"; "collection"; name |])

            return int response.StatusCode, { Id = None; Result = [] }
        }


    let CreateCollection (record: CollectionOptions) =
        task {
            let! response = defaultConfig.Client.PostAsync(host [| "_api"; "collection" |], serialize record)

            return int response.StatusCode, { Id = None; Result = [] }
        }
