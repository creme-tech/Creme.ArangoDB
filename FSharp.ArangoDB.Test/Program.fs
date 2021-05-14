open FSharp.ArangoDB

[<EntryPoint>]
let main args =
    ArangoDB.setConfig (fun c -> c)

    let h : Types.ViewLinks = { includeAllFields = true }
    let g = ("test", h)

    let indexOptions =
        { ArangoDB.viewOptions with
              name = "searchfodapracaralho"
              links = Map([ g ]) }

    let s = ArangoDB.createView indexOptions

    printfn "Status: %d" s
    0
