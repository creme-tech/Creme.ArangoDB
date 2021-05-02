namespace FSharp.ArangoDB

module internal __Query =
    open Client
    open Helper
    open Types

    let Query<'T> (record: Query) =
        let response =
            defaultConfig.__Client.PostAsync(Host [| "_api"; "cursor" |], Serialize record)
            |> Async.AwaitTask
            |> Async.RunSynchronously

        let status = int response.StatusCode

        let result =
            if status <> 201 then
                None
            else
                Some(response.Content |> Deserialize<QueryResult<'T>>)

        (status, result)

    let QueryNext<'T> cursorId (record: Query) =
        let response =
            defaultConfig.__Client.PostAsync(Host [| "_api"; "cursor"; cursorId |], Serialize record)
            |> Async.AwaitTask
            |> Async.RunSynchronously

        let status = int response.StatusCode

        let result =
            if status <> 201 then
                None
            else
                Some(response.Content |> Deserialize<QueryResult<'T>>)

        (status, result)
