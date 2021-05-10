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

    let CollectionKeyTypeAutoIncrement = "AutoIncrement".ToLower()

    let CollectionKeyTypePadded = "Padded".ToLower()
    let CollectionKeyTypeTraditional = "Traditional".ToLower()
    let CollectionKeyTypeUUID = "UUID".ToLower()

    let CollectionTypeDocument = 2
    let CollectionTypeEdge = 3

    (* Defaults *)

    let CollectionKeyOptions = { Type = CollectionKeyTypeTraditional }

    let CollectionOptions =
        { Name = null
          KeyOptions = CollectionKeyOptions
          Type = CollectionTypeDocument }

    let QueryOptions =
        { Query = null
          BindVars = Map.empty<string, obj> }

    (* Client *)

    let SetConfig = Client.SetConfig

    let GetCollection = Collection.GetCollection
    let CreateCollection = Collection.CreateCollection

    let Query<'T> = Query'.Query<'T>
    let QueryNext<'T> = Query'.QueryNext<'T>
