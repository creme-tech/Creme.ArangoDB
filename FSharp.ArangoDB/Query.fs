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
                    if status <> 201 then
                        return { Id = None; Result = [] }
                    else
                        let! object = response.Content |> deserialize<QueryResult<'T>>

                        return  object
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
                        return { Id = None; Result = [] }
                    else
                        let! object = response.Content |> deserialize<QueryResult<'T>>

                        return object
                }

            return status, rows
        }
