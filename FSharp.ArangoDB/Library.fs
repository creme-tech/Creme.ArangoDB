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


    let KeyTypeAutoIncrement = "autoincrement"

    let KeyTypePadded = "padded"
    let KeyTypeTraditional = "traditional"
    let KeyTypeUUID = "uuid"

    let DocumentCollection = 2
    let EdgeCollection = 3

    let PersistentIndex = "persistent"

    let SearchView = "arangosearch"

    (* Defaults *)

    let collectionOptions =
        { keyOptions = { type' = KeyTypeUUID }
          name = null
          type' = DocumentCollection }

    let indexOptions =
        { fields = []
          type' = PersistentIndex
          unique = true }

    let queryOptions =
        { bindVars = Map.empty<string, _>
          query = null }

    let viewOptions =
        { links = Map.empty<string, _>
          name = null
          type' = SearchView }

    (* Client *)

    let setConfig = Client.setConfig

    let getCollection = Collection.getCollection
    let createCollection = Collection.createCollection

    let createIndex = Index.createIndex

    let createSearch = Search.createSearch

    let query<'T> = Query.query<'T>
    let queryNext<'T> = Query.queryNext<'T>
