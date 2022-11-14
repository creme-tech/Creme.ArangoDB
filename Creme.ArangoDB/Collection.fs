namespace Creme.ArangoDB

module internal Collection =
    open ArangoDB
    open Client
    open Helper

    let GetCollection name =
        task {
            let! response = defaultConfig.Client.GetAsync (host [| "_api" ; "collection" ; name |])

            return int response.StatusCode, eQueryResult
        }


    let CreateCollection record =
        task {
            let record : CollectionOptions = record

            let! response = defaultConfig.Client.PostAsync (host [| "_api" ; "collection" |], serialize record)

            return int response.StatusCode, eQueryResult
        }
