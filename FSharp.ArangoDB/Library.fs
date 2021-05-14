namespace FSharp.ArangoDB

module ArangoDB =
    open Types

    (* Status *)

    [<Literal>]
    let OK = 200

    [<Literal>]
    let Created = 201

    [<Literal>]
    let BadRequest = 400

    [<Literal>]
    let NotFound = 404

    [<Literal>]
    let MethodNotAllowed = 405

    [<Literal>]
    let Conflict = 409

    [<Literal>]
    let LengthRequired = 411

    [<Literal>]
    let PayloadTooLarge = 413

    [<Literal>]
    let URITooLong = 414

    [<Literal>]
    let RequestHeaderFieldsTooLarge = 431

    [<Literal>]
    let HTTPVersionNotSupported = 505

    (* Options *)

    let collectionKeyTypeAutoIncrement = "AutoIncrement".ToLower()

    let collectionKeyTypePadded = "Padded".ToLower()
    let collectionKeyTypeTraditional = "Traditional".ToLower()
    let collectionKeyTypeUUID = "UUID".ToLower()

    let collectionTypeDocument = 2
    let collectionTypeEdge = 3

    (* Defaults *)

    let keyOptions = { type' = collectionKeyTypeTraditional }

    let collectionOptions =
        { name = null
          keyOptions = keyOptions
          type' = collectionTypeDocument }

    let indexOptions =
        { type' = "persistent"
          unique = false
          fields = [] }

    let queryOptions =
        { query = null
          bindVars = Map.empty<string, _> }

    let viewOptions =
        { links = Map.empty<string, _>
          name = null
          type' = "arangosearch" }

    (* Client *)

    let setConfig = Client.setConfig

    let getCollection = Collection.getCollection
    let createCollection = Collection.createCollection

    let createIndex = Index.createIndex

    let query<'T> = Query'.query<'T>
    let queryNext<'T> = Query'.queryNext<'T>

    let createView = View.createView
