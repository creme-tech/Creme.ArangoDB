namespace Creme.ArangoDB

module internal Transaction =
    open ArangoDB
    open Client
    open Helper

    let BeginTransaction record =
        task {
            let record: TransactionOptions = record

            let host =
                host [| "_api"
                        "transaction"
                        "begin" |]

            let record =
                { Collections =
                    record.Collections
                    |> List.map (fun (key, value) -> key.ToString().ToLower(), value)
                    |> Map.ofList }
                |> serialize

            let! response = defaultConfig.Client.PostAsync(host, record)

            let status = int response.StatusCode

            let! rows =
                task {
                    if status = 201 then
                        let! object =
                            response.Content
                            |> deserialize<TransactionResponse>

                        return object
                    else
                        return eTransactionResult
                }

            return status, rows
        }

    let AbortTransaction transactionID =
        task {
            let host =
                host [| "_api"
                        "transaction"
                        transactionID |]

            let! response = defaultConfig.Client.DeleteAsync host

            return int response.StatusCode
        }

    let CommitTransaction transactionID =
        task {
            let host =
                host [| "_api"
                        "transaction"
                        transactionID |]

            let! response = defaultConfig.Client.PutAsync(host, null)

            return int response.StatusCode, eTransactionResult
        }
