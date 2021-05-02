namespace FSharp.ArangoDB

module internal __Query =
    open Client
    open Helper
    open Types

    let Query<'T> (record: Query) =
        let response =
            defaultConfig.__Client.PostAsync(host [| "_api"; "cursor" |], serialize record)
            |> Async.AwaitTask
            |> Async.RunSynchronously

        let status = int response.StatusCode

        let result =
            if status <> 201 then
                None
            else
                Some(response.Content |> deserialize<QueryResult<'T>>)

        (status, result)

    let QueryNext<'T> cursorId (record: Query) =
        let response =
            defaultConfig.__Client.PostAsync(host [| "_api"; "cursor"; cursorId |], serialize record)
            |> Async.AwaitTask
            |> Async.RunSynchronously

        let status = int response.StatusCode

        let result =
            if status <> 201 then
                None
            else
                Some(response.Content |> deserialize<QueryResult<'T>>)

        (status, result)
