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


    let KeyTypeAutoIncrement = "AutoIncrement".ToLower()

    let KeyTypePadded = "Padded".ToLower()
    let KeyTypeTraditional = "Traditional".ToLower()
    let KeyTypeUUID = "UUID".ToLower()

    let DocumentCollection = 2
    let EdgeCollection = 3

    let PersistentIndex = "Persistent".ToLower()

    let SearchView = "ArangoSearch".ToLower()

    (* Defaults *)

    let CollectionOptions =
        { keyOptions = { _type = KeyTypeUUID }
          name = None
          _type = DocumentCollection }

    let IndexOptions =
        { fields = []
          _type = PersistentIndex
          unique = true }

    (* Check the default value of "batchSize" used by ArangoDB server later *)
    let QueryOptions =
        { batchSize = 32
          bindVars = Map.empty<string, _>
          query = None }

    let SearchOptions =
        { links = Map.empty<string, _>
          name = None
          _type = SearchView }

    (* Client *)

    let setConfig = Client.setConfig

    let getCollection = Collection.getCollection
    let createCollection = Collection.createCollection

    let createIndex = Index.createIndex

    let getSearch = Search.getSearch
    let createSearch = Search.createSearch

    let query<'T> = Query.query<'T>
    let queryNext<'T> = Query.queryNext<'T>
