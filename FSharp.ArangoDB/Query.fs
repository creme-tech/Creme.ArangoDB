namespace FSharp.ArangoDB

module internal Query' =
    open ArangoDB
    open Client
    open Helper

    open FSharp.Control.Tasks

    let Query<'T> (record: Query<_>) =
        task {
            let! response = defaultConfig.Client.PostAsync(host [| "_api"; "cursor" |], serialize record)

            let status = int response.StatusCode

            let! rows =
                task {
                    if status = 201 then
                        let! object = response.Content |> deserialize<QueryResult<'T>>

                        return object
                    else
                        return EmptyQueryResult
                }

            return status, rows
        }

    let QueryNext<'T> cursorId (record: Query<_>) =
        task {
            let! response = defaultConfig.Client.PutAsync(host [| "_api"; "cursor"; cursorId |], serialize record)

            let status = int response.StatusCode

            let! rows =
                task {
                    if status <> 200 then
                        let! object = response.Content |> deserialize<QueryResult<'T>>

                        return object
                    else
                        return EmptyQueryResult
                }

            return status, rows
        }
