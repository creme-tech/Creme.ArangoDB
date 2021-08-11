namespace Creme.ArangoDB

module internal Query' =
    open ArangoDB
    open Client
    open Helper

    open FSharp.Control.Tasks

    let Query<'T> record =
        task {
            let content = serialize record

            match record.TransactionId with
            | Some transactionId -> content.Headers.Add("X-Arango-TRX-Id", transactionId)
            | None -> None |> ignore

            let! response = defaultConfig.Client.PostAsync(host [| "_api"; "cursor" |], content)

            let status = int response.StatusCode

            let! rows =
                task {
                    if status = 201 then
                        let! object = response.Content |> deserialize<QueryResult<'T>>

                        return object
                    else
                        return emptyQueryResult
                }

            return status, rows
        }

    let QueryNext<'T> cursorId record =
        task {
            let content = serialize record

            match record.TransactionId with
            | Some transactionId -> content.Headers.Add("X-Arango-TRX-Id", transactionId)
            | None -> None |> ignore

            let! response = defaultConfig.Client.PutAsync(host [| "_api"; "cursor"; cursorId |], content)

            let status = int response.StatusCode

            let! rows =
                task {
                    if status = 200 then
                        let! object = response.Content |> deserialize<QueryResult<'T>>

                        return object
                    else
                        return emptyQueryResult
                }

            return status, rows
        }
