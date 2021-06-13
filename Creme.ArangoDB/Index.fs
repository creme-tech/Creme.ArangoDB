namespace Creme.ArangoDB

module internal Index =
    open ArangoDB
    open Client
    open Helper

    open FSharp.Control.Tasks

    let CreateIndex collectionName (record: IndexOptions) =
        task {
            let! response =
                defaultConfig.Client.PostAsync(
                    host [| "_api"
                            "index"
                            "?collection=" + collectionName |],
                    serialize record
                )

            return int response.StatusCode, EmptyQueryResult
        }
