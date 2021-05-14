namespace FSharp.ArangoDB

module internal Query' =
    open Client
    open Helper
    open Types

    let query<'T> (record: Query<_>) =
        let response =
            defaultConfig.client.PostAsync(host [| "_api"; "cursor" |], serialize record)
            |> Async.AwaitTask
            |> Async.RunSynchronously

        let status = int response.StatusCode

        let result =
            if status <> 201 then
                None
            else
                response.Content
                |> deserialize<QueryResult<'T>>
                |> Some

        (status, result)

    let queryNext<'T> cursorId (record: Query<_>) =
        let response =
            defaultConfig.client.PostAsync(host [| "_api"; "cursor"; cursorId |], serialize record)
            |> Async.AwaitTask
            |> Async.RunSynchronously

        let status = int response.StatusCode

        let result =
            if status <> 201 then
                None
            else
                response.Content
                |> deserialize<QueryResult<'T>>
                |> Some

        (status, result)
