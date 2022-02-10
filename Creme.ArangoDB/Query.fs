namespace Creme.ArangoDB

module internal Query' =
    open ArangoDB
    open Client
    open Helper

    let Query<'T> record =
        task {
            let content = serialize record

            match record.TransactionId with
            | Some transactionId ->
                if defaultConfig.Debug then
                    printfn "Transaction: %s" transactionId

                content.Headers.Add("X-Arango-TRX-Id", transactionId)
            | None ->
                if defaultConfig.Debug then
                    printfn "Running with no transaction"

                None |> ignore

            let url = host [| "_api"; "cursor" |]

            if defaultConfig.Debug then
                printfn "URL: %s" url

            if defaultConfig.Debug then
                defaultConfig.Client.DefaultRequestHeaders.ToString()
                |> printfn "Request headers: %s"

            let! response = defaultConfig.Client.PostAsync(url, content)

            let status = int response.StatusCode

            if defaultConfig.Debug then
                printfn "Response code received: %d" status

            let! rows =
                task {
                    if status = 201 then
                        let! object = response.Content |> deserialize<QueryResult<'T>>

                        return object
                    else
                        let! _ =
                            match record.TransactionId with
                            | Some transactionId -> Transaction.AbortTransaction transactionId
                            | None -> task { return 200 }

                        return emptyQueryResult
                }

            return status, rows
        }

    let QueryNext<'T> cursorId record =
        task {
            let content = serialize record

            match record.TransactionId with
            | Some transactionId ->
                if defaultConfig.Debug then
                    printfn "Transaction: %s" transactionId

                content.Headers.Add("X-Arango-TRX-Id", transactionId)
            | None ->
                if defaultConfig.Debug then
                    printfn "Running with no transaction"

                None |> ignore

            let url = host [| "_api"; "cursor"; cursorId |]

            if defaultConfig.Debug then
                printfn "URL: %s" url

            if defaultConfig.Debug then
                defaultConfig.Client.DefaultRequestHeaders.ToString()
                |> printfn "Request headers: %s"

            let! response = defaultConfig.Client.PutAsync(url, content)

            let status = int response.StatusCode

            if defaultConfig.Debug then
                printfn "Response code received: %d" status

            let! rows =
                task {
                    if status = 200 then
                        let! object = response.Content |> deserialize<QueryResult<'T>>

                        return object
                    else
                        let! _ =
                            match record.TransactionId with
                            | Some transactionId -> Transaction.AbortTransaction transactionId
                            | None -> task { return 200 }

                        return emptyQueryResult
                }

            return status, rows
        }
