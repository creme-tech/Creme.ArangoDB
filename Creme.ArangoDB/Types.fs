namespace Creme.ArangoDB

module ArangoDB =
    open System.Net.Http

    type CollectionKeyOptions = { Type: string }

    type CollectionOptions =
        { Name: string option
          KeyOptions: CollectionKeyOptions
          Type: int }

    type DatabaseOptions = { Name: string option }

    type IndexOptions =
        { Fields: string list
          ExpireAfter: int option
          Type: string
          Unique: bool }

    type QueryOptions = { Stream: bool }

    type Query<'T> =
        { BatchSize: int
          BindVars: Map<string, 'T>
          Options: QueryOptions
          Query: string option
          TransactionId: string option
          TTL: int }

    type QueryResult<'T> = { Id: string option; Result: 'T list }

    type SearchLinkOptions = { IncludeAllFields: bool }

    type SearchOptions =
        { Links: Map<string, SearchLinkOptions>
          Name: string option
          Type: string }

    type TransactionSubAttribute =
        | Read
        | Write
        | Exclusive

    type TransactionAttribute = TransactionSubAttribute * string list

    type TransactionOptions =
        { Collections: TransactionAttribute list }

    type TransactionPayload =
        { Collections: Map<string, string list> }

    type TransactionResult = { Id: string; Status: string }

    type TransactionResponse = { Result: TransactionResult }

    type Client =
        { Authorization: string
          Client: HttpClient
          Database: string
          Debug: bool
          Host: string
          Target: string }

    (* Options *)

    let KeyTypeAutoIncrement = "autoincrement"
    let KeyTypePadded = "padded"
    let KeyTypeTraditional = "traditional"
    let KeyTypeUUID = "uuid"

    let DocumentCollection = 2
    let EdgeCollection = 3

    let PersistentIndex = "persistent"
    let TTLIndex = "ttl"

    let SearchView = "arangosearch"

    (* Default options *)

    let CollectionKeyOptions = { Type = KeyTypeUUID }

    let CollectionOptions =
        { KeyOptions = CollectionKeyOptions
          Name = None
          Type = DocumentCollection }

    let IndexOptions =
        { Fields = []
          ExpireAfter = None
          Type = PersistentIndex
          Unique = true }

    let QueryOptions =
        { BatchSize = 30
          BindVars = Map.empty<string, _>
          Options = { Stream = true }
          Query = None
          TransactionId = None
          TTL = 30 }

    let SearchLinkOptions = { IncludeAllFields = false }

    let SearchOptions =
        { Links = Map.empty<string, _>
          Name = None
          Type = SearchView }

    let TransactionOptions: TransactionOptions = { Collections = [] }

    (* Status *)

    let (|OK|Error|) (status, response) =
        if status = 200 then OK response
        elif status = 201 then OK response
        else Error

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
