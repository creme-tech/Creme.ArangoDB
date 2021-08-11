namespace Creme.ArangoDB

module internal Index =
    open ArangoDB
    open Client
    open Helper

    open FSharp.Control.Tasks

    let CreateIndex collectionName record =
        task {
            let record: IndexOptions = record

            let host =
                host [| "_api"
                        "index"
                        "?collection=" + collectionName |]

            let! response = defaultConfig.Client.PostAsync(host, serialize record)

            return int response.StatusCode, emptyQueryResult
        }
