namespace FSharp.ArangoDB

module internal Database =
    open ArangoDB
    open Client
    open Helper

    open FSharp.Control.Tasks

    let GetAccessibleDatabases () =
        task {
            let! response = defaultConfig.Client.GetAsync(host [| "_api"; "database"; "user" |])

            let status = int response.StatusCode

            let! rows =
                task {
                    if status = 200 then
                        let! object =
                            response.Content
                            |> deserialize<QueryResult<string>>

                        return object
                    else
                        return EmptyQueryResult
                }

            return status, rows
        }

    let CreateDatabase (record: DatabaseOptions) =
        task {
            let! response = defaultConfig.Client.PostAsync(host [| "_api"; "database" |], serialize record)

            return int response.StatusCode, EmptyQueryResult
        }
